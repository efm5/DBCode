namespace DBCode {
   internal sealed class RadioButtonClusterTestHarness : Form {
      private RadioButtonCluster? mHorizontalCluster;
      private RadioButtonCluster? mVerticalCluster;
      private RadioButtonCluster? mFixedColumns3Cluster;
      private RadioButtonCluster? mFixedRows3Cluster;
      private RadioButtonCluster? mFlowCluster;
      private RadioButtonCluster? mAutoSquareCluster;
      private RadioButtonCluster? mMutualExclusionCluster;
      private Panel? mContentPanel;
      private Label? mResultLabel;
      private Button? mCloseButton;

      private static readonly List<string> mFewNames = ["Alpha", "Beta", "Gamma"];
      private static readonly List<string> mManyNames = [
         "Alpha", "Beta", "Gamma", "Delta", "Epsilon", "Iota", "Zeta", "Eta", "Theta", "Last"];
      //efm5 Duplicate epsilonFor testing
      //private static readonly List<string> mManyNames = [
      //   "Alpha", "Beta", "Gamma", "Delta", "Epsilon", "Epsilon", "Zeta", "Eta", "Theta"];

      internal static void Show(string pCaption) {
         using RadioButtonClusterTestHarness harness = new RadioButtonClusterTestHarness(pCaption);
         harness.ShowDialog();
      }

      private RadioButtonClusterTestHarness(string pCaption) {
         ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
         Text = pCaption;
         AutoSize = false;
         StartPosition = FormStartPosition.CenterScreen;
         MinimumSize = new Size(600, 500);
         MakeControls();
         WireEvents();
         LayoutControls();
      }

      private void MakeControls() {
         mContentPanel = new Panel {
            AutoSize = false,
            AutoScroll = true,
            BackColor = Color.BlueViolet,
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
         };
         Controls.Add(mContentPanel);
         Color backColor = BackColor;
         mHorizontalCluster = new RadioButtonCluster(mCurrentTheme!, "Horizontal:",
            LabelPosition.Left, mFewNames, 0, ClusterLayoutMode.FixedRows,
            pFixedRows: 1, pBackgroundColor: backColor);
         mVerticalCluster = new RadioButtonCluster(mCurrentTheme!, "Vertical:",
            LabelPosition.Left, mFewNames, 1, ClusterLayoutMode.FixedColumns,
            pFixedColumns: 1, pBackgroundColor: backColor);
         mFixedColumns3Cluster = new RadioButtonCluster(mCurrentTheme!, "FixedColumns(3):",
            LabelPosition.Left, mManyNames, 0, ClusterLayoutMode.FixedColumns,
            pFixedColumns: 3, pBackgroundColor: backColor);
         mFixedRows3Cluster = new RadioButtonCluster(mCurrentTheme!, "FixedRows(3):",
            LabelPosition.Left, mManyNames, 0, ClusterLayoutMode.FixedRows,
            pFixedRows: 3, pBackgroundColor: backColor);
         mFlowCluster = new RadioButtonCluster(mCurrentTheme!, "FlowLayout:",
            LabelPosition.Left, mManyNames, 0, ClusterLayoutMode.FlowLayout,
            pBackgroundColor: backColor);
         mAutoSquareCluster = new RadioButtonCluster(mCurrentTheme!, "AutoSquareGrid:",
            LabelPosition.Left, mManyNames, 0, ClusterLayoutMode.AutoSquareGrid,
            pBackgroundColor: backColor);
         mMutualExclusionCluster = new RadioButtonCluster(mCurrentTheme!, "MutualExclusion:",
            LabelPosition.Left, mFewNames, 0, ClusterLayoutMode.FixedRows,
            pFixedRows: 1, pBackgroundColor: backColor);
         mResultLabel = new Label {
            Name = $"RadioButtonClusterTestHarnessResult{mTabIndex}",
            TabIndex = TAB_INDEX_IGNORED,
            Text = "Click a radio button to see output here.",
            AutoSize = true,
            Font = CreateNewFont(),
            ForeColor = mCurrentTheme!.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont],
            BackColor = Color.Transparent
         };
         mContentPanel.Controls.AddRange([
            mHorizontalCluster,
            mVerticalCluster,
            mFixedColumns3Cluster,
            mFixedRows3Cluster,
            mFlowCluster,
            mAutoSquareCluster,
            mMutualExclusionCluster,
            mResultLabel
         ]);
         mCloseButton = new Button {
            Name = $"RadioButtonClusterTestHarnessClose{mTabIndex}",
            TabIndex = mTabIndex++,
            Text = "&Close",
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            Font = CreateNewFont()
         };
         Controls.Add(mCloseButton);
      }

      private void WireEvents() {
         Load += OnLoad;
         ClientSizeChanged += OnClientSizeChanged;
         mCloseButton!.Click += OnCloseClick;
         foreach (Control control in mMutualExclusionCluster!.Controls) {
            if (control is RadioButton radioButton)
               radioButton.CheckedChanged += OnMutualExclusionCheckedChanged;
         }
      }

      private void LayoutControls() {
         const int padding = 12, rowSpacing = 8, closeMargin = 8;
         int closeHeight = mCloseButton!.Height + closeMargin * 2;
         mContentPanel!.Location = new Point(padding, padding);
         mContentPanel.Size = new Size(
            ClientSize.Width - padding * 2,
            ClientSize.Height - padding - closeHeight - padding);
         int y = padding;
         LayoutClusterRow(mHorizontalCluster!, ref y, rowSpacing);
         LayoutClusterRow(mVerticalCluster!, ref y, rowSpacing);
         LayoutClusterRow(mFixedColumns3Cluster!, ref y, rowSpacing);
         LayoutClusterRow(mFixedRows3Cluster!, ref y, rowSpacing);
         LayoutClusterRow(mFlowCluster!, ref y, rowSpacing);
         LayoutClusterRow(mAutoSquareCluster!, ref y, rowSpacing);
         LayoutClusterRow(mMutualExclusionCluster!, ref y, rowSpacing);
         mResultLabel!.Location = new Point(padding, y + rowSpacing);
         mCloseButton.Location = new Point(
            ClientSize.Width - mCloseButton.Width - closeMargin,
            ClientSize.Height - mCloseButton.Height - closeMargin);
      }

      private static void LayoutClusterRow(RadioButtonCluster pCluster, ref int pY, int pSpacing) {
         pCluster.LayoutCluster();
         pCluster.Location = new Point(12, pY);
         pY += pCluster.Height + pSpacing;
      }

      private void OnLoad(object? pSender, EventArgs pEventArgs) {
         VerifyInitialCheckedIndices();
      }

      private void OnClientSizeChanged(object? pSender, EventArgs pEventArgs) {
         LayoutControls();
      }

      private void OnCloseClick(object? pSender, EventArgs pEventArgs) {
         Close();
      }

      private void OnMutualExclusionCheckedChanged(object? pSender, EventArgs pEventArgs) {
         if (pSender is not RadioButton radioButton || !radioButton.Checked)
            return;
         int checkedIndex = mMutualExclusionCluster!.GetCheckedIndex();
         int checkedCount = 0;
         foreach (Control control in mMutualExclusionCluster.Controls) {
            if (control is RadioButton rb && rb.Checked)
               checkedCount++;
         }
         string result = checkedCount == 1
            ? $"PASS – Mutual exclusion holds. Checked index: {checkedIndex}, text: \"{radioButton.Text}\"."
            : $"FAIL – {checkedCount} radio buttons checked simultaneously.";
         SetResult(result);
      }

      private void VerifyInitialCheckedIndices() {
         if (mHorizontalCluster!.GetCheckedIndex() != 0) {
            SetResult("FAIL – Horizontal cluster: expected checked index 0.");
            return;
         }
         if (mVerticalCluster!.GetCheckedIndex() != 1) {
            SetResult("FAIL – Vertical cluster: expected checked index 1.");
            return;
         }
         if (mFixedColumns3Cluster!.GetCheckedIndex() != 0) {
            SetResult("FAIL – FixedColumns3 cluster: expected checked index 0.");
            return;
         }
         if (mFixedRows3Cluster!.GetCheckedIndex() != 0) {
            SetResult("FAIL – FixedRows3 cluster: expected checked index 0.");
            return;
         }
         if (mFlowCluster!.GetCheckedIndex() != 0) {
            SetResult("FAIL – FlowLayout cluster: expected checked index 0.");
            return;
         }
         if (mAutoSquareCluster!.GetCheckedIndex() != 0) {
            SetResult("FAIL – AutoSquareGrid cluster: expected checked index 0.");
            return;
         }
         if (mMutualExclusionCluster!.GetCheckedIndex() != 0) {
            SetResult("FAIL – MutualExclusion cluster: expected checked index 0.");
            return;
         }
         SetResult("Load – All initial checked indices PASS. Click a radio button in the mutual exclusion cluster to test exclusion.");
      }

      private void SetResult(string pMessage) {
         mResultLabel!.Text = pMessage;
         mResultLabel.Refresh();
      }

      protected override void Dispose(bool pDisposing) {
         if (pDisposing) {
            Load -= OnLoad;
            ClientSizeChanged -= OnClientSizeChanged;
            mCloseButton!.Click -= OnCloseClick;
            if (mMutualExclusionCluster != null) {
               foreach (Control control in mMutualExclusionCluster.Controls) {
                  if (control is RadioButton radioButton)
                     radioButton.CheckedChanged -= OnMutualExclusionCheckedChanged;
               }
            }
            MainForm.DisposeFontIfOwned(mResultLabel?.Font);
            MainForm.DisposeFontIfOwned(mCloseButton?.Font);
         }
         base.Dispose(pDisposing);
      }
   }
}
