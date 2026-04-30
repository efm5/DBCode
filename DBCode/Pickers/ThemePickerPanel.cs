namespace DBCode {
   namespace Themes {
      internal sealed partial class ThemePickerPanel : Panel {
         private const string TemporaryThemePrefix = "\u26A0 TEMPORARY THEME PICKER \u26A0";
         private readonly Button mCancelButton, mHelpButton;
         private readonly MainForm mMainForm;
         private readonly HeaderLabelCluster mTitleLabel;
         private readonly Panel mButtonPanel;
         private readonly StatusStrip mStatusStrip;
         private readonly ToolStripControlHost mCancelHost, mHelpHost;
         private readonly ToolStripStatusLabel mSpringLabel;
         private List<ButtonCluster> mButtonClusters;

         public ThemePickerPanel(MainForm pMainForm) {
            ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));

            AutoScroll = true;
            AutoSize = false;
            BackColor = Color.Transparent;
            mCancelButton = new Button();
            mHelpButton = new Button();
            mTitleLabel = new HeaderLabelCluster(mCurrentTheme, "Theme Picker", HeaderLabelSize.Normal);
            mStatusStrip = new StatusStrip();
            mCancelHost = new ToolStripControlHost(mCancelButton);
            mHelpHost = new ToolStripControlHost(mHelpButton);
            mSpringLabel = new ToolStripStatusLabel();
            mButtonClusters = [];
            mButtonPanel = new Panel {
               Name = $"ThemePickerPanelScrollPanel{mTabIndex}",
               TabIndex = mTabIndex++,
               Dock = DockStyle.Fill,
               AutoScroll = true,
               BackColor = mCurrentTheme!.mInterfaceColors[(int)ColorUsage.InterfaceBackground]
            };
            mMainForm = pMainForm;
         }

         private void CreateLayout() {
            SuspendLayout();
            CreateStatusStrip();
            CreateButtons();
            mCancelButton.Click += CancelButton_Click;
            mHelpButton.Click += MainForm.Help_Click;
            Controls.AddRange([mButtonPanel, mStatusStrip, mTitleLabel]);
            mButtonPanel.Controls.AddRange(mButtonClusters.Cast<Control>().ToArray());//DEBUG efm5 2026 04 28 this might not be right either
            ApplyThemeToPanel();
            foreach (ButtonCluster clusterBase in mButtonClusters.OfType<ButtonCluster>())
               clusterBase.ResumeLayout(true);
            mButtonPanel.ResumeLayout(true);
            WidgetLayout(mButtonClusters.Cast<Control>().ToList(), mForm!.Width);
            ResumeLayout(true);
         }

         private void AddButtonCluster(List<ButtonCluster> pClusters, Theme pTheme) {
            ButtonCluster cluster = new ButtonCluster(pTheme, pTheme.mName) {
               Tag = pTheme
            };
            cluster.SuspendLayout();
            cluster.Click += PickThemeButton_Click;
            pClusters.Add(cluster);
         }

         private void CreateButtons() {
            mButtonPanel.SuspendLayout();
            foreach (Theme theme in mThemes.OfType<Theme>())
               AddButtonCluster(mButtonClusters, theme);
            mButtonPanel.Dock = DockStyle.Fill;
         }

         private void CreateStatusStrip() {
            mStatusStrip.SizingGrip = true;
            mStatusStrip.Dock = DockStyle.Bottom;
            mSpringLabel.Spring = true;
            mHelpButton.Text = "Help";
            mCancelButton.Text = "Cancel";
            mStatusStrip.Items.AddRange([mHelpHost, mSpringLabel, mCancelHost]);
         }

         private void CloseThemePickerPanel() {
            mFirstThemePicker = false;
            mThemePickerBounds = mForm!.Bounds;
            mForm.RestoreFromThemePickerPanel();
         }

         public void ApplyThemeToPanel() {
            Theme theme = mCurrentTheme!;
            BackColor = theme.mInterfaceColors[(int)ColorUsage.PanelBackground];
            foreach (Button item in mButtonPanel.Controls.OfType<Button>()) {
               if (item is Button button) {
                  button.ForeColor = theme.mInterfaceColors[(int)ColorUsage.InterfaceFont];
                  button.BackColor = theme.mInterfaceColors[(int)ColorUsage.InterfaceBackground];
                  button.Font = theme.mFonts[(int)FontUsage.Interface];
               }
            }
            mStatusStrip.Renderer = new ToolStripProfessionalRenderer();
            mStatusStrip.Invalidate(true);
            mStatusStrip.BackColor = theme.mInterfaceColors[(int)ColorUsage.StatusBackground];
            foreach (ToolStripItem item in mStatusStrip.Items) {
               if (item is ToolStripControlHost host) {
                  Control control = host.Control;
                  nint handle = control.Handle;
                  control.ForeColor = theme.mInterfaceColors[(int)ColorUsage.StatusFont];
                  control.BackColor = theme.mInterfaceColors[(int)ColorUsage.StatusBackground];
                  control.Font = theme.mFonts[(int)FontUsage.Status];
                  control.Invalidate();
                  control.Update();
               }
            }
         }
      }
   }
}
