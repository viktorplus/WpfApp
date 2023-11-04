using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace WpfApp
{
    public class GameModel : INotifyPropertyChanged
    {
        private Tiles[,] _tile;
        private int[] _tileValues = new int[16];
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
            UpdateTileValues();
        }
        public Tiles[,] Tile
        {
            get { return _tile; }
            set
            {
                _tile = value;
                OnPropertyChanged(nameof(Tile));
                UpdateTileValues();

            }
        }

        public int[] TileValues
        {
            get { return _tileValues; }
            set
            {
                _tileValues = value;
                OnPropertyChanged(nameof(TileValues));

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

        public void UpdateTileValues()
        {
            // Обновляем одномерный массив на основе двухмерного массива
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    int index = row * 4 + col;
                    TileValues[index] = Tile[row, col].Num;
                }
            }

            // Уведомляем интерфейс пользователя о изменении данных в одномерном массиве
            OnPropertyChanged(nameof(TileValues));
        }

        public void GenerateNewTile()
        {
            Random random = new Random();

            // Генерируем два значения: первое для первой новой плитки и второе для второй
            int value1 = random.Next(10) == 0 ? 4 : 2; // Вероятность 10% для 4, 90% для 2
            int value2 = random.Next(10) == 0 ? 4 : 2; // Второе случайное значение

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

            // Если есть пустые ячейки, выбираем случайные и добавляем новые плитки
            if (emptyCells.Count >= 2)
            {
                int randomIndex1 = random.Next(emptyCells.Count);
                Tuple<int, int> randomCell1 = emptyCells[randomIndex1];
                emptyCells.RemoveAt(randomIndex1); // Убираем первую выбранную ячейку

                int randomIndex2 = random.Next(emptyCells.Count);
                Tuple<int, int> randomCell2 = emptyCells[randomIndex2];

                int row1 = randomCell1.Item1;
                int col1 = randomCell1.Item2;
                int row2 = randomCell2.Item1;
                int col2 = randomCell2.Item2;

                Tile[row1, col1] = new Tiles(value1, false); // Создаем первую новую плитку
                Tile[row2, col2] = new Tiles(value2, false); // Создаем вторую новую плитку

                OnPropertyChanged(nameof(Tile)); // Вызываем событие PropertyChanged
                UpdateTileValues();
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
                            if (Tile[row, targetCol] == null)
                            {
                                // Перемещаем ячейку в пустую ячейку слева
                                Tile[row, targetCol] = Tile[row, col];
                                Tile[row, col].Num = 0;
                                hasMoved = true;
                            }
                            else if (Tile[row, targetCol].Num == Tile[row, col].Num && !Tile[row, targetCol].Merged)
                            {
                                // Слияние двух ячеек с одинаковыми значениями
                                Tile[row, targetCol].Num *= 2;
                                Score += Tile[row, targetCol].Num;
                                Tile[row, targetCol].Merged = true;
                                Tile[row, col].Num = 0;
                                hasMoved = true;
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

                // Сброс флагов слияния после каждой строки
                for (int col = 0; col < 4; col++)
                {
                    if (Tile[row, col].Num != 0)
                    {
                        Tile[row, col].Merged = false;
                    }
                }
            }

            if (hasMoved)
            {
                GenerateNewTile();
            }
            OnPropertyChanged(nameof(Tile));
            OnPropertyChanged(nameof(Score));
            UpdateTileValues();

        }



        public void MoveRight()
        {
            bool hasMoved = false;
            for (int row = 0; row < 4; row++)
            {
                int targetCol = 3; // Начинаем с крайней правой ячейки в строке
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
                            Tile[row, col].Num = 0;
                            hasMoved = true;
                        }
                        else if (Tile[row, targetCol].Num == Tile[row, col].Num && !Tile[row, targetCol].Merged)
                        {
                            // Обе ячейки имеют одинаковые значения и не были объединены
                            // Удваиваем значение ячейки справа и удаляем текущую ячейку
                            Tile[row, targetCol].Num *= 2;
                            Tile[row, targetCol].Merged = true;
                            Tile[row, col].Num = 0;
                            Score += Tile[row, targetCol].Num; // Увеличиваем счет
                        }
                        else
                        {
                            // Ячейка справа имеет разное значение или уже объединена,
                            // перемещаем текущую ячейку вправо, но на одну позицию левее
                            targetCol--;
                            Tile[row, targetCol] = Tile[row, col];
                            if (col != targetCol)
                                Tile[row, col].Num = 0;
                            hasMoved = true;
                        }
                    }
                }
                // Сброс флагов слияния после каждой строки
                for (int col = 0; col < 4; col++)
                {
                    if (Tile[row, col].Num != 0)
                    {
                        Tile[row, col].Merged = false;
                    }
                }
            }
            if (hasMoved)
            {
                GenerateNewTile();
            }
            OnPropertyChanged(nameof(Tile));
            OnPropertyChanged(nameof(Score));
            UpdateTileValues();

        }


        public void MoveUp()
        {
            bool hasMoved = false;
            for (int col = 0; col < 4; col++)
            {
                int targetRow = 0; // Начинаем с верхней строки в столбце
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
                            Tile[row, col].Num = 0;
                            hasMoved = true;
                        }
                        else if (Tile[targetRow, col].Num == Tile[row, col].Num && !Tile[targetRow, col].Merged)
                        {
                            // Обе ячейки имеют одинаковые значения и не были объединены
                            // Удваиваем значение ячейки сверху и удаляем текущую ячейку
                            Tile[targetRow, col].Num *= 2;
                            Tile[targetRow, col].Merged = true;
                            Tile[row, col].Num = 0;
                            Score += Tile[targetRow, col].Num; // Увеличиваем счет
                        }
                        else
                        {
                            // Ячейка сверху имеет разное значение или уже объединена,
                            // перемещаем текущую ячейку вверх, но на одну позицию ниже
                            targetRow++;
                            Tile[targetRow, col] = Tile[row, col];
                            if (row != targetRow)
                                Tile[row, col].Num = 0;
                            hasMoved = true;
                        }
                    }
                }
                // Сброс флагов слияния после каждого столбца
                for (int row = 0; row < 4; row++)
                {
                    if (Tile[row, col].Num != 0)
                    {
                        Tile[row, col].Merged = false;
                    }
                }
            }
            if (hasMoved)
            {
                GenerateNewTile();
            }
            OnPropertyChanged(nameof(Tile));
            OnPropertyChanged(nameof(Score));
            UpdateTileValues();

        }


        public void MoveDown()
        {
            bool hasMoved = false;
            for (int col = 0; col < 4; col++)
            {
                int targetRow = 3; // Начинаем с нижней строки в столбце
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
                            Tile[row, col].Num = 0;
                            hasMoved = true;
                        }
                        else if (Tile[targetRow, col].Num == Tile[row, col].Num && !Tile[targetRow, col].Merged)
                        {
                            // Обе ячейки имеют одинаковые значения и не были объединены
                            // Удваиваем значение ячейки снизу и удаляем текущую ячейку
                            Tile[targetRow, col].Num *= 2;
                            Tile[targetRow, col].Merged = true;
                            Tile[row, col].Num = 0;
                            Score += Tile[targetRow, col].Num; // Увеличиваем счет
                        }
                        else
                        {
                            // Ячейка снизу имеет разное значение или уже объединена,
                            // перемещаем текущую ячейку вниз, но на одну позицию выше
                            targetRow--;
                            Tile[targetRow, col] = Tile[row, col];
                            if (row != targetRow)
                                Tile[row, col].Num = 0;
                            hasMoved = true;
                        }
                    }
                }
                // Сброс флагов слияния после каждого столбца
                for (int row = 3; row >= 0; row--)
                {
                    if (Tile[row, col].Num != 0)
                    {
                        Tile[row, col].Merged = false;
                    }
                }
            }
            if (hasMoved)
            {
                GenerateNewTile();
            }
            OnPropertyChanged(nameof(Tile));
            OnPropertyChanged(nameof(Score));
            UpdateTileValues();

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
