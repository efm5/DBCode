using System.Collections.Generic;

namespace DBCode.Syntax
{
   internal static class LanguageRegistry
   {
      private static readonly Dictionary<LanguageKind, ILanguageDefinition> sDefinitions = new();
      private static readonly Dictionary<LanguageKind, ITokenizer> sTokenizers = new();
      private static readonly Dictionary<LanguageKind, IHighlighter> sHighlighters = new();

      internal static void Register(
         ILanguageDefinition pDefinition,
         ITokenizer pTokenizer,
         IHighlighter pHighlighter)
      {
         sDefinitions[pDefinition.Language] = pDefinition;
         sTokenizers[pTokenizer.Language] = pTokenizer;
         sHighlighters[pHighlighter.Language] = pHighlighter;
      }

      internal static ILanguageDefinition GetDefinition(LanguageKind pLanguage)
      {
         return sDefinitions[pLanguage];
      }

      internal static ITokenizer GetTokenizer(LanguageKind pLanguage)
      {
         return sTokenizers[pLanguage];
      }

      internal static IHighlighter GetHighlighter(LanguageKind pLanguage)
      {
         return sHighlighters[pLanguage];
      }
   }
}
