using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace lab1_email_parser
{
    class CsvHandler
    {
        public string Path { get; set; }
        public char Delimiter { get; set; }
        public List<string> Headers { get; set; }
        public List<List<string>> Rows { get; set; }

        public CsvHandler(char delimiter, List<string> headers, List<List<string>> rows)
        {
            Delimiter = delimiter;
            Headers = headers;
            Rows = rows;
        }

        public void AddRow(List<string> columns)
        {
            Rows.Add(columns);
        }

        public void SaveToFile(string filePath)
        {
            StringBuilder output = new StringBuilder();
            string headers = string.Empty;
            string body = string.Empty;

            foreach (string header in Headers)
            {
                headers += header + Delimiter;
            }

            output.Append(headers);
            output.AppendLine();

            foreach (List<string> row in Rows)
            {
                string rowText = string.Empty;

                foreach (string column in row)
                {
                    rowText += column + Delimiter;
                }

                output.Append(rowText);
                output.AppendLine();
            }

            File.WriteAllText(filePath, output.ToString());
        }
    }
}
