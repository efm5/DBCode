namespace DBCode {
   internal static partial class LayoutHelpers {
      internal enum ColorSwatchUsage {
         // Theme Panel
         [DisplayText("Panel Background Color")]
         PanelBackground,
         [DisplayText("GroupBox Background Color")]
         GroupBoxBackground,
         [DisplayText("GroupBox Font Color")]
         GroupBoxFont,
         [DisplayText("Interface Background Color")]
         InterfaceBackground,
         [DisplayText("Interface Font Color")]
         InterfaceFont,
         [DisplayText("Menu Background Color")]
         MenuBackground,
         [DisplayText("Menu Font Color")]
         MenuFont,
         [DisplayText("Status Bar Background Color")]
         StatusBackground,
         [DisplayText("Status Bar Font Color")]
         StatusFont,
         [DisplayText("Text Box Font Color")]
         TextBoxFont,
         [DisplayText("Text Box Color")]
         TextBox,
         [DisplayText("Tab Header Selected Background Color")]
         TabHeaderSelectedBackground,
         [DisplayText("Tab Header Unselected Background Color")]
         TabHeaderUnselectedBackground,
         [DisplayText("Tab Header Selected Font Color")]
         TabHeaderSelectedFont,
         [DisplayText("Tab Header Unselected Color")]
         TabHeaderUnselectedFont,
         // Syntax Tokens
         [DisplayText("Unknown Token Color")]
         Unknown,
         [DisplayText("Whitespace Token Color")]
         Whitespace,
         [DisplayText("Identifier Token Color")]
         Identifier,
         [DisplayText("Keyword Token Color")]
         Keyword,
         [DisplayText("Number Token Color")]
         Number,
         [DisplayText("String Literal Token Color")]
         StringLiteral,
         [DisplayText("Character Literal Token Color")]
         CharLiteral,
         [DisplayText("Comment Token Color")]
         Comment,
         [DisplayText("Preprocessor Directive Token Color")]
         PreprocessorDirective,
         [DisplayText("Operator Token Color")]
         Operator,
         [DisplayText("Punctuation Token Color")]
         Punctuation,
         // Color Panel
         Red,
         Green,
         Blue
      }
   }
}
