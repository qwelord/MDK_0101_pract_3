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
        public Classes.PersonInfo Player = new Classes.PersonInfo("Student", 100, 10, 1, 0, 0, 5);
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
        }
        private void AttackPlayer (object sender, System.EventArgs e)
        {
            if (Enemies.Count == 0) return;

            Random random = new Random();
            int enemyIndex = random.Next(0, Enemies.Count);
            Classes.PersonInfo enemy = Enemies[enemyIndex];

            int damageToPlayer = (int)Math.Max(1, enemy.Damage - Player.Armor);
            Player.Health -= damageToPlayer;

            UserInfoPlayer();

            if (Player.Health <= 0)
            {
                Player.Health = 0;
                dispatcherTimer.Stop();
                MessageBox.Show("Смэрть");
            }
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
