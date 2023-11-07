using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace WpfApp
{
    public class GameModel : INotifyPropertyChanged
    {
        private Tiles[,] _tile;
        private int _score;
        private bool _isGameOver;

        public GameModel()
        {
            _tile = new Tiles[4, 4]; // Инициализируем поле 4x4
            _score = 0;
            _isGameOver = false;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    _tile[i, j] = new Tiles(0, false); // Создаем экземпляры Tile для каждой ячейки
                }
            }

            GenerateNewTile(); // Вызываем метод для создания начальных плиток
            GenerateNewTile(); // Вызываем метод для создания начальных плиток
        }

        public Tiles Tile00 => _tile[0, 0];
        public Tiles Tile01 => _tile[0, 1];
        public Tiles Tile02 => _tile[0, 2];
        public Tiles Tile03 => _tile[0, 3];
        public Tiles Tile10 => _tile[1, 0];
        public Tiles Tile11 => _tile[1, 1];
        public Tiles Tile12 => _tile[1, 2];
        public Tiles Tile13 => _tile[1, 3];
        public Tiles Tile20 => _tile[2, 0];
        public Tiles Tile21 => _tile[2, 1];
        public Tiles Tile22 => _tile[2, 2];
        public Tiles Tile23 => _tile[2, 3];
        public Tiles Tile30 => _tile[3, 0];
        public Tiles Tile31 => _tile[3, 1];
        public Tiles Tile32 => _tile[3, 2];
        public Tiles Tile33 => _tile[3, 3];

        public Tiles[,] Tile
        {
            get { return _tile; }
            set
            {
                _tile = value;
                OnPropertyChanged(nameof(Tile));
            }
        }

        public int Score
        {
            get { return _score; }
            set
            {
                _score = value;
                OnPropertyChanged(nameof(Score));
            }
        }

        public bool IsGameOver
        {
            get { return _isGameOver; }
            set
            {
                _isGameOver = value;
                OnPropertyChanged(nameof(IsGameOver));
            }
        }
        public void GenerateNewTile()
        {
            Random random = new Random();
            int value = random.Next(10) == 0 ? 4 : 2; // Вероятность 10% для 4, 90% для 2

            List<Tuple<int, int>> emptyCells = new List<Tuple<int, int>>();

            // Находим все пустые ячейки на поле
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (Tile[row, col].Num == 0)
                    {
                        emptyCells.Add(new Tuple<int, int>(row, col));
                    }
                }
            }

            // Если есть пустые ячейки, выбираем случайную и добавляем новую плитку
            if (emptyCells.Count > 0)
            {
                int randomIndex = random.Next(emptyCells.Count);
                Tuple<int, int> randomCell = emptyCells[randomIndex];

                int row = randomCell.Item1;
                int col = randomCell.Item2;

                Tile[row, col] = new Tiles(value, false); // Создаем новую плитку
                OnPropertyChanged(nameof(Tile)); // Вызываем событие PropertyChanged
            }
        }


        public void MoveLeft()
        {
            bool hasMoved = false;

            for (int row = 0; row < 4; row++)
            {
                for (int col = 1; col < 4; col++)
                {
                    if (Tile[row, col].Num != 0)
                    {
                        int targetCol = col - 1;
                        while (targetCol >= 0)
                        {
                            if (Tile[row, targetCol].Num == 0)
                            {
                                // Перемещаем ячейку в пустую ячейку слева
                                Tile[row, targetCol] = Tile[row, targetCol + 1];
                                Tile[row, targetCol + 1] = new Tiles(0, false);
                                hasMoved = true;
                            }
                            else if (Tile[row, targetCol].Num == Tile[row, targetCol + 1].Num && !Tile[row, targetCol].Merged && !Tile[row, targetCol + 1].Merged)
                            {
                                // Слияние двух ячеек с одинаковыми значениями
                                Tile[row, targetCol].Num *= 2;
                                Score += Tile[row, targetCol].Num;
                                Tile[row, targetCol + 1] = new Tiles(0, false);
                                Tile[row, targetCol].Merged = true;
                                hasMoved = true;

                                // Удаление (очистка) старой плитки
                                Tile[row, col] = new Tiles(0, false);
                                break;
                            }
                            else
                            {
                                // Другая ячейка с разным значением, выходим из цикла
                                break;
                            }
                            targetCol--;
                        }
                    }
                }
            }

            if (hasMoved)
            {
                GenerateNewTile();
                GenerateNewTile();
            }

            OnPropertyChanged(nameof(Tile));
            OnPropertyChanged(nameof(Score));
        }



        public void MoveRight()
        {
            bool hasMoved = false;

            for (int row = 0; row < 4; row++)
            {
                int targetCol = 3;
                for (int col = 3; col >= 0; col--)
                {
                    if (Tile[row, col].Num != 0)
                    {
                        if (targetCol == col)
                        {
                            // Текущая ячейка уже находится в крайней правой позиции,
                            // нет необходимости перемещать ее
                        }
                        else if (Tile[row, targetCol].Num == 0)
                        {
                            // Ячейка справа пустая, перемещаем текущую ячейку вправо
                            Tile[row, targetCol] = Tile[row, col];
                            Tile[row, col] = new Tiles(0, false);
                            hasMoved = true;
                        }
                        else if (Tile[row, targetCol].Num == Tile[row, col].Num && !Tile[row, targetCol].Merged && !Tile[row, col].Merged)
                        {
                            // Обе ячейки имеют одинаковые значения и не были объединены
                            // Удваиваем значение ячейки справа и удаляем текущую ячейку
                            Tile[row, targetCol].Num *= 2;
                            Score += Tile[row, targetCol].Num;
                            Tile[row, targetCol].Merged = true;

                            // Удаляем старую плитку
                            Tile[row, col] = new Tiles(0, false);

                            hasMoved = true;
                        }
                        else
                        {
                            // Ячейка справа имеет разное значение или уже объединена,
                            // перемещаем текущую ячейку вправо, но на одну позицию левее
                            targetCol--;
                            Tile[row, targetCol] = Tile[row, col];
                            if (col != targetCol)
                                Tile[row, col] = new Tiles(0, false);
                            hasMoved = true;
                        }
                    }
                }
                // Сброс флагов слияния после каждой строки
                for (int col = 3; col >= 0; col--)
                {
                    Tile[row, col].Merged = false;
                }
            }
            if (hasMoved)
            {
                GenerateNewTile(); // Вызываем метод для создания начальных плиток
                GenerateNewTile(); // Вызываем метод для создания начальных плиток
            }
            OnPropertyChanged(nameof(Tile));
            OnPropertyChanged(nameof(Score));
        }


        public void MoveUp()
        {
            bool hasMoved = false;

            for (int col = 0; col < 4; col++)
            {
                int targetRow = 0;
                for (int row = 0; row < 4; row++)
                {
                    if (Tile[row, col].Num != 0)
                    {
                        if (targetRow == row)
                        {
                            // Текущая ячейка уже находится в верхней позиции,
                            // нет необходимости перемещать ее
                        }
                        else if (Tile[targetRow, col].Num == 0)
                        {
                            // Ячейка сверху пустая, перемещаем текущую ячейку вверх
                            Tile[targetRow, col] = Tile[row, col];
                            Tile[row, col] = new Tiles(0, false);
                            hasMoved = true;
                        }
                        else if (Tile[targetRow, col].Num == Tile[row, col].Num && !Tile[targetRow, col].Merged && !Tile[row, col].Merged)
                        {
                            // Обе ячейки имеют одинаковые значения и не были объединены
                            // Удваиваем значение ячейки сверху и удаляем текущую ячейку
                            Tile[targetRow, col].Num *= 2;
                            Score += Tile[targetRow, col].Num;
                            Tile[targetRow, col].Merged = true;

                            // Удаление (очистка) старой плитки
                            Tile[row, col] = new Tiles(0, false);
                            hasMoved = true;
                        }
                        else
                        {
                            // Ячейка сверху имеет разное значение или уже объединена,
                            // перемещаем текущую ячейку вверх, но на одну позицию ниже
                            targetRow++;
                            Tile[targetRow, col] = Tile[row, col];
                            if (row != targetRow)
                                Tile[row, col] = new Tiles(0, false);
                            hasMoved = true;
                        }
                    }
                }
                // Сброс флагов слияния после каждого столбца
                for (int row = 3; row >= 0; row--)
                {
                    Tile[row, col].Merged = false;
                }
            }

            if (hasMoved)
            {
                GenerateNewTile();
                GenerateNewTile();
            }

            OnPropertyChanged(nameof(Tile));
            OnPropertyChanged(nameof(Score));
        }


        public void MoveDown()
        {
            bool hasMoved = false;

            for (int col = 0; col < 4; col++)
            {
                int targetRow = 3;
                for (int row = 3; row >= 0; row--)
                {
                    if (Tile[row, col].Num != 0)
                    {
                        if (targetRow == row)
                        {
                            // Текущая ячейка уже находится в нижней позиции,
                            // нет необходимости перемещать ее
                        }
                        else if (Tile[targetRow, col].Num == 0)
                        {
                            // Ячейка снизу пустая, перемещаем текущую ячейку вниз
                            Tile[targetRow, col] = Tile[row, col];
                            Tile[row, col] = new Tiles(0, false);
                            hasMoved = true;
                        }
                        else if (Tile[targetRow, col].Num == Tile[row, col].Num && !Tile[targetRow, col].Merged && !Tile[row, col].Merged)
                        {
                            // Обе ячейки имеют одинаковые значения и не были объединены
                            // Удваиваем значение ячейки снизу и удаляем текущую ячейку
                            Tile[targetRow, col].Num *= 2;
                            Score += Tile[targetRow, col].Num;
                            Tile[targetRow, col].Merged = true;

                            // Удаление (очистка) старой плитки
                            Tile[row, col] = new Tiles(0, false);
                            hasMoved = true;
                        }
                        else
                        {
                            // Ячейка снизу имеет разное значение или уже объединена,
                            // перемещаем текущую ячейку вниз, но на одну позицию выше
                            targetRow--;
                            Tile[targetRow, col] = Tile[row, col];
                            if (row != targetRow)
                                Tile[row, col] = new Tiles(0, false);
                            hasMoved = true;
                        }
                    }
                }
                // Сброс флагов слияния после каждого столбца
                for (int row = 0; row < 4; row++)
                {
                    Tile[row, col].Merged = false;
                }
            }

            if (hasMoved)
            {
                GenerateNewTile();
                GenerateNewTile();
            }

            OnPropertyChanged(nameof(Tile));
            OnPropertyChanged(nameof(Score));
        }



        public void Reset()
        {
            // Логика для сброса игры, начальное расположение плиток и счета
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
