
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.IO;

namespace pomogite
{
    public partial class Page4 : Page
    {
        private Leaderboard leaderboard = new Leaderboard();
        public Page4()
        {
            InitializeComponent();
            leaderboard.LoadLeaderboard();
            LoadLeaderboardToView();
        }
        private void LoadLeaderboardToView()
        {
            LeaderboardListView.ItemsSource = leaderboard.GetSortedEntries();
        }

        private void AddPlayer_Click(object sender, RoutedEventArgs e)
        {
            // Пример добавления игрока: можно использовать ввод с текстового поля
            leaderboard.AddEntry("Игрок3", 8);  // Добавьте необходимую логику для получения имени и количества уровней
            LoadLeaderboardToView();
        }
    }

    public class LeaderboardEntry
    {
        public string PlayerName { get; set; }
        public int LevelsCompleted { get; set; }
    }

    public class Leaderboard
    {
        private List<LeaderboardEntry> entries = new List<LeaderboardEntry>();
        private string filePath = "leaderboard.txt";

        public void LoadLeaderboard()
        {
            if (File.Exists(filePath))
            {
                var lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 2)
                    {
                        entries.Add(new LeaderboardEntry
                        {
                            PlayerName = parts[0],
                            LevelsCompleted = int.Parse(parts[1])
                        });
                    }
                }
            }
        }

        public void AddEntry(string playerName, int levelsCompleted)
        {
            entries.Add(new LeaderboardEntry { PlayerName = playerName, LevelsCompleted = levelsCompleted });
            SaveLeaderboard();
        }

        private void SaveLeaderboard()
        {
            var sortedEntries = entries.OrderByDescending(entry => entry.LevelsCompleted).ToList();
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var entry in sortedEntries)
                {
                    writer.WriteLine($"{entry.PlayerName},{entry.LevelsCompleted}");
                }
            }
        }

        public List<LeaderboardEntry> GetSortedEntries()
        {
            return entries.OrderByDescending(entry => entry.LevelsCompleted).ToList();
        }
    }
}