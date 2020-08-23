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
using Tic_tac_toe.Models;
using Tic_tac_toe.Models.Enums;

namespace Tic_tac_toe.Views.Pages
{
    /// <summary>
    /// Interaction logic for WelcomePage.xaml
    /// </summary>
    public partial class WelcomePage : UserControl
    {
        private GameData gameData;

        public EventHandler StartGame_Event;
        //public EventHandler LanGame_Event;

        private readonly Button OnePlayer_Button = new Button() {
            Name = "one", Content = "1 Player",
            Padding = new Thickness(30, 8, 30, 8),
            Background = new SolidColorBrush(Color.FromRgb(20, 189, 172)),
            Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
            FontWeight = FontWeights.Bold,
            BorderThickness = new Thickness(0.2, 0.2, 0.2, 0.2), };
        private readonly Button TwoPlayers_Button = new Button() {
            Name = "two", Content = "2 Players",
            Padding = new Thickness(30, 8, 30, 8),
            Background = new SolidColorBrush(Color.FromRgb(20, 189, 172)),
            Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
            FontWeight = FontWeights.Bold,
            BorderThickness = new Thickness(0.2, 0.2, 0.2, 0.2), };

        private readonly Button OnePlayer_Easy_Button = new Button() {
            Name = "easy", Content = "Easy",
            Padding = new Thickness(30, 8, 30, 8),
            Background = new SolidColorBrush(Color.FromRgb(20, 189, 172)),
            Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
            FontWeight = FontWeights.Bold,
            BorderThickness = new Thickness(0.2, 0.2, 0.2, 0.2), };
        private readonly Button OnePlayer_Medium_Button = new Button() {
            Name = "medium", Content = "Medium",
            Padding = new Thickness(30, 8, 30, 8),
            Background = new SolidColorBrush(Color.FromRgb(20, 189, 172)),
            Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
            FontWeight = FontWeights.Bold,
            BorderThickness = new Thickness(0.2, 0.2, 0.2, 0.2), };
        private readonly Button OnePlayer_Hard_Button = new Button() {
            Name = "hard", Content = "Hard",
            Padding = new Thickness(30, 8, 30, 8),
            Background = new SolidColorBrush(Color.FromRgb(20, 189, 172)),
            Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
            FontWeight = FontWeights.Bold,
            BorderThickness = new Thickness(0.2, 0.2, 0.2, 0.2), };

        private readonly Button TwoPlayers_Normal_Button = new Button() {
            Name = "normal", Content = "Normal",
            Padding = new Thickness(30, 8, 30, 8),
            Background = new SolidColorBrush(Color.FromRgb(20, 189, 172)),
            Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
            FontWeight = FontWeights.Bold,
            BorderThickness = new Thickness(0.2, 0.2, 0.2, 0.2), };
        private readonly Button TwoPlayers_Lan_Button = new Button() {
            Name = "lan", Content = "Lan",
            Padding = new Thickness(30, 8, 30, 8),
            Background = new SolidColorBrush(Color.FromRgb(20, 189, 172)),
            Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
            FontWeight = FontWeights.Bold,
            BorderThickness = new Thickness(0.2, 0.2, 0.2, 0.2), };

        private readonly Button SizeSmall_Button = new Button() {
            Name = "ssmall", Content = "10 x 10",
            Padding = new Thickness(30, 8, 30, 8),
            Background = new SolidColorBrush(Color.FromRgb(20, 189, 172)),
            Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
            FontWeight = FontWeights.Bold,
            BorderThickness = new Thickness(0.2, 0.2, 0.2, 0.2), };
        private readonly Button SizeMedium_Button = new Button() {
            Name = "smedium", Content = "20 x 20",
            Padding = new Thickness(30, 8, 30, 8),
            Background = new SolidColorBrush(Color.FromRgb(20, 189, 172)),
            Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
            FontWeight = FontWeights.Bold,
            BorderThickness = new Thickness(0.2, 0.2, 0.2, 0.2), };
        private readonly Button SizeBig_Button = new Button() {
            Name = "sbig", Content = "30 x 30",
            Padding = new Thickness(30, 8, 30, 8),
            Background = new SolidColorBrush(Color.FromRgb(20, 189, 172)),
            Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
            FontWeight = FontWeights.Bold,
            BorderThickness = new Thickness(0.2, 0.2, 0.2, 0.2), };
        private readonly Button SizeHuge_Button = new Button() {
            Name = "shuge", Content = "50 x 50",
            Padding = new Thickness(30, 8, 30, 8),
            Background = new SolidColorBrush(Color.FromRgb(20, 189, 172)),
            Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
            FontWeight = FontWeights.Bold,
            BorderThickness = new Thickness(0.2, 0.2, 0.2, 0.2), };

        private readonly Button Circle_Button = new Button() {
            Name = "circle", Content = "O",
            Padding = new Thickness(30, 8, 30, 8),
            Background = new SolidColorBrush(Color.FromRgb(20, 189, 172)),
            Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
            FontWeight = FontWeights.Bold,
            BorderThickness = new Thickness(0.2, 0.2, 0.2, 0.2), };
        private readonly Button Cross_Button = new Button() {
            Name = "cross", Content = "X",
            Padding = new Thickness(30, 8, 30, 8),
            Background = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
            Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
            FontWeight = FontWeights.Bold,
            BorderThickness = new Thickness(0.2, 0.2, 0.2, 0.2), };

        private readonly Button MainMenu_Button = new Button() {
            Name = "mainmenu", Content = "Main menu",
            Padding = new Thickness(30, 8, 30, 8),
            Background = new SolidColorBrush(Color.FromRgb(0, 0, 0)),
            Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255)),
            FontWeight = FontWeights.Bold,
            BorderThickness = new Thickness(0.2, 0.2, 0.2, 0.2), };

        public WelcomePage() {
            InitializeComponent();
            OnePlayer_Button.Click += Selection_Button_Click;
            TwoPlayers_Button.Click += Selection_Button_Click;
            OnePlayer_Easy_Button.Click += Selection_Button_Click;
            OnePlayer_Medium_Button.Click += Selection_Button_Click;
            OnePlayer_Hard_Button.Click += Selection_Button_Click;
            TwoPlayers_Normal_Button.Click += Selection_Button_Click;
            TwoPlayers_Lan_Button.Click += Selection_Button_Click;
            SizeSmall_Button.Click += Selection_Button_Click;
            SizeMedium_Button.Click += Selection_Button_Click;
            SizeBig_Button.Click += Selection_Button_Click;
            SizeHuge_Button.Click += Selection_Button_Click;
            Cross_Button.Click += Selection_Button_Click;
            Circle_Button.Click += Selection_Button_Click;

            MainMenu_Button.Click += MainMenu_Button_Click;

            FirstRender();
        }

        private void RenderButtons(bool mainmenu, params Button[] buttons) {
            this.Button_StackPanel.Children.Clear();
            foreach (Button but in buttons) {
                this.Button_StackPanel.Children.Add(but);
            }
            if (mainmenu) {
                this.Button_StackPanel.Children.Add(new StackPanel() { Height = 10 });
                this.Button_StackPanel.Children.Add(MainMenu_Button);
            }
        }

        public void FirstRender() {
            this.RenderButtons(false, OnePlayer_Button, TwoPlayers_Button);
        }

        private void Selection_Button_Click(object sender, RoutedEventArgs e) {
            Button selectedButton = (sender as Button);
            switch (selectedButton.Name) {
                case "one":
                RenderButtons(true, OnePlayer_Easy_Button, OnePlayer_Medium_Button, OnePlayer_Hard_Button);
                break;
                case "two":
                RenderButtons(true, TwoPlayers_Normal_Button);
                break;
                case "easy":
                this.gameData = new GameData(GameType.ONE_PLAYER_EASY);
                RenderButtons(true, SizeSmall_Button, SizeMedium_Button, SizeBig_Button, SizeHuge_Button);
                break;
                case "medium":
                this.gameData = new GameData(GameType.ONE_PLAYER_MEDIUM);
                RenderButtons(true, SizeSmall_Button, SizeMedium_Button, SizeBig_Button, SizeHuge_Button);
                break;
                case "hard":
                this.gameData = new GameData(GameType.ONE_PLAYER_HARD);
                RenderButtons(true, SizeSmall_Button, SizeMedium_Button, SizeBig_Button, SizeHuge_Button);
                break;
                case "normal":
                this.gameData = new GameData(GameType.TWO_PLAYERS_NORMAL);
                RenderButtons(true, SizeSmall_Button, SizeMedium_Button, SizeBig_Button, SizeHuge_Button);
                break;
               /* case "lan":
                this.gameData = new GameData(GameType.TWO_PLAYERS_LAN);
                LanGame_Event(sender, e);
                break;*/
                case "ssmall":
                this.gameData.mapSize = MapSize.SMALL;
                StartOrRenderXO(sender, e);
                break;
                case "smedium":
                this.gameData.mapSize = MapSize.MEDIUM;
                StartOrRenderXO(sender, e);
                break;
                case "sbig":
                this.gameData.mapSize = MapSize.BIG;
                StartOrRenderXO(sender, e);
                break;
                case "shuge":
                this.gameData.mapSize = MapSize.HUGE;
                StartOrRenderXO(sender, e);
                break;
                case "cross":
                this.gameData.player = CrossCircle.CROSS;
                StartGame_Event(this.gameData, e);
                break;
                case "circle":
                this.gameData.player = CrossCircle.CIRCLE;
                StartGame_Event(this.gameData, e);
                break;
            }
        }

        private void StartOrRenderXO(object sender, RoutedEventArgs e) {
            if (this.gameData.gameType == GameType.TWO_PLAYERS_NORMAL || this.gameData.gameType == GameType.TWO_PLAYERS_LAN) {
                StartGame_Event(this.gameData, e);
            }
            else {
                RenderButtons(true, Circle_Button, Cross_Button);
            }
        }

        private void MainMenu_Button_Click(object sender, RoutedEventArgs e) {
            FirstRender();
        }

    }
}
