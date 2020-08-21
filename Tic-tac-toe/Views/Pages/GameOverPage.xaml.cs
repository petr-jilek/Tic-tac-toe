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
    /// Interaction logic for GameOverPage.xaml
    /// </summary>
    public partial class GameOverPage : UserControl
    {
        private readonly GameData gameData;

        public EventHandler PlayAgain_Button_Click_Event;
        public EventHandler Return_Button_Click_Event;

        public GameOverPage(GameData gameData) {
            InitializeComponent();
            this.gameData = gameData;
            if (this.gameData.Player == CrossCircle.CIRCLE) {
                Result_TextBlock.Text = "First player (O) won";
            }
            else {
                Result_TextBlock.Text = "Second player (X) won";
            }
        }

        private void PlayAgain_Button_Click(object sender, RoutedEventArgs e) {
            PlayAgain_Button_Click_Event(gameData.GameType, e);
        }

        private void Return_Button_Click(object sender, RoutedEventArgs e) {
            Return_Button_Click_Event(sender, e);
        }

    }
}
