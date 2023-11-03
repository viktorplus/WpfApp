using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace WpfApp
{
    public class GameModel : INotifyPropertyChanged
    {
        private Tile[,] _tile;
        private int _score;
        private bool _isGameOver;

        public GameModel()
        {
            _tile = new Tile[4, 4]; // Инициализируем поле 4x4
            _score = 0;
            _isGameOver = false;

            // Здесь можно добавить логику начального расположения плиток
        }

        public Tile[,] Tiles
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
        public void GenerateNewTiles()
        {
            Random random = new Random();
            int value = random.Next(10) == 0 ? 4 : 2; // Вероятность 10% для 4, 90% для 2

            List<Tuple<int, int>> emptyCells = new List<Tuple<int, int>>();

            // Находим все пустые ячейки на поле
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 4; col++)
                {
                    if (Tiles[row, col] == null)
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

                Tiles[row, col] = new Tile(value, false); // Создаем новую плитку
                OnPropertyChanged(nameof(Tiles)); // Вызываем событие PropertyChanged
            }
        }


        public void MoveLeft()
        {
            bool hasMoved = false;

            for (int row = 0; row < 4; row++)
            {
                for (int col = 1; col < 4; col++)
                {
                    if (Tiles[row, col] != null)
                    {
                        int targetCol = col - 1;
                        while (targetCol >= 0)
                        {
                            if (Tiles[row, targetCol] == null)
                            {
                                // Перемещаем ячейку в пустую ячейку слева
                                Tiles[row, targetCol] = Tiles[row, col];
                                Tiles[row, col] = null;
                                hasMoved = true;
                            }
                            else if (Tiles[row, targetCol].Num == Tiles[row, col].Num && !Tiles[row, targetCol].Merged)
                            {
                                // Слияние двух ячеек с одинаковыми значениями
                                Tiles[row, targetCol].Num *= 2;
                                Score += Tiles[row, targetCol].Num;
                                Tiles[row, targetCol].Merged = true;
                                Tiles[row, col] = null;
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
                    if (Tiles[row, col] != null)
                    {
                        Tiles[row, col].Merged = false;
                    }
                }
            }

            if (hasMoved)
            {
                GenerateNewTiles();
            }
            OnPropertyChanged(nameof(Tiles));
            OnPropertyChanged(nameof(Score));
        }



        public void MoveRight()
        {
            bool hasMoved = false;
            for (int row = 0; row < 4; row++)
            {
                int targetCol = 3; // Начинаем с крайней правой ячейки в строке
                for (int col = 3; col >= 0; col--)
                {
                    if (Tiles[row, col] != null)
                    {
                        if (targetCol == col)
                        {
                            // Текущая ячейка уже находится в крайней правой позиции,
                            // нет необходимости перемещать ее
                        }
                        else if (Tiles[row, targetCol] == null)
                        {
                            // Ячейка справа пустая, перемещаем текущую ячейку вправо
                            Tiles[row, targetCol] = Tiles[row, col];
                            Tiles[row, col] = null;
                            hasMoved = true;
                        }
                        else if (Tiles[row, targetCol].Num == Tiles[row, col].Num && !Tiles[row, targetCol].Merged)
                        {
                            // Обе ячейки имеют одинаковые значения и не были объединены
                            // Удваиваем значение ячейки справа и удаляем текущую ячейку
                            Tiles[row, targetCol].Num *= 2;
                            Tiles[row, targetCol].Merged = true;
                            Tiles[row, col] = null;
                            Score += Tiles[row, targetCol].Num; // Увеличиваем счет
                        }
                        else
                        {
                            // Ячейка справа имеет разное значение или уже объединена,
                            // перемещаем текущую ячейку вправо, но на одну позицию левее
                            targetCol--;
                            Tiles[row, targetCol] = Tiles[row, col];
                            if (col != targetCol)
                                Tiles[row, col] = null;
                            hasMoved = true;
                        }
                    }
                }
                // Сброс флагов слияния после каждой строки
                for (int col = 0; col < 4; col++)
                {
                    if (Tiles[row, col] != null)
                    {
                        Tiles[row, col].Merged = false;
                    }
                }
            }
            if (hasMoved)
            {
                GenerateNewTiles();
            }
            OnPropertyChanged(nameof(Tiles));
            OnPropertyChanged(nameof(Score));
        }


        public void MoveUp()
        {
            bool hasMoved = false;
            for (int col = 0; col < 4; col++)
            {
                int targetRow = 0; // Начинаем с верхней строки в столбце
                for (int row = 0; row < 4; row++)
                {
                    if (Tiles[row, col] != null)
                    {
                        if (targetRow == row)
                        {
                            // Текущая ячейка уже находится в верхней позиции,
                            // нет необходимости перемещать ее
                        }
                        else if (Tiles[targetRow, col] == null)
                        {
                            // Ячейка сверху пустая, перемещаем текущую ячейку вверх
                            Tiles[targetRow, col] = Tiles[row, col];
                            Tiles[row, col] = null;
                            hasMoved = true;
                        }
                        else if (Tiles[targetRow, col].Num == Tiles[row, col].Num && !Tiles[targetRow, col].Merged)
                        {
                            // Обе ячейки имеют одинаковые значения и не были объединены
                            // Удваиваем значение ячейки сверху и удаляем текущую ячейку
                            Tiles[targetRow, col].Num *= 2;
                            Tiles[targetRow, col].Merged = true;
                            Tiles[row, col] = null;
                            Score += Tiles[targetRow, col].Num; // Увеличиваем счет
                        }
                        else
                        {
                            // Ячейка сверху имеет разное значение или уже объединена,
                            // перемещаем текущую ячейку вверх, но на одну позицию ниже
                            targetRow++;
                            Tiles[targetRow, col] = Tiles[row, col];
                            if (row != targetRow)
                                Tiles[row, col] = null;
                            hasMoved = true;
                        }
                    }
                }
                // Сброс флагов слияния после каждого столбца
                for (int row = 0; row < 4; row++)
                {
                    if (Tiles[row, col] != null)
                    {
                        Tiles[row, col].Merged = false;
                    }
                }
            }
            if (hasMoved)
            {
                GenerateNewTiles();
            }
            OnPropertyChanged(nameof(Tiles));
            OnPropertyChanged(nameof(Score));
        }


        public void MoveDown()
        {
            bool hasMoved = false;
            for (int col = 0; col < 4; col++)
            {
                int targetRow = 3; // Начинаем с нижней строки в столбце
                for (int row = 3; row >= 0; row--)
                {
                    if (Tiles[row, col] != null)
                    {
                        if (targetRow == row)
                        {
                            // Текущая ячейка уже находится в нижней позиции,
                            // нет необходимости перемещать ее
                        }
                        else if (Tiles[targetRow, col] == null)
                        {
                            // Ячейка снизу пустая, перемещаем текущую ячейку вниз
                            Tiles[targetRow, col] = Tiles[row, col];
                            Tiles[row, col] = null;
                            hasMoved = true;
                        }
                        else if (Tiles[targetRow, col].Num == Tiles[row, col].Num && !Tiles[targetRow, col].Merged)
                        {
                            // Обе ячейки имеют одинаковые значения и не были объединены
                            // Удваиваем значение ячейки снизу и удаляем текущую ячейку
                            Tiles[targetRow, col].Num *= 2;
                            Tiles[targetRow, col].Merged = true;
                            Tiles[row, col] = null;
                            Score += Tiles[targetRow, col].Num; // Увеличиваем счет
                        }
                        else
                        {
                            // Ячейка снизу имеет разное значение или уже объединена,
                            // перемещаем текущую ячейку вниз, но на одну позицию выше
                            targetRow--;
                            Tiles[targetRow, col] = Tiles[row, col];
                            if (row != targetRow)
                                Tiles[row, col] = null;
                            hasMoved = true;
                        }
                    }
                }
                // Сброс флагов слияния после каждого столбца
                for (int row = 3; row >= 0; row--)
                {
                    if (Tiles[row, col] != null)
                    {
                        Tiles[row, col].Merged = false;
                    }
                }
            }
            if (hasMoved)
            {
                GenerateNewTiles();
            }
            OnPropertyChanged(nameof(Tiles));
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
