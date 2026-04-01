# Themes Subsystem

The Themes subsystem provides a deterministic, JSON‑based theming architecture for DBCode.
It is designed for accessibility, predictability, and long‑term maintainability. The subsystem
supports loading, saving, validating, previewing, and applying UI themes with zero drift and
strict completeness.

This document describes the architecture, file responsibilities, JSON format, and integration
points for the subsystem.

---

## Overview

A **Theme** is a collection of:

- A name
- A brightness classification (Light or Dark)
- A complete set of fonts (`FontUsage`)
- A complete set of colors (`ColorUsage`)

Themes are stored as JSON files and loaded at application startup. The subsystem guarantees:

- Every theme is complete (no missing fonts or colors)
- Every theme is validated before use
- A valid theme is always available (fallback to defaults)
- The last‑used theme is restored when possible
- Rendering previews is deterministic and accessible

---

## File Structure

/Themes
Theme.cs
ThemeDefaults.cs
ThemeManager.cs
ThemeWriter.cs
ThemeRegistry.cs
ThemeDiagnostics.cs
ThemePreviewRenderer.cs
ThemeTag.cs
FontUsage.cs
ColorUsage.cs
ThemeBrightness.cs

Each file has a single, explicit responsibility.

---

## Theme.cs

Defines the `Theme` class:

- Holds name, brightness, fonts, and colors
- Uses fixed‑size arrays indexed by enums
- No logic beyond storage

This class is intentionally simple and deterministic.

---

## ThemeDefaults.cs

Provides two built‑in themes:

- `DefaultLight`
- `DefaultDark`

These are fully explicit, accessibility‑driven defaults used when:

- No theme files exist
- All theme files fail validation
- The user resets to defaults

---

## ThemeManager.cs

Responsible for **loading** themes from JSON.

Key behaviors:

- Loads all `*.json` files from a folder
- Parses fonts and colors using strict enum alignment
- Enforces strict completeness (missing fields = exception)
- Returns a list of fully constructed `Theme` objects

This file does **not** select the active theme — that is handled by `ThemeRegistry`.

---

## ThemeWriter.cs

Responsible for **saving** themes to JSON.

Key behaviors:

- Serializes themes with stable ordering
- Writes indented, human‑readable JSON
- Ensures round‑trip compatibility with ThemeManager

Used by Preferences UI when editing themes.

---

## ThemeRegistry.cs

The central controller for the subsystem.

Responsibilities:

- Loads themes at startup
- Validates themes using ThemeDiagnostics
- Ensures fallback defaults if needed
- Selects the last‑used theme
- Exposes:
  - `Themes`
  - `CurrentTheme`
  - `LastUsedThemeName`
- Allows switching themes via `SetCurrentTheme`

This class guarantees that **CurrentTheme is always valid**.

---

## ThemeDiagnostics.cs

Strict validator for theme correctness.

Checks:

- Name validity
- Brightness enum validity
- Every font:
  - Exists
  - Has a valid family name
  - Has a positive size
- Every color:
  - Exists
  - Is not fully transparent
  - Is not “unnamed black” (common parse failure)

Two modes:

- `RunStrict()` — throws on any issue
- `RunReport()` — returns a list of issues

Used internally by ThemeRegistry.

---

## ThemePreviewRenderer.cs

Renders theme previews for the Preferences UI.

Features:

- Deterministic layout constants
- High‑contrast swatch borders
- Centered labels for color swatches
- Left‑aligned labels for font samples
- DPI‑safe text measurement
- No UI dependencies (pure renderer)

This file is UI‑agnostic and can be used in WinForms, WPF, or custom controls.

---

## ThemeTag.cs

A small helper class used by ThemeBinder and Preferences UI to associate UI controls with:

- A `FontUsage`
- A `ColorUsage`

Extracted into its own file for architectural clarity.

---

## Enums

### FontUsage
Defines all font roles used by the application.

### ColorUsage
Defines all color roles used by the application.

### ThemeBrightness
Defines Light vs Dark classification.

These enums are the backbone of the subsystem — all arrays and JSON structures are indexed by them.

---

## JSON Theme Format

Each theme is stored as a JSON file:
{
"Name": "Dark",
"Brightness": "Dark",
"Fonts": {
"Interface": "Segoe UI, 18pt",
"Menu": "Segoe UI, 18pt",
"Status": "Segoe UI, 16pt",
"Text": "Consolas, 18pt"
},
"Colors": {
"PanelBackground": "#000000",
"GroupBoxBackground": "#3C3C3C",
"GroupBoxFont": "#FFFFFF",
"InterfaceBackground": "#202020",
"InterfaceFont": "#FFFFFF",
"MenuBackground": "#303030",
"MenuFont": "#FFFFFF",
"StatusBackground": "#202020",
"StatusFont": "#FFFFFF",
"TextBoxFont": "#FFFFFF"
}
}
Rules:

- Every `FontUsage` must appear exactly once
- Every `ColorUsage` must appear exactly once
- Brightness must be a valid enum value
- Fonts must be convertible via `FontConverter`
- Colors must be hex or named colors

---

## Lifecycle Summary

1. **Startup**
   - ThemeRegistry.Initialize()
   - ThemeManager loads JSON themes
   - ThemeDiagnostics validates them
   - ThemeRegistry selects last‑used theme

2. **Preferences UI**
   - ThemePreviewRenderer draws previews
   - User edits fonts/colors
   - ThemeWriter saves updated theme
   - ThemeRegistry.Reload() refreshes the list

3. **Runtime**
   - CurrentTheme is used by ThemeBinder to apply UI styling

---

## Design Principles

The subsystem follows these architectural principles:

- **Deterministic**  
  No randomness, no inference, no hidden behavior.

- **Strict completeness**  
  Missing fields are errors, not defaults.

- **Accessibility‑driven**  
  Large fonts, high contrast, predictable layout.

- **Zero drift**  
  JSON format is stable and explicit.

- **Pure responsibilities**  
  Each file does exactly one thing.

- **Future‑proof**  
  Easy to extend with new usages or UI panels.

---

## Extending the Subsystem

To add a new font or color role:

1. Add a new value to `FontUsage` or `ColorUsage`
2. Update ThemeDefaults
3. Update ThemePreviewRenderer
4. Update Preferences UI
5. (Optional) Add new diagnostics

ThemeManager and ThemeWriter automatically adapt because they enumerate the enums.

---

## Status

The Themes subsystem is **complete** and ready for integration into the Preferences UI.
