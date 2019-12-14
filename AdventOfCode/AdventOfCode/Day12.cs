using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace AdventOfCode
{
    class Moon
    {
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public int ZPosition { get; set; }
        public int XVelocity { get; set; }
        public int YVelocity { get; set; }
        public int ZVelocity { get; set; }

        public Moon(int x, int y, int z)
        {
            XPosition = x;
            YPosition = y;
            ZPosition = z;
            XVelocity = 0;
            YVelocity = 0;
            ZVelocity = 0;
        }
    }
    

    class Day12
    {
        public static long Run()
        {

            List<Moon> moons = new List<Moon>()
            {
                new Moon(16, -8, 13),
                new Moon(4, 10, 10),
                new Moon(17, -5, 6),
                new Moon(13, -3, 0)
            };

            List<(int, int, int, int, int, int, int, int)> xHistory = new List<(int, int, int, int, int, int, int, int)>();
            List<(int, int, int, int, int, int, int, int)> yHistory = new List<(int, int, int, int, int, int, int, int)>();
            List<(int, int, int, int, int, int, int, int)> zHistory = new List<(int, int, int, int, int, int, int, int)>();

            long xPhase = -1;
            long yPhase = -1;
            long zPhase = -1;
            int stepCount = 0;

            while (xPhase < 0 || yPhase < 0 || zPhase < 0) 
            {
                for (int j = 0; j < 4; ++j)
                {
                    for (int k = j + 1; k < 4; ++k)
                    {
                        AdjustVelocity(moons[j], moons[k]);
                    }
                }

                foreach (var moon in moons)
                {
                    moon.XPosition += moon.XVelocity;
                    moon.YPosition += moon.YVelocity;
                    moon.ZPosition += moon.ZVelocity;
                }

                if (xPhase < 0)
                {
                    var xTrack = (moons[0].XPosition, moons[0].XVelocity, moons[1].XPosition, moons[1].XVelocity, moons[2].XPosition, moons[2].XVelocity, moons[3].XPosition, moons[3].XVelocity);
                    if (xHistory.Contains(xTrack))
                    {
                        if (xHistory.IndexOf(xTrack) != 0)
                        {
                            throw new ArgumentException(("Uh Oh!"));
                        }
                        xPhase = stepCount - xHistory.IndexOf(xTrack);
                    }
                    else
                    {
                        xHistory.Add(xTrack);
                    }
                }
                if (yPhase < 0)
                {
                    var YTrack = (moons[0].YPosition, moons[0].YVelocity, moons[1].YPosition, moons[1].YVelocity, moons[2].YPosition, moons[2].YVelocity, moons[3].YPosition, moons[3].YVelocity);
                    if (yHistory.Contains(YTrack))
                    {
                        if (yHistory.IndexOf(YTrack) != 0)
                        {
                            throw new ArgumentException(("Uh Oh!"));
                        }
                        yPhase = stepCount - yHistory.IndexOf(YTrack);
                    }
                    else
                    {
                        yHistory.Add(YTrack);
                    }
                }
                if (zPhase < 0)
                {
                    var ZTrack = (moons[0].ZPosition, moons[0].ZVelocity, moons[1].ZPosition, moons[1].ZVelocity, moons[2].ZPosition, moons[2].ZVelocity, moons[3].ZPosition, moons[3].ZVelocity);
                    if (zHistory.Contains(ZTrack))
                    {
                        if (zHistory.IndexOf(ZTrack) != 0)
                        {
                            throw new ArgumentException(("Uh Oh!"));
                        }
                        zPhase = stepCount - zHistory.IndexOf(ZTrack);
                    }
                    else
                    {
                        zHistory.Add(ZTrack);
                    }
                }
                ++stepCount;
            }

            long lcmOne = GetLCM(xPhase, yPhase);
            long lcmTwo = GetLCM(lcmOne, zPhase);

            return lcmTwo;
        }


        private static long GCD(long a, long b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);

            // Pull out remainders.
            for (; ; )
            {
                long remainder = a % b;
                if (remainder == 0) return b;
                a = b;
                b = remainder;
            };
        }



        static long GetLCM(long num1, long num2)

        {

            return (num1 * num2) / GCD(num1, num2);

        }


        private static void AdjustVelocity(Moon moonOne, Moon moonTwo)
        {
            if (moonOne.XPosition > moonTwo.XPosition)
            {
                moonOne.XVelocity -= 1;
                moonTwo.XVelocity += 1;
            }
            else if (moonOne.XPosition < moonTwo.XPosition)
            {
                moonOne.XVelocity += 1;
                moonTwo.XVelocity -= 1;
            }
            if (moonOne.YPosition > moonTwo.YPosition)
            {
                moonOne.YVelocity -= 1;
                moonTwo.YVelocity += 1;
            }
            else if (moonOne.YPosition < moonTwo.YPosition)
            {
                moonOne.YVelocity += 1;
                moonTwo.YVelocity -= 1;
            }
            if (moonOne.ZPosition > moonTwo.ZPosition)
            {
                moonOne.ZVelocity -= 1;
                moonTwo.ZVelocity += 1;
            }
            else if (moonOne.ZPosition < moonTwo.ZPosition)
            {
                moonOne.ZVelocity += 1;
                moonTwo.ZVelocity -= 1;
            }
        }
    }
}
