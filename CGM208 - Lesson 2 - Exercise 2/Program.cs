using System;

namespace CGM208___Lesson_2___Exercise_2
{
    enum PatternType
    {
        Horizontal,
        Vertical
    }

    class Program
    {
        const int MAX_STAR = 10;
        const char STAR = '*';

        static readonly ushort[,] baseHorizontalFlagPattern = new ushort[MAX_STAR, 1];
        static readonly ushort[,] baseVerticalFlagPattern = new ushort[MAX_STAR, 1];

        static void Main(string[] args)
        {
            Initialize();
            PrintResult();
            Console.ReadLine();
        }

        static void Initialize()
        {
            InitializeBasePattern(baseHorizontalFlagPattern, MAX_STAR, PatternType.Horizontal);
            InitializeBasePattern(baseVerticalFlagPattern, MAX_STAR, PatternType.Vertical);
        }

        static void InitializeBasePattern(ushort[,] pattern, ushort maxCharacter, PatternType patternType)
        {
            if (maxCharacter <= 0)
                throw new Exception("Max character need to be greater than : 0");

            if (maxCharacter > sizeof(ushort) * 8)
                throw new Exception("Cannot initialize, Max character is greater than a size of ushort in bit");

            switch (patternType)
            {
                case PatternType.Horizontal:
                {
                    ushort buffer = 1;
                    for (int i = 0; i < maxCharacter; ++i) {
                        buffer |= (ushort)(1 << i);
                        pattern[i, 0] = buffer;
                    }
                    break;
                }

                case PatternType.Vertical:
                {
                    ushort buffer = 1;
                    for (int i = 0; i < maxCharacter; ++i) {
                        buffer |= (ushort) (1 << i);
                        int index = ((short) maxCharacter - 1) - i;
                        pattern[index, 0] = buffer;
                    }
                    break;
                }

                default:
                    break;
            }
        }

        static void PrintResult()
        {
            PrintPattern(STAR, baseHorizontalFlagPattern, MAX_STAR, false);
            Console.WriteLine();

            PrintPattern(STAR, baseHorizontalFlagPattern, MAX_STAR, true);
            Console.WriteLine();

            PrintPattern(STAR, baseVerticalFlagPattern, MAX_STAR, false);
            Console.WriteLine();

            PrintPattern(STAR, baseVerticalFlagPattern, MAX_STAR, true);
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
