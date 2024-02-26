namespace zadacha_9_Shamilova_Hayala
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<char, int> valuesOfRowOnChessboard = new Dictionary<char, int>()
            {
                {'a', 1}, {'b', 2}, {'c', 3}, {'d', 4}, {'e', 5}, {'f', 6},{'g', 7}, {'h', 8},
            };
            List<string> nameOfFigures = new List<string> { "ладья", "конь", "слон", "ферзь", "король" };
            string error = "Вы ввели некорректные исходные данные";
            Console.WriteLine("Правило: Необходимо определить может ли белая фигура - x1y1 дойти до поля x3y3, не попав при этом под удар черной фигуры - x2y2.\n" +
                            "Данные необходимо вводить в формате: название белой фигуры, пробел, координаты x1y1, пробел, название черной фигуры, пробел, координаты x2y2, пробел, координаты конечной точки x3y3\n");

            Console.WriteLine("Введите исходные данные: ");
            string input = Console.ReadLine();
            string[] dividedInputIntoParts = input.Split(' ');

            int x1, x2, x3, y1, y2, y3, value1, value2, value3;
            string nameWhiteFigure, nameBlackFigure;

            if (dividedInputIntoParts.Length == 5)
            {
                nameWhiteFigure = dividedInputIntoParts[0];
                if (nameOfFigures.Contains(dividedInputIntoParts[2]))
                {
                    nameBlackFigure = dividedInputIntoParts[2];
                }
                else
                {
                    Console.WriteLine(error);
                    return;
                }
            }
            else
            {
                Console.WriteLine(error);
                return;
            }
            // Проверка на существование в словаре "valuesOfRowOnChessboard" введенных пользователем точек "х1", "х2", "x3". Если существует, то переменным х1, х2, x3 присваются значения ключей.
            if ((valuesOfRowOnChessboard.TryGetValue(dividedInputIntoParts[1][0], out value1)) && (valuesOfRowOnChessboard.TryGetValue(dividedInputIntoParts[3][0], out value2)) && (valuesOfRowOnChessboard.TryGetValue(dividedInputIntoParts[4][0], out value3)))
            {
                x1 = value1;
                x2 = value2;
                x3 = value3;
            }
            else
            {
                Console.WriteLine(error);
                return;
            }
            try
            {
                y1 = int.Parse(dividedInputIntoParts[1][1].ToString());
                y2 = int.Parse(dividedInputIntoParts[3][1].ToString());
                y3 = int.Parse(dividedInputIntoParts[4][1].ToString());
            }
            catch
            {
                Console.WriteLine(error);
                return;
            }

            bool canReach = CanWhitePieceReachTarget(nameWhiteFigure, x1, y1, x2, y2, x3, y3);
            GetResultOfGame(nameWhiteFigure, dividedInputIntoParts, canReach);
        }

        static public void GetResultOfGame(string nameWhiteFigure, string[] input, bool canReach)
        {
            if (canReach == true)
            {
                Console.WriteLine($"Результат: {nameWhiteFigure} дойдет до {input[4]}");
            }
            else
            {
                Console.WriteLine($"Результат: {nameWhiteFigure} НЕ дойдет до {input[4]}");
            }
        }

        static bool CanWhitePieceReachTarget(string nameWhiteFigure, int x1, int y1, int x2, int y2, int x3, int y3)
        {
            switch (nameWhiteFigure)
            {
                case "ладья":
                    return IsRookCanReachTarget(x1, y1, x2, y2, x3, y3);
                case "конь":
                    return IsKnightCanReachTarget(x1, y1, x2, y2, x3, y3);
                case "слон":
                    return IsBishopCanReachTarget(x1, y1, x2, y2, x3, y3);
                case "ферзь":
                    return IsQueenCanReachTarget(x1, y1, x2, y2, x3, y3);
                case "король":
                    return IsKingCanReachTarget(x1, y1, x2, y2, x3, y3);
                default:
                    return false;
            }
        }

        private static bool IsRookCanReachTarget(int x1, int y1, int x2, int y2, int x3, int y3)
        {
            bool canCome;
            if ((x1 == x3 && x1 == x2) && ((y2 < y1 && y2 < y3) || (y2 > y1 && y2 > y3)))
            {
                canCome = true;
            }
            else if ((y1 == y3 && y1 == y2) && ((x2 < x1 && x2 < x3) || (x2 > x1 && x2 > x3)))
            {
                canCome = true;
            }
            else if ((x1 == x3 && x1 != x2) || (y1 == y3 && y1 != y2))
            {
                canCome = true;
            }
            else
            {
                canCome = false;
            }
            return canCome;
        }

        private static bool IsKnightCanReachTarget(int x1, int y1, int x2, int y2, int x3, int y3)
        {
            bool canCome;
            if (((Math.Abs(x1 - x3) == 2 && Math.Abs(y1 - y3) == 1) || ((Math.Abs(x1 - x3) == 1 && Math.Abs(y1 - y3) == 2)))
                && ((x3 != x2 && y3 != y2) || (x3 != x2 && y3 == y2) || (x3 == x2 && y3 != y2)))
            {
                canCome = true;
            }
            else
            {
                canCome = false;
            }
            return canCome;
        }

        private static bool IsBishopCanReachTarget(int x1, int y1, int x2, int y2, int x3, int y3)
        {
            bool canCome;
            if (Math.Abs(x1 - x3) == Math.Abs(y1 - y3) && ((Math.Abs(x1 - x2) != Math.Abs(y1 - y2))
                    || (Math.Abs(x1 - x2) == Math.Abs(y1 - y2) && ((x2 > x1 && x2 > x3) || (x2 < x1 && x2 < x3)))))
            {
                canCome = true;
            }
            else
            {
                canCome = false;
            }
            return canCome;
        }

        private static bool IsQueenCanReachTarget(int x1, int y1, int x2, int y2, int x3, int y3)
        {
            bool canCome = false;
            // Проверка на возможность ферзя достигнуть цели по диагонали.
            if (Math.Abs(x1 - x3) == Math.Abs(y1 - y3))
            {
                if (Math.Abs(x1 - x2) == Math.Abs(y1 - y2) && ((x2 > x1 && x2 > x3) || (x2 < x1 && x2 < x3)))
                {
                    canCome = true;
                }
                else if (Math.Abs(x1 - x2) != Math.Abs(y1 - y2))
                {
                    canCome = true;
                }
            }
            // Проверка на возможность ферзя достигнуть цели по горизонтали и вертикали.
            else if ((x1 == x3 && x1 == x2) && ((y2 < y1 && y2 < y3) || (y2 > y1 && y2 > y3)))
            {
                canCome = true;
            }
            else if ((y1 == y3 && y1 == y2) && ((x2 < x1 && x2 < x3) || (x2 > x1 && x2 > x3)))
            {
                canCome = true;
            }
            else if ((x1 == x3 && x1 != x2) || (y1 == y3 && y1 != y2))
            {
                canCome = true;
            }
            else
            {
                canCome = false;
            }
            return canCome;
        }

        private static bool IsKingCanReachTarget(int x1, int y1, int x2, int y2, int x3, int y3)
        {
            bool canCome;
            if (((Math.Abs(x1 - x3) <= 1 && Math.Abs(y1 - y3) <= 1) || (x2 == x1 || y2 == y1)) || (x1 == x3 || y1 == y3))
            {
                canCome = true;
            }
            else
            {
                canCome = false;
            }
            return canCome;
        }
    }
}