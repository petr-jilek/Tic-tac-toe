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
using Tic_tac_toe.Views.UserControls;

namespace Tic_tac_toe.Views.Pages
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : UserControl
    {
        private GameGrid Game_Grid;

        private readonly GameType gameType;

        private CrossCircle playerOnMove;

        public EventHandler GameOver_Event;

        public GamePage(GameType gameType) {
            InitializeComponent();
            this.gameType = gameType;
            this.Game_Grid = new GameGrid(10, 10);
            this.Game_Grid.GridButton_Click_Event += Play;
            this.Game_Content_Grid.Children.Clear();
            this.Game_Content_Grid.Children.Add(Game_Grid);
            this.playerOnMove = CrossCircle.CIRCLE;
        }

        private void Play(object sender, EventArgs e) {
            GameGridButton gameGridButton = (sender as GameGridButton);
            if (this.Game_Grid[gameGridButton.X, gameGridButton.Y] == CrossCircle.NOTHING) {
                this.Game_Grid.Play(gameGridButton.X, gameGridButton.Y, this.playerOnMove);

                bool win = this.Game_Grid.CheckWin(this.playerOnMove);
                if (win) {
                    GameOver_Event(new GameData(this.playerOnMove, this.gameType), e);
                }


                if (this.playerOnMove == CrossCircle.CIRCLE) this.playerOnMove = CrossCircle.CROSS;
                else this.playerOnMove = CrossCircle.CIRCLE;
            }
            else {
                MessageBox.Show("Cannot");
            }
        }

    }
}
