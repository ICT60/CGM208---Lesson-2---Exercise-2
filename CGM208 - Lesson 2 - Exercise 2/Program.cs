using System;

namespace CGM208___Lesson_2___Exercise_2
{
    enum PatternType
    {
        TriangleHorizontal,
        TriangleVertical,
        Diamond
    }

    class Program
    {
        const int MAX_TRIANGLE_STAR = 10;
        const int MAX_DIAMOND_STAR = 9;

        const char STAR = '*';

        static readonly ushort[,] baseHorizontalFlagPattern = new ushort[MAX_TRIANGLE_STAR, 1];
        static readonly ushort[,] baseVerticalFlagPattern = new ushort[MAX_TRIANGLE_STAR, 1];
        static readonly ushort[,] baseDiamondFlagPattern = new ushort[MAX_DIAMOND_STAR, 1];

        static void Main(string[] args)
        {
            Initialize();
            PrintResult();
            Console.ReadLine();
        }

        static void Initialize()
        {
            InitializeBasePattern(baseHorizontalFlagPattern, MAX_TRIANGLE_STAR, PatternType.TriangleHorizontal);
            InitializeBasePattern(baseVerticalFlagPattern, MAX_TRIANGLE_STAR, PatternType.TriangleVertical);
            InitializeBasePattern(baseDiamondFlagPattern, MAX_DIAMOND_STAR, PatternType.Diamond);
        }

        static void InitializeBasePattern(ushort[,] pattern, ushort maxCharacter, PatternType patternType)
        {
            if (maxCharacter <= 0)
                throw new Exception("Max character need to be greater than : 0");

            if (maxCharacter > sizeof(ushort) * 8)
                throw new Exception("Cannot initialize, Max character is greater than a size of ushort in bit");

            switch (patternType)
            {
                case PatternType.TriangleHorizontal:
                {
                    ushort buffer = 1;
                    for (int i = 0; i < maxCharacter; ++i) {
                        buffer |= (ushort)(1 << i);
                        pattern[i, 0] = buffer;
                    }
                    break;
                }

                case PatternType.TriangleVertical:
                {
                    ushort buffer = 1;
                    for (int i = 0; i < maxCharacter; ++i) {
                        buffer |= (ushort) (1 << i);
                        int index = ((short) maxCharacter - 1) - i;
                        pattern[index, 0] = buffer;
                    }
                    break;
                }

                case PatternType.Diamond:
                {
                    if ((maxCharacter % 2) == 0)
                        throw new Exception("Max character need to be and odd number..");

                    int midpoint = (int)(maxCharacter / 2);
                    int midpointFlag = (1 << midpoint);
                    int buffer = midpointFlag;
                    int bufferLeft = 0;
                    int bufferRight = 0;

                    for (int i = 0; i < maxCharacter; ++i) {
                        if (i < (midpoint + 1)) {
                            bufferLeft = (midpointFlag << i);
                            bufferRight = (midpointFlag >> i);
                            buffer |= (bufferLeft | bufferRight);
                            pattern[i, 0] = (ushort) buffer;
                        }
                        else {
                            int shiftAmount = (maxCharacter - i);

                            bufferLeft = (midpointFlag << shiftAmount);
                            bufferRight = (midpointFlag >> shiftAmount);

                            buffer &= ~bufferLeft;
                            buffer &= ~bufferRight;

                            pattern[i, 0] = (ushort) buffer;
                            shiftAmount -= 1;
                        }
                    }

                    break;
                }

                default:
                    break;
            }
        }

        static void PrintResult()
        {
            PrintPattern(STAR, baseHorizontalFlagPattern, MAX_TRIANGLE_STAR, false);
            Console.WriteLine();

            PrintPattern(STAR, baseHorizontalFlagPattern, MAX_TRIANGLE_STAR, true);
            Console.WriteLine();

            PrintPattern(STAR, baseVerticalFlagPattern, MAX_TRIANGLE_STAR, false);
            Console.WriteLine();

            PrintPattern(STAR, baseVerticalFlagPattern, MAX_TRIANGLE_STAR, true);
            Console.WriteLine();

            PrintPattern(STAR, baseDiamondFlagPattern, MAX_DIAMOND_STAR, false);
            Console.WriteLine();
        }

        static void PrintPattern(char character, ushort[,] pattern, ushort maxCharacter, bool isFlip)
        {
            if (maxCharacter <= 0)
                throw new Exception("Max character need to be greater than : 0");

            ushort testFlag = 0;

            for (int i = 0; i < maxCharacter; ++i) {
                for (int j = (maxCharacter - 1); j >= 0; --j) {

                    testFlag = (ushort)(1 << j);
                    bool isShouldPrint = (testFlag & pattern[i, 0]) == testFlag;

                    if (isShouldPrint) {
                        Console.Write(character);
                    }
                    else {
                        if (!isFlip)
                            Console.Write(' ');
                    }
                }

                Console.WriteLine();
            }
        }
    }
}
