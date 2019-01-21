using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentsDemo1.Models
{
    /// <summary>
    /// StudentRank maps each student name to a linear Position within a 2D matrix Time x Marks
    /// for example (0,0) -> 0, (0,1) -> 1
    /// </summary>
    public class StudentRank
    {
        public static int NofCols { get; set; }

        public int Position { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Given an instance of a student rank, it is possible to determine if a neighbour in the 2D matrix linear representation
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public bool IsInGroup(StudentRank student)
        {
            return Position == student.Position ||
                student.Position == Position - 1 ||
                student.Position == Position + 1 ||
                student.Position == Position + NofCols ||
                student.Position == Position + NofCols - 1 ||
                student.Position == Position + NofCols + 1;
        }

        /// <summary>
        /// Deserialises formatted javascript arrary {{'s1','s2'},{'s3'} } into StudentRank list
        /// </summary>
        /// <param name="stringArray"></param>
        /// <returns></returns>
        public static List<StudentRank> Deserialize(string stringArray)
        {
            // handle malformed string
            if (String.IsNullOrWhiteSpace(stringArray) || stringArray.FirstOrDefault() != '{' || stringArray.LastOrDefault() != '}')
                throw new Exception("Invalid input format");

            var list = new List<StudentRank>();

            // remove { } & end of lines
            stringArray = stringArray.Substring(1, stringArray.Length - 1);
            stringArray = stringArray.Replace(System.Environment.NewLine, "");
            string[] separatingChars = { "}," };
            string[] lines = stringArray.Split(separatingChars, System.StringSplitOptions.RemoveEmptyEntries);
            if (lines.Any())
            {
                var position = 1;
                foreach (var line in lines)
                {
                    var cols = line.Replace("{", "").Replace("}", "").Replace("'", "").Split(',');
                    // set NofCols
                    var nocols = cols.Count();
                    if (nocols > NofCols) NofCols = nocols;
                    foreach (var col in cols)
                    {
                        var name = col.Trim().TrimEnd();
                        if (!String.IsNullOrWhiteSpace(name))
                        {
                            list.Add(new StudentRank { Position = position, Name = col });
                        }
                        position++;
                    }

                }
            }
            return list;
        }


        public static string Serialize(List<HashSet<string>> lists)
        {
            if (lists == null) return string.Empty;

            var res = new StringBuilder();

            if (lists.Any())
            {
                res.Append("{");
                var groups = 0;
                foreach (var list in lists)
                {
                    if (list.Any())
                    {
                        if (groups > 0) res.Append(",");
                        res.Append("{");
                        var students = 0;
                        foreach (var student in list)
                        {
                            if (students > 0) res.Append(",");
                            res.Append($"'{student}'");
                            students++;
                        }
                        res.Append("}");
                    }
                    groups++;
                }
                res.Append("}");
            }
            return res.ToString();
        }
    }
}
