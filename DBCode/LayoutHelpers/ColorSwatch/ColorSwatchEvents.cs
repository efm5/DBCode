namespace DBCode {
   internal static partial class LayoutHelpers {
      // Event model for ColorSwatch interactions.
      internal delegate void ColorSwatchClickedHandler(object? pSender, ColorSwatchUsage pUsage);
      internal delegate void ColorPickerSwatchClickedHandler(object? pSender, ColorPickerSwatchUsage pUsage);
      internal delegate void SyntaxColorSwatchClickedHandler(object? pSender, SyntaxColorSwatchUsage pUsage);
   }
}
