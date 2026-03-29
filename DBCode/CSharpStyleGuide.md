Ed’s C# Coding Style Guide
(For Visual Studio 2026, .NET 10, C# 14, WinForms)

This guide defines the explicit, deterministic, Dragon‑friendly coding style used throughout the DBCode project. All generated code must follow these rules unless explicitly overridden.

GENERAL PHILOSOPHY

• Correctness first, ergonomics second.
• Explicit over implicit.
• Predictable over clever.
• Deterministic initialization.
• No magic, no hidden behavior.
• Code must be Dragon‑dictation‑friendly.
• Unicode‑safe and Windows‑aware.

FILE STRUCTURE AND ORGANIZATION

• One class per file unless tightly coupled.
• Namespace matches folder structure.
• No unused usings.
• Use implicit global usings only when they do not introduce ambiguity.
• Designer‑generated files remain untouched except for catastrophic fixes.

NAMING CONVENTIONS

3.1 Fields
• Private fields use mCamelCase.
• No underscores.
• No Hungarian notation.
Example: private int mTokenIndex;

3.2 Properties
• PascalCase.
• No abbreviations unless industry‑standard.
Example: public string FilePath { get; private set; }

3.3 Methods
• PascalCase.
• Verbs for actions, nouns for queries.

3.4 Parameters
• camelCase.
• Exception parameters use:
pException
pOuter / pInner
pInner1, pInner2 for multiple nested blocks.

INDENTATION AND SPACING

• Indentation is 3 spaces, never tabs.
• No blank lines inside methods except:
– One blank line after a pure variable‑declaration block.
• No blank line before return.
• Maximum line length: approximately 130 characters (soft limit).

BRACES AND BLOCKS

• Opening brace on the same line.
• Closing brace aligned with the statement that opened the block.

Example:
if (condition) {
DoSomething();
}

NULLABILITY

• <Nullable>enable</Nullable> is required.
• Fields must be initialized deterministically.
• Avoid ? unless semantically required.
• Avoid ! except for designer‑generated fields.

EXCEPTIONS AND ERROR HANDLING

• Use explicit try/catch blocks.
• Catch parameters follow naming rules (pException, etc.).
• No empty catch blocks.
• No swallowing exceptions silently.
• Centralized fatal‑error boundary in Program.Main.

WINFORMS RULES

• Designer fields may use = null!; only when unavoidable.
• No logic in designer files.
• Event handlers must be explicit methods, not lambdas.
• Timers must be fully qualified if ambiguous:
private readonly System.Windows.Forms.Timer mTimer;

SYNTAX HIGHLIGHTING SUBSYSTEM RULES

• Tokenizers must be deterministic.
• No backtracking unless explicitly documented.
• Highlighters must not mutate tokens.
• LanguageRegistry is the single source of truth.
• All language definitions live in /Syntax.

LONG STATEMENT CONTINUATION RULE (NEW — 29 MARCH 2026)

Method calls, constructor calls, and object/collection initializers must remain on a single line whenever the entire statement fits within the ~130‑character soft limit.

If the statement exceeds the soft limit, break the line only at natural operator boundaries (such as commas, binary operators, or concatenation points). The continuation line must be indented one indentation level (3 spaces). If the continuation line itself exceeds the soft limit, break again at the next natural boundary. All continuation lines must use the same indentation level; do not stair‑step.

The closing parenthesis should remain on the same line as the final argument unless doing so would exceed the soft limit, in which case it appears on its own line aligned with the start of the statement.

Examples (shown conceptually):

Fits on one line:
SomeReallyLongMethodName(someReallyLongParameter, anotherReallyReallyReallyLongParameter, andYetAnotherReallyRealleLongParameter, goingForSomethingEvenLongerThanExpected);

Break at natural boundary:
SomeReallyLongMethodName(someReallyLongParameter, anotherReallyReallyReallyLongParameter, andYetAnotherReallyRealleLongParameter,
goingForSomethingEvenLongerThanExpected, YetAnotherReallyReallyReallyLongParameter, andEvenYetAnotherReallyRealleLongParameter);

Break again if still too long:
SomeReallyLongMethodName(someReallyLongParameter, anotherReallyReallyReallyLongParameter, andYetAnotherReallyRealleLongParameter,
goingForSomethingEvenLongerThanExpected, YetAnotherReallyReallyReallyLongParameter, andEvenYetAnotherReallyRealleLongParameter,
goingFinallyForSomethingEvenLongerThanExpected);

P/INVOKE RULES

• All P/Invoke declarations live in NativeMethods.cs.
• Use explicit marshaling:
[return: MarshalAs(UnmanagedType.Bool)]
• No Win32 naming conventions leak into the main codebase.
• Domain‑level names replace HWND constants:
Example: mInsertAfterWindow instead of HWND_TOP.

COMMENTS AND DOCUMENTATION

• Use // for inline comments.
• Use XML documentation only for public APIs.
• Comments must explain why, not what.

GITHUB AND REPO STRUCTURE

• Style guide lives in:
/DBCode/StyleGuide.md
• All code must compile cleanly with warnings treated as errors.
• No unused files or dead code.

DRAGON DICTATION CONSIDERATIONS

• Avoid symbols that are hard to dictate.
• Avoid clever formatting.
• Keep method names pronounceable.
• Avoid nested lambdas or fluent chains.

END OF STYLE GUIDE