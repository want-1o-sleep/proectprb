using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace pomogite
{
    public partial class Page2 : Page
    {
        private const int GridSize = 10;
        private const int MineCount = 10;
        private Button[,] buttons = new Button[GridSize, GridSize];
        private bool[,] isMine = new bool[GridSize, GridSize];
        private int[,] mineCounts = new int[GridSize, GridSize];
        private int remainingMines;

        public Page2()
        {
            InitializeComponent();
            remainingMines = MineCount;
            MineCounter.Text = $"Осталось флажков: {remainingMines}";
            CreateButtons();
            PlaceMines();
            CalculateMineCounts();
        }
        private void CreateButtons()
        {
            for (int row = 0; row < GridSize; row++)
            {
                for (int col = 0; col < GridSize; col++)
                {
                    Button button = new Button
                    {
                        Background = Brushes.LightCoral,
                        Content = " ",
                        Tag = new Point(row, col)
                    };
                    button.Click += Button_Click;
                    button.MouseRightButtonDown += Button_RightClick; // Обработка правого клика
                    buttons[row, col] = button;
                    Minefield.Children.Add(button);
                }
            }
        }

        private void PlaceMines()
        {
            Random rand = new Random();
            int placedMines = 0;

            while (placedMines < MineCount)
            {
                int row = rand.Next(GridSize);
                int col = rand.Next(GridSize);

                if (!isMine[row, col])
                {
                    isMine[row, col] = true;
                    placedMines++;
                }
            }
        }

        private void CalculateMineCounts()
        {
            for (int row = 0; row < GridSize; row++)
            {
                for (int col = 0; col < GridSize; col++)
                {
                    if (isMine[row, col])
                    {
                        mineCounts[row, col] = -1; // Обозначаем мину
                        continue;
                    }

                    int count = 0;
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            int newRow = row + i;
                            int newCol = col + j;

                            if (newRow >= 0 && newRow < GridSize && newCol >= 0 && newCol < GridSize && isMine[newRow, newCol])
                            {
                                count++;
                            }
                        }
                    }
                    mineCounts[row, col] = count;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Point position = (Point)button.Tag;
            int row = (int)position.X;
            int col = (int)position.Y;

            if (isMine[row, col])
            {
                button.Background = Brushes.Magenta; // Минирование
                button.Content = "💣"; // Открытие мины
                MessageBox.Show("Вы попали на мину! Игра окончена.");
                NavigationService.Navigate(new Page3());
            }
            else
            {
                RevealCell(row, col);
            }
            
        }

        private void Button_RightClick(object sender, MouseButtonEventArgs e)
        {
            Button button = sender as Button;
            Point position = (Point)button.Tag;
            int row = (int)position.X;
            int col = (int)position.Y;

            if (button.Content.ToString() == "⚐")
            {
                button.Background = Brushes.LightCoral;
                button.Content = " "; // Снять флажок
                remainingMines++;
            }
            else if (remainingMines > 0)
            {
                button.Content = "⚐"; // Установить флажок
                remainingMines--;
                button.Background = Brushes.Magenta;
                
            }


            MineCounter.Text = $"Осталось мин: {remainingMines}";
        }

        private void RevealCell(int row, int col)
        {
            // Проверка на выход за пределы и нажатие на уже открытую кнопку
            if (row < 0 || row >= GridSize || col < 0 || col >= GridSize || buttons[row, col].Background == Brushes.Ivory)
                return; // Выход за пределы или уже открыт

            buttons[row, col].Background = Brushes.Ivory;
            buttons[row, col].IsEnabled = false;
            buttons[row, col].Content = mineCounts[row, col] > 0 ? mineCounts[row, col].ToString() : ""; // Показываем число или оставляем пустым

            if (mineCounts[row, col] == 0)
            {
                for (int i = -1; i <= 1; i++)
                {
                    for (int j = -1; j <= 1; j++)
                    {
                        RevealCell(row + i, col + j); // Рекурсивное открытие клеток
                    }
                }
            }
        }
    }
}