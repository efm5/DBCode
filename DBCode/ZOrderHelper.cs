using System.Runtime.CompilerServices;

namespace DBCode {
   internal static class ZOrderHelper {
      #region public methods
      public static bool FocusedControlAcceptsClipboard(IntPtr pWindow) {
         uint targetThread = GetWindowThreadProcessId(pWindow, out _);
         uint currentThread = GetCurrentThreadId();
         // Attach to the target thread so GetFocus() returns its focused control
         bool attached = AttachThreadInput(currentThread, targetThread, true);
         IntPtr focusedControl = IntPtr.Zero;

         try {
            focusedControl = GetFocus();
         }
         finally {
            if (attached)
               AttachThreadInput(currentThread, targetThread, false);
         }
         if (focusedControl == IntPtr.Zero)
            return false;
         StringBuilder? className = new StringBuilder(256);
         if (GetClassName(focusedControl, className, className.Capacity) == 0)
            return false;
         string name = className.ToString();

         // Win32 class names for text-accepting controls
         return name.Equals("Edit", StringComparison.OrdinalIgnoreCase) // TextBox, RichTextBox (WinForms), raw Edit
            || name.Equals("RichEdit", StringComparison.OrdinalIgnoreCase)
            || name.Equals("RichEdit20A", StringComparison.OrdinalIgnoreCase)
            || name.Equals("RichEdit20W", StringComparison.OrdinalIgnoreCase)
            || name.Equals("RichEdit50W", StringComparison.OrdinalIgnoreCase)   // Word, WordPad, etc.
            || name.Equals("RICHEDIT60W", StringComparison.OrdinalIgnoreCase)   // Office 2007+
            || name.StartsWith("WindowsForms10.EDIT", StringComparison.OrdinalIgnoreCase)      // WinForms TextBox
            || name.StartsWith("WindowsForms10.RichEdit", StringComparison.OrdinalIgnoreCase) // WinForms RichTextBox
            || name.StartsWith("Scintilla", StringComparison.OrdinalIgnoreCase);
      }

      public static List<IntPtr> GetZOrderSnapshot() {
         List<IntPtr> pList = [];
         EnumWindowsCollect(pList);
         return pList;
      }

      public static IntPtr GetNextVisibleWindow(IntPtr pTopWindow) {
         List<IntPtr> pSnapshot = GetZOrderSnapshot();
         List<IntPtr> pFiltered = [];
         for (int pIndex = 0; pIndex < pSnapshot.Count; pIndex++) {
            IntPtr pWindowHandle = pSnapshot[pIndex];
            if (IsRealVisibleWindow(pWindowHandle))
               pFiltered.Add(pWindowHandle);
         }
         if (pFiltered.Count < 2)
            return IntPtr.Zero;
         int pTopIndex = pFiltered.IndexOf(pTopWindow);
         if (pTopIndex < 0)
            return IntPtr.Zero;
         if (pTopIndex >= pFiltered.Count - 1)
            return IntPtr.Zero;
         return pFiltered[pTopIndex + 1];
      }

      public static IntPtr GetPenultimateVisibleWindow(IntPtr pTopWindow) {
         List<IntPtr> pSnapshot = GetZOrderSnapshot();
         List<IntPtr> pFiltered = [];
         for (int pIndex = 0; pIndex < pSnapshot.Count; pIndex++) {
            IntPtr pWindowHandle = pSnapshot[pIndex];
            if ((IsRealVisibleWindow(pWindowHandle)) && (IsDisallowedWindow(pWindowHandle) == false))
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
            if (!IsRealVisibleWindow(pWindowHandle) || IsDisallowedWindow(pWindowHandle))
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

      public static IntPtr GetNextWindow(IntPtr pTarget) {
         IntPtr foregroundWindow = GetForegroundWindow();

         if ((foregroundWindow == IntPtr.Zero) || (foregroundWindow == pTarget)) {
            int iterations = 0;
            do {
               foregroundWindow = GetForegroundWindow();
               if ((foregroundWindow == IntPtr.Zero) || (foregroundWindow == pTarget)) {
                  iterations++;
                  Thread.Sleep(mUiState.mActivationDelayMs);
               }
            } while ((iterations < 5) || (foregroundWindow == pTarget));
         }
         return foregroundWindow;
      }

      public static IntPtr GetMostSuitableWindow() {
         List<IntPtr> pCandidates = GetAcceptableWindowsInZOrder();
         return SelectFromMultiple(pCandidates);
      }

      public static IntPtr GetMostSuitableWindowAllowedFirst() {
         List<IntPtr> pCandidates = GetAcceptableWindowsInZOrder(); // already excludes disallowed and invisible
         foreach (IntPtr pWindowHandle in pCandidates) { // pass 1: prefer allowed windows
            if (IsAllowedWindow(pWindowHandle))
               return pWindowHandle;
         }
         foreach (IntPtr pWindowHandle in pCandidates) { // pass 2: accept first neutral window
            if (!IsAllowedWindow(pWindowHandle))
               return pWindowHandle;
         }
         return IntPtr.Zero;
      }

      public static bool IsValidTargetWindow(IntPtr pWindowHandle) {
         if ((pWindowHandle == IntPtr.Zero) || (IsWindow(pWindowHandle) == false) || (IsRealVisibleWindow(pWindowHandle) == false) || (IsDisallowedWindow(pWindowHandle)))
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
         if ((pWindowHandle == IntPtr.Zero) || (IsWindow(pWindowHandle) == false) ||
            (IsWindowVisible(pWindowHandle) == false))
            return false;
         int pLength = GetWindowTextLength(pWindowHandle);
         if (pLength == 0)
            return false;
         int pStyle = (int)GetWindowLongPtr(pWindowHandle, GWL_STYLE);
         if ((pStyle & WS_VISIBLE) == 0)
            return false;
         int pExStyle = (int)GetWindowLongPtr(pWindowHandle, GWL_EXSTYLE);
         if (((pExStyle & WS_EX_TOOLWINDOW) != 0) || ((pExStyle & WS_EX_NOACTIVATE) != 0) ||
               ((pExStyle & WS_EX_LAYERED) != 0) || ((pExStyle & WS_EX_NOREDIRECTIONBITMAP) != 0))
            return false;
         INT32 pOCloakedValue;
         int pResult = DwmGetWindowAttributeInt(pWindowHandle, (int)DWMWINDOWATTRIBUTE.DWMWA_CLOAKED, out pOCloakedValue, sizeof(int));
         int pCloaked = pOCloakedValue.Value;
         if ((pResult == 0) && (pCloaked != 0))
            return false;
         return true;
      }

      private static bool IsDisallowedWindow(IntPtr pWindowHandle) {
         string title = GetWindowTitle(pWindowHandle);
         if (TargetListManager.IsDisallowed(title))
            return true;
         StringBuilder className = new StringBuilder(256);
         if (GetClassName(pWindowHandle, className, className.Capacity) > 0) {
            string name = className.ToString();
            if (name.Equals("ApplicationFrameHost", StringComparison.OrdinalIgnoreCase))
               return true;
         }
         return false;
      }

      public static bool IsAllowedWindow(IntPtr pWindowHandle) {
         return TargetListManager.IsAllowed(GetWindowTitle(pWindowHandle));
      }
      #endregion
   }
}
