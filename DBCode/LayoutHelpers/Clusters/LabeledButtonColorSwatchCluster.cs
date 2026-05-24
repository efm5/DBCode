namespace DBCode {
   internal static partial class LayoutHelpers {
      internal delegate void ClusterSwatchClickedHandler(LabeledButtonColorSwatchCluster pSender);

      internal sealed class LabeledButtonColorSwatchCluster : BaseCluster {
         private Button mButton;
         private ColorSwatch mSwatch;
         private Label mLabel;

         public event ClusterSwatchClickedHandler? SwatchClicked;

         public LabeledButtonColorSwatchCluster(Theme pTheme, string pLabelText, string pButtonText,
            ColorSwatchUsage pUsage, LabelPosition pLabelPosition, Color pInitialColor,
            Color? pBackgroundColor = null) : base(pTheme, pBackgroundColor) {
            ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
            mLabelPosition = pLabelPosition;
            mLabel = new Label() {
               TabIndex = TAB_INDEX_IGNORED,
               Name = $"LabeledButtonColorSwatchCluster{nameof(mLabel)}{mTabIndex++}",
               Text = pLabelText,
               AutoSize = true,
               TextAlign = ContentAlignment.MiddleCenter,
               Font = CreateNewFont(),
               ForeColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont],
               BackColor = pBackgroundColor ?? Color.Transparent
            };
            mButton = new Button() {
               TabIndex = mTabIndex,
               Name = $"LabeledButtonColorSwatchCluster{nameof(mButton)}{mTabIndex++}",
               Text = pButtonText,
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               Font = CreateNewFont(),
               ForeColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont],
               Tag = pUsage
            };
            mButton.Click += OnClusterClicked;
            mSwatch = new ColorSwatch(pUsage, pInitialColor, -1);
            mSwatch.ColorSwatchClicked += OnSwatchClicked;
            Controls.AddRange([mLabel, mButton, mSwatch]);
            LayoutControls();
         }

         public LabeledButtonColorSwatchCluster(Theme pTheme, string pLabelText, string pButtonText,
            TokenKind pTokenKind, LabelPosition pLabelPosition, Color pInitialColor,
            Color? pBackgroundColor = null) : base(pTheme, pBackgroundColor) {
            ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
            mLabelPosition = pLabelPosition;
            mLabel = new Label() {
               TabIndex = TAB_INDEX_IGNORED,
               Name = $"LabeledButtonColorSwatchCluster{nameof(mLabel)}{mTabIndex++}",
               Text = pLabelText,
               AutoSize = true,
               TextAlign = ContentAlignment.MiddleCenter,
               Font = CreateNewFont(),
               ForeColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont],
               BackColor = pBackgroundColor ?? Color.Transparent
            };
            mButton = new Button() {
               TabIndex = mTabIndex,
               Name = $"LabeledButtonColorSwatchCluster{nameof(mButton)}{mTabIndex++}",
               Text = pButtonText,
               AutoSize = true,
               AutoSizeMode = AutoSizeMode.GrowAndShrink,
               Font = CreateNewFont(),
               ForeColor = mCurrentTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont],
               Tag = pTokenKind
            };
            mButton.Click += OnClusterClicked;
            mSwatch = new ColorSwatch(pTokenKind, pInitialColor, -1);
            mSwatch.SyntaxSwatchClicked += OnSyntaxSwatchClicked;
            Controls.AddRange([mLabel, mButton, mSwatch]);
            LayoutControls();
         }

         private void OnClusterClicked(object? pSender, EventArgs pArgs) {
            ThrowIfNull(SwatchClicked, nameof(SwatchClicked));
            SwatchClicked.Invoke(this);
         }

         private void OnSwatchClicked(object? pSender, ColorSwatchUsage pUsage) {
            ThrowIfNull(SwatchClicked, nameof(SwatchClicked));
            SwatchClicked.Invoke(this);
         }

         private void OnSyntaxSwatchClicked(object? pSender, TokenKind pTokenKind) {
            ThrowIfNull(SwatchClicked, nameof(SwatchClicked));
            SwatchClicked.Invoke(this);
         }

         internal override void LayoutCluster() {
            SetFontAndColor();
            ApplyLabelPosition(mLabel, mButton, mSwatch);
            if (mButton.Tag is ColorSwatchUsage usage)
               mSwatch.BackColor = mTheme.mInterfaceColors[(int)usage];
            mLabel.Invalidate();
            mButton.Invalidate();
            mSwatch.Invalidate();
         }

         internal override void LayoutControlsOnly() {
            ApplyLabelPosition(mLabel, mButton, mSwatch);
            if (mButton.Tag is ColorSwatchUsage usage)
               mSwatch.BackColor = mTheme.mInterfaceColors[(int)usage];
            mLabel.Invalidate();
            mButton.Invalidate();
            mSwatch.Invalidate();
         }

         private void LayoutControls() {
            if (mLabelPosition == LabelPosition.Left) {
               mLabel.Left = 0;
               mLabel.Top = 0;
               mButton.Left = mLabel.Right + mEm;
               mButton.Top = 0;
               mSwatch.Left = mButton.Right + mEm;
               mSwatch.Top = 0;
            }
            else if (mLabelPosition == LabelPosition.Right) {
               mButton.Left = 0;
               mButton.Top = 0;
               mSwatch.Left = mButton.Right + mEm;
               mSwatch.Top = 0;
               mLabel.Left = mSwatch.Right + mEm;
               mLabel.Top = 0;
            }
            else if (mLabelPosition == LabelPosition.Top) {
               mLabel.Left = 0;
               mLabel.Top = 0;
               mButton.Left = 0;
               mButton.Top = mLabel.Bottom + mEmHalf;
               mSwatch.Left = mButton.Right + mEm;
               mSwatch.Top = mButton.Top;
            }
            else {
               mButton.Left = 0;
               mButton.Top = 0;
               mSwatch.Left = mButton.Right + mEm;
               mSwatch.Top = 0;
               mLabel.Left = 0;
               mLabel.Top = Math.Max(mButton.Bottom, mSwatch.Bottom) + mEmHalf;
            }
         }

         public void SetButtonText(string pButtonText) {
            mButton.Text = pButtonText;
         }

         public void SetSize(int pSize) {
            mSwatch.SetSize(pSize);
         }

         public Color GetColor() {
            return mSwatch.BackColor;
         }

         public void SetColor(Color pColor) {
            mSwatch.SetColor(pColor);
         }

         internal override void SetFontAndColor() {
            Theme.ThemeInterfaceThings(mTheme, out Font poFont, out Color poForeColor, out Color poBackColor);
            UpdateFont(mLabel, CreateNewFont(poFont));
            mLabel.ForeColor = poForeColor;
            mLabel.BackColor = poBackColor;
            UpdateFont(mButton, CreateNewFont(poFont));
            mButton.ForeColor = poForeColor;
            mButton.BackColor = poBackColor;
         }

         protected override void Dispose(bool pDisposing) {
            if (pDisposing) {
               mButton.Click -= OnClusterClicked;
               mSwatch.ColorSwatchClicked -= OnSwatchClicked;
               mSwatch.SyntaxSwatchClicked -= OnSyntaxSwatchClicked;
               MainForm.DisposeFontIfOwned(mLabel.Font);
               MainForm.DisposeFontIfOwned(mButton.Font);
            }
            base.Dispose(pDisposing);
         }
      }
   }
}
