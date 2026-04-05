namespace DBCode.Themes {
   internal sealed class ThemePanel : Panel {
      private LayoutHelpers.ClusterContainer? mClusterContainer = null;
      private int mSwatchSize = 24;
      private int mSpacing = 8;

      public ThemePanel(Color? pBackgroundColor) {
         BackColor = pBackgroundColor ?? Color.Transparent;
         AutoSize = true;
         TabIndex = LayoutHelpers.NextTabIndex();
         Name = "ThemePanel" + TabIndex;

         CreateLayout();
      }

      private void CreateLayout() {
         //mClusterContainer = new DBCode.LayoutHelpers.ClusterContainer(true, mSpacing, mSpacing, BackColor);
         //mClusterContainer.mLocation = new Point(0, 0);
         //Controls.Add(mClusterContainer);

         //AddSwatch(ColorSwatchUsage.PanelBackground, Color.LightGray);
         //AddSwatch(ColorSwatchUsage.PanelBorder, Color.Black);
         //AddSwatch(ColorSwatchUsage.TextPrimary, Color.Black);
         //AddSwatch(ColorSwatchUsage.TextSecondary, Color.DarkGray);
         //AddSwatch(ColorSwatchUsage.Highlight, Color.Yellow);

         //UpdateSize();
      }

#pragma warning disable IDE0060 // Remove unused parameter//DEBUG efm5 2026 04 4 until used
      private void AddSwatch(ColorSwatchUsage pUsage, Color pDefaultColor) {
         //DEBUG efm5 2026 04 4 we need a swatch class which is a label a button and the swatch
         //efm5 derived label text from the annotated usage 
         //ColorSwatch swatch = new ColorSwatch(pUsage, pDefaultColor, mSwatchSize, BackColor);
         //swatch.SwatchClicked += OnSwatchClicked;

         //BaseCluster cluster = new LabeledButtonCluster(pUsage.ToString(), "Select", LabelPosition.Left, BackColor);
         //cluster.Controls.Add(swatch);
         //swatch.Location = new Point(cluster.Width - mSwatchSize - mSpacing, (cluster.Height - mSwatchSize) / 2);

         //mClusterContainer?.AddCluster(cluster);
      }
#pragma warning restore IDE0060 // Remove unused parameter

      private void OnSwatchClicked(object? pSender, ColorSwatchUsage pUsage) {
         ApplyThemeChange(pUsage);
      }

#pragma warning disable IDE0060 // Remove unused parameter//DEBUG efm5 2026 04 4 until used
      private void ApplyThemeChange(ColorSwatchUsage pUsage) {
         //placeholder for theme update logic
         //this will be expanded once theme rules are defined
      }
#pragma warning restore IDE0060 // Remove unused parameter

      private void UpdateSize() {
         if (mClusterContainer == null)
            return;
         Width = mClusterContainer.Width;
         Height = mClusterContainer.Height;
      }

      protected override void Dispose(bool pDisposing) {
         if (pDisposing) {
            if (mClusterContainer != null) {
               foreach (Control control in mClusterContainer.Controls) {
                  if (control is ColorSwatch swatch) {
                     swatch.SwatchClicked -= OnSwatchClicked;
                  }
               }
               mClusterContainer.Dispose();
               mClusterContainer = null;
            }
         }
         base.Dispose(pDisposing);
      }
   }
}
