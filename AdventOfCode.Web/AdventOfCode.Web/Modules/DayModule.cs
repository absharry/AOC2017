namespace AdventOfCode.Web.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Nancy;
    using Nancy.ModelBinding;
    using AdventOfCode.Web.Requests;
    using AdventOfCode.Web.Responses;

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
                default:
                    break;
            }

            responseModel.Day = day;

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
                Answer1 = count1,
                Answer2 = count2
            };
        }

        private AnswerResponse Day2(string input)
        {
            var code = input.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Split('\t').Select(y => int.Parse(y)).ToArray()).ToList();

            return new AnswerResponse
            {
                Answer1 = code.Sum(x => x.Max() - x.Min()),
                Answer2 = code.Sum(array => array.Sum(item => array.Where(x => item != x && item % x == 0).Select(x => item / x).FirstOrDefault()))
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
                Answer1 = distance,
                Answer2 = array[coordinates[0], coordinates[1]]
            };
        }

        private AnswerResponse Day4 (string input)
        {
            var code = input.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var count1 = 0;
            var count2 = 0;

            foreach (var passphrase in code)
            {
                var containsSameWord = false;
                var containsSameLetters = false;
                var words = passphrase.Split(' ');
                foreach (var word in words)
                {
                    var containsSameWordList = words.Where(x => x == word).ToList();

                    var containsSameLettersList = words.Where(x => IsAnagram(x,word)).ToList();                    

                    if (containsSameWordList.Count > 1)
                    {
                        containsSameWord = true;
                    }

                    if(containsSameLettersList.Count > 1)
                    {
                        containsSameLetters = true;
                    }
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
                Answer1 = code.Length - count1,
                Answer2 = code.Length - count2
            };
        }

        private static bool IsAnagram(string s1, string s2)
        {
            if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2))
                return false;
            if (s1.Length != s2.Length)
                return false;

            foreach (char c in s2)
            {
                int ix = s1.IndexOf(c);
                if (ix >= 0)
                    s1 = s1.Remove(ix, 1);
                else
                    return false;
            }

            return string.IsNullOrEmpty(s1);
        }
    }    
}