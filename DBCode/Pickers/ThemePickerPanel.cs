namespace DBCode {
   namespace Themes {
      internal sealed partial class ThemePickerPanel : Panel {
         private readonly HeaderLabelCluster? mTitleLabel;
         private readonly BottomPanel? mThemePickerBottomPanel;
         internal ClusterContainer? mClusterContainer;
         private List<BaseCluster>? mButtonBaseClusters;
         private bool mLayoutReady = false;

         public ThemePickerPanel() {
            ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
            mTitleLabel = new HeaderLabelCluster(mCurrentTheme, "Theme Picker", HeaderLabelSize.Normal);
            mButtonBaseClusters = [];
            mClusterContainer = new ClusterContainer(this, mButtonBaseClusters, ClusterLayoutMode.FlowLayout) {
               AutoSize = false,
               mLayoutReadyGuard = () => mLayoutReady
            };
            mThemePickerBottomPanel = new BottomPanel(mCurrentTheme);
            mActiveLayoutable = mThemePickerBottomPanel;
         }

         public void LayoutPanel() {
            ThrowIfNull(mClusterContainer, nameof(mClusterContainer));
            ThrowIfNull(mThemePickerBottomPanel, nameof(mThemePickerBottomPanel));
            SuspendLayout();
            mThemePickerBottomPanel.SuspendLayout();
            ApplyTheme();
            mClusterContainer.Dock = DockStyle.Fill;
            mThemePickerBottomPanel.LayoutControls();
            mThemePickerBottomPanel.ResumeLayout(true);
            mLayoutReady = true;
            ResumeLayout(true);
         }

         private void CreateLayout() {
            ThrowIfNull(mClusterContainer, nameof(mClusterContainer));
            ThrowIfNull(mTitleLabel, nameof(mTitleLabel));
            ThrowIfNull(mThemePickerBottomPanel, nameof(mThemePickerBottomPanel));
            ThrowIfNull(mClusterContainer, nameof(mClusterContainer));
            ThrowIfNull(mButtonBaseClusters, nameof(mCurrentTheme));
            CreateButtons();
            mThemePickerBottomPanel.mCancelButton!.Click += CancelButton_Click;
            mThemePickerBottomPanel.mHelpButton!.Click += MainForm.Help_Click;
            Controls.AddRange([mThemePickerBottomPanel, mClusterContainer, mTitleLabel]);
            mClusterContainer.Invalidate(true);
         }

         private void CreateButtons() {
            ThrowIfNull(mTitleLabel, nameof(mTitleLabel));
            foreach (Theme theme in mThemes.OfType<Theme>()) {
               ThrowIfNull(mButtonBaseClusters, nameof(mButtonBaseClusters));
               AddButtonCluster(mButtonBaseClusters, theme);
            }
         }

         private void AddButtonCluster(List<BaseCluster> pClusters, Theme pTheme) {
            ThrowIfNull(mClusterContainer, nameof(mClusterContainer));
            ButtonCluster cluster = new ButtonCluster(pTheme, pTheme.mName);
            cluster.SuspendLayout();
            cluster.mButton!.Tag = pTheme;
            cluster.mButton.Click += PickThemeButton_Click;
            pClusters.Add(cluster);
            mClusterContainer.Controls.Add(cluster);
         }

         public void ApplyTheme() {
            ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
            ThrowIfNull(mButtonBaseClusters, nameof(mButtonBaseClusters));
            ThrowIfNull(mThemePickerBottomPanel, nameof(mThemePickerBottomPanel));
            BackColor = mCurrentTheme.mInterfaceColors[(int)ColorUsage.PanelBackground];
            foreach (BaseCluster cluster in mButtonBaseClusters)
               cluster.SetFontAndColor();
            mThemePickerBottomPanel.SetFontAndColor();
         }

         private void CloseThemePickerPanel() {
            ThrowIfNull(mForm, nameof(mForm));
            mForm.RestoreFromThemePickerPanel();
         }

         protected override void Dispose(bool pDisposing) {
            if (pDisposing) {
               ThrowIfNull(mTitleLabel, nameof(mTitleLabel));
               ThrowIfNull(mClusterContainer, nameof(mClusterContainer));
               ThrowIfNull(mThemePickerBottomPanel, nameof(mThemePickerBottomPanel));
               ThrowIfNull(mButtonBaseClusters, nameof(mButtonBaseClusters));
               mThemePickerBottomPanel.mCancelButton!.Click -= CancelButton_Click;
               mThemePickerBottomPanel.mHelpButton!.Click -= MainForm.Help_Click;
               foreach (BaseCluster cluster in mButtonBaseClusters) {
                  ThrowIfNull(cluster, nameof(cluster));
                  if (cluster is ButtonCluster buttonCluster)
                     buttonCluster.mButton!.Click -= PickThemeButton_Click;
               }
               mTitleLabel.Dispose();
               mClusterContainer.Dispose();
               mThemePickerBottomPanel.Dispose();
            }
            base.Dispose(pDisposing);
         }
      }
   }
}
