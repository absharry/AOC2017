﻿namespace AdventOfCode.Web.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Nancy;
    using Nancy.ModelBinding;
    using AdventOfCode.Web.Requests;
    using AdventOfCode.Web.Responses;
    using AdventOfCode.Web.Model;
    using System.Diagnostics;

    public class DayModule : NancyModule
    {
        public DayModule() : base("/day/")
        {
            this.Get["/{day}/"] = x => this.View["DayInput"].WithModel(new { Day = x.Day });
            this.Post["/{day}/"] = this.GetDaysAnswer;
        }

        public dynamic GetDaysAnswer(dynamic arg)
        {
            var input = this.Bind<InputRequest>();
            var day = int.Parse(arg.day);
            var responseModel = new AnswerResponse();

            var stopWatch = new Stopwatch();
            stopWatch.Start();
            switch (day)
            {
                case 1:
                    responseModel = Day1(input.Input);
                    break;
                case 2:
                    responseModel = Day2(input.Input);
                    break;
                case 3:
                    responseModel = Day3(input.Input);
                    break;
                case 4:
                    responseModel = Day4(input.Input);
                    break;
                case 5:
                    responseModel = Day5(input.Input);
                    break;
                case 6:
                    responseModel = Day6(input.Input);
                    break;
                case 7:
                    responseModel = Day7(input.Input);
                    break;
                case 8:
                    responseModel = Day8(input.Input);
                    break;
                case 9:
                    responseModel = Day9(input.Input);
                    break;
                case 10:
                    responseModel = Day10(input.Input);
                    break;
                case 11:
                    responseModel = Day11(input.Input);
                    break;
                case 12:
                    responseModel = Day12(input.Input);
                    break;
                case 13:
                    responseModel = Day13(input.Input);
                    break;
                case 14:
                    responseModel = Day14(input.Input);
                    break;
                case 15:
                    responseModel = Day15(input.Input);
                    break;
                case 16:
                    responseModel = Day16(input.Input);
                    break;
                case 17:
                    responseModel = Day17(input.Input);
                    break;
                case 18:
                    responseModel = Day18(input.Input);
                    break;
                case 19:
                    responseModel = Day19(input.Input);
                    break;
                case 20:
                    responseModel = Day20(input.Input);
                    break;
                case 21:
                    responseModel = Day21(input.Input);
                    break;
                case 22:
                    responseModel = Day22(input.Input);
                    break;
                case 23:
                    responseModel = Day23(input.Input);
                    break;
                case 24:
                    responseModel = Day24(input.Input);
                    break;
                case 25:
                    responseModel = Day25(input.Input);
                    break;
                default:
                    break;
            }
            stopWatch.Stop();
            responseModel.Day = day;
            responseModel.TimeTaken = stopWatch.Elapsed.TotalSeconds;

            return this.View["DayOutput"].WithModel(responseModel);
        }

        private AnswerResponse Day1(string input)
        {
            var count1 = 0;
            var count2 = 0;
            var halfway = input.Length / 2;

            var codeArray = input.ToCharArray();

            for (int i = 0; i < codeArray.Length; i++)
            {
                var current = int.Parse(codeArray[i].ToString());
                var next = i == codeArray.Length - 1 ? int.Parse(codeArray[0].ToString()) : int.Parse(codeArray[i + 1].ToString());

                if (current == next)
                {
                    count1 += current;
                }
            }

            for (int i = 0; i < codeArray.Length; i++)
            {
                var current = int.Parse(codeArray[i].ToString());
                var nextIndex = i + halfway;

                if (nextIndex > codeArray.Length - 1)
                {
                    nextIndex = nextIndex - codeArray.Length;
                }

                var next = int.Parse(codeArray[nextIndex].ToString());

                if (current == next)
                {
                    count2 += current;
                }
            }

            return new AnswerResponse
            {
                Answer1 = count1.ToString(),
                Answer2 = count2.ToString()
            };
        }

        private AnswerResponse Day2(string input)
        {
            var code = input.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Split('\t').Select(y => int.Parse(y)).ToArray()).ToList();

            return new AnswerResponse
            {
                Answer1 = code.Sum(x => x.Max() - x.Min()).ToString(),
                Answer2 = code.Sum(array => array.Sum(item => array.Where(x => item != x && item % x == 0).Select(x => item / x).FirstOrDefault())).ToString()
            };
        }

        private AnswerResponse Day3(string input)
        {
            var code = int.Parse(input);
            var multiplier = 3;
            var ring = 0;

            while( ring == 0)
            {
                if(code < (multiplier * multiplier))
                {
                    ring = (multiplier + 1) / 2;
                    break;
                }

                multiplier += 2; 
            }

            var center = new int[]{ ring, ring };
            var inputLocation = new int[2];
            var bottomRight = multiplier * multiplier;
            var difference = bottomRight - code;

            var edge = Math.DivRem(difference, multiplier, out int remainder);

            switch (edge)
            {
                case 0:
                    inputLocation[0] = multiplier - difference; // position along the row
                    inputLocation[1] = 1; // height from the bottom
                    break;
                case 1:
                    inputLocation[0] = 1; // position along the row
                    inputLocation[1] = remainder; // height from the bottom
                    break;
                case 2:
                    inputLocation[0] = remainder; // position along the row
                    inputLocation[1] = multiplier; // height from the top
                    break;
                case 3:
                    inputLocation[0] = multiplier; // position along the row
                    inputLocation[1] = multiplier - remainder; // height from the top
                    break;
                default:
                    break;
            }

            var distance = Math.Abs(center[0] - inputLocation[0]) + Math.Abs(center[1] - inputLocation[1]);

            var array = new int[100, 100];
            array[50, 50] = 1;

            var coordinates = new int[] { 51, 50 };

            var xDirection = 0;
            var yDirection = 1;
            var steps = 1;
            var row = 2;

            for (int i = 0; i < 10000; i++)
            {
                var newRow = false;
                array[coordinates[0], coordinates[1]] =
                    array[coordinates[0] + 1, coordinates[1]] +
                    array[coordinates[0] + 1, coordinates[1] + 1] +
                    array[coordinates[0], coordinates[1] + 1] +
                    array[coordinates[0] - 1, coordinates[1] + 1] +
                    array[coordinates[0] - 1, coordinates[1]] +
                    array[coordinates[0] - 1, coordinates[1] - 1] +
                    array[coordinates[0], coordinates[1] - 1] +
                    array[coordinates[0] + 1, coordinates[1] - 1];

                if (array[coordinates[0], coordinates[1]] > code)
                {
                    break;
                }

                if(xDirection != 0 && steps == (row * 2) - 2)
                {
                    if(xDirection == -1) // top left turning point
                    {
                        xDirection = 0;
                        yDirection = -1;
                        steps = 0;
                    }
                    else // bottom right turning point (continue right onto next row)
                    {
                        row += 1;
                        steps = 0; // starting at bottom right
                        xDirection = 1;
                        yDirection = 0;
                        newRow = true;
                    }
                }else if(yDirection != 0 && steps == (row * 2) - 2)
                {
                    if(yDirection == 1) // top right turning point
                    {
                        yDirection = 0;
                        xDirection = -1;
                        steps = 0;
                    }
                    else // bottom left turning point
                    {
                        yDirection = 0;
                        xDirection = 1;
                        steps = 0;
                    }
                }

                steps += 1;

                coordinates = new int[] { coordinates[0] + xDirection, coordinates[1] + yDirection };

                if (newRow)
                {
                    xDirection = 0;
                    yDirection = 1;
                }
            }

            return new AnswerResponse
            {
                Answer1 = distance.ToString(),
                Answer2 = array[coordinates[0], coordinates[1]].ToString()
            };
        }

        private AnswerResponse Day4 (string input)
        {
            var code = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var count1 = 0;
            var count2 = 0;

            foreach (var passphrase in code)
            {                
                var words = passphrase.Split(' ');

                var containsSameWord = false;
                var containsSameLetters = false;

                foreach (var word in words)
                {
                    containsSameWord = words.Where(x => x == word).ToList().Count > 1;

                    containsSameLetters = words.Where(x => IsAnagram(x, word)).ToList().Count > 1;
                }

                if (containsSameWord)
                {
                    count1 += 1;
                }
                if (containsSameLetters)
                {
                    count2 += 1;
                }
            }

            return new AnswerResponse
            {
                Answer1 = (code.Length - count1).ToString(),
                Answer2 = (code.Length - count2).ToString()
            };
        }

        private AnswerResponse Day5(string input)
        {
            var code = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
            var exit = false;
            var currentPosition = 0;
            var count1 = 0;

            while (!exit)
            {
                if (currentPosition > code.Length - 1)
                {
                    exit = true;
                    break;
                }

                var currentInstruction = code[currentPosition];                

                code[currentPosition] += 1;
                count1 += 1;

                currentPosition += currentInstruction;                
            }

            exit = false;
            currentPosition = 0;
            var count2 = 0;
            code = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();

            while (!exit)
            {
                if (currentPosition > code.Length - 1)
                {
                    exit = true;
                    break;
                }

                var currentInstruction = code[currentPosition];

                code[currentPosition] += currentInstruction >= 3 ? -1 : 1;
                count2 += 1;

                currentPosition += currentInstruction;
            }

            return new AnswerResponse
            {
                Answer1 = count1.ToString(),
                Answer2 = count2.ToString()
            };
        }

        private AnswerResponse Day6 (string input)
        {
            var code = input.Split('\t').Select(int.Parse).ToArray();
            var list = new List<int[]>();
            var loopSize = 0;
            list.Add(code);
            var exit = false;

            while (!exit)
            {
                var temp = new int[code.Length];
                list.Last().CopyTo(temp, 0);
                var value = temp.Max();
                var indices = temp.Select((number, index) => number == value ? index : -1).Where(index => index != -1).FirstOrDefault();
                temp[indices] = 0;

                for (int i = indices + 1; i < value + indices + 1; i++)
                {
                    var currentIndex = i > temp.Length - 1 ? i - temp.Length : i;
                    temp[currentIndex]++;
                }

                if (list.Count(x => 
                x[0] == temp[0] && 
                x[1] == temp[1] &&
                x[2] == temp[2] &&
                x[3] == temp[3] &&
                x[4] == temp[4] &&
                x[5] == temp[5] &&
                x[6] == temp[6] &&
                x[7] == temp[7] &&
                x[8] == temp[8] &&
                x[9] == temp[9] &&
                x[10] == temp[10] &&
                x[11] == temp[11] &&
                x[12] == temp[12] &&
                x[13] == temp[13] &&
                x[14] == temp[14] &&
                x[15] == temp[15]) > 0)
                {
                    var matchingArray = list.Where(x =>
                x[0] == temp[0] &&
                x[1] == temp[1] &&
                x[2] == temp[2] &&
                x[3] == temp[3] &&
                x[4] == temp[4] &&
                x[5] == temp[5] &&
                x[6] == temp[6] &&
                x[7] == temp[7] &&
                x[8] == temp[8] &&
                x[9] == temp[9] &&
                x[10] == temp[10] &&
                x[11] == temp[11] &&
                x[12] == temp[12] &&
                x[13] == temp[13] &&
                x[14] == temp[14] &&
                x[15] == temp[15]).First();

                    var index = list.IndexOf(matchingArray);
                    loopSize = list.Count() - index;

                    exit = true;
                    break;
                }
                else
                {
                    list.Add(temp);
                }
            }

            return new AnswerResponse
            {
                Answer1 = list.Count().ToString(),
                Answer2 = loopSize.ToString()
            };
        }

        private AnswerResponse Day7(string input)
        {
            var code = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            var parentNodes = new List<TreeNode>();

            foreach (var item in code)
            {
                var split = item.Split(new string[] { "->" }, StringSplitOptions.RemoveEmptyEntries);

                var nameWeight = split[0].Split(' ');
                var name = nameWeight[0];
                var weight = nameWeight[1].Replace("(", string.Empty).Replace(")", string.Empty);
                var node = new TreeNode(name, int.Parse(weight));

                if (split.Length > 1)
                {
                    var children = split[1].Split(',');
                    node.Children = children.Select(x => x.Trim()).ToArray();
                }

                parentNodes.Add(node);
            }

            var bottomNode = string.Empty;

            foreach (var node in parentNodes.Where(x => x.Children != null))
            {
                if(parentNodes.Count(x => x.Children != null && x.Children.Contains(node.Name)) == 0)
                {
                    bottomNode = node.Name;
                }
            }

            var weight2 = 0;

            foreach (var node in parentNodes)
            {
                if(node.Children != null)
                {
                    var incorrectChild = this.GetIncorrectChild(parentNodes, node.Name);

                    if (incorrectChild != node.Name)
                    {
                        var parent = parentNodes.Where(x => x.Children != null && x.Children.Contains(incorrectChild)).First();
                        var children = parentNodes.Where(x => parent.Children.Contains(x.Name)).ToArray();
                        var incorrect = children.Where(x => x.Name == incorrectChild).First();
                        var correct = children.Where(x => x.Name != incorrectChild).First();
                        var childrenWeight = new int[children.Count()];

                        for (int i = 0; i < childrenWeight.Length; i++)
                        {
                            childrenWeight[i] = this.GetChildrensWeight(parentNodes, children[i].Name);
                        }

                        var weightDifference = childrenWeight[Array.IndexOf(children, correct)] - childrenWeight[Array.IndexOf(children, incorrect)];
                        weight2 = incorrect.Weight + weightDifference;
                    }
                }                
            }

            return new AnswerResponse
            {
                Answer1 = bottomNode,
                Answer2 = weight2.ToString()
            };
        }

        public AnswerResponse Day8(string input)
        {
            var code = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var instructions = new List<Instruction>();

            foreach (var line in code)
            {
                var instructionsString = line.Split(' ');

                var instruction = new Instruction()
                {
                    Register = instructionsString[0],
                    IsIncrease = instructionsString[1] == "inc",
                    ChangeValue = int.Parse(instructionsString[2]),
                    Condition = new Condition()
                    {
                        Register = instructionsString[4],
                        Operator = instructionsString[5],
                        Value = int.Parse(instructionsString[6])
                    }
                };

                instructions.Add(instruction);
            }

            var registers = new int[(long)Math.Pow(26, instructions.Max(x => x.Register.Length))];
            var max = 0;

            foreach (var instruction in instructions)
            {
                if(this.TestCondition(registers, instruction.Condition))
                {
                    registers[this.GetIndexOfRegister(instruction.Register)] = instruction.IsIncrease ? registers[this.GetIndexOfRegister(instruction.Register)] + instruction.ChangeValue : registers[this.GetIndexOfRegister(instruction.Register)] - instruction.ChangeValue;
                    if (registers[this.GetIndexOfRegister(instruction.Register)] > max)
                    {
                        max = registers[this.GetIndexOfRegister(instruction.Register)];
                    }
                }
            }

            return new AnswerResponse
            {
                Answer1 = registers.Max().ToString(),
                Answer2 = max.ToString()
            };
        }
        public AnswerResponse Day9(string input)
        {
            return new AnswerResponse
            {
                Answer1 = string.Empty,
                Answer2 = string.Empty
            };
        }
        public AnswerResponse Day10(string input)
        {
            return new AnswerResponse
            {
                Answer1 = string.Empty,
                Answer2 = string.Empty
            };
        }
        public AnswerResponse Day11(string input)
        {
            return new AnswerResponse
            {
                Answer1 = string.Empty,
                Answer2 = string.Empty
            };
        }
        public AnswerResponse Day12(string input)
        {
            return new AnswerResponse
            {
                Answer1 = string.Empty,
                Answer2 = string.Empty
            };
        }
        public AnswerResponse Day13(string input)
        {
            return new AnswerResponse
            {
                Answer1 = string.Empty,
                Answer2 = string.Empty
            };
        }
        public AnswerResponse Day14(string input)
        {
            return new AnswerResponse
            {
                Answer1 = string.Empty,
                Answer2 = string.Empty
            };
        }
        public AnswerResponse Day15(string input)
        {
            return new AnswerResponse
            {
                Answer1 = string.Empty,
                Answer2 = string.Empty
            };
        }
        public AnswerResponse Day16(string input)
        {
            return new AnswerResponse
            {
                Answer1 = string.Empty,
                Answer2 = string.Empty
            };
        }
        public AnswerResponse Day17(string input)
        {
            return new AnswerResponse
            {
                Answer1 = string.Empty,
                Answer2 = string.Empty
            };
        }
        public AnswerResponse Day18(string input)
        {
            return new AnswerResponse
            {
                Answer1 = string.Empty,
                Answer2 = string.Empty
            };
        }
        public AnswerResponse Day19(string input)
        {
            return new AnswerResponse
            {
                Answer1 = string.Empty,
                Answer2 = string.Empty
            };
        }
        public AnswerResponse Day20(string input)
        {
            return new AnswerResponse
            {
                Answer1 = string.Empty,
                Answer2 = string.Empty
            };
        }
        public AnswerResponse Day21(string input)
        {
            return new AnswerResponse
            {
                Answer1 = string.Empty,
                Answer2 = string.Empty
            };
        }
        public AnswerResponse Day22(string input)
        {
            return new AnswerResponse
            {
                Answer1 = string.Empty,
                Answer2 = string.Empty
            };
        }
        public AnswerResponse Day23(string input)
        {
            return new AnswerResponse
            {
                Answer1 = string.Empty,
                Answer2 = string.Empty
            };
        }
        public AnswerResponse Day24(string input)
        {
            return new AnswerResponse
            {
                Answer1 = string.Empty,
                Answer2 = string.Empty
            };
        }
        public AnswerResponse Day25(string input)
        {
            return new AnswerResponse
            {
                Answer1 = string.Empty,
                Answer2 = string.Empty
            };
        }

        private static bool IsAnagram(string word1, string word2)
        {
            if (word1.Length != word2.Length)
            {
                return false;
            }               

            foreach (char letter in word2)
            {
                int index = word1.IndexOf(letter);
                if (index >= 0)
                {
                    word1 = word1.Remove(letter, 1);
                }
                else
                {
                    return false;
                }                    
            }

            return string.IsNullOrEmpty(word1);
        }

        private int GetChildrensWeight(List<TreeNode> treeNodes, string parent)
        {
            var parentNode = treeNodes.Where(x => x.Name == parent).First();

            var weight = parentNode.Weight;

            if (parentNode.Children != null)
            {
                foreach (var child in parentNode.Children)
                {
                    weight += GetChildrensWeight(treeNodes, child);
                }
            }

            return weight;
        }

        private string GetIncorrectChild(List<TreeNode> treeNodes, string parent)
        {
            var parentNode = treeNodes.Where(x => x.Name == parent).First();  

            var weight = new int[parentNode.Children.Length];
            for (int i = 0; i < weight.Length; i++)
            {
                weight[i] = this.GetChildrensWeight(treeNodes, parentNode.Children[i]);                    
            }

            for (int i = 0; i < weight.Length; i++)
            {
                if(weight.Count(x => x == weight[i]) == 1)
                {
                    return GetIncorrectChild(treeNodes, parentNode.Children[i]);
                }
            }

            return parent;
        }

        private bool TestCondition(int[] registers, Condition condition)
        {
            var index = this.GetIndexOfRegister(condition.Register);
            switch (condition.Operator)
            {
                case "<":
                    return registers[index] < condition.Value;
                case ">":
                    return registers[index] > condition.Value;
                case "==":
                    return registers[index] == condition.Value;
                case "!=":
                    return registers[index] != condition.Value;
                case ">=":
                    return registers[index] >= condition.Value;
                case "<=":
                    return registers[index] <= condition.Value;
                default:
                    return false;
            }
        }

        private int GetIndexOfRegister(string register)
        {
            var index = 0;

            for (int i = 0; i < register.Length; i++)
            {
                if (i < register.Length - 1)
                {
                    index += ((char.ToUpper(register[i]) - 64) * 26) - 1; // If it's a and it's returning 0, multiplying by 26 isn't going to do anything
                }
                else
                {
                    index += char.ToUpper(register[i]) - 65;
                }
            }

            return index;
        }
    }    
}