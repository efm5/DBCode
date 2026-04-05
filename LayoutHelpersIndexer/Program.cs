using System;
using System.Collections.Generic;
using System.IO;

namespace LayoutHelpersIndexer {
   internal static class Program {
      private static void Main(string[] args) {
         string rootFolder = @"D:\DBCode\DBCode\LayoutHelpers";
         if (!Directory.Exists(rootFolder)) {
            Console.WriteLine("Folder not found: " + rootFolder);
            return;
         }

         List<string> filePaths = new List<string>(Directory.GetFiles(rootFolder, "*.cs", SearchOption.AllDirectories));
         Dictionary<string, List<string>> perFileIndex = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
         List<(string Signature, string FileName)> globalIndex = new List<(string, string)>();

         for (int fileIndex = 0; fileIndex < filePaths.Count; fileIndex++) {
            string filePath = filePaths[fileIndex];
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            List<string> signatures = new List<string>();

            string[] lines = File.ReadAllLines(filePath);
            int lineCount = lines.Length;

            for (int lineIndex = 0; lineIndex < lineCount; lineIndex++) {
               string trimmed = lines[lineIndex].Trim();
               if ((trimmed.StartsWith("internal static", StringComparison.Ordinal) &&
                   trimmed.EndsWith(") {", StringComparison.Ordinal)) ||
                   (trimmed.StartsWith("public static", StringComparison.Ordinal) &&
                   trimmed.EndsWith(") {", StringComparison.Ordinal))) {
                  string cleaned = CleanSignature(trimmed);
                  string withLineNumber = cleaned + "    (line " + (lineIndex + 1).ToString() + ")";
                  signatures.Add(withLineNumber);
                  globalIndex.Add((withLineNumber, fileName));
               }
            }

            signatures.Sort(StringComparer.Ordinal);
            perFileIndex[fileName] = signatures;
         }

         WritePerFileIndex(perFileIndex);
         WriteGlobalIndex(globalIndex);

         Console.WriteLine("Index generation complete.");
      }

      private static string CleanSignature(string signature) {
         string cleaned = signature.Replace("{", string.Empty).Trim();
         return cleaned;
      }

      private static void WritePerFileIndex(Dictionary<string, List<string>> perFileIndex) {
         string outputPath = "PerFileIndex.txt";
         using (StreamWriter writer = new StreamWriter(outputPath, false)) {
            List<string> fileNames = new List<string>(perFileIndex.Keys);
            fileNames.Sort(StringComparer.Ordinal);

            for (int index = 0; index < fileNames.Count; index++) {
               string fileName = fileNames[index];
               writer.WriteLine(fileName);
               List<string> signatures = perFileIndex[fileName];

               for (int sigIndex = 0; sigIndex < signatures.Count; sigIndex++)
                  writer.WriteLine("   " + signatures[sigIndex]);

               writer.WriteLine();
            }
         }
      }

      private static void WriteGlobalIndex(List<(string Signature, string FileName)> globalIndex) {
         string outputPath = "GlobalIndex.txt";
         using (StreamWriter writer = new StreamWriter(outputPath, false)) {
            globalIndex.Sort((a, b) => string.CompareOrdinal(a.Signature, b.Signature));

            for (int index = 0; index < globalIndex.Count; index++) {
               string signature = globalIndex[index].Signature;
               string fileName = globalIndex[index].FileName;
               writer.WriteLine(signature + " [" + fileName + "]");
            }
         }
      }
   }
}
