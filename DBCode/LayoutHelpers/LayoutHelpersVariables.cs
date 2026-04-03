namespace DBCode {
   internal static partial class LayoutHelpers {
#pragma warning disable IDE1006

      internal const int OFFSET = 5;
      internal const int DOUBLE_OFFSET = OFFSET * 2;

      internal const int POST_CLIP_DELAY = 300;
      internal const int SHORT_DELAY = 50;
      internal const int LONG_DELAY = 450;
      internal const int CLIPBOARD_DELAY = 350;

      internal const int FIND_WIDTH = 200;
      internal const int WINDOW_REDUCER = 7;

      // PANEL_BORDER should be evenly divisible by 4
      internal const int PANEL_BORDER = 12;
      internal const int MAIN_BORDER = 4;
      internal const int HALF_BORDER = PANEL_BORDER / 2;
      internal const int INSET_BORDER = PANEL_BORDER / 4;

      internal const int BORDER = 4;
      internal const int DOUBLE_BORDER = BORDER * 2;

      internal const int DETAILS_VERTICAL_PADDING = 1;
      internal const int DETAILS_HORIZONTAL_PADDING = 1;
      internal const int ADDRESS_BAR_PADDING = 30;

#pragma warning disable CS8602
      // Screen.PrimaryScreen may be annotated nullable, but is never null on Windows
      internal static readonly int COMBOBOX_MAXIMUM_DROPDOWN_WIDTH =
         Screen.PrimaryScreen.WorkingArea.Width - 100;
#pragma warning restore CS8602

      // Dynamic layout offsets recalculated by AdjustForThemeFont()
      internal static float mFontWidthAdjustment = 1.0f;

      internal static int mGroupLeftPad = 0;
      internal static int mGroupRightPad = 0;
      internal static int mGroupBottomPad = 0;
      internal static int mGroupTopPad = 0;
      internal static int mComboBoxMaxDropdownHeight = 0, mComboBoxMaxDropdownWidth = 0, mScalingGroupBoxTopLinePad = 0,
         mTitleBarHeight = 0, mScalePad = 0, mMaximumGridWidth = 0, mVerticalScrollOffset = 0, mHorizontalScrollOffset = 0,
         mEm = 0, mEmHalf = 0, mEm2 = 0, mEm3 = 0,
         mBottomButtonTop = 0, mAssociatedCheckBoxVerticalOffset = 0, mTabIndex = 1;

      internal static int mAssociatedButtonPostCheckBoxVerticalOffset = 0;
      internal static int mAssociatedButtonPostLabelHorizontalSpace = 0;
      internal static int mAssociatedButtonPostLabelVerticalOffset = 0;
      internal static int mAssociatedButtonPostTextBoxVerticalOffset = 0;

      internal static int mAssociatedCheckBoxPostButtonHorizontalSpace = 0;
      internal static int mAssociatedCheckBoxPostButtonVerticalOffset = 0;

      internal static int mAssociatedComboBoxPostButtonHorizontalSpace = 0;
      internal static int mAssociatedComboBoxPostButtonVerticalOffset = 0;

      internal static int mAssociatedLabelPostButtonHorizontalSpace = 0;
      internal static int mAssociatedLabelPostButtonVerticalOffset = 0;
      internal static int mAssociatedLabelPostCheckBoxHorizontalSpace = 0;
      internal static int mAssociatedLabelPostCheckBoxVerticalOffset = 0;
      internal static int mAssociatedLabelPostPanelVerticalOffset = 0;
      internal static int mAssociatedLabelPostUpDownHorizontalSpace = 0;
      internal static int mAssociatedLabelPostUpDownVerticalOffset = 0;

      internal static int mAssociatedPostVerticalOffset = 0;

      internal static int mAssociatedSliderPostUpDownHorizontalSpace = 0;
      internal static int mAssociatedSliderPostUpDownVerticalOffset = 0;

      internal static int mAssociatedTextBoxPostButtonHorizontalSpace = 0;
      internal static int mAssociatedTextBoxPostButtonVerticalOffset = 0;
      internal static int mAssociatedTextBoxPostCheckBoxVerticalOffset = 0;
      internal static int mAssociatedTextBoxPostComboBoxHorizontalSpace = 0;
      internal static int mAssociatedTextBoxPostComboBoxVerticalOffset = 0;

      internal static int mAssociatedTextPostButtonVerticalOffset = 0;

      internal static int mAssociatedUpDownPostButtonHorizontalSpace = 0;
      internal static int mAssociatedUpDownPostButtonVerticalOffset = 0;
      internal static int mAssociatedUpDownPostCheckBoxHorizontalSpace = 0;
      internal static int mAssociatedUpDownPostCheckBoxVerticalOffset = 0;

      internal static int mIndent = 0;
      internal static int mCancelOffset = 0;
      internal static int mOkOffset = 0;

      internal static int mWidgetHorizontalSpace = 0;
      internal static int mWidgetBigHorizontalSpace = 0;
      internal static int mWidgetVerticalOffset = 0;
      internal static int mWidgetBigVerticalOffset = 0;

      internal static int mEM = 0;
      internal static int mMenuLeftOffset = 0;

      internal static Size mMonitorSize = new Size(0, 0);

#pragma warning restore IDE1006
   }
}
