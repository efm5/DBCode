namespace DBCode.Syntax {
   internal sealed class SqlLanguageDefinition : ILanguageDefinition {
      private static readonly string[] mKeywords = [
         "ALL", "ALTER", "AND", "ANY", "AS", "BEGIN", "BETWEEN", "BIGINT",
         "BINARY", "BIT", "BREAK", "BY", "CASE", "CAST", "CATCH", "CHAR",
         "CHECK", "COALESCE", "COLUMN", "COMMIT", "CONSTRAINT", "CONTINUE",
         "CREATE", "CROSS", "DATABASE", "DATE", "DATETIME", "DATETIME2",
         "DECIMAL", "DECLARE", "DEFAULT", "DELETE", "DENSE_RANK", "DISTINCT",
         "DROP", "ELSE", "END", "EXCEPT", "EXEC", "EXECUTE", "EXISTS",
         "FETCH", "FLOAT", "FOR", "FOREIGN", "FROM", "FULL", "FUNCTION",
         "GO", "GOTO", "GROUP", "HAVING", "IDENTITY", "IF", "IN", "INDEX",
         "INNER", "INSERT", "INT", "INTEGER", "INTERSECT", "INTO", "IS",
         "ISNULL", "JOIN", "KEY", "LAG", "LEAD", "LEFT", "LIKE", "MERGE",
         "MONEY", "NCHAR", "NEXT", "NOT", "NTEXT", "NULL", "NULLIF",
         "NUMERIC", "NVARCHAR", "OF", "OFFSET", "ON", "ONLY", "OR", "ORDER",
         "OUTER", "OUTPUT", "OVER", "PARTITION", "PRIMARY", "PRINT",
         "PROCEDURE", "RAISERROR", "RANK", "REAL", "REFERENCES", "RETURN",
         "RIGHT", "ROLLBACK", "ROW_NUMBER", "ROWS", "SAVEPOINT", "SCHEMA",
         "SELECT", "SET", "SMALLINT", "SOME", "TABLE", "TEXT", "THEN",
         "THROW", "TIME", "TIMESTAMP", "TINYINT", "TOP", "TRANSACTION",
         "TRIGGER", "TRUNCATE", "TRY", "UNION", "UNIQUE", "UNIQUEIDENTIFIER",
         "UPDATE", "USE", "VALUES", "VARBINARY", "VARCHAR", "VIEW", "WHEN",
         "WHERE", "WHILE", "WITH", "XML", "NTILE"
      ];

      public LanguageKind Language => LanguageKind.Sql;

      public IReadOnlyCollection<string> Keywords => mKeywords;

      public StringComparer KeywordComparer => StringComparer.OrdinalIgnoreCase;
   }
}
