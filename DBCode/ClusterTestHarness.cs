//namespace DBCode {
//   internal sealed class ClusterTestHarness : Form {
//      private ButtonCluster? mButtonClusterNoOp;
//      private ButtonCluster? mButtonClusterWithClick;
//      private FlattenedButtonCluster? mFlattenedNoColon;
//      private FlattenedButtonCluster? mFlattenedWithColon;
//      private FlattenedButtonCluster? mFlattenedNullBackground;
//      private TextBox? mTextBox;
//      private Label? mAssocLabel1;
//      private Label? mAssocLabel3;
//      private Panel? mContentPanel;
//      private Label? mResultLabel;
//      private Button? mCloseButton;

//      internal static void Show(string pCaption) {
//         using ClusterTestHarness harness = new ClusterTestHarness(pCaption);
//         harness.ShowDialog();
//      }

//      private ClusterTestHarness(string pCaption) {
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
//         mContentPanel = new Panel {
//            AutoSize = false,
//            AutoScroll = true,
//            BackColor = Color.BlueViolet,
//            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom
//         };
//         Controls.Add(mContentPanel);
//         mButtonClusterNoOp = new ButtonCluster(mCurrentTheme!, "No-Op Button");
//         mButtonClusterWithClick = new ButtonCluster(mCurrentTheme!, "Click Me");
//         mAssocLabel1 = new Label {
//            Name = $"ClusterTestHarnessAssoc1{mTabIndex}",
//            TabIndex = TAB_INDEX_IGNORED,
//            Text = "(assoc label)",
//            AutoSize = true,
//            Font = CreateNewFont(),
//            ForeColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont],
//            BackColor = Color.Transparent
//         };
//         mFlattenedWithColon = new FlattenedButtonCluster(mCurrentTheme!, "Already has colon:", mAssocLabel1, BackColor);
//         mTextBox = new TextBox {
//            Size = new Size(200, 30),
//            BackColor = Color.Aquamarine
//         };
//         mFlattenedNoColon = new FlattenedButtonCluster(mCurrentTheme!, "No colon", mTextBox, BackColor);
//         mAssocLabel3 = new Label {
//            Name = $"ClusterTestHarnessAssoc3{mTabIndex}",
//            TabIndex = TAB_INDEX_IGNORED,
//            Text = "(assoc label)",
//            AutoSize = true,
//            Font = CreateNewFont(),
//            ForeColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont],
//            BackColor = Color.Transparent
//         };
//         mFlattenedNullBackground = new FlattenedButtonCluster(mCurrentTheme!, "Null bg (not flat)", mAssocLabel3, null);
//         mResultLabel = new Label {
//            Name = $"ClusterTestHarnessResult{mTabIndex}",
//            TabIndex = TAB_INDEX_IGNORED,
//            Text = "Click a cluster button to see output here.",
//            AutoSize = true,
//            Font = CreateNewFont(),
//            ForeColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont],
//            BackColor = Color.Transparent
//         };
//         mContentPanel.Controls.AddRange([
//            mButtonClusterNoOp,
//            mButtonClusterWithClick,
//            mFlattenedWithColon,
//            mFlattenedNoColon,
//            mFlattenedNullBackground,
//            mResultLabel
//         ]);
//         mCloseButton = new Button {
//            Name = $"ClusterTestHarnessClose{mTabIndex}",
//            TabIndex = mTabIndex++,
//            Text = "&Close",
//            AutoSize = true,
//            AutoSizeMode = AutoSizeMode.GrowAndShrink,
//            Font = CreateNewFont()
//         };
//         Controls.Add(mCloseButton);
//      }

//      private void WireEvents() {
//         ThrowIfNull(mButtonClusterNoOp, nameof(mButtonClusterNoOp));
//         ThrowIfNull(mButtonClusterWithClick, nameof(mButtonClusterWithClick));
//         ThrowIfNull(mFlattenedWithColon, nameof(mFlattenedWithColon));
//         ThrowIfNull(mFlattenedNoColon, nameof(mFlattenedNoColon));
//         ThrowIfNull(mFlattenedNullBackground, nameof(mFlattenedNullBackground));
//         ThrowIfNull(mCloseButton, nameof(mCloseButton));
//         ThrowIfNull(mButtonClusterNoOp.mButton, nameof(mButtonClusterNoOp.mButton));
//         ThrowIfNull(mButtonClusterWithClick.mButton, nameof(mButtonClusterWithClick.mButton));
//         ThrowIfNull(mFlattenedWithColon.mButton, nameof(mFlattenedWithColon.mButton));
//         ThrowIfNull(mFlattenedNoColon.mButton, nameof(mFlattenedNoColon.mButton));
//         ThrowIfNull(mFlattenedNullBackground.mButton, nameof(mFlattenedNullBackground.mButton));

//         Load += OnLoad;
//         ClientSizeChanged += OnClientSizeChanged;
//         mCloseButton.Click += OnCloseClick;
//         mButtonClusterNoOp.mButton.Click += OnNoOpButtonClick;
//         mButtonClusterWithClick.mButton.Click += OnClickMeClick;
//         mFlattenedWithColon.mButton.Click += OnFlattenedWithColonClick;
//         mFlattenedNoColon.mButton.Click += OnFlattenedNoColonClick;
//         mFlattenedNullBackground.mButton.Click += OnFlattenedNullBgClick;
//      }

//      private void LayoutControls() {
//         ThrowIfNull(mContentPanel, nameof(mContentPanel));
//         ThrowIfNull(mCloseButton, nameof(mCloseButton));
//         ThrowIfNull(mResultLabel, nameof(mResultLabel));
//         const int padding = 12, rowSpacing = 8, closeMargin = 8;
//         int closeHeight = mCloseButton.Height + closeMargin * 2;
//         mContentPanel.Location = new Point(padding, padding);
//         mContentPanel.Size = new Size(
//            ClientSize.Width - padding * 2,
//            ClientSize.Height - padding - closeHeight - padding);
//         int y = padding;
//         LayoutClusterRow(mButtonClusterNoOp!, ref y, rowSpacing);
//         LayoutClusterRow(mButtonClusterWithClick!, ref y, rowSpacing);
//         LayoutClusterRow(mFlattenedWithColon!, ref y, rowSpacing);
//         LayoutClusterRow(mFlattenedNoColon!, ref y, rowSpacing);
//         LayoutClusterRow(mFlattenedNullBackground!, ref y, rowSpacing);
//         mResultLabel.Location = new Point(padding, y + rowSpacing);
//         mCloseButton.Location = new Point(
//            ClientSize.Width - mCloseButton.Width - closeMargin,
//            ClientSize.Height - mCloseButton.Height - closeMargin);
//      }

//      private static void LayoutClusterRow(BaseCluster pCluster, ref int pY, int pSpacing) {
//         pCluster.LayoutCluster();
//         pCluster.Location = new Point(12, pY);
//         pY += pCluster.Height + pSpacing;
//      }

//      private void OnLoad(object? pSender, EventArgs pEventArgs) {
//         VerifyColonInvariant();
//         VerifyNoOpLayoutControls();
//      }

//      private void OnClientSizeChanged(object? pSender, EventArgs pEventArgs) {
//         LayoutControls();
//      }

//      private void OnCloseClick(object? pSender, EventArgs pEventArgs) {
//         Close();
//      }

//      private void OnNoOpButtonClick(object? pSender, EventArgs pEventArgs) {
//         ThrowIfNull(mButtonClusterNoOp, nameof(mButtonClusterNoOp));
//         ThrowIfNull(mButtonClusterNoOp.mButton, nameof(mButtonClusterNoOp.mButton));
//         Rectangle before = mButtonClusterNoOp.mButton.Bounds;
//         mButtonClusterNoOp.LayoutControls();
//         Rectangle after = mButtonClusterNoOp.mButton.Bounds;
//         string result = before == after
//            ? $"PASS – ButtonCluster.LayoutControls() is a no-op. Bounds before and after: {before}."
//            : $"FAIL – ButtonCluster.LayoutControls() changed button bounds from {before} to {after}.";
//         SetResult(result);
//      }

//      private void OnClickMeClick(object? pSender, EventArgs pEventArgs) {
//         SetResult("PASS – ButtonCluster click event fired correctly.");
//      }

//      private void OnFlattenedWithColonClick(object? pSender, EventArgs pEventArgs) {
//         ThrowIfNull(mFlattenedWithColon, nameof(mFlattenedWithColon));
//         ThrowIfNull(mFlattenedWithColon.mButton, nameof(mFlattenedWithColon.mButton));
//         string buttonText = mFlattenedWithColon.mButton.Text;
//         string result = buttonText.EndsWith(':') && buttonText.IndexOf(':', StringComparison.Ordinal) == buttonText.Length - 1
//            ? $"PASS – Colon not doubled. Button text is \"{buttonText}\"."
//            : $"FAIL – Unexpected button text: \"{buttonText}\".";
//         SetResult(result);
//      }

//      private void OnFlattenedNoColonClick(object? pSender, EventArgs pEventArgs) {
//         ThrowIfNull(mFlattenedNoColon, nameof(mFlattenedNoColon));
//         ThrowIfNull(mFlattenedNoColon.mButton, nameof(mFlattenedNoColon.mButton));
//         ThrowIfNull(mTextBox, nameof(mTextBox));
//         string buttonText = mFlattenedNoColon.mButton.Text;
//         string result = buttonText.EndsWith(':')
//            ? $"PASS – Colon was appended. Button text is \"{buttonText}\"."
//            : $"FAIL – Colon was not appended. Button text is \"{buttonText}\".";
//         SetResult(result);
//         mTextBox.Text = buttonText;
//         mTextBox.Focus();
//      }

//      private void OnFlattenedNullBgClick(object? pSender, EventArgs pEventArgs) {
//         ThrowIfNull(mFlattenedNullBackground, nameof(mFlattenedNullBackground));
//         ThrowIfNull(mFlattenedNullBackground.mButton, nameof(mFlattenedNullBackground.mButton));
//         Button button = mFlattenedNullBackground.mButton;
//         bool isFlat = button.FlatStyle == FlatStyle.Flat;
//         string result = !isFlat
//            ? $"PASS – Null background correctly skips FlattenButton. FlatStyle is {button.FlatStyle}."
//            : $"FAIL – Button was flattened despite null background.";
//         SetResult(result);
//      }

//      private void VerifyColonInvariant() {
//         ThrowIfNull(mFlattenedWithColon, nameof(mFlattenedWithColon));
//         ThrowIfNull(mFlattenedWithColon.mButton, nameof(mFlattenedWithColon.mButton));
//         ThrowIfNull(mFlattenedNoColon, nameof(mFlattenedNoColon));
//         ThrowIfNull(mFlattenedNoColon.mButton, nameof(mFlattenedNoColon.mButton));
//         ThrowIfNull(mFlattenedNullBackground, nameof(mFlattenedNullBackground));
//         ThrowIfNull(mFlattenedNullBackground.mButton, nameof(mFlattenedNullBackground.mButton));
//         if (!mFlattenedWithColon.mButton.Text.EndsWith(':'))
//            SetResult("FAIL – mFlattenedWithColon button text does not end in ':'.");
//         else if (!mFlattenedNoColon.mButton.Text.EndsWith(':'))
//            SetResult("FAIL – mFlattenedNoColon button text does not end in ':'.");
//         else if (!mFlattenedNullBackground.mButton.Text.EndsWith(':'))
//            SetResult("FAIL – mFlattenedNullBackground button text does not end in ':'.");
//         else
//            SetResult("Load – Colon invariant PASS for all FlattenedButtonCluster instances. Click a button to run its test.");
//      }

//      private void VerifyNoOpLayoutControls() {
//         ThrowIfNull(mButtonClusterNoOp, nameof(mButtonClusterNoOp));
//         ThrowIfNull(mButtonClusterNoOp.mButton, nameof(mButtonClusterNoOp.mButton));
//         Rectangle b1 = mButtonClusterNoOp.mButton.Bounds;
//         mButtonClusterNoOp.LayoutControls();
//         Rectangle b2 = mButtonClusterNoOp.mButton.Bounds;
//         mButtonClusterNoOp.LayoutControls();
//         Rectangle b3 = mButtonClusterNoOp.mButton.Bounds;
//         if (b1 != b2 || b2 != b3) // else: colon-invariant message already shown; no need to overwrite it
//            SetResult($"FAIL – ButtonCluster.LayoutControls() is NOT a no-op. Bounds: {b1} → {b2} → {b3}");
//      }

//      private void SetResult(string pMessage) {
//         ThrowIfNull(mResultLabel, nameof(mResultLabel));
//         mResultLabel.Text = pMessage;
//         mResultLabel.Refresh();
//      }

//      protected override void Dispose(bool pDisposing) {
//         ThrowIfNull(mResultLabel, nameof(mResultLabel));
//         ThrowIfNull(mCloseButton, nameof(mCloseButton));
//         ThrowIfNull(mButtonClusterNoOp, nameof(mButtonClusterNoOp));
//         ThrowIfNull(mButtonClusterNoOp.mButton, nameof(mButtonClusterNoOp.mButton));
//         ThrowIfNull(mButtonClusterWithClick, nameof(mButtonClusterWithClick));
//         ThrowIfNull(mButtonClusterWithClick.mButton, nameof(mButtonClusterWithClick.mButton));
//         ThrowIfNull(mFlattenedWithColon, nameof(mFlattenedWithColon));
//         ThrowIfNull(mFlattenedWithColon.mButton, nameof(mFlattenedWithColon.mButton));
//         ThrowIfNull(mFlattenedNoColon, nameof(mFlattenedNoColon));
//         ThrowIfNull(mFlattenedNoColon.mButton, nameof(mFlattenedNoColon.mButton));
//         ThrowIfNull(mFlattenedNullBackground, nameof(mFlattenedNullBackground));
//         ThrowIfNull(mFlattenedNullBackground.mButton, nameof(mFlattenedNullBackground.mButton));
//         ThrowIfNull(mAssocLabel1, nameof(mAssocLabel1));
//         ThrowIfNull(mAssocLabel3, nameof(mAssocLabel3));
//         if (pDisposing) {
//            Load -= OnLoad;
//            ClientSizeChanged -= OnClientSizeChanged;
//            mCloseButton.Click -= OnCloseClick;
//            mButtonClusterNoOp.mButton.Click -= OnNoOpButtonClick;
//            mButtonClusterWithClick.mButton.Click -= OnClickMeClick;
//            mFlattenedWithColon.mButton.Click -= OnFlattenedWithColonClick;
//            mFlattenedNoColon.mButton.Click -= OnFlattenedNoColonClick;
//            mFlattenedNullBackground.mButton.Click -= OnFlattenedNullBgClick;
//            MainForm.DisposeFontIfOwned(mResultLabel.Font);
//            MainForm.DisposeFontIfOwned(mCloseButton.Font);
//            MainForm.DisposeFontIfOwned(mAssocLabel1.Font);
//            MainForm.DisposeFontIfOwned(mAssocLabel3.Font);
//         }
//         base.Dispose(pDisposing); // base walks Controls tree — clusters disposed here
//      }
//   }
//}
