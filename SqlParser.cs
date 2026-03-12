using System;

namespace IntroductionToADO.Introduction
{
    public static class SqlParser
    {
        public static string GetTableName(string cmd)
        {
            string upperCmd = cmd.ToUpperInvariant();
            int insertIndex = upperCmd.IndexOf("INSERT INTO");
            if (insertIndex == -1)
            {
                return "";
            }

            int tableStart = insertIndex + 11;
            int tableEnd = tableStart;
            while (tableEnd < cmd.Length && cmd[tableEnd] != ' ' && cmd[tableEnd] != '(')
            {
                tableEnd++;
            }

            string tableName = cmd.Substring(tableStart, tableEnd - tableStart);
            return tableName.Trim();
        }

        public static string GetColumnNames(string cmd)
        {
            int openParenIndex = cmd.IndexOf('(');
            int closeParenIndex = cmd.IndexOf(')');

            if (openParenIndex == -1 || closeParenIndex == -1)
            {
                return "";
            }

            string columns = cmd.Substring(openParenIndex + 1, closeParenIndex - openParenIndex - 1);
            return columns.Trim();
        }

        public static string GetRowValues(string cmd)
        {
            string upperCmd = cmd.ToUpperInvariant();
            int valuesIndex = upperCmd.IndexOf("VALUES");

            if (valuesIndex == -1)
            {
                return "";
            }

            int valuesStart = valuesIndex + 6;
            string afterValues = cmd.Substring(valuesStart);
            
            int openParenIndex = afterValues.IndexOf('(');
            int closeParenIndex = afterValues.IndexOf(')');

            if (openParenIndex == -1 || closeParenIndex == -1)
            {
                return "";
            }

            string values = afterValues.Substring(openParenIndex + 1, closeParenIndex - openParenIndex - 1);
            return values.Trim();
        }
    }
}
