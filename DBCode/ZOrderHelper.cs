namespace DBCode {
   internal static class ZOrderHelper {
      #region public methods
      public static List<IntPtr> GetZOrderSnapshot() {
#pragma warning disable IDE0028
         List<IntPtr> pList = new List<IntPtr>();
#pragma warning restore IDE0028

         EnumWindowsCollect(pList);
         return pList;
      }

      public static IntPtr GetPenultimateVisibleWindow(IntPtr pTopWindow) {
#pragma warning disable IDE0028
         List<IntPtr> pSnapshot = GetZOrderSnapshot();
         List<IntPtr> pFiltered = new List<IntPtr>();
#pragma warning restore IDE0028

         for (int i = 0; i < pSnapshot.Count; i++) {
            IntPtr pWindowHandle = pSnapshot[i];
            if ((IsRealVisibleWindow(pWindowHandle)) && (IsBlacklistedWindow(pWindowHandle) == false))
               pFiltered.Add(pWindowHandle);
         }
         if (pFiltered.Count < 2)
            return IntPtr.Zero;
         int index = pFiltered.IndexOf(pTopWindow);
         if (index <= 0)
            return IntPtr.Zero;
         return pFiltered[index - 1];
      }

      public static List<IntPtr> GetAcceptableWindowsInZOrder() {
#pragma warning disable IDE0028
         List<IntPtr> pResult = new List<IntPtr>();
         List<IntPtr> pSnapshot = GetZOrderSnapshot();
#pragma warning restore IDE0028

         for (int i = 0; i < pSnapshot.Count; i++) {
            IntPtr pHwnd = pSnapshot[i];
            if (!IsRealVisibleWindow(pHwnd) || IsBlacklistedWindow(pHwnd))
               continue;
            pResult.Add(pHwnd);
         }
         return pResult;
      }

      public static IntPtr SelectFromMultiple(List<IntPtr> pCandidates) {
         if (pCandidates.Count == 0)
            return IntPtr.Zero;
         return pCandidates[0];
      }

      public static IntPtr GetMostSuitableWindow() {
         //#pragma warning disable IDE0028//efm5 warning
         List<IntPtr> pCandidates = GetAcceptableWindowsInZOrder();
         //#pragma warning restore IDE0028

         return SelectFromMultiple(pCandidates);
      }

      public static bool IsValidTargetWindow(IntPtr pHwnd) {
         if ((pHwnd == IntPtr.Zero) || (IsWindow(pHwnd) == false) || (IsRealVisibleWindow(pHwnd) == false) ||
            (IsBlacklistedWindow(pHwnd)))
            return false;
         return true;
      }

      public static string GetWindowTitle(IntPtr pHwnd) {
         int length = GetWindowTextLength(pHwnd);
         if (length <= 0)
            return string.Empty;

         //#pragma warning disable IDE0028//efm5 warning
         StringBuilder pBuilder = new StringBuilder(length + 1);
         //#pragma warning restore IDE0028
#pragma warning disable CA1806
         GetWindowText(pHwnd, pBuilder, pBuilder.Capacity);
#pragma warning restore CA1806
         return pBuilder.ToString();
      }
      #endregion

      #region private methods
      private static void EnumWindowsCollect(List<IntPtr> pList) {
         EnumWindows(delegate (IntPtr pWindowHandle, IntPtr pCallbackParam) {
            pList.Add(pWindowHandle);
            return true;
         }, IntPtr.Zero);
      }

      private static bool IsRealVisibleWindow(IntPtr pWindowHandle) {
         if ((pWindowHandle == IntPtr.Zero) || (IsWindow(pWindowHandle) == false) || (IsWindowVisible(pWindowHandle) == false))
            return false;
         int length = GetWindowTextLength(pWindowHandle);
         if (length == 0)
            return false;
         int style = (int)GetWindowLongPtr(pWindowHandle, GWL_STYLE);
         if ((style & WS_VISIBLE) == 0)
            return false;
         int exStyle = (int)GetWindowLongPtr(pWindowHandle, GWL_EXSTYLE);
         if (((exStyle & WS_EX_TOOLWINDOW) != 0) || ((exStyle & WS_EX_NOACTIVATE) != 0) || ((exStyle & WS_EX_LAYERED) != 0))
            return false;
         int cloaked = 0;
         int result = DwmGetWindowAttribute(pWindowHandle, DWMWINDOWATTRIBUTE.DWMWA_CLOAKED, out cloaked, sizeof(int));
         if ((result == 0) && (cloaked != 0))
            return false;
         return true;
      }

#pragma warning disable IDE0060//DEBUG efm5 2026 03 28 temporarily
      private static bool IsBlacklistedWindow(IntPtr pHwnd) {
         return false;
      }
#pragma warning restore IDE0060
      #endregion
   }
}
