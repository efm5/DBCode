# DBCode Unified Style Guide
A complete, canonical, Markdown‑compliant guide covering:
- C# coding style rules
- UI & accessibility rules
- Subsystem‑specific rules
- Meta‑rules
- Deterministic architectural principles

This document governs all code written for DBCode and all related subsystems.

---

# 1. C# CODING STYLE RULES

## 1.1 Naming Conventions
- Use PascalCase for:
  - Classes
  - Structs
  - Enums
  - Public methods
  - Public properties
- Use camelCase for:
  - Method parameters
  - Local variables
- Use `mName` for fields:
  - `mName`, `mFonts`, `mColors`, etc.
- Avoid abbreviations unless universally understood.
- Avoid similar‑sounding names (Dragon dictation constraint).
- Names must be dictatable in a single short breath.

## 1.2 Indentation
- Indent using **3 spaces**.
- No tabs.
- All braces on the **same line** as the declaration.
- Closing brace aligned with the start of the declaration.

## 1.3 Line Length
- Maximum line length: ~130 characters.
- Break lines only when necessary.
- Break at natural boundaries:
  - After commas
  - After operators
  - Before method arguments (continuation lines)

## 1.4 Blank Lines
- No blank lines inside methods except:
  - After a pure variable‑declaration block.
- No blank line before a `return`.

## 1.5 Operator Boundary Rules
- Break lines at operator boundaries when needed.
- Never hide operators at the end of a long line.
- Continuation lines must be indented one level (3 spaces).

## 1.6 Method Layout
- Method signature on one line unless too long.
- Opening brace on same line.
- Variable declarations first.
- Logic follows.
- No blank line before return.

## 1.7 Exception Handling Rules
- Catch parameter name: `pException`.
- Nested try/catch:
  - Outer: `pOuter`
  - Inner: `pInner`
  - Multiple inner: `pInner1`, `pInner2`, etc.
- All exceptions must be logged or surfaced.
- No silent failures.
- Centralized exception handling preferred.

## 1.8 Enum Rules
### 1.8.1 General
- Enums represent deterministic categories.
- Enums must be stable and indexable.

### 1.8.2 **Enum Initialization Rule**
All new enums must explicitly initialize their first value (e.g., `Value = 0`).  
Existing enums may remain implicit unless the file is being refactored for other reasons.  
When refactoring an existing file, update any noncompliant enums in that file to follow this rule.

**Rationale:**  
Explicit initialization prevents accidental renumbering, preserves array‑index stability, and ensures deterministic behavior across the codebase. This rule applies forward and incrementally during normal maintenance; no retroactive project‑wide cleanup is required.

### 1.8.3 Flags Enums
- Only use when bitwise combinations are intended.
- Must explicitly assign powers of two.

## 1.9 File Structure
- One class per file unless tightly coupled.
- Using directives at top.
- Namespace follows immediately.
- No region blocks.

## 1.10 Solution Structure Style Guide
- Console apps, WinForms apps, and Testbeds follow deterministic structure.
- `Main()` must have **no parameters** unless command‑line arguments are used.
- Startup paths must be deterministic and explicit.

---

# 2. UI & ACCESSIBILITY STYLE RULES

## 2.1 Deterministic Layout
- No auto‑flow.
- All positions and sizes explicit.
- All spacing uses `Fields.mEm`.

## 2.2 Anchoring Rules
- ListBox: Top/Left/Right
- Theme buttons: Top/Left
- Clusters: Left only
- Help: Bottom/Left
- Apply/OK/Cancel: Bottom/Right

## 2.3 Spacing Rules
- `Fields.mEm` is the base spacing unit.
- `Fields.mRightInset = 2 × Fields.mEm`.
- Cancel button inset: 2 × mEm.

## 2.4 Tab Order Rules
- `Fields.mTabIndex = 1`.
- All tabbed controls use `TabIndex = mTabIndex++`.
- Sequential, deterministic, no gaps.

## 2.5 Dragon Dictation Constraints
- All actionable text must be dictatable in a single short breath.
- Avoid similar‑sounding labels.
- Avoid homophones.
- Avoid unnatural pauses.
- Renderer‑safe spacing required.

## 2.6 Accessibility Rules
- All UI must be screen‑reader friendly.
- Modal dialogs:
  - Verbose message text
  - Concise question text
  - Buttons: Yes / No / (optional Cancel)
  - Follow spacing and inset rules
- TimedMessages:
  - Titles: ERROR or WARNING (all caps)
  - Body may be verbose
  - No Dragon constraints required

---

# 3. SUBSYSTEM‑SPECIFIC RULES

# 3.1 Preferences / Theme Subsystem

## 3.1.1 Arrays
- Theme fonts and colors stored in arrays indexed by enums.
- Arrays are deterministic, fixed‑length, never resized.

## 3.1.2 Brightness
- Colors classified into Light / Medium / Dark buckets.

## 3.1.3 Color Tables
- All UI colors stored in ColorUseCase‑indexed arrays.
- No inference.
- No magic.

## 3.1.4 Defaults
- Default Light and Default Dark themes defined explicitly.
- Defaults used whenever a font or color is missing.

## 3.1.5 Enums
- FontUseCase
- ColorUseCase
- HelpTopic
- ThemeAction (string tags)
- All routing is enum‑driven except ThemeAction.

## 3.1.6 File I/O
- `Themes.txt` is canonical storage.
- Load:
  - Validate
  - Skip bad themes
  - Fallback to defaults if needed
- Save:
  - Only when Preferences Panel closes with OK or Apply
- No nulls allowed; defaults substituted before writing.

## 3.1.7 Help System
- Help buttons use Tag = HelpTopic enum.
- HTML files stored in `ExecutableDirectory\Help`.
- Missing file → TimedMessage.

## 3.1.8 Initialization
- Load themes.
- Validate.
- Select LastUsedThemeName or fallback to Default Dark.
- Apply theme immediately.

## 3.1.9 Preferences Panel Layout
- Three top lines:
  1. Centered title (“Preferences”)
  2. PrefixButton + ListBox (sequential TabIndexes)
  3. Theme action buttons (New/Delete/Rename/Duplicate)
- Middle section: Label/Button clusters for fonts and colors.
- Color labels explicitly include the word “Color”.
- Bottom-left: Help (inset 1 × mEm).
- Bottom-right: Apply, OK, Cancel.
- ListBox height set dynamically during Layout.

## 3.1.10 Tag Routing
- Font buttons: Tag = FontUseCase.
- Color buttons: Tag = ColorUseCase.
- Theme action buttons: Tag = "ThemeAction.*".
- Help buttons: Tag = HelpTopic.
- One handler per subsystem.

## 3.1.11 Validation
- Theme names must be:
  - Unique
  - Non‑empty
  - Serialization‑safe
- Dragon‑specific issues:
  - TimedMessage + reject (for now)
- Missing fonts/colors:
  - Default + TimedMessage
- Missing theme:
  - Fallback to Default Dark + TimedMessage

## 3.1.12 Warnings / TimedMessages
- Titles: ERROR or WARNING (all caps).
- Body may be verbose.
- Modal dialogs (future):
  - Verbose message + concise question
  - Yes/No/(optional Cancel)

## 3.1.13 XML
- Not used.
- All storage is plain text (`Themes.txt`).

## 3.1.14 Zero‑Drift Rules
- No inference.
- No hidden defaults.
- No dynamic control creation (except future tabs).
- All behavior explicit, deterministic, accessibility‑driven.

---

# 4. META‑RULES

## 4.1 Zero Drift
- No silent changes.
- No inferred behavior.
- No “magic.”
- All behavior must be explicit.

## 4.2 Determinism
- Same inputs → same outputs.
- No randomness.
- No implicit defaults.

## 4.3 Accessibility First
- Dragon dictation constraints apply globally.
- Screen‑reader compatibility required.
- Breath‑limited labels.
- Renderer‑safe spacing.

## 4.4 Future‑Proofing
- All new code must follow the latest rules.
- Old code updated only when touched for refactoring.
- No project‑wide retroactive cleanup unless explicitly scheduled.

---

# 5. PROJECT ORGANIZATION RULES

## 5.1 Folder Structure
- `DBCode\Themes` for theme subsystem.
- `ProjectAssets` for:
  - Style guides
  - Architecture notes
  - `.csproj` (if desired)
  - Non‑source artifacts

## 5.2 File Naming
- One class per file unless tightly coupled.
- File name matches class name.

## 5.3 Build Determinism
- No dynamic code generation.
- No runtime resizing of arrays.
- No hidden defaults.

---

# END OF DOCUMENT