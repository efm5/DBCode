namespace DBCode {
   internal static partial class LayoutHelpers {
      /// <summary>
      /// A general-purpose draggable container panel. Can be used in two ways:
      ///
      /// 1. Self-hosting (inheritance): A subclass such as GetString builds its content directly
      ///    inside this panel and calls Attach(pForm). DraggablePanel adds itself to pForm.Controls
      ///    and uses its own already-set Size.
      ///
      /// 2. External hosting (composition): A caller constructs a DraggablePanel, passes an
      ///    already-sized Panel subclass to Attach(pForm, pHostedPanel), and DraggablePanel wraps
      ///    it with the standard insets (mEm top, mEmHalf left/right/bottom).
      ///
      /// In both cases: the current ScrollablePanel is disabled on Attach(), sampled for its
      /// dominant tone, and ApplyDragTone() is called so the subclass can apply contrast colors
      /// against the disabled background. The ScrollablePanel is re-enabled on Detach().
      /// Movement is constrained to the Form's client rectangle with edge snapping.
      /// The caller is responsible for disposing this panel after Detach().
      /// </summary>
      internal abstract class DraggablePanel : Panel {
         private Form? mForm;
         private bool mDragging;
         private Point mDragOffset;
         private Cursor? mSavedCursor;

         // ── Abstract contract ──────────────────────────────────────────────────────────────────

         /// <summary>
         /// Called by AttachCore() after the ScrollablePanel has been disabled and sampled.
         /// Subclasses must implement this to apply contrast colors appropriate to pTone,
         /// which reflects the dominant tone of the disabled ScrollablePanel the user sees
         /// behind this DraggablePanel.
         /// </summary>
         protected abstract void ApplyDragTone(ColorTones pTone);

         // ── Attach / Detach ────────────────────────────────────────────────────────────────────

         /// <summary>
         /// Self-hosting overload. Used when this DraggablePanel (or a subclass) has already
         /// built and sized its own content internally. Disables the current ScrollablePanel,
         /// samples its dominant tone, calls ApplyDragTone(), adds this panel to pForm.Controls,
         /// and centers it within pForm's client area.
         /// Size must already be set correctly before calling this overload.
         /// </summary>
         public void Attach(Form pForm) {
            ThrowIfNull(pForm, nameof(pForm));
            mForm = pForm;
            AttachCore();
         }

         /// <summary>
         /// External-hosting overload. Wraps pHostedPanel inside this DraggablePanel using the
         /// standard insets (mEm top, mEmHalf left/right/bottom), sizes this panel accordingly,
         /// disables the current ScrollablePanel, samples its dominant tone, calls ApplyDragTone(),
         /// adds this panel to pForm.Controls, and centers it within pForm's client area.
         /// pHostedPanel must already be fully sized before calling this overload.
         /// </summary>
         public void Attach(Form pForm, Panel pHostedPanel) {
            ThrowIfNull(pForm, nameof(pForm));
            ThrowIfNull(pHostedPanel, nameof(pHostedPanel));
            mForm = pForm;
            pHostedPanel.Location = new Point(mEmHalf, mEm);
            Controls.Add(pHostedPanel);
            Size = new Size(pHostedPanel.Width + (mEmHalf * 2), pHostedPanel.Height + mEm + mEmHalf);
            AttachCore();
         }

         /// <summary>
         /// Shared implementation for both Attach overloads. mForm must be set before calling.
         /// Disables the ScrollablePanel, samples its dominant tone, calls ApplyDragTone(),
         /// then adds this panel to the Form and makes it visible.
         /// </summary>
         private void AttachCore() {
            ScrollablePanel? scrollablePanel = FindScrollablePanel();
            if (scrollablePanel != null)
               scrollablePanel.Enabled = false;
            ColorTones tone = scrollablePanel != null
               ? SampleDominantTone(scrollablePanel)
               : ColorTones.Light;
            ApplyDragTone(tone);
            mForm!.SuspendLayout();
            mForm.Controls.Add(this);
            PerformLayout();
            mForm.ResumeLayout(true);
            CenterOnClientArea();
            BringToFront();
            Visible = true;
         }

         /// <summary>
         /// Removes this DraggablePanel from the Form, re-enables the ScrollablePanel, and
         /// releases all references. The caller is responsible for disposing this panel.
         /// </summary>
         public void Detach() {
            ThrowIfNull(mForm, nameof(mForm));
            ScrollablePanel? scrollablePanel = FindScrollablePanel();
            mForm!.SuspendLayout();
            Visible = false;
            SendToBack();
            if (mForm.Controls.Contains(this))
               mForm.Controls.Remove(this);
            mForm.ResumeLayout(true);
            if (scrollablePanel != null)
               scrollablePanel.Enabled = true;
            mForm = null;
         }

         // ── Mouse handling ─────────────────────────────────────────────────────────────────────

         protected override void OnMouseDown(MouseEventArgs pEventArguments) {
            base.OnMouseDown(pEventArguments);
            if (pEventArguments.Button != MouseButtons.Left)
               return;
            mDragging = true;
            mDragOffset = pEventArguments.Location;
            mSavedCursor = Cursor.Current;
            Cursor.Current = Cursors.SizeAll;
            Capture = true;
         }

         protected override void OnMouseMove(MouseEventArgs pEventArguments) {
            base.OnMouseMove(pEventArguments);
            if (!mDragging || mForm == null)
               return;
            Point newLocation = Location + (Size)(pEventArguments.Location - (Size)mDragOffset);
            Location = ClampToClientArea(newLocation);
         }

         protected override void OnMouseUp(MouseEventArgs pEventArguments) {
            base.OnMouseUp(pEventArguments);
            if (!mDragging)
               return;
            mDragging = false;
            Capture = false;
            Cursor.Current = mSavedCursor;
            mSavedCursor = null;
         }

         // ── Layout helpers ─────────────────────────────────────────────────────────────────────

         private void CenterOnClientArea() {
            ThrowIfNull(mForm, nameof(mForm));
            Rectangle client = mForm!.ClientRectangle;
            Location = new Point(
               client.Left + (client.Width - Width) / 2,
               client.Top + (client.Height - Height) / 2);
         }

         private Point ClampToClientArea(Point pLocation) {
            ThrowIfNull(mForm, nameof(mForm));
            Rectangle client = mForm!.ClientRectangle;
            int x = Math.Clamp(pLocation.X, client.Left, client.Right - Width);
            int y = Math.Clamp(pLocation.Y, client.Top, client.Bottom - Height);
            return new Point(x, y);
         }

         /// <summary>
         /// Finds the current ScrollablePanel among the Form's direct children.
         /// DraggablePanel itself is a Panel subclass but not a ScrollablePanel, so
         /// OfType naturally excludes it. Returns null without throwing if none is present.
         /// </summary>
         private ScrollablePanel? FindScrollablePanel() =>
            mForm?.Controls.OfType<ScrollablePanel>().FirstOrDefault();

         // ── Dispose ────────────────────────────────────────────────────────────────────────────

         protected override void Dispose(bool pDisposing) {
            if (pDisposing && mDragging) {
               Capture = false;
               Cursor.Current = mSavedCursor;
               mSavedCursor = null;
               mDragging = false;
            }
            base.Dispose(pDisposing);
         }
      }
   }
}
