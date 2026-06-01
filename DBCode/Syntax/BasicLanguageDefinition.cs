namespace DBCode.Syntax {
   internal sealed class BasicLanguageDefinition : ILanguageDefinition {
      private static readonly string[] mKeywords = [
         "AddHandler", "AddressOf", "Alias", "And", "AndAlso", "As",
         "Boolean", "ByRef", "ByVal", "Byte",
         "CBool", "CByte", "CChar", "CDate", "CDbl", "CDec", "CInt", "CLng",
         "CObj", "CShort", "CSng", "CStr", "CType", "CUInt", "CULng",
         "CUShort", "Call", "Case", "Catch", "Char", "Class", "Const",
         "Continue",
         "Date", "Decimal", "Declare", "Delegate", "Dim", "DirectCast", "Do",
         "Double",
         "Each", "Else", "ElseIf", "End", "Enum", "Error", "Event", "Exit",
         "False", "Finally", "For", "Function",
         "Get", "GetType", "GetXMLNamespace", "Global", "GoSub", "GoTo",
         "Handles",
         "If", "Implements", "Imports", "In", "Inherits", "Integer",
         "Interface", "Is", "IsNot",
         "Let", "Like", "Long", "Loop",
         "Mod", "Module", "MustInherit", "MustOverride", "MyBase", "MyClass",
         "Namespace", "New", "Next", "Not", "NotInheritable", "NotOverridable",
         "Nothing",
         "Object", "Of", "On", "Operator", "Optional", "Or", "OrElse",
         "Out", "Overloads", "Overridable", "Overrides",
         "ParamArray", "Partial", "Private", "Property", "Protected", "Public",
         "RaiseEvent", "ReadOnly", "RemoveHandler", "Resume", "Return",
         "SByte", "Select", "Set", "Shadow", "Shared", "Short", "Single",
         "Static", "Step", "Stop", "String", "Structure", "Sub",
         "Then", "Throw", "To", "True", "Try", "TryCast", "TypeOf",
         "UInteger", "ULong", "UShort", "Until",
         "Wend", "While", "Widening", "With", "WithEvents", "WriteOnly",
         "Xor"
      ];

      public LanguageKind Language => LanguageKind.Basic;

      public IReadOnlyCollection<string> Keywords => mKeywords;

      public StringComparer KeywordComparer => StringComparer.Ordinal;
   }
}
