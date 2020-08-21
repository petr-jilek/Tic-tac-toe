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
using Tic_tac_toe.Models.Enums;

namespace Tic_tac_toe.Views.Pages
{
    /// <summary>
    /// Interaction logic for WelcomePage.xaml
    /// </summary>
    public partial class WelcomePage : UserControl
    {
        public EventHandler StartGame_Event;

        private readonly Button OnePlayer_Button = new Button() { Content = "1 Player" };
        private readonly Button TwoPlayers_Button = new Button() { Content = "2 Players" };

        private readonly Button OnePlayer_Easy_Button = new Button() { Content = "Easy" };
        private readonly Button OnePlayer_Medium_Button = new Button() { Content = "Medium" };
        private readonly Button OnePlayer_Hard_Button = new Button() { Content = "Hard" };

        private readonly Button TwoPlayers_Normal_Button = new Button() { Content = "Normal" };
        private readonly Button TwoPlayers_Lan_Button = new Button() { Content = "Lan" };

        public WelcomePage() {
            InitializeComponent();
            OnePlayer_Button.Click += OnePlayer_Button_Click;
            TwoPlayers_Button.Click += TwoPlayers_Button_Click;
            OnePlayer_Easy_Button.Click += OnePlayer_Easy_Button_Click;
            OnePlayer_Medium_Button.Click += OnePlayer_Medium_Button_Click;
            OnePlayer_Hard_Button.Click += OnePlayer_Hard_Button_Click;
            TwoPlayers_Normal_Button.Click += TwoPlayers_Normal_Button_Click;
            TwoPlayers_Lan_Button.Click += TwoPlayers_Lan_Button_Click;

            FirstRender();
        }

        private void RenderButtons(params Button[] buttons) {
            this.Button_StackPanel.Children.Clear();
            foreach (Button but in buttons) {
                this.Button_StackPanel.Children.Add(but);
            }
        }

        public void FirstRender() {
            this.RenderButtons(OnePlayer_Button, TwoPlayers_Button);
        }

        private void OnePlayer_Button_Click(object sender, RoutedEventArgs e) {
            this.RenderButtons(OnePlayer_Easy_Button, OnePlayer_Medium_Button, OnePlayer_Hard_Button);
        }

        private void TwoPlayers_Button_Click(object sender, RoutedEventArgs e) {
            this.RenderButtons(TwoPlayers_Normal_Button, TwoPlayers_Lan_Button);
        }

        private void OnePlayer_Easy_Button_Click(object sender, RoutedEventArgs e) {
            this.StartGame_Event(GameType.ONE_PLAYER_EASY, e);
        }

        private void OnePlayer_Medium_Button_Click(object sender, RoutedEventArgs e) {
            this.StartGame_Event(GameType.ONE_PLAYER_MEDIUM, e);
        }

        private void OnePlayer_Hard_Button_Click(object sender, RoutedEventArgs e) {
            this.StartGame_Event(GameType.ONE_PLAYER_HARD, e);
        }

        private void TwoPlayers_Normal_Button_Click(object sender, RoutedEventArgs e) {
            this.StartGame_Event(GameType.TWO_PLAYERS_NORMAL, e);

        }

        private void TwoPlayers_Lan_Button_Click(object sender, RoutedEventArgs e) {
            this.StartGame_Event(GameType.TWO_PLAYERS_LAN, e);
        }

    }
}
