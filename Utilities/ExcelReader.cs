using System;
using System.Collections.Generic;
using System.IO;
using ClosedXML.Excel;

namespace WebAutomationSuite.Utilities
{
    public static class ExcelReader
    {
        // Simple helper to read first worksheet into list of dictionaries
        public static List<Dictionary<string, string>> Read(string filepath)
        {
            var results = new List<Dictionary<string, string>>();
            if (!File.Exists(filepath)) throw new FileNotFoundException($"Excel file not found: {filepath}");

            using var wb = new XLWorkbook(filepath);
            var ws = wb.Worksheet(1);
            var headerRow = new List<string>();
            var firstRow = ws.FirstRowUsed();
            var header = firstRow.RowUsed();

            foreach (var cell in header.CellsUsed())
            {
                headerRow.Add(cell.GetString());
            }

            var rows = ws.RowsUsed().Skip(1);
            foreach (var row in rows)
            {
                var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                int i = 0;
                foreach (var cell in row.Cells(1, headerRow.Count))
                {
                    var key = headerRow[i];
                    dict[key] = cell.GetString();
                    i++;
                }

                results.Add(dict);
            }

            return results;
        }
    }
}

