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
### Event Handler Sender Nullability Rule
All event handlers must declare the sender parameter as `object? pSender`. WinForms and other
.NET event sources may legally pass null as the sender, and this annotation prevents unnecessary
nullability warnings. Handlers that do not use `pSender` still follow this rule for consistency.


EXCEPTIONS AND ERROR HANDLING

• Use explicit try/catch blocks.
• Catch parameters follow naming rules (pException, etc.).
• No empty catch blocks.
• No swallowing exceptions silently.
• Centralized fatal‑error boundary in Program.Main.

**“Only catch exceptions when the method can recover or add meaningful diagnostic information.
Otherwise, allow the exception to propagate to the fatal handler in Program.Main.”**Exception Handling Policy
1. Do not catch exceptions you cannot meaningfully handle.
A method must not catch exceptions simply to prevent a crash.
If the method cannot fix the problem or enrich the diagnostic information, the exception must be allowed to propagate.

Examples of non‑recoverable errors (do not catch):

A required parameter is null

A required list is empty

A control reference is unexpectedly null

A method is called in an invalid state

A contract is violated

These are programming errors, not runtime conditions.
They must fall through to the unified fatal handler in Program.Main.

2. Only catch exceptions when the method can recover.
A method may catch exceptions only when it can continue execution safely and predictably.

Examples of recoverable situations:

Clipboard is temporarily unavailable → retry or fall back

Graphics measurement fails → use a safe default size

File not found → create the file

Network timeout → retry

If the method can restore a valid state, catching is appropriate.

3. Catching is also allowed when adding meaningful diagnostic context.
If a method can’t recover but can provide additional, actionable information, it may catch and rethrow.

Example:
Wrapping a low‑level exception with subsystem‑specific context:
catch (Exception pException) {
   throw new InvalidOperationException("Syntax subsystem failed during tokenization.", pException);
}
This is allowed because it improves debuggability without hiding the failure.

4. Never swallow exceptions.
Empty catch blocks or catch‑and‑ignore patterns are strictly prohibited.
They hide bugs and break the deterministic error‑handling model.

5. Contract violations must throw immediately.
If a method requires non‑null or non‑empty inputs, it must validate and throw:
if ((pNumbers == null) || (pNumbers.Count == 0))
   throw new ArgumentException("pNumbers must not be null or empty.", nameof(pNThese exceptions are intentional and must not be caught inside the method.

6. The fatal boundary lives in Program.Main.
All unhandled exceptions must flow to the centralized fatal handler.
This ensures:

deterministic shutdown

consistent logging

predictable user messaging

no silent failures

no inconsistent UI state

This is the backbone of the application’s reliability model.umbers));


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

### Foreach Variable Naming Rule
The loop variable in a foreach statement follows the same naming rules as any local variable:
camelCase, descriptive, and without the `p` prefix (which is reserved exclusively for method
parameters). Example:
   foreach (ToolStripMenuItem tsmi in menu.DropDownItems.OfType<ToolStripMenuItem>()) {

### Foreach Type Filtering Rule
When iterating over a heterogeneous collection (e.g., ControlCollection, ToolStripItemCollection),
always use `.OfType<T>()` to guarantee type safety and avoid runtime casting. This ensures explicit,
predictable behavior and eliminates nullability warnings related to invalid casts.

Always explicitly encompass logical tests. Never rely on operator precedence. Every comparison or boolean expression 
must be wrapped in its own parentheses, even when the language would evaluate it correctly without them. This applies 
to all logical operators, including AND, OR, and mixed comparison‑logical expressions. The goal is clarity, predictability, 
and Dragon‑friendly readability.
This:
         if ((result == 0) && (cloaked != 0))
not this:
         if (result == 0 && cloaked != 0)

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