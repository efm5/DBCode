===========================================
DBCode C# STYLE GUIDE — UNIFIED & AUTHORITATIVE
===========================================

This style guide defines the mandatory formatting, naming, structural,
and architectural rules used across all DBCode projects. These rules
are optimized for:
• Hands‑free programming (Dragon NaturallySpeaking)
• Maximum clarity and maintainability
• Zero ambiguity in generated code
• Vertical compactness
• Deterministic formatting
• WinForms, interop, and Unicode‑heavy workflows

All generated code must follow these rules without exception.

===========================================
1. NAMING RULES
===========================================

1.1 Parameter Naming
• All parameters must begin with lowercase "p" followed by PascalCase.
  Examples:
    pWindowHandle
    pIndex
    pClassName
    pWindowName

1.2 Out‑Parameter Naming
• All out parameters must begin with "pO" followed by PascalCase.
  Examples:
    out int pOCount
    out RECT pORect
    out INT32 pOValue

1.3 Local Variables
• Must be full English words.
• No abbreviations (idx, tmp, rgb, etc.).
• Dragon‑friendly and pronounceable.

1.4 Field Naming
• Grouped, alphabetized, vertically compact.
• Full English words only.

1.5 Type Names
• PascalCase.
• Full English words.
• No abbreviations.

===========================================
2. FORMATTING RULES
===========================================

2.1 Indentation
• 3 spaces per indentation level.
• No tabs.

2.2 Braces
• Opening brace on same line as declaration.
• No braces for single‑statement control structures.

2.3 Vertical Compactness
• No blank lines inside methods.
• No blank line before return statements.
• No blank line before comments inside methods.
• Methods separated by exactly one blank line.

2.4 Region Spacing Rule (NEW — FINAL)
To maintain consistent vertical compactness and predictable structure:

• A single blank line MUST appear *before* each `#region` directive.
• A single blank line MUST appear *after* each `#endregion` directive.
• NO blank line may appear immediately *after* a `#region` directive.
• NO blank line may appear immediately *before* an `#endregion` directive.

Correct:
    <code>

    #region public methods
    public void MethodA() {
       ...
    }
    #endregion

    #region private methods
    private void MethodB() {
       ...
    }
    #endregion

Incorrect:
    (blank line after #region)
    #region public methods

    public void MethodA() { }

    (blank line before #endregion)
    public void MethodA() { }

    #endregion

This rule applies to ALL files and ALL regions.

===========================================
3. LANGUAGE RULES
===========================================

3.1 No LINQ in subsystem code.
3.2 No cleverness; clarity always wins.
3.3 No expression‑bodied members in subsystem code.
3.4 No implicit typing except in foreach loops.
3.5 No nullable reference types in subsystem code.

===========================================
4. INTEROP RULES
===========================================

4.1 LibraryImport Usage
• Always use explicit W‑entry points (FindWindowW, GetWindowTextW, etc.).
• NEVER specify CharSet — LibraryImport does not support it.
• All parameters must follow naming rules.
• All out parameters must use blittable wrapper structs when required.

4.2 Blittable Wrapper Types
• INT32, UINT32, BOOL, etc. must be used for byref primitives.
• RECT, POINT, SIZE must remain blittable.

4.3 No overloads for LibraryImport
• If multiple output types are needed, use distinct method names:
    DwmGetWindowAttribute
    DwmGetWindowAttributeInt
    DwmGetWindowAttributeBool

===========================================
5. STRUCTURE & ARCHITECTURE RULES
===========================================

5.1 Subsystem Layout
• Each subsystem must be self‑contained.
• No cross‑subsystem namespace drift.
• No ghost files or stale partials.

5.2 UI Subsystem Rules
• Modular, no monolithic panels.
• Clear boundaries between helpers, layout, and rendering.

5.3 Analyzer Hygiene
• All suppressions must be centralized in .editorconfig.
• No inline suppressions except temporary debugging cases.

===========================================
6. DRAGON DICTATION RULES
===========================================

6.1 Identifier Pronounceability
• All identifiers must be easily dictated.
• No abbreviations.
• No multi‑step dictation sequences.

6.2 Loop Variables
• Use full English names:
    currentIndex
    currentControl
    nextControl

6.3 Method Names
• Full English words.
• Verb‑first.

===========================================
END OF STYLE GUIDE
===========================================
