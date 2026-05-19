//namespace DBCode {
//   internal sealed class ScalableCheckBoxClusterTestHarness : Form {
//      private ButtonCluster? mCancelCluster;
//      private ButtonCluster? mBiggerCluster;
//      private ClusterContainer? mClusterContainer;
//      private Panel? mScrollablePanel;
//      private ScalableCheckBoxCluster? mCheckBoxCluster1;
//      private ScalableCheckBoxCluster? mCheckBoxCluster2;
//      private ScalableRadioButtonCluster? mHorizontalRadioCluster;
//      private ScalableRadioButtonCluster? mVerticalRadioCluster;

//      internal static void Show(string pCaption) {
//         using ScalableCheckBoxClusterTestHarness harness = new ScalableCheckBoxClusterTestHarness(pCaption);
//         harness.ShowDialog();
//      }

//      private ScalableCheckBoxClusterTestHarness(string pCaption) {
//         ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
//         Text = pCaption;
//         AutoSize = false;
//         StartPosition = FormStartPosition.CenterScreen;
//         MinimumSize = new Size(480, 380);
//         MakeControls();
//         WireEvents();
//         LayoutControls();
//      }

//      private void MakeControls() {
//         ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
//         mScrollablePanel = new Panel {
//            Name = $"ScalableCheckBoxTestHarnessScrollPanel{mTabIndex}",
//            TabIndex = TAB_INDEX_IGNORED,
//            AutoSize = false,
//            AutoScroll = true,
//            BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceBackground],
//            Anchor = mAnchorTopLeftBottomRight
//         };
//         mCancelCluster = new ButtonCluster(mCurrentTheme!, "&Cancel");
//         mBiggerCluster = new ButtonCluster(mCurrentTheme!, "&Bigger");
//         mCheckBoxCluster1 = new ScalableCheckBoxCluster(mCurrentTheme!, "&First Option", true);
//         mCheckBoxCluster2 = new ScalableCheckBoxCluster(mCurrentTheme!, "&Second Option", true);
//         List<ScalableRadioButtons.RadioButtonQuad> horizontalQuads = [
//            new ScalableRadioButtons.RadioButtonQuad("&Option A", "A", true, 0),
//            new ScalableRadioButtons.RadioButtonQuad("&Option B", "B", false, 1),
//            new ScalableRadioButtons.RadioButtonQuad("&Option C", "C", false, 2)
//         ];
//         mHorizontalRadioCluster = new ScalableRadioButtonCluster(mCurrentTheme!, horizontalQuads, true, true);
//         List<ScalableRadioButtons.RadioButtonQuad> verticalQuads = [
//            new ScalableRadioButtons.RadioButtonQuad("&First", "1", true, 0),
//            new ScalableRadioButtons.RadioButtonQuad("&Second", "2", false, 1)
//         ];
//         mVerticalRadioCluster = new ScalableRadioButtonCluster(mCurrentTheme!, verticalQuads, true, false);
//         List<BaseCluster> clusters = [
//            mCancelCluster, mBiggerCluster,
//            mCheckBoxCluster1, mCheckBoxCluster2,
//            mHorizontalRadioCluster, mVerticalRadioCluster
//         ];
//         mClusterContainer = new ClusterContainer(mScrollablePanel, clusters, ClusterLayoutMode.FlowLayout);
//         mScrollablePanel.Controls.Add(mClusterContainer);
//         Controls.Add(mScrollablePanel);
//      }

//      private void WireEvents() {
//         ThrowIfNull(mCancelCluster, nameof(mCancelCluster));
//         ThrowIfNull(mCancelCluster.mButton, nameof(mCancelCluster.mButton));
//         ThrowIfNull(mBiggerCluster, nameof(mBiggerCluster));
//         ThrowIfNull(mBiggerCluster.mButton, nameof(mBiggerCluster.mButton));
//         Load += OnLoad;
//         ClientSizeChanged += OnClientSizeChanged;
//         mCancelCluster.mButton.Click += OnCancelClick;
//         mBiggerCluster.mButton.Click += OnBiggerClick;
//      }

//      private void LayoutControls() {
//         ThrowIfNull(mScrollablePanel, nameof(mScrollablePanel));
//         ThrowIfNull(mClusterContainer, nameof(mClusterContainer));
//         ThrowIfNull(mCancelCluster, nameof(mCancelCluster));
//         ThrowIfNull(mBiggerCluster, nameof(mBiggerCluster));
//         ThrowIfNull(mCheckBoxCluster1, nameof(mCheckBoxCluster1));
//         ThrowIfNull(mCheckBoxCluster2, nameof(mCheckBoxCluster2));
//         ThrowIfNull(mHorizontalRadioCluster, nameof(mHorizontalRadioCluster));
//         ThrowIfNull(mVerticalRadioCluster, nameof(mVerticalRadioCluster));
//         const int padding = 12;
//         mScrollablePanel.Location = new Point(padding, padding);
//         mScrollablePanel.Size = new Size(ClientSize.Width - padding * 2, ClientSize.Height - padding * 2);
//         mCancelCluster.LayoutCluster();
//         mBiggerCluster.LayoutCluster();
//         mCheckBoxCluster1.LayoutCluster();
//         mCheckBoxCluster2.LayoutCluster();
//         mHorizontalRadioCluster.LayoutCluster();
//         mVerticalRadioCluster.LayoutCluster();
//         mClusterContainer.LayoutClusters();
//      }

//      private void OnLoad(object? pSender, EventArgs pEventArgs) {
//         LayoutControls();
//      }

//      private void OnClientSizeChanged(object? pSender, EventArgs pEventArgs) {
//         LayoutControls();
//      }

//      private void OnCancelClick(object? pSender, EventArgs pEventArgs) {
//         Close();
//      }

//      private void OnBiggerClick(object? pSender, EventArgs pEventArgs) {
//         ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
//         Font oldFont = mCurrentTheme.mFonts[(int)FontUsage.Interface];
//         mCurrentTheme.mFonts[(int)FontUsage.Interface] = new Font(oldFont.Name, oldFont.SizeInPoints * 2f, oldFont.Style);
//         oldFont.Dispose();
//         LayoutControls();
//      }

//      protected override void Dispose(bool pDisposing) {
//         if (pDisposing) {
//            ThrowIfNull(mCancelCluster, nameof(mCancelCluster));
//            ThrowIfNull(mCancelCluster.mButton, nameof(mCancelCluster.mButton));
//            ThrowIfNull(mBiggerCluster, nameof(mBiggerCluster));
//            ThrowIfNull(mBiggerCluster.mButton, nameof(mBiggerCluster.mButton));
//            Load -= OnLoad;
//            ClientSizeChanged -= OnClientSizeChanged;
//            mCancelCluster.mButton.Click -= OnCancelClick;
//            mBiggerCluster.mButton.Click -= OnBiggerClick;
//         }
//         base.Dispose(pDisposing);
//      }
//   }
//}
