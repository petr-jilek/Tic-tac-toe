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
            this.welcomePage.FirstRender();
            RenderPage(this.welcomePage);
        }

        private void StartGame(object sender, EventArgs e) {
            this.gamePage = new GamePage((GameType)sender);
            this.gamePage.GameOver_Event += GameOver;
            this.RenderPage(gamePage);
        }

        private void GameOver(object sender, EventArgs e) {
            GameData gameData = sender as GameData;
            GameOverPage gameOverPage = new GameOverPage(gameData);
            gameOverPage.PlayAgain_Button_Click_Event += StartGame;
            gameOverPage.Return_Button_Click_Event += WelcomePage;
            this.RenderPage(gameOverPage);
        }

        private void PlayAgain_Button_Click(object sender, RoutedEventArgs e) {
            this.gamePage.PlayAgain();
        }
    }
}
