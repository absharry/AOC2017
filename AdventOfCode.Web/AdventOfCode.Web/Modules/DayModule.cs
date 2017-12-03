using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Nancy;

namespace AdventOfCode.Web.Modules
{
    public class DayModule : NancyModule
    {
        public DayModule() : base("/day/")
        {
            this.Get["/1/"] = x => this.GetDay1();
            this.Get["/2/"] = x => this.GetDay2();
            this.Get["/3/"] = x => this.GetDay3();
        }

        public dynamic GetDay1()
        {
            var code = "237369991482346124663395286354672985457326865748533412179778188397835279584149971999798512279429268727171755461418974558538246429986747532417846157526523238931351898548279549456694488433438982744782258279173323381571985454236569393975735715331438256795579514159946537868358735936832487422938678194757687698143224139243151222475131337135843793611742383267186158665726927967655583875485515512626142935357421852953775733748941926983377725386196187486131337458574829848723711355929684625223564489485597564768317432893836629255273452776232319265422533449549956244791565573727762687439221862632722277129613329167189874939414298584616496839223239197277563641853746193232543222813298195169345186499866147586559781523834595683496151581546829112745533347796213673814995849156321674379644323159259131925444961296821167483628812395391533572555624159939279125341335147234653572977345582135728994395631685618135563662689854691976843435785879952751266627645653981281891643823717528757341136747881518611439246877373935758151119185587921332175189332436522732144278613486716525897262879287772969529445511736924962777262394961547579248731343245241963914775991292177151554446695134653596633433171866618541957233463548142173235821168156636824233487983766612338498874251672993917446366865832618475491341253973267556113323245113845148121546526396995991171739837147479978645166417988918289287844384513974369397974378819848552153961651881528134624869454563488858625261356763562723261767873542683796675797124322382732437235544965647934514871672522777378931524994784845817584793564974285139867972185887185987353468488155283698464226415951583138352839943621294117262483559867661596299753986347244786339543174594266422815794658477629829383461829261994591318851587963554829459353892825847978971823347219468516784857348649693185172199398234123745415271222891161175788713733444497592853221743138324235934216658323717267715318744537689459113188549896737581637879552568829548365738314593851221113932919767844137362623398623853789938824592";
            var count1 = 0;
            var count2 = 0;
            var halfway = code.Length / 2;

            var codeArray = code.ToCharArray();

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

            var returnModel = new
            {
                Part1Answer = count1,
                Part2Answer = count2
            };

            return this.View["/days/one"].WithModel(returnModel);
        }

        public dynamic GetDay2()
        {
            var input = new List<int[]>
            {
                new int[] {6046,6349,208,276,4643,1085,1539,4986,7006,5374,252,4751,226,6757,7495,2923},
                new int[] {1432,1538,1761,1658,104,826,806,109,939,886,1497,280,1412,127,1651,156},
                new int[] {244,1048,133,232,226,1072,883,1045,1130,252,1038,1022,471,70,1222,957},
                new int[] {87,172,93,73,67,192,249,239,155,23,189,106,55,174,181,116},
                new int[] {5871,204,6466,6437,5716,232,1513,7079,6140,268,350,6264,6420,3904,272,5565},
                new int[] {1093,838,90,1447,1224,744,1551,59,328,1575,1544,1360,71,1583,75,370},
                new int[] {213,166,7601,6261,247,210,4809,6201,6690,6816,7776,2522,5618,580,2236,3598},
                new int[] {92,168,96,132,196,157,116,94,253,128,60,167,192,156,76,148},
                new int[] {187,111,141,143,45,132,140,402,134,227,342,276,449,148,170,348},
                new int[] {1894,1298,1531,1354,1801,974,85,93,1712,130,1705,110,314,107,449,350},
                new int[] {1662,1529,784,1704,1187,83,422,146,147,1869,1941,110,525,1293,158,1752},
                new int[] {162,1135,3278,1149,3546,3686,182,149,119,1755,3656,2126,244,3347,157,865},
                new int[] {2049,6396,4111,6702,251,669,1491,245,210,4314,6265,694,5131,228,6195,6090},
                new int[] {458,448,324,235,69,79,94,78,515,68,380,64,440,508,503,452},
                new int[] {198,216,5700,4212,2370,143,5140,190,4934,539,5054,3707,6121,5211,549,2790},
                new int[] {3021,3407,218,1043,449,214,1594,3244,3097,286,114,223,1214,3102,257,3345}
            };

            var count1 = 0;
            var count2 = 0;
            foreach (var array in input)
            {
                count1 += (array.Max() - array.Min());
            }

            foreach (var array in input)
            {
                foreach (var item in array)
                {
                    var divisibleItem = array.Where(x => item != x && item % x == 0).FirstOrDefault();

                    if (divisibleItem > 0)
                    {
                        count2 += (item / divisibleItem);
                        break;
                    }
                }
            }

            var returnModel = new
            {
                Part1Answer = count1,
                Part2Answer = count2
            };

            return this.View["/days/one"].WithModel(returnModel);
        }

        public dynamic GetDay3()
        {
            var input = 312051;
            var multiplier = 3;
            var ring = 0;

            while( ring == 0)
            {
                if(input < (multiplier * multiplier))
                {
                    ring = (multiplier + 1) / 2;
                    break;
                }

                multiplier += 2; 
            }

            var center = new int[]{ ring, ring };
            var inputLocation = new int[2];

            var bottomRight = multiplier * multiplier;
            var bottomLeft = bottomRight - (multiplier - 1);
            var topLeft = bottomLeft - (multiplier - 1);
            var topRight = topLeft - (multiplier - 1);

            if (bottomRight - input < multiplier - 1)
            {
                var difference = bottomRight - input;
                inputLocation[0] = multiplier - difference; // position along the row
                inputLocation[1] = 1; // height from the bottom
            }
            else if(input < bottomLeft)
            {
                var difference = input - topLeft;
                inputLocation[0] = 1; // position along the row
                inputLocation[1] = difference; // height from the top
            }
            else if(input > topRight)
            {
                var difference = input - topRight;
                inputLocation[0] = multiplier - difference; // position along the row
                inputLocation[1] = 1; // height from the top
            }
            else
            {
                var difference = topRight - input;
                inputLocation[0] = multiplier; // position along the row
                inputLocation[1] = multiplier - difference; // height from the top
            }

            var distance1 = Math.Abs(center[0] - inputLocation[0]) + Math.Abs(center[1] - inputLocation[1]);

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

                if (array[coordinates[0], coordinates[1]] > input)
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

            var returnModel = new
            {
                Part1Answer = distance1,
                Part2Answer = array[coordinates[0], coordinates[1]]
            };

            return this.View["/days/three"].WithModel(returnModel);
        }
    }
}