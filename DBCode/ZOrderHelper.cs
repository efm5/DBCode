using System.Runtime.CompilerServices;

namespace DBCode {
   internal static class ZOrderHelper {

      #region public methods
      public static List<IntPtr> GetZOrderSnapshot() {
         List<IntPtr> pList = [];
         EnumWindowsCollect(pList);
         return pList;
      }

      public static IntPtr GetPenultimateVisibleWindow(IntPtr pTopWindow) {
         List<IntPtr> pSnapshot = GetZOrderSnapshot();
         List<IntPtr> pFiltered = [];
         for (int pIndex = 0; pIndex < pSnapshot.Count; pIndex++) {
            IntPtr pWindowHandle = pSnapshot[pIndex];
            if ((IsRealVisibleWindow(pWindowHandle)) && (IsBlacklistedWindow(pWindowHandle) == false))
               pFiltered.Add(pWindowHandle);
         }
         if (pFiltered.Count < 2)
            return IntPtr.Zero;
         int pTopIndex = pFiltered.IndexOf(pTopWindow);
         if (pTopIndex <= 0)
            return IntPtr.Zero;
         return pFiltered[pTopIndex - 1];
      }

      public static List<IntPtr> GetAcceptableWindowsInZOrder() {
         List<IntPtr> pResult = [];
         List<IntPtr> pSnapshot = GetZOrderSnapshot();
         for (int pIndex = 0; pIndex < pSnapshot.Count; pIndex++) {
            IntPtr pWindowHandle = pSnapshot[pIndex];
            if (!IsRealVisibleWindow(pWindowHandle) || IsBlacklistedWindow(pWindowHandle))
               continue;
            pResult.Add(pWindowHandle);
         }
         return pResult;
      }

      public static IntPtr SelectFromMultiple(List<IntPtr> pCandidates) {
         if (pCandidates.Count == 0)
            return IntPtr.Zero;
         return pCandidates[0];
      }

      public static IntPtr GetMostSuitableWindow() {
         List<IntPtr> pCandidates = GetAcceptableWindowsInZOrder();
         return SelectFromMultiple(pCandidates);
      }

      public static bool IsValidTargetWindow(IntPtr pWindowHandle) {
         if ((pWindowHandle == IntPtr.Zero) || (IsWindow(pWindowHandle) == false) || (IsRealVisibleWindow(pWindowHandle) == false) || (IsBlacklistedWindow(pWindowHandle)))
            return false;
         return true;
      }

      public static unsafe string GetWindowTitle(IntPtr pWindowHandle) {
         int pLength = NativeMethods.GetWindowTextLength(pWindowHandle);
         if (pLength <= 0)
            return string.Empty;

         char[] pBuffer = new char[pLength + 1];

         fixed (char* pFixed = pBuffer) {
            int pWritten = NativeMethods.GetWindowText(
                pWindowHandle,
                pFixed,
                pBuffer.Length
            );

            return new string(pBuffer, 0, pWritten);
         }
      }
      #endregion

      #region private methods
      private static unsafe void EnumWindowsCollect(List<IntPtr> pList) {
         var handle = GCHandle.Alloc(pList);
         try {
            delegate* unmanaged[Stdcall]<nint, nint, int> pCallback = &EnumWindowsCallback;
            NativeMethods.EnumWindows(pCallback, GCHandle.ToIntPtr(handle));
         }
         finally {
            handle.Free();
         }
      }

      [UnmanagedCallersOnly(CallConvs = [typeof(CallConvStdcall)])]
      private static int EnumWindowsCallback(nint pHwnd, nint pParam) {
         var handle = GCHandle.FromIntPtr(pParam);
         if (handle.Target is List<IntPtr> list)
            list.Add(pHwnd);
         return 1; // continue enumeration
      }

      private static bool IsRealVisibleWindow(IntPtr pWindowHandle) {
         if ((pWindowHandle == IntPtr.Zero) || (IsWindow(pWindowHandle) == false) || (IsWindowVisible(pWindowHandle) == false))
            return false;
         int pLength = GetWindowTextLength(pWindowHandle);
         if (pLength == 0)
            return false;
         int pStyle = (int)GetWindowLongPtr(pWindowHandle, GWL_STYLE);
         if ((pStyle & WS_VISIBLE) == 0)
            return false;
         int pExStyle = (int)GetWindowLongPtr(pWindowHandle, GWL_EXSTYLE);
         if (((pExStyle & WS_EX_TOOLWINDOW) != 0) || ((pExStyle & WS_EX_NOACTIVATE) != 0) || ((pExStyle & WS_EX_LAYERED) != 0))
            return false;
         INT32 pOCloakedValue;
         int pResult = DwmGetWindowAttributeInt(pWindowHandle, (int)DWMWINDOWATTRIBUTE.DWMWA_CLOAKED, out pOCloakedValue, sizeof(int));
         int pCloaked = pOCloakedValue.Value;
         if ((pResult == 0) && (pCloaked != 0))
            return false;
         return true;
      }

#pragma warning disable IDE0060 // Remove unused parameter - this is a placeholder for future logic that may need the window handle.
      private static bool IsBlacklistedWindow(IntPtr pWindowHandle) {
         //DEBUG efm5 2026 04 5 flesh out this stub
         return false;
      }
#pragma warning restore IDE0060
      #endregion
   }
}
