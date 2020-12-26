using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdventOfCode2020.Utility;

namespace AdventOfCode2020.Nineteen
{
    public class DayNineteen : IAdventProblemSet
    {
        public string Description()
        {
            return "Monster Messages [HARD]";
        }

        public int SortOrder()
        {
            return 19;
        }

        public string PartA()
        {
            string rulesFilePath = @"Nineteen\DayNineteenInput_Rules.txt";
            string messagesFilePath = @"Nineteen\DayNineteenInput_Messages.txt";

            int validMessageCount = CountValidMessages(rulesFilePath, messagesFilePath);
            return validMessageCount.ToString();
        }

        public string PartB()
        {
            string filePath = @"Sixteen\DaySixteenInput.txt";
            return "";
        }

        public int CountValidMessages(string rulesFilePath, string messagesFilePath)
        {
            Dictionary<int, Rule> rules = FileUtility.ParseFileToList(rulesFilePath, line => new Rule(line))
                .ToDictionary(d => d.Id, r => r);

            // TODO: Determine if we need to connect, might be able to get by with Ids only
            // Connect the rules
            foreach (Rule rule in rules.Values)
            {
                foreach (List<int> idList in rule.ConnectionIds)
                {
                    List<Rule> list = idList.Select(i => rules[i]).ToList();
                    rule.Connections.Add(list);
                }
            }

            // Get possible correct answers
            HashSet<string> validMessages = GetValidMessages(rules);

            List<string> messages = FileUtility.ParseFileToList(messagesFilePath, line => line);
            int countValidMessages = messages.Count(m => validMessages.Contains(m));

            return countValidMessages;
        }

        private HashSet<string> GetValidMessages(Dictionary<int, Rule> rules)
        {
            HashSet<string> validMessages = new HashSet<string>();
            Queue<string> queue = new Queue<string>();

            // Start with rule 0
            foreach (string textInfo in rules[0].GetTextInfos())
            {
                queue.Enqueue(textInfo);
            }

            do
            {
                string current = queue.Dequeue();
                string[] currentArray = current.Split('|');

                // We can construct a possibility
                if (currentArray.All(r => int.TryParse(r, out int output) == false))
                {
                    validMessages.Add(string.Join("", currentArray));
                }
                // Otherwise enqueue a new list of the possibilities
                else
                {
                    for (int i = 0; i < currentArray.Length; i++)
                    {
                        if (int.TryParse(currentArray[i], out int output))
                        {
                            Rule match = rules[output];
                            var textInfos = match.GetTextInfos();
                            foreach (var textInfo in textInfos)
                            {
                                currentArray[i] = textInfo;
                                string potential = string.Join("|", currentArray);
                                queue.Enqueue(potential);
                            }
                            /*
                            if (textInfos.Count == 1)
                                currentArray[i] = textInfos.First();
                            else
                            {
                                foreach (var textInfo in textInfos)
                                {
                                    currentArray[i] = textInfo;
                                    string potential = string.Join("|", currentArray);
                                    queue.Enqueue(potential);
                                }

                                break;
                            }
                            */
                        }
                    }

                    //queue.Enqueue(string.Join("|", currentArray));
                }
            } while (queue.Any());

            return validMessages;
        }
    }


    public class Rule
    {
        /*
        0: 4 1 5
        1: 2 3 | 3 2
        2: 4 4 | 5 5
        3: 4 5 | 5 4
        4: "a"
        5: "b"
         */
        public Rule(string input)
        {
            string[] splits = input.Split(':');
            Id = int.Parse(splits[0]);

            Connections = new List<List<Rule>>();
            ConnectionIds = new List<List<int>>();

            if (splits[1].Contains("\""))
            {
                string[] quotes = splits[1].Trim().Split('"');
                Value = quotes[1];
            }
            else
            {
                foreach (string section in splits[1].Trim().Split('|'))
                {
                    List<int> list = section.Trim().Split(' ').Select(s => int.Parse(s)).ToList();
                    ConnectionIds.Add(list);
                }
            }
        }
        
        public int Id { get; set; }
        
        public string Value { get; set; }

        public List<List<Rule>> Connections { get; set; }

        public List<List<int>> ConnectionIds { get; set; }

        public List<string> GetTextInfos()
        {
            List<string> textIds = new List<string>();
            foreach (List<Rule> ruleSets in Connections)
            {
                StringBuilder builder = new StringBuilder();
                foreach (Rule rule in ruleSets)
                {
                    builder.Append($"{rule.Value ?? rule.Id.ToString()}|");
                }

                string output = builder.ToString();
                textIds.Add(output.Substring(0,output.Length - 1));
            }

            return textIds;
        }
    }
}