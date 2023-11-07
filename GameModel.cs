using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace WpfApp
{
    public class GameModel : INotifyPropertyChanged
    {
        //private Tiles[,] _tile;
        private int[,] _tile;
        private int _score;
        private bool _isGameOver;
        private bool _hasMoved;

        public GameModel()
        {
            _tile = new int [4, 4]; // Инициализируем поле 4x4
            _score = 0;
            _isGameOver = false;
            _hasMoved = false;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    _tile[i, j] = 0; // Создаем экземпляры Tile для каждой ячейки
                }
            }

            GenerateNewTiles(2); // Вызываем метод для создания начальных плиток

        }

        public int Tile00 => _tile[0, 0];
        public int Tile01 => _tile[0, 1];
        public int Tile02 => _tile[0, 2];
        public int Tile03 => _tile[0, 3];
        public int Tile10 => _tile[1, 0];
        public int Tile11 => _tile[1, 1];
        public int Tile12 => _tile[1, 2];
        public int Tile13 => _tile[1, 3];
        public int Tile20 => _tile[2, 0];
        public int Tile21 => _tile[2, 1];
        public int Tile22 => _tile[2, 2];
        public int Tile23 => _tile[2, 3];
        public int Tile30 => _tile[3, 0];
        public int Tile31 => _tile[3, 1];
        public int Tile32 => _tile[3, 2];
        public int Tile33 => _tile[3, 3];

        public int [,] Tile
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

        public bool HasMoved
        {
            get { return _hasMoved; }
            set
            {
                _hasMoved = value;
                OnPropertyChanged(nameof(HasMoved));
            }
        }

        public void GenerateNewTiles(int count)
        {
            Random random = new Random();

            int emptyCellCount = 0;
            // Подсчитываем количество пустых ячеек на поле
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (Tile[row, col] == 0)
                    {
                        emptyCellCount++;
                    }
                }
            }

            // Если количество пустых ячеек недостаточно для добавления count * 2 плиток, устанавливаем _isGameOver в true
            if (emptyCellCount < count * 2)
            {
                IsGameOver = true;
                return;
            }

            for (int i = 0; i < count; i++)
            {
                int value = random.Next(10) == 0 ? 4 : 2; // Вероятность 10% для 4, 90% для 2

                List<Tuple<int, int>> emptyCells = new List<Tuple<int, int>>();

                // Находим все пустые ячейки на поле
                for (int row = 0; row < 4; row++)
                {
                    for (int col = 0; col < 4; col++)
                    {
                        if (Tile[row, col] == 0)
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

                    Tile[row, col] = value; // Создаем новую плитку
                    OnPropertyChanged($"Tile{row}{col}"); // Вызываем событие PropertyChanged
                }
            }
        }



        public void MoveLeft()
        {
            for (int row = 0; row < 4; row++)
            {
                for (int col = 1; col < 4; col++)
                {
                    if (Tile[row, col] != 0)
                    {
                        int targetCol = col - 1;
                        while (targetCol >= 0)
                        {
                            if (Tile[row, targetCol] == 0)
                            {
                                // Перемещаем ячейку в пустую ячейку слева
                                Tile[row, targetCol] = Tile[row, col];
                                Tile[row, col] = 0;
                                OnPropertyChanged($"Tile{row}{targetCol}");
                                OnPropertyChanged($"Tile{row}{col}");
                                col = targetCol; // Перемещение завершено, обновляем текущую позицию col
                                HasMoved = true;
                            }
                            else
                            {
                                break;
                            }
                            targetCol--;
                        }
                    }
                }
            }
        }

        public void MergeLeft()
        {
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (Tile[row, col] != 0 && Tile[row, col] == Tile[row, col + 1])
                    {
                        // Объединяем соседние ячейки с одинаковыми значениями
                        Tile[row, col] *= 2;
                        Score += Tile[row, col];
                        Tile[row, col + 1] = 0;
                        OnPropertyChanged($"Tile{row}{col}");
                        OnPropertyChanged($"Tile{row}{col+1}");
                        OnPropertyChanged(nameof(Score));
                    }
                }
            }
        }

        public void MoveAndMergeLeft()
        {
            ShowDebugInfo("Before moving left:");
            MoveLeft();
            ShowDebugInfo("After moving left:");
            MergeLeft();
            ShowDebugInfo("After merging:");
            MoveLeft();
            ShowDebugInfo("After moving again:");

            if (HasMoved)
            {
                GenerateNewTiles(2);
            }
            HasMoved = false;
        }

        private void ShowDebugInfo(string message)
        {
            string debugInfo = $"{message}\n";
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    debugInfo += $"Tile[{row},{col}]: {Tile[row, col]}\n";
                }
            }
            MessageBox.Show(debugInfo, "Debug Info");
        }

        public void MoveRight()
        {
            for (int row = 0; row < 4; row++)
            {
                for (int col = 2; col >= 0; col--)
                {
                    if (Tile[row, col] != 0)
                    {
                        int targetCol = col + 1;
                        while (targetCol <= 3)
                        {
                            if (Tile[row, targetCol] == 0)
                            {
                                // Перемещаем ячейку в пустую ячейку справа
                                Tile[row, targetCol] = Tile[row, col];
                                Tile[row, col] = 0;
                                OnPropertyChanged($"Tile{row}{targetCol}");
                                OnPropertyChanged($"Tile{row}{col}");
                                col = targetCol; // Перемещение завершено, обновляем текущую позицию col
                                HasMoved = true;
                            }
                            else
                            {
                                break;
                            }
                            targetCol++;
                        }
                    }
                }
            }
        }

        public void MergeRight()
        {
            for (int row = 0; row < 4; row++)
            {
                for (int col = 3; col > 0; col--)
                {
                    if (Tile[row, col] != 0 && Tile[row, col] == Tile[row, col - 1])
                    {
                        // Объединяем соседние ячейки с одинаковыми значениями
                        Tile[row, col] *= 2;
                        Score += Tile[row, col];
                        Tile[row, col - 1] = 0;
                        OnPropertyChanged($"Tile{row}{col}");
                        OnPropertyChanged($"Tile{row}{col - 1}");
                    }
                }
            }
        }

        public void MoveAndMergeRight()
        {
            MoveRight();
            MergeRight();
            MoveRight();

            if (HasMoved)
            {
                GenerateNewTiles(2); // Добавляем 2 новую плитку после движения
            }
            HasMoved = false;

            OnPropertyChanged(nameof(Score));
            OnPropertyChanged(nameof(IsGameOver));
        }



        public void MoveUp()
        {
            //bool hasMoved = false;

            //for (int col = 0; col < 4; col++)
            //{
            //    int targetRow = 0;
            //    for (int row = 0; row < 4; row++)
            //    {
            //        if (Tile[row, col].Num != 0)
            //        {
            //            if (targetRow == row)
            //            {
            //                // Текущая ячейка уже находится в верхней позиции,
            //                // нет необходимости перемещать ее
            //            }
            //            else if (Tile[targetRow, col].Num == 0)
            //            {
            //                // Ячейка сверху пустая, перемещаем текущую ячейку вверх
            //                Tile[targetRow, col] = Tile[row, col];
            //                Tile[row, col] = new Tiles(0, false);
            //                hasMoved = true;
            //            }
            //            else if (Tile[targetRow, col].Num == Tile[row, col].Num && !Tile[targetRow, col].Merged && !Tile[row, col].Merged)
            //            {
            //                // Обе ячейки имеют одинаковые значения и не были объединены
            //                // Удваиваем значение ячейки сверху и удаляем текущую ячейку
            //                Tile[targetRow, col].Num *= 2;
            //                Score += Tile[targetRow, col].Num;
            //                Tile[targetRow, col].Merged = true;

            //                // Удаление (очистка) старой плитки
            //                Tile[row, col] = new Tiles(0, false);
            //                hasMoved = true;
            //            }
            //            else
            //            {
            //                // Ячейка сверху имеет разное значение или уже объединена,
            //                // перемещаем текущую ячейку вверх, но на одну позицию ниже
            //                targetRow++;
            //                Tile[targetRow, col] = Tile[row, col];
            //                if (row != targetRow)
            //                    Tile[row, col] = new Tiles(0, false);
            //                hasMoved = true;
            //            }
            //        }
            //    }
            //    // Сброс флагов слияния после каждого столбца
            //    for (int row = 3; row >= 0; row--)
            //    {
            //        Tile[row, col].Merged = false;
            //    }
            //}

            //if (hasMoved)
            //{
            //    GenerateNewTile();
            //    GenerateNewTile();
            //}

            //OnPropertyChanged(nameof(Tile));
            //OnPropertyChanged(nameof(Score));
        }


        public void MoveDown()
        {
            //bool hasMoved = false;

            //for (int col = 0; col < 4; col++)
            //{
            //    int targetRow = 3;
            //    for (int row = 3; row >= 0; row--)
            //    {
            //        if (Tile[row, col].Num != 0)
            //        {
            //            if (targetRow == row)
            //            {
            //                // Текущая ячейка уже находится в нижней позиции,
            //                // нет необходимости перемещать ее
            //            }
            //            else if (Tile[targetRow, col].Num == 0)
            //            {
            //                // Ячейка снизу пустая, перемещаем текущую ячейку вниз
            //                Tile[targetRow, col] = Tile[row, col];
            //                Tile[row, col] = new Tiles(0, false);
            //                hasMoved = true;
            //            }
            //            else if (Tile[targetRow, col].Num == Tile[row, col].Num && !Tile[targetRow, col].Merged && !Tile[row, col].Merged)
            //            {
            //                // Обе ячейки имеют одинаковые значения и не были объединены
            //                // Удваиваем значение ячейки снизу и удаляем текущую ячейку
            //                Tile[targetRow, col].Num *= 2;
            //                Score += Tile[targetRow, col].Num;
            //                Tile[targetRow, col].Merged = true;

            //                // Удаление (очистка) старой плитки
            //                Tile[row, col] = new Tiles(0, false);
            //                hasMoved = true;
            //            }
            //            else
            //            {
            //                // Ячейка снизу имеет разное значение или уже объединена,
            //                // перемещаем текущую ячейку вниз, но на одну позицию выше
            //                targetRow--;
            //                Tile[targetRow, col] = Tile[row, col];
            //                if (row != targetRow)
            //                    Tile[row, col] = new Tiles(0, false);
            //                hasMoved = true;
            //            }
            //        }
            //    }
            //    // Сброс флагов слияния после каждого столбца
            //    for (int row = 0; row < 4; row++)
            //    {
            //        Tile[row, col].Merged = false;
            //    }
            //}

            //if (hasMoved)
            //{
            //    GenerateNewTile();
            //    GenerateNewTile();
            //}

            //OnPropertyChanged(nameof(Tile));
            //OnPropertyChanged(nameof(Score));
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
