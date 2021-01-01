using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode2020.Utility
{
    public static class FileUtility
    {
        /// <summary>
        /// Returns the contents of a file as a single string
        /// </summary>
        /// <param name="filePath">The path of the file, relative to the root of the main project</param>
        /// <returns>The contents of a file as a string</returns>
        public static string ReadFileToString(string filePath)
        {
            StreamReader file = new StreamReader(filePath);
            string contents = file.ReadToEnd();
            file.Close();

            return contents;
        }

        /// <summary>
        /// Splits a file into values of type T by carriage return, using a defined parser function.
        /// Returns the values as a List of type T
        /// </summary>
        /// <typeparam name="T">The type to cast each line into</typeparam>
        /// <param name="filePath">The path of the file, relative to the root of the main project</param>
        /// <param name="parser">The parser function used to cast each row into type T</param>
        /// <returns>A List of type T from each line of the file</returns>
        public static List<T> ParseFileToList<T>(string filePath, Func<string, T> parser)
        {
            List<T> splits = new List<T>();
            string line;
            StreamReader file = new StreamReader(filePath);

            // Iterate over each line in the input
            while ((line = file.ReadLine()) != null)
            {
                splits.Add(parser(line));
            }
            file.Close();
            return splits;
        }

        /// <summary>
        /// Splits a file into values of type T by empty line, using a defined parser function.
        /// Assumes that the string input spans multiple lines and only stops when there is an
        /// empty line and a carriage return.
        /// Returns the values as a List of type T.
        /// </summary>
        /// <typeparam name="T">The type to cast each "group" of lines into</typeparam>
        /// <param name="filePath">The path of the file, relative to the root of the main project</param>
        /// <param name="parser">The parser function used to cast group of rows into type T</param>
        /// <param name="separater">If the gathered strings represent a parseable group, this is what separates the group.  If none, then pass in an empty string.</param>
        /// <returns>A List of type T from each line of the file</returns>
        public static List<T> ParseFileToMultiLineList<T>(string filePath, Func<string, T> parser, string separater)
        {
            List<T> output = new List<T>();
            string line;
            string input = "";
            StreamReader file = new StreamReader(filePath);

            // Iterate over each line in the input
            while ((line = file.ReadLine()) != null)
            {
                // Passport can span multiple lines, so no easy way to split up the file with easy rules
                // We need to combine lines until there is a blank one
                if (line == "")
                {
                    // Trim off extra separater at end
                    string trimmed = input.Substring(0, input.Length - separater.Length);
                    output.Add(parser(trimmed));
                    input = "";
                }
                else
                {
                    input = $"{input}{line}{separater}";
                }
            }
            file.Close();

            // Get the final line if valid
            if (input != "")
            {
                // Trim off extra separater at end
                string trimmed = input.Substring(0, input.Length - separater.Length);
                output.Add(parser(trimmed));
            }

            return output;
        }
    }
}