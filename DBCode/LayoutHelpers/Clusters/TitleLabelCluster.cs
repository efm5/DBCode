namespace DBCode {
   internal static partial class LayoutHelpers {
      internal sealed class TitleLabelCluster : BaseCluster {
         internal Label mLabel { get; private set; }
         internal Color? mBackgroundColor;
         internal float mSize;

         public TitleLabelCluster(string pTitle, float pSize, Color? pBackgroundColor = null) : base(pBackgroundColor) {
            mBackgroundColor = pBackgroundColor;
            mSize = pSize;
            Dock = DockStyle.Top;
            mLabel = new Label() {
               TabIndex = LayoutHelpers.TAB_INDEX_IGNORED,
               Name = $"TitleLabelCluster{nameof(mLabel)}{mTabIndex++}",
               Text = pTitle,
               Top = mEm,
               TextAlign = ContentAlignment.MiddleCenter,
               Dock = DockStyle.Fill,
               Height = 40,
               Font = LayoutHelpers.CreateNewTitleFont(pSize),
               ForeColor = mCurrentTheme!.mColors[(int)ColorUsage.InterfaceFont],
               BackColor = pBackgroundColor ?? Color.Transparent
            };
            Controls.Add(mLabel);
         }

         protected override void LayoutCluster() {
            mLabel.Font = LayoutHelpers.CreateNewTitleFont(mSize);
            mLabel.ForeColor = mCurrentTheme!.mColors[(int)ColorUsage.InterfaceFont];
            mLabel.BackColor = mBackgroundColor ?? Color.Transparent;
         }
      }
   }
}
