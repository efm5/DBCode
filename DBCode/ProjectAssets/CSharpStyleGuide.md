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

1.8 Static Fields
Static fields that serve as class-wide shared state use mSCamelCase. This mirrors the instance 
field mCamelCase convention but signals static lifetime. No underscores.
Example: private static bool mSIsCntrolKeyDown = false;
Note: In DBCode, the preferred pattern is to eliminate static shared state entirely by moving 
it into a dedicated Fields class with instance fields using mCamelCase. 
The s prefix is retained in CurlyPad, EasyPad and other applications for historical continuity.

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
4.3 Expression-Bodied Members and Lambda Expressions
• Expression-bodied members are not permitted.
• Single-expression lambdas are permitted where they cleanly replace a one-statement method or delegate.
• Multi-statement lambdas (block-body lambdas using `{ }`) are not permitted; extract to a named method instead.
• Lambda parameters must follow standard naming rules (p-prefix, PascalCase).
• If the expression requires explanation, prefer a named method over a lambda.
  Examples:
    Permitted (single-expression lambda):
      button.Click += (pSender, pEventArguments) => HandleClick(pEventArguments);
      mControls.Sort((pA, pB) => string.Compare(pA.Name, pB.Name, StringComparison.Ordinal));
    Not permitted (block body — extract to a named method instead):
      button.Click += (pSender, pEventArguments) => {
         PrepareState();
         HandleClick(pEventArguments);
      };
    Not permitted (expression-bodied member):
      public int Count => mTotalCount;
      public void Refresh() => DoRefresh();
4.4 No implicit typing.
  Example:
    Prefer:
            foreach (Control control in Controls.OfType<Control>()) {
            string windowTitle = GetWindowTitle(pWindowHandle);
    Over:
            foreach (Control control in Controls)
            var windowTitle = GetWindowTitle(pWindowHandle);
    even where in the type is unmistakable.
4.5 Enumerations
• Short enums are collapsed onto a single line when they fit comfortably within the ~130 letter maximum line length rule.
• Multi‑line enums are preserved when they include attributes or descriptive metadata.
• Enum members are not reordered unless explicitly instructed.

===========================================
5. INTEROP RULES
===========================================

5.1 LibraryImport Usage
• Always use explicit W‑entry points (FindWindowW, GetWindowTextW, etc.).
• NEVER specify CharSet — LibraryImport does not support it.
• All parameters must follow naming rules.
• All out parameters must use blittable wrapper structs when required.
5.2 Blittable Wrapper Types
• INT32, UINT32, BOOL, etc. must be used for byref primitives.
• RECT, POINT, SIZE must remain blittable.
5.3 No overloads for LibraryImport
• If multiple output types are needed, use distinct method names:
    DwmGetWindowAttribute
    DwmGetWindowAttributeInt
    DwmGetWindowAttributeBool

5.4 DllImport vs LibraryImport Selection
• Use LibraryImport when ALL parameters are blittable (nint, int, bool, structs, etc.).
• Use DllImport with CharSet = CharSet.Unicode when ANY parameter is a string type.
  LibraryImport source generation does not handle string parameters and will produce
  CS8795 ("partial method must have an implementation part").
• The file-level #pragma disable SYSLIB1054 already present in all NativeMethods
  files suppresses the "prefer LibraryImport" analyzer warning for DllImport methods.

5.5 Win32 Return Value Handling
• Explicitly discard with _ = when a call is best-effort and failure is non-actionable
  (cosmetic or visual operations such as SetWindowTheme, where falling back to the
  default appearance is acceptable).
• Check or handle the return value when failure has consequences — file operations,
  device control, security boundaries, or data integrity.
• Never silently ignore a return value without conscious intent. _ = makes the
  discard visible and reviewable; it is not a suppression mechanism.

===========================================
6. STRUCTURE & ARCHITECTURE RULES
===========================================

WinForms context is assumed unless explicitly stated otherwise.
Control ownership, layout logic, and event wiring are not changed unless requested.
Existing naming conventions for controls and UI elements are respected.
Monolithic panels with overlapping responsibilities are avoided.
UI components do not persist state.

6.1 Subsystem Layout
• Each subsystem must be self‑contained.
• No cross‑subsystem namespace drift.
• No ghost files or stale partials.

6.2 UI Subsystem Rules
• Modular, no monolithic panels.
• Clear boundaries between helpers, layout, and rendering.

6.3 Analyzer Hygiene
• Most suppressions will be centralized in .editorconfig.
• Occasionally inline suppressions are used when I do not want to change the code to satisfy the analyzer's rule. 
   This is also useful when the warning is too general or broad and the specific instance is not actually improved 
   by changing the code. I also make exceptions for temporary debugging cases or when postponing a code change;
   these will be explained in comments.

===========================================
7. UI STATE AND PERSISTENCE ARCHITECTURE
===========================================

• Settings.Default is used for serialization only.
• All runtime UI state lives in UiState.
• UI components never persist state directly.
• Persistence occurs once at shutdown.
• First‑launch initialization logic is not part of UiState.

===========================================
8. LANGUAGE AND STYLE RULES
===========================================

• Variable initialization is explicit.
• Local variables are declared at the top of methods.
• Clever or minimalist constructs that sacrifice clarity are avoided.

8.1 Collection Initialization
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

8.2 Named Arguments
• Do not use named arguments in method calls.
• Pass all arguments positionally.
  Examples:
    Preferred:
      EnsureWindowFitsMonitor(mForm, false);
    Discouraged:
      EnsureWindowFitsMonitor(mForm, pControlBox: false);

8.3 Object Initialization
• Use object initializers to set properties at construction whenever the value is known at that point.
• Do not follow a constructor call with immediate property assignments that could be included in the initializer.
• IDE0017 (Object initialization can be simplified) is honored — do not suppress it.
  Examples:
    Preferred:
      mCurrentFindRecord = new FindRecord(pSearchText, matches) {
         mPosition = index
      };
    Discouraged:
      mCurrentFindRecord = new FindRecord(pSearchText, matches);
      mCurrentFindRecord.mPosition = index;

8.4 Guard Clauses (ThrowIfNull)
• `ThrowIfNull` calls signal fatal programming errors — a null that should never occur.
• They must appear at the very top of the method body, before any logic or local variable declarations.
• All guards for a given method are grouped together with no blank lines between them.
• No other statements may precede them except other `ThrowIfNull` calls.
  Examples:
    Correct:
      private void Apply(Theme pTheme) {
         ThrowIfNull(mForm, nameof(mForm));
         ThrowIfNull(mCurrentTheme, nameof(mCurrentTheme));
         int width = mForm.ClientSize.Width;
         ...
      }
    Incorrect:
      private void Apply(Theme pTheme) {
         int width = mForm.ClientSize.Width;
         ThrowIfNull(mForm, nameof(mForm));   // guard is too late
         ...
      }

===========================================
9. DRAGON DICTATION RULES
===========================================

9.1 Identifier Pronounceability
• All identifiers must be easily dictated.
• No abbreviations.
• No multi‑step dictation sequences.
9.2 Loop Variables
• Use full English names:
    currentIndex
    currentControl
    nextControl
9.3 Method Names
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
10. WORKFLOW ASSUMPTIONS
===========================================

• The codebase is written and maintained using a hands‑free, dictation‑driven workflow.
• Naming and structure must minimize friction for Dragon dictation.
• Predictability and consistency are more important than stylistic novelty.

===========================================
11. APPLYING THE STYLE GUIDE
===========================================

• When instructed to apply the style guide, all rules above are assumed without restatement.
• Ambiguities are resolved using the most conservative interpretation.
• Violations are corrected explicitly rather than worked around.

===========================================
END OF STYLE GUIDE
===========================================
