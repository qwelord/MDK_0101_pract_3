using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ClickerGameApplication
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        DispatcherTimer autodmg = new DispatcherTimer();
        public Classes.PersonInfo Player = new Classes.PersonInfo("Student", 100, 10, 1, 0, 0, 5);
        public Classes.PersonInfo Enemy;
        public MainWindow()
        {
            InitializeComponent();
            UserInfoPlayer();
            Enemies.Add(new Classes.PersonInfo("Ощепков", 100, 20, 1, 15, 5, 20));
            Enemies.Add(new Classes.PersonInfo("Куртагина", 20, 5, 1, 5, 2, 5));
            Enemies.Add(new Classes.PersonInfo("Ситчихин", 50, 3, 1, 10, 10, 15));
            dispatcherTimer.Tick += AttackPlayer;
            dispatcherTimer.Interval = new System.TimeSpan(0, 0, 10);
            dispatcherTimer.Start();
            autodmg.Tick += AutoAttackEnemy;
            autodmg.Interval = new System.TimeSpan(0, 0, 5);
            autodmg.Start();
            SelectEnemy();
        }
        private void CheckGameOver()
        {
            if (Player.Health <= 0)
            {
                dispatcherTimer.Stop();
                autodmg.Stop();
                MessageBox.Show("Game Over!");
            }
        }
        private void UpdateEnemyImage()
        {
            if (Enemy.Name == "Ощепков") emptyImage.Source = new BitmapImage(new Uri("Image/monster1.png", UriKind.Relative));
            if (Enemy.Name == "Куртагина") emptyImage.Source = new BitmapImage(new Uri("Image/monster2.png", UriKind.Relative));
            if (Enemy.Name == "Ситчихин") emptyImage.Source = new BitmapImage(new Uri("Image/monster3.png", UriKind.Relative));
        }
        private void AutoAttackEnemy(object sender, System.EventArgs e)
        {
            if (Enemy == null) return;

            Enemy.Health -= Convert.ToInt32(Player.Damage * 100f / (100f - Enemy.Armor));

            if (Enemy.Health <= 0)
            {
                Player.Exp += Enemy.Exp;
                Player.Money += Enemy.Money;
                UserInfoPlayer();
                SelectEnemy();
            }
            else
            {
                emptyHealth.Content = "Жизненные показатели: " + Enemy.Health;
                emptyArmor.Content = "Броня: " + Enemy.Armor;
            }
        }
        public void SelectEnemy()
        {
            int Id = new Random().Next(0, Enemies.Count);
            Enemy = new Classes.PersonInfo(
            Enemies[Id].Name,
            Enemies[Id].Health,
            Enemies[Id].Armor,
            Enemies[Id].Level,
            Enemies[Id].Exp,
            Enemies[Id].Money,
            Enemies[Id].Damage);
            UpdateEnemyImage();
        }
        private void AttackPlayer (object sender, System.EventArgs e)
        {
            Player.Health -= Convert.ToInt32(Enemy.Damage * 100f / (100f - Player.Armor));
            UserInfoPlayer();
            CheckGameOver();
        }
        private void AttackEnemy(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Enemy.Health -= Convert.ToInt32(Player.Damage * 100f / (100f -  Enemy.Armor));
            Random random = new Random();
            if (random.Next(0, 100) < 20)
            {
                Player.Health -= Convert.ToInt32(Enemy.Damage * 100f / (100f - Player.Armor));
                MessageBox.Show("Сработала контатака!");
                UserInfoPlayer();
                CheckGameOver();
            }
            if (Enemy.Health <= 0)
            {
                Player.Exp += Enemy.Exp;
                Player.Money += Enemy.Money;
                UserInfoPlayer();
                SelectEnemy();
            }
            else
            {
                emptyHealth.Content = "Жизненные показатели: " + Enemy.Health;
                emptyArmor.Content = "Броня: " + Enemy.Armor;
            }
            CheckGameOver();
        }
        public void UserInfoPlayer()
        {
            if (Player.Exp > 100 * Player.Level)
            {
                Player.Level++;
                Player.Exp = 0;
                Player.Health += 100;
                Player.Damage++;
                Player.Armor++;
            }
            playerHealth.Content = "Жизненные показатели: " + Player.Health;
            playerArmor.Content = "Броня: " + Player.Armor;
            playerLevel.Content = "Уровень: " + Player.Level;
            playerExp.Content = "Опыт: " + Player.Exp;
            playerMoney.Content = "Монеты: " + Player.Money;
        }
        public List<Classes.PersonInfo> Enemies = new List<Classes.PersonInfo>();
    }
}
