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
• Prefer descriptive, full English words over abbreviations.
• Examples:
    Good:
      pWindowHandle
      pEventArguments
      pClassName
      pWindowName
    Acceptable but discouraged:
      pE (use pEventArguments instead)
      pIdx (use pIndex instead)

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
• Semantic richness is preferred over brevity.
• camelCase.
  Examples:
    int totalCount (a local field)

1.4 Field Naming
• Grouped, alphabetized, vertically compact.
• Full English words only.
• No abbreviations.
    int mTotalCount (a class field)

1.5 Type Names
• PascalCase.
• Full English words.
• No abbreviations.
• When a member of a class precede the name with “m”.
  Example:
    private static int mCount;
1.6 Field Declaration Rules (Strict)
• Fields are grouped strictly by type.
• All fields of the same type appear together.
• No blank lines are inserted between fields of the same type.
• Within each type group, fields are alphabetized by identifier.
• Fields are not grouped by semantic role; grouping is by type only.
• Fields of the same type are declared in a single comma‑separated statement when reasonable.
• Reference‑type fields are explicitly initialized to null unless otherwise specified.

1.7 Local Variables and Loop Variables
• Plain camelCase — no prefix.
• Keep names short but meaningful: index, control, token, line.
• Single-level loops may use i as a counter.
• Once loops nest, abandon i/j/k entirely and use full camelCase
  names that make each level's purpose unambiguous.

===========================================
2. COMMENTING, FORMATTING AND LAYOUT RULES
===========================================

2.1 General Philosophy
• The target maximum line width is approximately 130 characters.
• Long declarations are wrapped only at comma boundaries.
• Mid‑expression wrapping is avoided unless unavoidable.
• Continuation lines are indented exactly three spaces beyond the existing indentation level.
• Formatting is compact and vertically dense.
• Unnecessary blank lines are avoided.
• Blank lines are not inserted before comments or return statements.

2.2 Indentation
• 3 spaces per indentation level.
• No tabs.

2.3 Braces
• Opening brace on same line as declaration.
• No braces for single‑statement control structures.

2.4 Vertical Compactness
• No blank lines inside methods except after variable declaration blocks.
• No blank line before return statements.
• No blank line before comments inside methods.
• Methods separated by exactly one blank line.

2.5 Region Spacing Rule
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

2.6 Commenting
To maintain vertical compactness:
• Refrain from embedded comments within method bodies except for critical clarifications. 
• When comments are necessary, they should be concise and placed immediately above the relevant code without intervening blank lines.
• No summary blocks or XML documentation comments are required unless explicitly requested.

===========================================
3. CONTROL STRUCTURE RULES
===========================================

3.1 Braces are always used, except for single‑statement control structures.
3.2 Explicit block structure is preferred over compact syntax.

===========================================
4. LANGUAGE RULES
===========================================

4.1 No LINQ in subsystem code.
4.2 No cleverness; clarity always wins.
4.3 No expression‑bodied members in subsystem code.
4.4 No implicit typing.
  Example:
    Prefer:
            foreach (Control control in Controls.OfType<Control>()) {
            string windowTitle = GetWindowTitle(pWindowHandle);
    Over:
            foreach (Control control in Controls)
            var windowTitle = GetWindowTitle(pWindowHandle);
    even where in the type is unmistakable.
4.5 No nullable reference types in subsystem code.
4.6 Enumerations
• Short enums are collapsed onto a single line when they fit comfortably within the ~130 letter maximum line length rule.
• Multi‑line enums are preserved when they include attributes or descriptive metadata.
• Enum members are not reordered unless explicitly instructed.

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

WinForms context is assumed unless explicitly stated otherwise.
Control ownership, layout logic, and event wiring are not changed unless requested.
Existing naming conventions for controls and UI elements are respected.
Monolithic panels with overlapping responsibilities are avoided.
UI components do not persist state.

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
6. UI STATE AND PERSISTENCE ARCHITECTURE
===========================================

• Settings.Default is used for serialization only.
• All runtime UI state lives in UiState.
• UI components never persist state directly.
• Persistence occurs once at shutdown.
• First‑launch initialization logic is not part of UiState.

===========================================
7. LANGUAGE AND STYLE RULES
===========================================

• Variable initialization is explicit.
• Local variables are declared at the top of methods.
• Clever or minimalist constructs that sacrifice clarity are avoided.

7.1 Collection Initialization
• Prefer new‑style AddRange(...) over multiple Add(...) statements.
• Use single‑line AddRange calls when the line fits within the ~130 letter maximum line length.
• When wrapping is necessary, wrap at comma boundaries with continuation indentation.
  Examples:
    Preferred:
      Controls.AddRange(mTitleCluster, mPromptLabel, mStatusStrip, mInputTextBox);
    Discouraged:
      Controls.Add(mTitleCluster);
      Controls.Add(mPromptLabel);
      Controls.Add(mStatusStrip);
      Controls.Add(mInputTextBox);

7.2 Named Arguments
• Do not use named arguments in method calls.
• Pass all arguments positionally.
  Examples:
    Preferred:
      EnsureWindowFitsMonitor(mForm, false);
    Discouraged:
      EnsureWindowFitsMonitor(mForm, pControlBox: false);

===========================================
8. DRAGON DICTATION RULES
===========================================

8.1 Identifier Pronounceability
• All identifiers must be easily dictated.
• No abbreviations.
• No multi‑step dictation sequences.
8.2 Loop Variables
• Use full English names:
    currentIndex
    currentControl
    nextControl
8.3 Method Names
• Full English words.
• Verb‑first by default for clarity and action‑oriented naming.
• Noun‑first (military nomenclature) when disambiguation is necessary across similar methods in different contexts.
  Examples:
    Preferred (verb‑first):
      ApplyTheme()
      ValidateInput()
      CalculateTotal()
    Acceptable (noun‑first for disambiguation):
      ThemeApplyCallback()         // vs ColorPickerApplyCallback(), FontPickerApplyCallback()
      ColorPickerShowPanel()       // vs FontPickerShowPanel(), ThemePickerShowPanel()
      FontValidationComplete()     // vs ThemeValidationComplete()
    Context matters:
      • Use verb‑first when the method's action is unique or context is obvious.
      • Use noun‑first when multiple similar methods exist across different subsystems or panels.

===========================================
9. WORKFLOW ASSUMPTIONS
===========================================

• The codebase is written and maintained using a hands‑free, dictation‑driven workflow.
• Naming and structure must minimize friction for Dragon dictation.
• Predictability and consistency are more important than stylistic novelty.

===========================================
10. APPLYING THE STYLE GUIDE
===========================================

• When instructed to apply the style guide, all rules above are assumed without restatement.
• Ambiguities are resolved using the most conservative interpretation.
• Violations are corrected explicitly rather than worked around.

===========================================
END OF STYLE GUIDE
===========================================
