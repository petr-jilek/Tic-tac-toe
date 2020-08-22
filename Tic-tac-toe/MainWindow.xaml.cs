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
using Tic_tac_toe.Views.Pages;

namespace Tic_tac_toe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly WelcomePage welcomePage = new WelcomePage();
        private GamePage gamePage;

        public MainWindow() {
            InitializeComponent();
            this.welcomePage.StartGame_Event = StartGame;
            this.RenderPage(welcomePage);
        }

        private void RenderPage(UIElement page) {
            this.Page_Grid.Children.Clear();
            this.Page_Grid.Children.Add(page);
        }

        private void WelcomePage(object sender, EventArgs e) {
            this.Page_Grid.IsEnabled = true;
            this.Result_Grid.Children.Clear();
            this.welcomePage.FirstRender();
            RenderPage(this.welcomePage);
        }

        private void StartGame(object sender, EventArgs e) {
            this.gamePage = new GamePage((GameData)sender);
            this.gamePage.GameOver_Event += GameOver;
            this.RenderPage(gamePage);
        }

        private void GameOver(object sender, EventArgs e) {
            GameData gameData = sender as GameData;
            StackPanel GameOver_StackPanel = new StackPanel();
            Grid.SetRowSpan(GameOver_StackPanel, 2);
            GameOver_StackPanel.Orientation = Orientation.Vertical;
            GameOver_StackPanel.HorizontalAlignment = HorizontalAlignment.Center;
            GameOver_StackPanel.VerticalAlignment = VerticalAlignment.Center;
            TextBlock Result_TextBlock = new TextBlock() { };
            switch (gameData.winner) {
                case CrossCircle.CIRCLE:
                Result_TextBlock.Text = "First player (O) won";
                break;
                case CrossCircle.CROSS:
                Result_TextBlock.Text = "Second player (X) won";
                break;
                case CrossCircle.NOTHING:
                Result_TextBlock.Text = "Draw";
                break;
            }
            Button Again_Button = new Button() { Content = "Again" };
            Button MainMenu_Button = new Button() { Content = "Main Menu" };
            MainMenu_Button.Click += WelcomePage;
            GameOver_StackPanel.Children.Clear();
            GameOver_StackPanel.Children.Add(Result_TextBlock);
            GameOver_StackPanel.Children.Add(Again_Button);
            GameOver_StackPanel.Children.Add(MainMenu_Button);
            this.Result_Grid.Children.Clear();
            this.Result_Grid.Children.Add(GameOver_StackPanel);
            this.Page_Grid.IsEnabled = false;
        }

        private void PlayAgain_Button_Click(object sender, RoutedEventArgs e) {
            this.gamePage.CreateGameGrid();
        }

        private void BackMainMenu_Button_Click(object sender, RoutedEventArgs e) {
            WelcomePage(sender, e);
        }
    }
}
