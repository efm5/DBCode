namespace DBCode {
   internal static partial class LayoutHelpers {
      //
      // Event model for ColorSwatch interactions.
      // This event notifies listeners that a specific swatch
      // (identified by ColorSwatchUsage) has been clicked.
      //
      internal delegate void ColorSwatchClickedHandler(
         object? pSender,
         ColorSwatchUsage pUsage
      );
   }
}
