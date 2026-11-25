using System.Windows;
using System.Windows.Input;

namespace Clicker
{
    public partial class MainWindow : Window
    {
        private Player player;
        private Enemy enemy;

        public MainWindow()
        {
            InitializeComponent();

            player = new Player();
            enemy = new Enemy("Bird", "30", "12");

            UpdateUI();
        }

        private void UpdateUI()
        {
            tbEnemyName.Text = "Противник: " + enemy.Name;
            tbEnemyHp.Text = "HP: " + enemy.CurrentHitPoints;
            tbEnemyGold.Text = "Награда: " + enemy.GoldReward;

            tbLevel.Text = "Уровень: " + player.Lvl;
            tbGold.Text = "Золото: " + player.Gold;
            tbDamage.Text = "Урон: " + player.Damage;
        }

        private void Enemy_Click(object sender, MouseButtonEventArgs e)
        {
            UpdateUI();
        }

        private void Upgrade_Click(object sender, RoutedEventArgs e)
        {
            UpdateUI();
        }
    }
}