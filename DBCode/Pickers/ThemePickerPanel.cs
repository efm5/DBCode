namespace DBCode {
   namespace Themes {
      internal sealed partial class ThemePickerPanel : Panel {
         private readonly HeaderLabelCluster mTitleLabel;
         private readonly Panel mButtonPanel;
         private readonly BottomPanel mThemePickerBottomPanel;
         private ClusterContainer? mClusterContainer;
         private List<BaseCluster> mButtonBaseClusters;

         public ThemePickerPanel() {
            ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
            AutoScroll = true;
            AutoSize = false;
            mTitleLabel = new HeaderLabelCluster(mCurrentTheme, "Theme Picker", HeaderLabelSize.Normal);
            mButtonPanel = new Panel {
               Name = $"ThemePickerPanelScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill,
               AutoScroll = true,
               BackColor = mCurrentTheme!.mInterfaceColors[(int)ColorUsage.GroupBoxBackground]
            };
            mButtonBaseClusters = [];
            mClusterContainer = new ClusterContainer(mButtonPanel, mButtonBaseClusters, ClusterLayoutMode.FlowLayout);
            mButtonPanel.Controls.Add(mClusterContainer);
            mThemePickerBottomPanel = new BottomPanel(mCurrentTheme);
         }

         private void CreateLayout() {
            SuspendLayout();
            CreateButtons();
            mThemePickerBottomPanel.mCancelButton!.Click += CancelButton_Click;
            mThemePickerBottomPanel.mHelpButton!.Click += MainForm.Help_Click;
            Controls.AddRange([mThemePickerBottomPanel, mButtonPanel, mTitleLabel]);
            ApplyThemeToPanel();
            foreach (BaseCluster cluster in mButtonBaseClusters) {
               //cluster.LayoutCluster();//efm5 did not help
               cluster.ResumeLayout(true);
               //cluster.RefreshControls();//efm5 did not help
            }
            mButtonPanel.ResumeLayout(true);
            mClusterContainer!.LayoutClusters();
            ResumeLayout(true);
         }

         private void CreateButtons() {
            mButtonPanel.SuspendLayout();
            foreach (Theme theme in mThemes.OfType<Theme>())
               AddButtonCluster(mButtonBaseClusters, theme);
            mButtonPanel.Controls.Add(mClusterContainer);
         }

         private void AddButtonCluster(List<BaseCluster> pClusters, Theme pTheme) {
            ButtonCluster cluster = new ButtonCluster(pTheme, pTheme.mName);
            cluster.SuspendLayout();
            cluster.mButton!.Tag = pTheme;
            cluster.mButton.Click += PickThemeButton_Click;
            pClusters.Add(cluster);
            mClusterContainer.Controls.Add(cluster);
         }

         public void ApplyThemeToPanel() {
            Theme theme = mCurrentTheme!;
            BackColor = theme.mInterfaceColors[(int)ColorUsage.PanelBackground];
            mButtonPanel.BackColor = theme.mInterfaceColors[(int)ColorUsage.InterfaceBackground];
            foreach (BaseCluster cluster in mButtonBaseClusters)
               cluster.SetFontAndColor();
            mThemePickerBottomPanel.SetFontAndColor();
         }

         private void CloseThemePickerPanel() {
            mFirstThemePicker = false;
            mThemePickerBounds = mForm!.Bounds;
            mForm.RestoreFromThemePickerPanel();
         }

         protected override void Dispose(bool pDisposing) {
            if (pDisposing) {
               mThemePickerBottomPanel.mCancelButton!.Click -= CancelButton_Click;
               mThemePickerBottomPanel.mHelpButton!.Click -= MainForm.Help_Click;
               foreach (BaseCluster cluster in mButtonBaseClusters)
                  if (cluster is ButtonCluster buttonCluster)
                     buttonCluster.mButton!.Click -= PickThemeButton_Click;
               mTitleLabel.Dispose();
               mClusterContainer?.Dispose();
               mThemePickerBottomPanel.Dispose();
               mButtonPanel.Dispose();
            }
            base.Dispose(pDisposing);
         }
      }
   }
}
