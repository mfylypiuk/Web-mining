using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace lab2_email_sender_console.Models
{
    class CsvFile
    {
        public string Path { get; set; }
        public char Delimiter { get; set; }
        public List<string> Headers { get; set; }
        public List<List<string>> Rows { get; set; }

        public CsvFile(char delimiter)
        {
            Delimiter = delimiter;
            Headers = new List<string>();
            Rows = new List<List<string>>();
        }

        public CsvFile(char delimiter, List<string> headers)
        {
            Delimiter = delimiter;
            Headers = headers;
            Rows = new List<List<string>>();
        }

        public CsvFile(char delimiter, List<string> headers, List<List<string>> rows)
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

        public bool ReadFile(string filePath, out string reason)
        {
            reason = string.Empty;

            try
            {
                
                List<string> lines = new List<string>(File.ReadAllLines(filePath));

                if (lines.Count > 1)
                {
                    foreach (string line in lines)
                    {
                        if (line == lines.First())
                        {
                            Headers.AddRange(lines.First().Split(Delimiter));
                        }
                        else
                        {
                            Rows.Add(new List<string>(line.Split(Delimiter)));
                        }
                    }
                }
                else
                {
                    reason = "File with emails is empty";
                    return false;
                }
            }
            catch (Exception ex)
            {
                reason = ex.Message;
                return false;
            }

            return true;
        }
    }
}
