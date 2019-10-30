using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SemearTest.Entities
{
    class Question
    {
        public String QuestionEntitie { get; set; }

        public Question(String question)
        {
            QuestionEntitie = question;
        }

        public string Process(string question)
        {
            try
            {
                if (question == string.Empty)
                    return Const.Const.EMPTY_MESSAGE;

                if (!question.Contains("?"))
                    return Const.Const.EMPTY_INTERROGATION;

                question = question.Replace("?", " ?");

                var symbol = this.GetSymbol(question);
                string metal = this.GetMetal(question);
                var romainNumberComp = this.getRomanNumber(symbol);
                double number = this.NumberTransform(romainNumberComp);
                var credit = this.Calculate(metal, (int)number);

                return credit > 0 ? metal == "None" ? string.Format("{0} {1} {2}", Concatenate(symbol.Select(x => x.Item1).ToList()), " is ", credit.ToString()) : string.Format("{0} {1} {2} {3} {4}", Concatenate(symbol.Select(x => x.Item1).ToList()), metal, " is ", credit.ToString(), " credits.") : Const.Const.ERROR_MESSAGE;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public List<Tuple<string, string>> GetSymbol(string question)
        {
            try
            {
                var symbols = new List<Tuple<string, string>>();
                symbols.Add(Tuple.Create("GLOB", "I"));
                symbols.Add(Tuple.Create("PROK", "V"));
                symbols.Add(Tuple.Create("PISH", "X"));
                symbols.Add(Tuple.Create("TEGJ", "L"));

                List<string> symbolList = question.ToUpper().Split(' ').ToList();
                var romanNumber = new List<Tuple<string, string>>();
                foreach (var item in symbolList.Where(x => symbols.Any(b => b.Item1 == x)))
                {
                    romanNumber.Add(Tuple.Create(item, symbols.Where(r => r.Item1 == item).Select(c => c.Item2).FirstOrDefault()));
                }
                return romanNumber;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public string GetMetal(string question)
        {
            var words = question.ToUpper().Split(" ");
            foreach (var word in words)
            {
                switch (word)
                {
                    case "SILVER":
                        return "Silver";
                    case "GOLD":
                        return "Gold";
                    case "IRON":
                        return "Iron";
                    default:
                        break;
                }
            }
            return "None";
        }

        public string getRomanNumber(List<Tuple<string, string>> romanList)
        {
            try
            {
                if (romanList.Count == 0)
                    return null;

                string romanNunber = string.Empty;

                if (romanList.GroupBy(x => x).Where(g => g.Count() > 3).Select(y => y.Key).ToList().Count > 1)
                    return "Wrong Format";

                var romList = romanList.Select(x => x.Item2).ToList();
                foreach (var item in romList)
                {
                    romanNunber += item.ToString();
                }

                return romanNunber;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public double NumberTransform(string romainNumber)
        {
            try
            {
                int[] RomanValue;
                int romanLength;
                var sum = 0;

                romanLength = romainNumber.Length;

                if (romanLength == 0)
                {
                    return 0;
                }

                RomanValue = new int[romanLength + 1];
                for (int number = 0; number < romanLength; number++)
                {
                    switch (romainNumber.Substring(number, 1))
                    {
                        case "M":
                            {
                                RomanValue[number] = 1000;
                                break;
                            }

                        case "D":
                            {
                                RomanValue[number] = 500;
                                break;
                            }

                        case "C":
                            {
                                RomanValue[number] = 100;
                                break;
                            }

                        case "L":
                            {
                                RomanValue[number] = 50;
                                break;
                            }

                        case "X":
                            {
                                RomanValue[number] = 10;
                                break;
                            }

                        case "V":
                            {
                                RomanValue[number] = 5;
                                break;
                            }

                        case "I":
                            {
                                RomanValue[number] = 1;
                                break;
                            }
                    }
                }

                for (int i = 0; i < romanLength; i++)
                {
                    if (i == romanLength)
                        sum = sum + RomanValue[i];
                    else if (RomanValue[i] >= RomanValue[i + 1])
                        sum = sum + RomanValue[i];
                    else
                        sum = sum - RomanValue[i];
                }

                return sum;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public double Calculate(string metal, int? numero)
        {
            if (numero == null || numero < 0)
                return 0;

            switch (metal)
            {
                case "None":
                    return (int)numero;
                case "Iron":
                    return (195.5 * (int)numero);
                case "Gold":
                    return (14450 * (int)numero);
                case "Silver":
                    return (17 * (int)numero);
                default:
                    return 0;
            }
        }

        string Concatenate(List<string> symbols)
        {
            StringBuilder result = new StringBuilder();
            foreach (var item in symbols)
            {
                result.Append(item);
                result.Append(" ");
            }
            return result.ToString().ToLower();
        }
    }
}
