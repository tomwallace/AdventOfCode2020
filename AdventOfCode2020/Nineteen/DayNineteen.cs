using AdventOfCode2020.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public int CountValidMessagesRecursive(string rulesFilePath, string messagesFilePath)
        {
            Dictionary<int, RuleSimple> rules = FileUtility.ParseFileToList(rulesFilePath, line => new RuleSimple(line))
                .ToDictionary(d => d.Id, r => r);

            // Compile the rules to derive possibilities
            foreach (int ruleId in rules.Keys)
            {
                Compile(rules[ruleId], rules);
            }

            // Get possible correct answers
            HashSet<string> validMessages = rules[0].Compiled.ToHashSet();

            List<string> messages = FileUtility.ParseFileToList(messagesFilePath, line => line);
            int countValidMessages = messages.Count(m => validMessages.Contains(m));

            return countValidMessages;
        }

        // Compile the rule by mutating and creating its Compiled collection
        private void Compile(RuleSimple rule, Dictionary<int, RuleSimple> rules)
        {
            // If we have already compiled the rule, exit
            if (rule.Compiled != null)
                return;

            // This rule is a letter, so set Compiled to the letter
            if (rule.Source[0] == '"')
            {
                rule.Compiled = new[] { rule.Source.Substring(1, rule.Source.Length - 2) };
                return;
            }

            List<string> compiled = new List<string>();

            // TODO: Extract out to a separate private method
            void CombineValues(string prefix, List<string[]> fragments)
            {
                if (fragments.Count == 0)
                {
                    compiled.Add(prefix);
                    return;
                }

                foreach (var s in fragments[0])
                {
                    List<string[]> laterFragments = fragments.Skip(1).ToList();
                    CombineValues(prefix + s, laterFragments);
                }
            }

            foreach (var subRules in rule.Source.Split('|'))
            {
                var fragmentsInner = new List<string[]>();
                foreach (var r in subRules.Trim().Split())
                {
                    int id = int.Parse(r.Trim());
                    RuleSimple rr = rules[id];
                    Compile(rr, rules);
                    fragmentsInner.Add(rr.Compiled);
                }

                CombineValues("", fragmentsInner);
            }

            rule.Compiled = compiled.ToArray();
        }

        // TODO: This takes forever too
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

        /*
        public bool IsValidMessage(Dictionary<int, Rule> rules, string message)
        {
            Stack<MessageCheckStep> stack = new Stack<MessageCheckStep>();
            HashSet<string> visitedAlready = new HashSet<string>();

            stack.Push(new MessageCheckStep(0, new List<int>()));

            while (stack.Any())
            {
                MessageCheckStep current = stack.Pop();

                // We have been here already
                if (visitedAlready.Contains(current.ToString()))
                    continue;
                visitedAlready.Add(current.ToString());

                int currentIndex = current.CharIndex;
                List<int> currentSubRules = current.RulesChecked;

                // Skip out if there are no sub rules to check or we are at the end of message
                if (!currentSubRules.Any() || currentIndex >= message.Length)
                    continue;
            }
        }
        */

        // TODO: This takes forever for the given input size
        /*
        public HashSet<string> PossibleValidMessages(string rulesFilePath)
        {
            HashSet<string> possibleValues = new HashSet<string>();
            Queue<string> queue = new Queue<string>();
            string compoundValidMessage = GenerateCompoundValidMessage(rulesFilePath);
            queue.Enqueue(compoundValidMessage);

            while (queue.Any())
            {
                string current = queue.Dequeue();
                if (current.ToCharArray().All(c => char.IsLetter(c) || char.IsWhiteSpace(c)))
                    possibleValues.Add(current.Replace(" ", ""));
                else
                {
                    // Create copies of possibilities
                    // Work with nested parentheses
                    MatchCollection innerMatches = Regex.Matches(current, @"\([a-z\|\s]+?\)");
                    if (innerMatches.Count > 0)
                    {
                        // Recursive case
                        foreach (Match match in innerMatches)
                        {
                            string inner = match.Value.Replace("(", "").Replace(")", "");
                            string[] splits = inner.Split(new[] {" | "}, StringSplitOptions.None);
                            foreach (string part in splits)
                            {
                                string currentCopy = current;
                                string potential = currentCopy.Replace(match.Value, part);
                                queue.Enqueue(potential);
                            }
                            // Not sure if I need this
                            current = current.Replace(match.Value, "");
                        }
                    }
                }
            }

            return possibleValues;
        }

        // Left public for unit tests
        public string GenerateCompoundValidMessage(string rulesFilePath)
        {
            Dictionary<int, RuleString> rules = FileUtility.ParseFileToList(rulesFilePath, line => new RuleString(line))
                .ToDictionary(d => d.Id, r => r);

            return GenerateCompoundMessageRecursive(rules, rules[0].Value);
        }

        private string GenerateCompoundMessageRecursive(Dictionary<int, RuleString> rules, string message)
        {
            char[] messageArray = message.ToCharArray();

            // Exit condition - all of the numbers are replace
            if (messageArray.All(c => !char.IsNumber(c)))
                return message;

            // TODO: get away from builder as it was out of memory
            string messagePart = String.Empty;
            // Otherwise, move through and replace all numbers that you can
            for (int i = 0; i < messageArray.Length; i++)
            {
                if (char.IsNumber(messageArray[i]))
                {
                    messagePart = $"{messagePart}{rules[int.Parse(messageArray[i].ToString())].Value}";
                }
                else
                {
                    messagePart = $"{messagePart}{messageArray[i]}";
                }
            }

            return GenerateCompoundMessageRecursive(rules, messagePart);
        }

        private bool IsValidChar(char c)
        {
            return char.IsLetter(c) || char.IsWhiteSpace(c) || c == '|' || c == '(' || c == ')';
        }
        */

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

    public class RuleSimple
    {
        public RuleSimple(string input)
        {
            string[] splits = input.Split(':');
            Id = int.Parse(splits[0]);
            Source = splits[1].Trim();
        }

        public int Id { get; set; }

        public string Source { get; set; }

        public string[] Compiled { get; set; }
    }

    public class MessageCheckStep
    {
        public MessageCheckStep(int charIndex, List<int> rulesChecked)
        {
            CharIndex = charIndex;
            RulesChecked = rulesChecked;
        }

        // index of the character in the message currently being checked
        public int CharIndex { get; set; }

        public List<int> RulesChecked { get; set; }

        public override string ToString()
        {
            return $"{CharIndex}:{string.Join(",", RulesChecked)}";
        }
    }

    public class RuleString
    {
        public RuleString(string input)
        {
            string[] splits = input.Split(':');
            Id = int.Parse(splits[0]);
            string potentialValue = splits[1].Trim();

            if (potentialValue.Contains("|"))
                potentialValue = $"({potentialValue})";

            if (potentialValue.Contains("\""))
                potentialValue = potentialValue.Replace("\"", "");

            Value = potentialValue;
        }

        public int Id { get; set; }
        public string Value { get; set; }
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
                textIds.Add(output.Substring(0, output.Length - 1));
            }

            return textIds;
        }
    }
}