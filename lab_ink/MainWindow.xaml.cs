using System.Windows;
using System.Windows.Input;

namespace Clicker
{
    public partial class MainWindow : Window
    {
        private Player player;
        private Enemy enemy;
        private int enemyNumber = 1;

        public MainWindow()
        {
            InitializeComponent();

            player = new Player();
            SpawnNewEnemy();
            UpdateUI();
        }

        private void SpawnNewEnemy()
        {
            int hp = 20 + enemyNumber * 15;
            int gold = 8 + enemyNumber * 5;

            enemy = new Enemy("Bird " + enemyNumber, hp.ToString(), gold.ToString());
            enemyNumber++;
        }

        private void UpdateUI()
        {
            tbEnemyName.Text = enemy.Name;
            tbEnemyHp.Text = "HP: " + enemy.CurrentHitPoints;
            tbEnemyGold.Text = "Награда: " + enemy.GoldReward + " золота";

            tbLevel.Text = "Уровень: " + player.Lvl;
            tbGold.Text = "Золото: " + player.Gold;
            tbDamage.Text = "Урон: " + player.Damage;
            tbUpgradeCost.Text = "Стоимость улучшения: " + player.UpgradeCost;
        }

        private void Enemy_Click(object sender, RoutedEventArgs e)
        {
            BigNumber reward;
            bool isDead = enemy.TakeDamage(player.DealDamage(), out reward);

            if (isDead)
            {
                player.AddGold(reward);
                SpawnNewEnemy();
            }

            UpdateUI();
        }

        private void Upgrade_Click(object sender, RoutedEventArgs e)
        {
            if (player.TryUpgrade())
            {
            }
            else
            {
                MessageBox.Show("Недостаточно золота!", "Ошибка");
            }

            UpdateUI();
        }
    }
}