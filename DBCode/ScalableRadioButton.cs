namespace DBCode {
   internal static class ScalableRadioButtons {
      private const string mUnselectedRadioButtonPrefix = "○ ", mSelectedRadioButtonPrefix = "⬤ ";
      private static int mRadioPanelTabBase = 0;

      private static void ScalableRadioButton_Click(object? pSender, EventArgs pEventArgs) {
         ThrowIfNull(pSender, nameof(pSender));
         ScalableRadioButton? myRadioButton = pSender as ScalableRadioButton;
         if (myRadioButton == null)
            ThrowBadCode($"{nameof(myRadioButton)} ScalableRadioButton_Click: bad pSender.");
         bool currentlyChecked = myRadioButton!.mChecked;

         if (currentlyChecked)
            return;
         ThrowIfNull(myRadioButton.mRadioPanel, nameof(myRadioButton.mRadioPanel));
         foreach (ScalableRadioButton button in myRadioButton.mRadioPanel.mScalableRadioButtons)
            button.mChecked = false;
         myRadioButton.mChecked = true;
         myRadioButton.mRadioPanel.PaintButtons();
      }

      public class ScalableRadioButton : Button {
         public bool mChecked = false;
         public int mReturnValue = 0;
         public RadioPanel? mRadioPanel = null;

         public ScalableRadioButton(RadioPanel pRadioPanel, int pReturnValue) {
            mRadioPanel = pRadioPanel;
            mReturnValue = pReturnValue;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            TextAlign = ContentAlignment.MiddleLeft;
            ImageAlign = ContentAlignment.MiddleLeft;
            TextImageRelation = TextImageRelation.ImageBeforeText;
         }
      }

      public class RadioButtonQuad {
         public string mText = string.Empty;
         public string mTag = string.Empty;
         public bool mInitiallyChecked = false;
         public int mReturnValue = 0;

#pragma warning disable IDE0290
         public RadioButtonQuad(string pText, string pTag, bool pInitiallyChecked, int pReturnValue) {
            mText = pText;
            mTag = pTag;
            mInitiallyChecked = pInitiallyChecked;
            mReturnValue = pReturnValue;
         }
#pragma warning restore IDE0290
      }

      public class RadioPanel : Panel {
         public List<ScalableRadioButton> mScalableRadioButtons = [];
         private Theme? mTheme = null;
         private bool mInterface = false, mHorizontal = true, mLayout = true;
         private int mMaximumWidth = 50;

         public RadioPanel(List<RadioButtonQuad>? pRadioButtonQuads, Theme pTheme, bool pInterface,
            bool pHorizontal = true, int pMaximumWidth = 0,
            bool pLayout = true) {
            if ((pRadioButtonQuads == null) || (pRadioButtonQuads.Count < 2))
               ThrowBadCode("ScalableRadioButtons.RadioPanel: pRadioButtonQuads was null or had fewer than 2 items.");
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            HashSet<string> quadNames = [];
            foreach (RadioButtonQuad quad in pRadioButtonQuads!) {
               if (!quadNames.Add(quad.mText.Replace("&", string.Empty).Replace(" ", string.Empty))) {
                  TimedMessage("The “RadioButtonQuad” list contained more than one item\n" +
                     "whose texts would resolve to duplicate names.", "Programming ERROR", 0);
                  return;
               }
            }
            mTheme = pTheme;
            mInterface = pInterface;
            mMaximumWidth = pMaximumWidth;
            mHorizontal = pHorizontal;
            mLayout = pLayout;
            foreach (RadioButtonQuad quad in pRadioButtonQuads) {
               string name = quad.mText.Replace("&", string.Empty).Replace(" ", string.Empty);
               name += ("MyRadioPanelButton" + string.Format("{0}", mRadioPanelTabBase));
               ScalableRadioButton button = new ScalableRadioButton(this, quad.mReturnValue) {
                  mChecked = quad.mInitiallyChecked,
                  Text = mUnselectedRadioButtonPrefix + quad.mText,
                  TextAlign = ContentAlignment.MiddleLeft,
                  ImageAlign = ContentAlignment.MiddleLeft,
                  Name = name,
                  Tag = quad.mTag,
                  TabIndex = mRadioPanelTabBase++,
                  TextImageRelation = TextImageRelation.ImageBeforeText
               };
               button.Click += ScalableRadioButton_Click;
               mScalableRadioButtons.Add(button);
            }
            foreach (ScalableRadioButton button in mScalableRadioButtons)
               Controls.Add(button);
            LayoutRadioPanel();
         }

         internal void LayoutRadioPanel() {
            PaintButtons();
            if (mLayout) {
               if (mMaximumWidth != 0) {
                  List<Control> locatingControls = [.. mScalableRadioButtons];
                  WidgetLayout(locatingControls, mMaximumWidth);
               }
               else {
                  if (mHorizontal) {
                     int left = 1;
                     foreach (ScalableRadioButton button in mScalableRadioButtons) {
                        button.Location = new Point(left, 1);
                        left += (button.Width + mIndent);
                     }
                  }
                  else {
                     int top = 1;
                     foreach (ScalableRadioButton button in mScalableRadioButtons) {
                        button.Location = new Point(1, top);
                        top += (button.Height + mIndent);
                     }
                  }
               }
               SizePanel(this);
            }
            else {
               AutoSize = true;
               AutoSizeMode = AutoSizeMode.GrowAndShrink;
            }
         }

         public void PaintButtons() {
            PaintFlatScalableRadioButtonList(mScalableRadioButtons, mTheme, mInterface);
         }

         private static void PaintFlatScalableRadioButton(ScalableRadioButton pControl, Theme pTheme,
            bool pInterface = false) {
            pControl.Font = CreateNewFont();
            if (pInterface) {
               pControl.ForeColor = pTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont];
               pControl.FlatAppearance.BorderColor =
                  pTheme.mInterfaceColors[(int)ColorSwatchUsage.InterfaceFont];
            }
            else {
               pControl.ForeColor = pTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxFont];
               pControl.FlatAppearance.BorderColor =
                  pTheme.mInterfaceColors[(int)ColorSwatchUsage.GroupBoxFont];
            }
            pControl.BackColor = Color.Transparent;
            pControl.FlatAppearance.BorderSize = 0;
            pControl.FlatStyle = FlatStyle.Flat;
            string words = pControl.Text.Substring(2);
            if (pControl.mChecked)
               pControl.Text = mSelectedRadioButtonPrefix + words;
            else
               pControl.Text = mUnselectedRadioButtonPrefix + words;
         }

         private static void PaintFlatScalableRadioButtonList(List<ScalableRadioButton> pScalableRadioButtons,
            Theme? pTheme, bool pInterface = false) {
            ThrowIfNull(pTheme, nameof(pTheme));

            foreach (ScalableRadioButton button in pScalableRadioButtons)
               PaintFlatScalableRadioButton(button, pTheme, pInterface);
         }

         public ScalableRadioButton? GetChecked() {
            foreach (ScalableRadioButton button in mScalableRadioButtons)
               if (button.mChecked)
                  return button;
            return null;
         }

         public void SetCheckedByTag(string pTag) {
            foreach (ScalableRadioButton button in mScalableRadioButtons)
               button.mChecked = button.Tag?.ToString() == pTag;
            PaintButtons();
         }

         public List<ScalableRadioButton> GetScalableRadioButtons() {
            return mScalableRadioButtons;
         }

         public bool GetReturnValue(out int pOValue) {
            pOValue = 0;
            foreach (ScalableRadioButton button in mScalableRadioButtons)
               if (button.mChecked) {
                  pOValue = button.mReturnValue;
                  return true;
               }
            return false;
         }

         public int AssignTabIndexes(int pIndex) {
            foreach (Control control in Controls)
               control.TabIndex = pIndex++;
            return pIndex;
         }

         protected override void Dispose(bool pDisposing) {
            if (pDisposing) {
               foreach (ScalableRadioButton button in mScalableRadioButtons)
                  button.Click -= ScalableRadioButton_Click;
            }
            base.Dispose(pDisposing);
         }
      }
   }
}
