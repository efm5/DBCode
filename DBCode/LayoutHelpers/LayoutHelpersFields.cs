namespace DBCode {
   internal static partial class LayoutHelpers {
      internal const int ADDRESS_BAR_PADDING = 30, BORDER = 4, CLIPBOARD_DELAY = 350, DETAILS_HORIZONTAL_PADDING = 1,
         DETAILS_VERTICAL_PADDING = 1, DOUBLE_BORDER = BORDER * 2, DOUBLE_OFFSET = OFFSET * 2, FIND_WIDTH = 200,
         HALF_BORDER = PANEL_BORDER / 2, INSET_BORDER = PANEL_BORDER / 4, LONG_DELAY = 450, MAIN_BORDER = 4,
         OFFSET = 5, PANEL_BORDER = 12, POST_CLIP_DELAY = 300, SHORT_DELAY = 50, TAB_INDEX_IGNORED = int.MaxValue - 1,
         WINDOW_REDUCER = 7;
      internal static float AnimationSpeedFast = 0.15f, AnimationSpeedNormal = 0.25f, AnimationSpeedSlow = 0.40f,
         FontScaleFactor = 1.0f, OpacityDisabled = 0.5f, OpacityHover = 0.85f, OpacityPressed = 0.70f,
         UiScaleFactor = 1.0f, mFontWidthAdjustment = 1.0f;
      internal static int BorderThicknessNormal = 2, BorderThicknessThick = 3, BorderThicknessThin = 1,
         ButtonWidthLarge = 100, ButtonWidthMedium = 80, ButtonWidthSmall = 60, CaretBlinkTime = 530, CaretWidth = 2,
         CheckBoxSize = 14, ComboBoxDropDownWidthLarge = 200, ComboBoxDropDownWidthMedium = 150,
         ComboBoxDropDownWidthSmall = 100, DialogPaddingBottom = 12, DialogPaddingLeft = 12, DialogPaddingRight = 12,
         DialogPaddingTop = 12, DoubleClickTime = 500, DragThreshold = 4, EmHeight = 16, EmWidth = 8,
         FocusRectanglePadding = 2, GroupBoxHeaderHeight = 18, GroupBoxPadding = 8, IconSizeLarge = 32,
         IconSizeMedium = 24, IconSizeSmall = 16, LabelWidthLarge = 160, LabelWidthMedium = 120, LabelWidthSmall = 80,
         ListRowHeight = 18, ListRowSpacing = 2, MaximumControlHeight = 2000, MaximumControlWidth = 2000,
         MenuBarHeight = 24, MinimumControlHeight = 20, MinimumControlWidth = 40, MonitorDpiX = 96, MonitorDpiY = 96,
         ProgressBarChunkSpacing = 2, ProgressBarChunkWidth = 6, ProgressBarHeight = 18, RadioButtonSize = 14,
         ScrollBarHeight = 17, ScrollBarWidth = 17, ScrollPadding = 4, SliderHeight = 22, SliderThumbWidth = 12,
         SplitterHeight = 4, SplitterWidth = 4, StatusBarHeight = 22, TabHeaderHeight = 24, TabHeaderSpacing = 6,
         TextBoxWidthLarge = 200, TextBoxWidthMedium = 150, TextBoxWidthSmall = 100, ThumbnailSizeLarge = 128,
         ThumbnailSizeMedium = 96, ThumbnailSizeSmall = 64, ToolStripButtonWidth = 23, ToolStripHeight = 25,
         TooltipDelayAutoPop = 5000, TooltipDelayInitial = 500, TooltipDelayReshow = 100, TooltipMaxWidth = 300,
         TreeIndent = 16, TreeNodeHeight = 18, WindowDefaultHeight = 600, WindowDefaultWidth = 800,
         WindowMinimumHeight = 200, WindowMinimumWidth = 300, mAssociatedButtonPostCheckBoxVerticalOffset = 0,
         mAssociatedButtonPostLabelHorizontalSpace = 0, mAssociatedButtonPostLabelVerticalOffset = 0,
         mAssociatedButtonPostTextBoxVerticalOffset = 0, mAssociatedCheckBoxPostButtonHorizontalSpace = 0,
         mAssociatedCheckBoxPostButtonVerticalOffset = 0, mAssociatedComboBoxPostButtonHorizontalSpace = 0,
         mAssociatedComboBoxPostButtonVerticalOffset = 0, mAssociatedLabelPostButtonHorizontalSpace = 0,
         mAssociatedLabelPostButtonVerticalOffset = 0, mAssociatedLabelPostCheckBoxHorizontalSpace = 0,
         mAssociatedLabelPostCheckBoxVerticalOffset = 0, mAssociatedLabelPostPanelVerticalOffset = 0,
         mAssociatedLabelPostUpDownHorizontalSpace = 0, mAssociatedLabelPostUpDownVerticalOffset = 0,
         mAssociatedPostVerticalOffset = 0, mAssociatedSliderPostUpDownHorizontalSpace = 0,
         mAssociatedSliderPostUpDownVerticalOffset = 0, mAssociatedTextBoxPostButtonHorizontalSpace = 0,
         mAssociatedTextBoxPostButtonVerticalOffset = 0, mAssociatedTextBoxPostCheckBoxVerticalOffset = 0,
         mAssociatedTextBoxPostComboBoxHorizontalSpace = 0, mAssociatedTextBoxPostComboBoxVerticalOffset = 0,
         mAssociatedTextPostButtonVerticalOffset = 0, mAssociatedUpDownPostButtonHorizontalSpace = 0,
         mAssociatedUpDownPostButtonVerticalOffset = 0, mAssociatedUpDownPostCheckBoxHorizontalSpace = 0,
         mAssociatedUpDownPostCheckBoxVerticalOffset = 0, mBottomButtonTop = 0, mCancelOffset = 0,
         mComboBoxMaxDropdownHeight = 0, mComboBoxMaxDropdownWidth = 0, mEm = 0, mEm2 = 0, mEm3 = 0, mEmHalf = 0,
         mGroupBottomPad = 0, mGroupLeftPad = 0, mGroupRightPad = 0, mGroupTopPad = 0, mHorizontalScrollOffset = 0,
         mIndent = 0, mMaximumGridWidth = 0, mMenuLeftOffset = 0, mOkOffset = 0, mScalePad = 0,
         mScalingGroupBoxTopLinePad = 0, mTabIndex = 1, mTitleBarHeight = 0, mVerticalScrollOffset = 0,
         mWidgetBigHorizontalSpace = 0, mWidgetBigVerticalOffset = 0, mWidgetHorizontalSpace = 0,
         mWidgetVerticalOffset = 0,
#pragma warning disable CS8602
         COMBOBOX_MAXIMUM_DROPDOWN_WIDTH = Screen.PrimaryScreen.WorkingArea.Width - 100;
#pragma warning restore CS8602
      internal static Size DefaultControlSize = new Size(100, 24), mMonitorSize = new Size(0, 0);
   }
}
