using System.Collections.Generic;

namespace DBCode.Syntax
{
   internal interface ILanguageDefinition
   {
      LanguageKind Language { get; }
      IReadOnlyCollection<string> Keywords { get; }
   }
}
