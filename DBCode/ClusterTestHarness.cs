//namespace DBCode {
//   internal sealed class ClusterTestHarness : Form {
//      private readonly List<ClusterContainer> mContainers = [];
//      private readonly List<GroupBox> mGroupBoxes = [];
//      private readonly List<Font> mOwnedFonts = [];
//      private Panel mContentPanel;
//      private Button mCloseButton;

//      internal static void Show(string pCaption) {
//         using ClusterTestHarness harness = new ClusterTestHarness(pCaption);
//         harness.ShowDialog();
//      }

//      private ClusterTestHarness(string pCaption) {
//         ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
//         Text = pCaption;
//         AutoScaleMode = AutoScaleMode.None;
//         StartPosition = FormStartPosition.CenterScreen;
//         MinimumSize = new Size(600, 400);
//         MakeControls();
//         WireEvents();
//         LayoutControls();
//      }

//      private void MakeControls() {
//         ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
//         Color groupBoxBackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxBackground];
//         Color groupBoxForeColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxFont];
//         Font groupBoxFont = CreateNewBoldFont(mCurrentTheme.mFonts[(int)FontUsage.Interface]);
//         mOwnedFonts.Add(groupBoxFont);
//         BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.PanelBackground];
//         mContentPanel = new Panel {
//            Name = $"ClusterTestHarnessContentPanel{mTabIndex}",
//            TabIndex = mTabIndex++,
//            AutoScroll = true,
//            BackColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.PanelBackground]
//         };
//         mCloseButton = new Button {
//            Name = $"ClusterTestHarnessCloseButton{mTabIndex}",
//            TabIndex = mTabIndex++,
//            Text = "&Close",
//            AutoSize = true,
//            AutoSizeMode = AutoSizeMode.GrowAndShrink
//         };
//         Color[] swatchColors = [
//            Color.FromArgb(200, 60, 60),
//            Color.FromArgb(60, 160, 60),
//            Color.FromArgb(60, 60, 200),
//            Color.FromArgb(200, 160, 20),
//            Color.FromArgb(60, 170, 170),
//            Color.FromArgb(170, 60, 170),
//            Color.FromArgb(200, 100, 60),
//            Color.FromArgb(100, 200, 60),
//            Color.FromArgb(60, 100, 200)
//         ];
//         LabelPosition[] positions = [
//            LabelPosition.Left, LabelPosition.Right, LabelPosition.Top, LabelPosition.Bottom
//         ];
//         ClusterLayoutMode[] modes = [
//            ClusterLayoutMode.MaxWidth, ClusterLayoutMode.MaxHeight,
//            ClusterLayoutMode.FixedColumns, ClusterLayoutMode.FixedRows,
//            ClusterLayoutMode.AutoSquareGrid, ClusterLayoutMode.FlowLayout
//         ];
//         foreach (ClusterLayoutMode mode in modes) {
//            foreach (LabelPosition position in positions) {
//               List<BaseCluster> clusters = [];
//               for (int i = 0; i < 9; i++) {
//                  LabeledButtonColorSwatchCluster cluster = new LabeledButtonColorSwatchCluster(
//                     mCurrentTheme, $"Pair {i + 1}", $"&{i + 1}",
//                     ColorSwatchUsage.InterfaceBackground, position, swatchColors[i], groupBoxBackColor);
//                  cluster.SwatchClicked += OnSwatchClicked;
//                  clusters.Add(cluster);
//               }
//               int fixedColumns = 0, fixedRows = 0;
//               if (mode == ClusterLayoutMode.FixedColumns)
//                  fixedColumns = 3;
//               else if (mode == ClusterLayoutMode.FixedRows)
//                  fixedRows = 3;
//               ClusterContainer container = new ClusterContainer(mContentPanel, clusters, mode,
//                  0, 0, fixedColumns, fixedRows) {
//                  BackColor = groupBoxBackColor,
//                  AutoSize = false
//               };
//               mContainers.Add(container);
//               GroupBox groupBox = new GroupBox {
//                  Name = $"ClusterTestHarnessGroupBox{mTabIndex}",
//                  TabIndex = mTabIndex++,
//                  Text = $"{position} + {mode}",
//                  Font = groupBoxFont,
//                  ForeColor = groupBoxForeColor,
//                  BackColor = groupBoxBackColor
//               };
//               groupBox.Controls.Add(container);
//               mGroupBoxes.Add(groupBox);
//               mContentPanel.Controls.Add(groupBox);
//            }
//         }
//         Controls.AddRange([mContentPanel, mCloseButton]);
//      }

//      private static void OnSwatchClicked(LabeledButtonColorSwatchCluster pSender) { }

//      private void WireEvents() {
//         mCloseButton.Click += OnCloseClick;
//      }

//      private void LayoutControls() {
//         ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
//         LabelPosition[] positions = [
//            LabelPosition.Left, LabelPosition.Right, LabelPosition.Top, LabelPosition.Bottom
//         ];
//         ClusterLayoutMode[] modes = [
//            ClusterLayoutMode.MaxWidth, ClusterLayoutMode.MaxHeight,
//            ClusterLayoutMode.FixedColumns, ClusterLayoutMode.FixedRows,
//            ClusterLayoutMode.AutoSquareGrid, ClusterLayoutMode.FlowLayout
//         ];
//         int containerIndex = 0;
//         for (int modeIndex = 0; modeIndex < modes.Length; modeIndex++) {
//            for (int positionIndex = 0; positionIndex < positions.Length; positionIndex++) {
//               ClusterContainer container = mContainers[containerIndex];
//               GroupBox groupBox = mGroupBoxes[containerIndex];
//               containerIndex++;
//               foreach (BaseCluster cluster in container)
//                  cluster.LayoutCluster();
//               if (modes[modeIndex] == ClusterLayoutMode.FlowLayout) {
//                  int clusterWidth = container.mClusters[0].Width;
//                  container.Size = new Size(clusterWidth * 3 + mEm * 2 + mIndent + mRightPad, 1);
//               }
//               container.LayoutClusters();
//               int containerRight = 0, containerBottom = 0;
//               foreach (BaseCluster cluster in container) {
//                  if (cluster.Right > containerRight)
//                     containerRight = cluster.Right;
//                  if (cluster.Bottom > containerBottom)
//                     containerBottom = cluster.Bottom;
//               }
//               container.Size = new Size(containerRight + mEm, containerBottom + mEm);
//               container.Location = GetGroupBoxFirstLineOffset(groupBox);
//               SizeGroupBox(groupBox);
//            }
//         }
//         int maxGroupBoxWidth = 0, maxGroupBoxHeight = 0;
//         foreach (GroupBox groupBox in mGroupBoxes) {
//            if (groupBox.Width > maxGroupBoxWidth)
//               maxGroupBoxWidth = groupBox.Width;
//            if (groupBox.Height > maxGroupBoxHeight)
//               maxGroupBoxHeight = groupBox.Height;
//         }
//         int cellWidth = maxGroupBoxWidth + mEm;
//         int cellHeight = maxGroupBoxHeight + mEm;
//         containerIndex = 0;
//         for (int modeIndex = 0; modeIndex < modes.Length; modeIndex++) {
//            for (int positionIndex = 0; positionIndex < positions.Length; positionIndex++) {
//               GroupBox groupBox = mGroupBoxes[containerIndex++];
//               groupBox.Location = new Point(
//                  mIndent + positionIndex * cellWidth,
//                  mIndent + modeIndex * cellHeight);
//               groupBox.Size = new Size(maxGroupBoxWidth, maxGroupBoxHeight);
//            }
//         }
//         int totalContentWidth = mIndent + positions.Length * cellWidth;
//         int totalContentHeight = mIndent + modes.Length * cellHeight;
//         Screen screen = Screen.PrimaryScreen ?? Screen.AllScreens[0];
//         int formWidth = Math.Min(totalContentWidth + mEm * 2, (int)(screen.WorkingArea.Width * 0.95));
//         int formHeight = Math.Min(totalContentHeight + mCloseButton.Height + mEm * 4,
//            (int)(screen.WorkingArea.Height * 0.95));
//         ClientSize = new Size(formWidth, formHeight);
//         mCloseButton.Location = new Point(
//            ClientSize.Width - mCloseButton.Width - mEm,
//            ClientSize.Height - mCloseButton.Height - mEm);
//         mContentPanel.Location = Point.Empty;
//         mContentPanel.Size = new Size(ClientSize.Width, mCloseButton.Top - mEm);
//      }

//      private void OnCloseClick(object? pSender, EventArgs pEventArguments) {
//         Close();
//      }

//      protected override void Dispose(bool pDisposing) {
//         if (pDisposing) {
//            mCloseButton.Click -= OnCloseClick;
//            foreach (ClusterContainer container in mContainers)
//               foreach (BaseCluster cluster in container)
//                  if (cluster is LabeledButtonColorSwatchCluster swatchCluster)
//                     swatchCluster.SwatchClicked -= OnSwatchClicked;
//            foreach (Font font in mOwnedFonts)
//               MainForm.DisposeFontIfOwned(font);
//            mOwnedFonts.Clear();
//         }
//         base.Dispose(pDisposing);
//      }
//   }
//}
