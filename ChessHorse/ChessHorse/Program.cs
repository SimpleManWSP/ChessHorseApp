using System;

namespace ChessHorse
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Введите координаты изначального положения коня:");
            string coordinates = Console.ReadLine();
            Console.WriteLine("Введите координаты конечного положения коня:");
            coordinates += Console.ReadLine();

            int x1, y1, x2, y2;

            GetCoordinates(out x1, out y1, out x2, out y2, coordinates);

            Horse h = new Horse();
            Console.WriteLine("Минимальное число ходов для перехода с {0} на {1} равно: {2}", 
                               coordinates.Substring(0,2).ToUpper(), coordinates.Substring(2,2).ToUpper(),
                               h.HorseMove(x1, y1, x2, y2));
        }

        static void GetCoordinates(out int x1, out int y1, out int x2, out int y2, string strCoord)
        {
            strCoord = strCoord.ToUpper();
            char[] coord = strCoord.ToCharArray();
            x1 = coord[0] - 65;
            y1 = coord[1] - 49;
            x2 = coord[2] - 65;
            y2 = coord[3] - 49;
        }
    }

    class Horse
    {
        private const int Size = 8;
        readonly int[,] _battleField = new int[Size, Size];

        private int _k , _move;


        void FillField()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    _battleField[i, j] = -1;
                }
            }
        }

        void Try(int p, int q)
        {
            if (p >= 0 && p < Size && q >= 0 && q < Size && _battleField[p, q] < 0)
            {
                _battleField[p, q] = _k + 1;
                _move = 1;
            }
        }

        public int HorseMove(int x1, int y1, int x2, int y2)
        {
            int[] dx = {2, 2, -2, -2, 1, 1, -1, -1};
            int[] dy = {1, -1, 1, -1, 2, -2, 2, -2};
            FillField();
            _battleField[x1, y1] = 0;

            do
            {
                _move = 0;
                for (int i = Size-1; i >= 0; i--)
                {
                    for (int j = 0; j < Size; j++)
                    {
                        if (_battleField[i, j] == _k)
                            for (int f = 0; f < Size; f++)
                            {
                                Try(i + dx[f], j + dy[f]);
                            }
                    }
                }
                _k++;
            } while (_move == 1);
            return _battleField[x2, y2];
        }
    }
}
