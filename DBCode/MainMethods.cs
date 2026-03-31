namespace DBCode {
   internal static partial class Program {

      [AttributeUsage(AttributeTargets.All)]
      public class DisplayText : Attribute {
         public DisplayText(string pText) {
            this.Text = pText;
         }
         public string Text { get; set; } = string.Empty;
      }

      public static string ToDescription(object pType) {
         Type type = pType.GetType();
         string? name = pType.ToString();
         if (string.IsNullOrEmpty(name))
            name = string.Empty;
         MemberInfo[] memInfo = type.GetMember(name);

         if (memInfo.Length == 0)
            return string.Empty;
         object[] attrs = memInfo[0].GetCustomAttributes(typeof(DisplayText), false);
         if (attrs.Length == 0)
            return string.Empty;
         return ((DisplayText)attrs[0]).Text;
      }

      public static void TimedMessage(string pMessage, string pTitle = "", int pDuration = 4500) {
         pMessage ??= string.Empty;
         pTitle ??= string.Empty;
         _ = MessageBoxTimeout(IntPtr.Zero, pMessage, pTitle, TIMED_MESSAGEBOX_FLAGS, 0, pDuration);
      }
   }
}