namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class RadioButtonCluster : BaseCluster {
         private Label? mLabel = null;
         private Panel mInnerPanel;
         private List<RadioButton> mRadioButtons = [];
         private ClusterLayoutMode mLayoutMode;
         private int mFixedColumns;
         private int mFixedRows;

         public RadioButtonCluster(Theme pTheme, string pLabelText, LabelPosition pLabelPosition,
            List<string> pRadioButtonNames, int pInitiallyChecked, ClusterLayoutMode pLayoutMode,
            int pFixedColumns = 1, int pFixedRows = 1, Color? pBackgroundColor = null)
            : base(pTheme, pBackgroundColor) {
            // Duplicate name guard — programmer error, must be first.
            for (int i = 0; i < pRadioButtonNames.Count; i++) {
               for (int j = i + 1; j < pRadioButtonNames.Count; j++) {
                  if (string.Equals(pRadioButtonNames[i], pRadioButtonNames[j], StringComparison.Ordinal))
                     ThrowBadCode($"{nameof(RadioButtonCluster)} constructor: duplicate radio button " +
                        $"name \"{pRadioButtonNames[i]}\" at indices {i} and {j}.");
               }
            }
            ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
            mLabelPosition = pLabelPosition;
            mLayoutMode = pLayoutMode;
            mFixedColumns = (pFixedColumns > 0) ? pFixedColumns : 1;
            mFixedRows = (pFixedRows > 0) ? pFixedRows : 1;
            mInnerPanel = new Panel {
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               BackColor = Color.Transparent
            };
            mLabel = new Label {
               TabIndex = TAB_INDEX_IGNORED,
               Name = $"RadioButtonCluster{nameof(mLabel)}{mTabIndex++}",
               Text = pLabelText,
               AutoSize = true,
               Font = CreateNewFont(),
               ForeColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont],
               BackColor = Color.Transparent
            };
            int checkedIndex = 0;
            foreach (string labelName in pRadioButtonNames.OfType<string>()) {
               RadioButton radioButton = new RadioButton {
                  TabIndex = mTabIndex,
                  Name = $"RadioButtonCluster{nameof(radioButton)}{mTabIndex++}",
                  Text = labelName,
                  AutoSize = true,
                  Font = CreateNewFont(),
                  ForeColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont],
                  BackColor = Color.Transparent
               };
               if ((pInitiallyChecked > -1) && (pInitiallyChecked < pRadioButtonNames.Count)) {
                  if (checkedIndex++ == pInitiallyChecked)
                     radioButton.Checked = true;
               }
               mRadioButtons.Add(radioButton);
               mInnerPanel.Controls.Add(radioButton);
            }
            Controls.AddRange([mInnerPanel, mLabel]);
         }

         internal int GetCheckedIndex() {
            for (int i = 0; i < mRadioButtons.Count; i++) {
               if (mRadioButtons[i].Checked)
                  return i;
            }
            return -1;
         }

         protected override void OnPaint(PaintEventArgs pEventArgs) {
            base.OnPaint(pEventArgs);
            int borderInset = ColorSwatchHelpers.BorderInset();
            Color[] borderColors = ColorSwatchHelpers.BorderColors(this);
            Rectangle outerRect = new Rectangle(
               mInnerPanel.Left - borderInset,
               mInnerPanel.Top - borderInset,
               mInnerPanel.Width + borderInset * 2,
               mInnerPanel.Height + borderInset * 2);
            Rectangle innerRect = new Rectangle(
               outerRect.Left + 1,
               outerRect.Top + 1,
               outerRect.Width - 2,
               outerRect.Height - 2);
            using Pen outerPen = new Pen(borderColors[0]);
            using Pen innerPen = new Pen(borderColors[1]);
            pEventArgs.Graphics.DrawRectangle(outerPen, outerRect);
            pEventArgs.Graphics.DrawRectangle(innerPen, innerRect);
         }

         internal override void SetFontAndColor() {
            ThrowIfNull(mLabel, nameof(mLabel));
            Theme.ThemeInterfaceThings(mTheme, out Font poFont, out Color poForeColor, out Color poBackColor);
            mLabel.Font = CreateNewFont(poFont);
            mLabel.ForeColor = poForeColor;
            mLabel.BackColor = Color.Transparent;
            foreach (RadioButton radioButton in mRadioButtons) {
               radioButton.Font = CreateNewFont(poFont);
               radioButton.ForeColor = poForeColor;
               radioButton.BackColor = Color.Transparent;
            }
         }

         internal void LayoutControls() {
            if (mRadioButtons.Count == 0)
               return;
            switch (mLayoutMode) {
               default:
               case ClusterLayoutMode.FlowLayout:
                  LayoutFlow();
                  break;
               case ClusterLayoutMode.MaxWidth:
                  LayoutMaxWidth();
                  break;
               case ClusterLayoutMode.MaxHeight:
                  LayoutMaxHeight();
                  break;
               case ClusterLayoutMode.FixedColumns:
                  LayoutFixedColumns();
                  break;
               case ClusterLayoutMode.FixedRows:
                  LayoutFixedRows();
                  break;
               case ClusterLayoutMode.AutoSquareGrid:
                  LayoutAutoSquareGrid();
                  break;
            }
            mInnerPanel.PerformLayout();
            int borderInset = ColorSwatchHelpers.BorderInset();
            int gap = borderInset * 2;
            // Shift inner panel first so ApplyLabelPosition sees its final position.
            mInnerPanel.Location = new Point(gap, gap);
            ThrowIfNull(mLabel, nameof(mLabel));
            ApplyLabelPosition(mLabel, mInnerPanel);
            // Now read post-shift, post-label bounds to size the outer panel.
            int right = 0, bottom = 0;
            foreach (Control control in Controls) {
               if (control.Right > right)
                  right = control.Right;
               if (control.Bottom > bottom)
                  bottom = control.Bottom;
            }
            AutoSize = false;
            Size = new Size(right + gap, bottom + gap);
            Invalidate();
         }

         private void LayoutFlow() {
            int usableWidth = (Parent != null) ? Parent.ClientSize.Width : int.MaxValue;
            int x = 0, y = 0, rowHeight = 0;
            foreach (RadioButton radioButton in mRadioButtons) {
               if (x > 0 && x + radioButton.Width > usableWidth) {
                  x = 0;
                  y += rowHeight + mEmHalf;
                  rowHeight = 0;
               }
               radioButton.Location = new Point(x, y);
               x += radioButton.Width + mEmHalf;
               if (radioButton.Height > rowHeight)
                  rowHeight = radioButton.Height;
            }
         }

         private void LayoutMaxWidth() {
            ThrowIfNull(Parent, nameof(Parent));
            int usableWidth = Parent.ClientSize.Width;
            int x = 0, y = 0, rowHeight = 0;
            foreach (RadioButton radioButton in mRadioButtons) {
               if (x > 0 && x + radioButton.Width > usableWidth) {
                  x = 0;
                  y += rowHeight + mEmHalf;
                  rowHeight = 0;
               }
               radioButton.Location = new Point(x, y);
               x += radioButton.Width + mEmHalf;
               if (radioButton.Height > rowHeight)
                  rowHeight = radioButton.Height;
            }
         }

         private void LayoutMaxHeight() {
            ThrowIfNull(Parent, nameof(Parent));
            int usableHeight = Parent.ClientSize.Height;
            int x = 0, y = 0, columnWidth = 0;
            foreach (RadioButton radioButton in mRadioButtons) {
               if (y > 0 && y + radioButton.Height > usableHeight) {
                  y = 0;
                  x += columnWidth + mEmHalf;
                  columnWidth = 0;
               }
               radioButton.Location = new Point(x, y);
               y += radioButton.Height + mEmHalf;
               if (radioButton.Width > columnWidth)
                  columnWidth = radioButton.Width;
            }
         }

         private void LayoutFixedColumns() {
            int columns = mFixedColumns;
            int[] columnWidths = new int[columns];
            for (int i = 0; i < mRadioButtons.Count; i++) {
               int col = i % columns;
               if (mRadioButtons[i].Width > columnWidths[col])
                  columnWidths[col] = mRadioButtons[i].Width;
            }
            int[] columnX = new int[columns];
            columnX[0] = 0;
            for (int col = 1; col < columns; col++)
               columnX[col] = columnX[col - 1] + columnWidths[col - 1] + mEmHalf;
            int y = 0, rowHeight = 0, columnIndex = 0;
            foreach (RadioButton radioButton in mRadioButtons) {
               radioButton.Location = new Point(columnX[columnIndex], y);
               if (radioButton.Height > rowHeight)
                  rowHeight = radioButton.Height;
               columnIndex++;
               if (columnIndex >= columns) {
                  columnIndex = 0;
                  y += rowHeight + mEmHalf;
                  rowHeight = 0;
               }
            }
         }

         private void LayoutFixedRows() {
            int rows = mFixedRows;
            int[] rowHeights = new int[rows];
            for (int i = 0; i < mRadioButtons.Count; i++) {
               int row = i % rows;
               if (mRadioButtons[i].Height > rowHeights[row])
                  rowHeights[row] = mRadioButtons[i].Height;
            }
            int[] rowY = new int[rows];
            rowY[0] = 0;
            for (int row = 1; row < rows; row++)
               rowY[row] = rowY[row - 1] + rowHeights[row - 1] + mEmHalf;
            int x = 0, columnWidth = 0, rowIndex = 0;
            foreach (RadioButton radioButton in mRadioButtons) {
               radioButton.Location = new Point(x, rowY[rowIndex]);
               if (radioButton.Width > columnWidth)
                  columnWidth = radioButton.Width;
               rowIndex++;
               if (rowIndex >= rows) {
                  rowIndex = 0;
                  x += columnWidth + mEmHalf;
                  columnWidth = 0;
               }
            }
         }

         private void LayoutAutoSquareGrid() {
            int count = mRadioButtons.Count,
               columns = (int)Math.Ceiling(Math.Sqrt(count)),
               rows = (int)Math.Ceiling(count / (double)columns),
               maxWidth = 0, maxHeight = 0;
            foreach (RadioButton radioButton in mRadioButtons) {
               if (radioButton.Width > maxWidth)
                  maxWidth = radioButton.Width;
               if (radioButton.Height > maxHeight)
                  maxHeight = radioButton.Height;
            }
            int cellWidth = maxWidth + mEmHalf, cellHeight = maxHeight + mEmHalf, index = 0;
            for (int currentRow = 0; currentRow < rows; currentRow++) {
               int x = 0;
               for (int currentColumn = 0; currentColumn < columns; currentColumn++) {
                  if (index >= count)
                     break;
                  mRadioButtons[index].Location = new Point(x, currentRow * cellHeight);
                  x += cellWidth;
                  index++;
               }
            }
         }

         internal override void LayoutCluster() {
            if (Parent == null &&
               (mLayoutMode == ClusterLayoutMode.MaxWidth || mLayoutMode == ClusterLayoutMode.MaxHeight))
               ThrowBadCode($"{nameof(RadioButtonCluster)}.{nameof(LayoutCluster)}: Parent is null. " +
                  "Cluster must be added to a container before LayoutCluster is called with MaxWidth or MaxHeight.");
            ThrowIfNull(mLabel, nameof(mLabel));
            SetFontAndColor();
            LayoutControls();
            mLabel.Invalidate();
            mLabel.Refresh();
            foreach (RadioButton radioButton in mRadioButtons) {
               radioButton.Invalidate();
               radioButton.Refresh();
            }
         }

         internal override void LayoutControlsOnly() {
            if (Parent == null &&
               (mLayoutMode == ClusterLayoutMode.MaxWidth || mLayoutMode == ClusterLayoutMode.MaxHeight))
               ThrowBadCode($"{nameof(RadioButtonCluster)}.{nameof(LayoutControlsOnly)}: Parent is null. " +
                  "Cluster must be added to a container before LayoutControlsOnly is called with MaxWidth or MaxHeight.");
            ThrowIfNull(mLabel, nameof(mLabel));
            LayoutControls();
            mLabel.Invalidate();
            foreach (RadioButton radioButton in mRadioButtons)
               radioButton.Invalidate();
         }

         protected override void Dispose(bool pDisposing) {
            if (pDisposing) {
               ThrowIfNull(mLabel, nameof(mLabel));
               MainForm.DisposeFontIfOwned(mLabel.Font);
               foreach (RadioButton radioButton in mRadioButtons) {
                  ThrowIfNull(radioButton, nameof(radioButton));
                  MainForm.DisposeFontIfOwned(radioButton.Font);
               }
            }
            base.Dispose(pDisposing);
         }
      }
   }
}
