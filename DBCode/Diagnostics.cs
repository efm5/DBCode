using System.Runtime.CompilerServices;

namespace DBCode {
   namespace Diagnostics {
#pragma warning disable IDE0290
      internal sealed class FatalLayoutException : Exception {
         public FatalLayoutException(string pMessage) : base(pMessage) { }
      }
#pragma warning restore IDE0290

      internal static class Fatal {
         public static T Require<T>(
            T? pValue,
            string pName,
            [CallerMemberName] string pCaller = "",
            [CallerFilePath] string pFile = "",
            [CallerLineNumber] int pLine = 0) where T : class {

            if (pValue == null)
               throw new FatalLayoutException(
                  $"Fatal parameter error:{Environment.NewLine}" +
                  $"Parameter: {pName}{Environment.NewLine}" +
                  $"Why: Required parameter was null.{Environment.NewLine}" +
                  $"Method: {pCaller}{Environment.NewLine}" +
                  $"File: {pFile}{Environment.NewLine}" +
                  $"Line: {pLine}{Environment.NewLine}");
            return pValue;
         }

         public static T RequireStruct<T>(
            T? pValue,
            string pName,
            [CallerMemberName] string pCaller = "",
            [CallerFilePath] string pFile = "",
            [CallerLineNumber] int pLine = 0) where T : struct {

            if (!pValue.HasValue)
               throw new FatalLayoutException(
                  $"Fatal parameter error:{Environment.NewLine}" +
                  $"Parameter: {pName}{Environment.NewLine}" +
                  $"Why: Required nullable value was null.{Environment.NewLine}" +
                  $"Method: {pCaller}{Environment.NewLine}" +
                  $"File: {pFile}{Environment.NewLine}" +
                  $"Line: {pLine}{Environment.NewLine}");
            return pValue.Value;
         }

         public static FatalLayoutException Layout(
            string pName,
            string pMessage,
            [CallerMemberName] string pCaller = "",
            [CallerFilePath] string pFile = "",
            [CallerLineNumber] int pLine = 0) {

            return new FatalLayoutException(
               $"Fatal layout error:{Environment.NewLine}" +
               $"Field/Parameter: {pName}{Environment.NewLine}" +
               $"Why: {pMessage}{Environment.NewLine}" +
               $"Method: {pCaller}{Environment.NewLine}" +
               $"File: {pFile}{Environment.NewLine}" +
               $"Line: {pLine}{Environment.NewLine}");
         }
      }
   }
}
