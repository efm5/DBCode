namespace DBCode {
   internal static partial class LayoutHelpers {
      internal class NativeMathMethods {
         internal const uint ABM_GETTASKBARPOS = 0x00000005;

         internal const uint ABE_LEFT = 0;
         internal const uint ABE_TOP = 1;
         internal const uint ABE_RIGHT = 2;
         internal const uint ABE_BOTTOM = 3;

         [DllImport("shell32.dll")]
         internal static extern uint SHAppBarMessage(uint pMessage, ref APPBARDATA pData);

         [StructLayout(LayoutKind.Sequential)]
         internal struct APPBARDATA {
            internal int cbSize;
            internal IntPtr hWnd;
            internal uint uCallbackMessage;
            internal uint uEdge;
            internal RECT rc;
            internal int lParam;
         }
      }
   }
}
