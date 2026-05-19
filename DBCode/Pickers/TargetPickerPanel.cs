namespace DBCode {
   public class Target {
      internal bool mIsValid;
      internal IntPtr mHandle;
      internal string mName;
      internal string mTooltip;

#pragma warning disable IDE0290
      public Target(string pName, IntPtr pHandle, bool pIsValid, string pTooltip) {
         mName = pName;
         mHandle = pHandle;
         mIsValid = pIsValid;
         mTooltip = pTooltip;
      }
#pragma warning restore IDE0290
   }

   internal sealed partial class TargetPickerPanel : ScrollablePanel {
      private readonly HeaderLabelCluster? mTitleLabel;
      internal readonly BottomPanel? mTargetPickerBottomPanel;
      internal ClusterContainer? mClusterContainer;
      private List<BaseCluster>? mButtonBaseClusters;
      internal bool mTargetSelected = false;
      private List<ToolTip> mToolTips = [];

      public TargetPickerPanel() {
         ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
         Name = "targetPickerPanel";
         TabIndex = mTabIndex++;
         mTitleLabel = new HeaderLabelCluster(mCurrentTheme, "Target Window Picker", HeaderLabelSize.Normal);
         mButtonBaseClusters = [];
         mClusterContainer = new ClusterContainer(this, mButtonBaseClusters, ClusterLayoutMode.FlowLayout) {
            Dock = DockStyle.None,
            AutoSize = false
         };
         mTargetPickerBottomPanel = new BottomPanel(mCurrentTheme);
      }

      public void LayoutPanel() {
         ThrowIfNull(mTargetPickerBottomPanel, nameof(mTargetPickerBottomPanel));
         ThrowIfNull(mClusterContainer, nameof(mClusterContainer));
         ThrowIfNull(mTitleLabel, nameof(mTitleLabel));
         SuspendLayout();
         mTitleLabel.SuspendLayout();
         mTitleLabel.LayoutCluster();
         mTargetPickerBottomPanel.LayoutControls();
         mClusterContainer.AutoSize = false;
         mClusterContainer.Size = new Size(ClientSize.Width - mIndent,
            ClientSize.Height - mTitleLabel.Height - mTargetPickerBottomPanel.Height - mEm4);
         mClusterContainer.Location = new Point(mIndent, mTitleLabel.Bottom + mEm);
         mClusterContainer.LayoutClusters();
         mClusterContainer.AutoSize = true;
         mTitleLabel.ResumeLayout(true);
         ResumeLayout(true);
      }

      private void CreateLayout() {
         ThrowIfNull(mClusterContainer, nameof(mClusterContainer));
         ThrowIfNull(mTitleLabel, nameof(mTitleLabel));
         ThrowIfNull(mTargetPickerBottomPanel, nameof(mTargetPickerBottomPanel));
         ThrowIfNull(mButtonBaseClusters, nameof(mCurrentTheme));
         CreateButtons();
         ThrowIfNull(mTargetPickerBottomPanel.mCancelButton, nameof(mTargetPickerBottomPanel.mCancelButton));
         ThrowIfNull(mTargetPickerBottomPanel.mHelpButton, nameof(mTargetPickerBottomPanel.mHelpButton));
         mTargetPickerBottomPanel.mHelpButton.Tag = new HelpTag(HelpContext.TargetPicker);
         mTargetPickerBottomPanel.mCancelButton.Click += CancelButton_Click;
         mTargetPickerBottomPanel.mHelpButton.Click += MainForm.Help_Click;
         mClusterContainer.Invalidate(true);
      }

      private void CreateButtons() {
         ThrowIfNull(mTitleLabel, nameof(mTitleLabel));
         foreach (Target target in mTargets.OfType<Target>()) {
            ThrowIfNull(mButtonBaseClusters, nameof(mButtonBaseClusters));
            AddButtonCluster(mButtonBaseClusters, target);
         }
      }

      private void AddButtonCluster(List<BaseCluster> pClusters, Target pTarget) {
         ThrowIfNull(mClusterContainer, nameof(mClusterContainer));
         ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
         ButtonCluster cluster = new ButtonCluster(mCurrentTheme, pTarget.mName);
         cluster.SuspendLayout();
         ThrowIfNull(cluster.mButton, nameof(cluster.mButton));
         cluster.mButton.Tag = pTarget;
         if (pTarget.mIsValid) {
            Font oldFont = cluster.mButton.Font;
            cluster.mButton.Font = new Font(oldFont, FontStyle.Bold);
            oldFont.Dispose();
            cluster.mButton.BackColor = ContrastingColor(cluster.mButton.BackColor);
            if (!string.IsNullOrEmpty(pTarget.mTooltip)) {
               ToolTip toolTip = new ToolTip();
               toolTip.SetToolTip(cluster.mButton, pTarget.mTooltip);
               mToolTips.Add(toolTip);
            }
         }
         cluster.mButton.Click += PickTargetButton_Click;
         pClusters.Add(cluster);
         mClusterContainer.Controls.Add(cluster);
      }

      public void ApplyTarget() {
         ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
         ThrowIfNull(mButtonBaseClusters, nameof(mButtonBaseClusters));
         ThrowIfNull(mTargetPickerBottomPanel, nameof(mTargetPickerBottomPanel));
         ThrowIfNull(mTitleLabel, nameof(mTitleLabel));
         BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.PanelBackground];
         foreach (BaseCluster cluster in mButtonBaseClusters)
            cluster.SetFontAndColor();
         mTargetPickerBottomPanel.SetFontAndColor();
         mTitleLabel.SetFontAndColor();
      }

      private static void CloseTargetPickerPanel() {
         ThrowIfNull(mForm, nameof(mForm));
         mForm.RestoreFromTargetPickerPanel();
      }

      protected override void Dispose(bool pDisposing) {
         if (pDisposing) {
            ThrowIfNull(mTitleLabel, nameof(mTitleLabel));
            ThrowIfNull(mClusterContainer, nameof(mClusterContainer));
            ThrowIfNull(mTargetPickerBottomPanel, nameof(mTargetPickerBottomPanel));
            ThrowIfNull(mButtonBaseClusters, nameof(mButtonBaseClusters));
            ThrowIfNull(mTargetPickerBottomPanel.mCancelButton, nameof(mTargetPickerBottomPanel.mCancelButton));
            ThrowIfNull(mTargetPickerBottomPanel.mHelpButton, nameof(mTargetPickerBottomPanel.mHelpButton));
            mTargetPickerBottomPanel.mCancelButton.Click -= CancelButton_Click;
            mTargetPickerBottomPanel.mHelpButton.Click -= MainForm.Help_Click;
            foreach (BaseCluster cluster in mButtonBaseClusters) {
               ThrowIfNull(cluster, nameof(cluster));
               if (cluster is ButtonCluster buttonCluster) {
                  ThrowIfNull(buttonCluster.mButton, nameof(buttonCluster.mButton));
                  buttonCluster.mButton.Click -= PickTargetButton_Click;
               }
            }
            foreach (ToolTip toolTip in mToolTips)
               toolTip.Dispose();
            mToolTips.Clear();
            mTitleLabel.Dispose();
            mClusterContainer.Dispose();
            mTargetPickerBottomPanel.Dispose();
         }
         base.Dispose(pDisposing);
      }
   }
}
