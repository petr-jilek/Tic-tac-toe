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
        private readonly GameData gameData;

        private GameGrid Game_Grid;

        private CrossCircle playerOnMove;

        public EventHandler EditInformation_Event;
        public EventHandler GameOver_Event;

        public GamePage(GameData gameData) {
            InitializeComponent();
            this.gameData = gameData;
            CreateGameGrid();
        }
        public void CreateGameGrid() {
            switch (this.gameData.mapSize) {
                case MapSize.SMALL:
                this.Game_Grid = new GameGrid(10, 10);
                break;
                case MapSize.MEDIUM:
                this.Game_Grid = new GameGrid(20, 20);
                break;
                case MapSize.BIG:
                this.Game_Grid = new GameGrid(30, 30);
                break;
                case MapSize.HUGE:
                this.Game_Grid = new GameGrid(50, 50);
                break;
            }
            this.Game_Grid.GridButton_Click_Event += Play;
            this.Game_Content_Grid.Children.Clear();
            this.Game_Content_Grid.Children.Add(Game_Grid);

            this.playerOnMove = CrossCircle.CIRCLE;
            this.gameData.rounds = 1;

            if (this.gameData.player == CrossCircle.CROSS) {
                AIPlay();
                SwichPlayer();
            }
        }

        public void SendMessage() {
            string message = this.gameData.rounds.ToString() + ";";
            if (this.playerOnMove == CrossCircle.CIRCLE) {
                message += "(O)";
            }
            else {
                message += "(X)";
            }
            EventArgs e = new EventArgs();
            this.EditInformation_Event(message, e);
        }

        private void Play(object sender, EventArgs e) {
            GameGridButton gameGridButton = (sender as GameGridButton);

            if (this.Game_Grid[gameGridButton.X, gameGridButton.Y] == CrossCircle.NOTHING) {
                this.Game_Grid.Play(gameGridButton.X, gameGridButton.Y, this.playerOnMove);
                CheckWin(sender, e);
                if (this.playerOnMove == CrossCircle.CROSS) {
                    CheckDraw();
                    this.gameData.rounds++;
                }
                SwichPlayer();
                SendMessage();

                if (this.gameData.gameType == GameType.ONE_PLAYER_EASY ||
                    this.gameData.gameType == GameType.ONE_PLAYER_MEDIUM ||
                    this.gameData.gameType == GameType.ONE_PLAYER_HARD) {
                    AIPlay();
                    CheckWin(sender, e);
                    if (this.playerOnMove == CrossCircle.CROSS) {
                        CheckDraw();
                        this.gameData.rounds++;
                    }
                    SwichPlayer();
                    SendMessage();
                }
            }
            else {
                MessageBox.Show("Cannot");
            }
        }

        private void CheckWin(object sender, EventArgs e) {
            bool win = this.Game_Grid.CheckWin(this.playerOnMove);
            if (win) {
                this.gameData.winner = this.playerOnMove;
                GameOver_Event(this.gameData, e);
                return;
            }
        }

        private void SwichPlayer() {
            if (this.playerOnMove == CrossCircle.CIRCLE) {
                this.playerOnMove = CrossCircle.CROSS;
            }
            else {
                this.playerOnMove = CrossCircle.CIRCLE;
            }
        }

        private void CheckDraw() {
            EventArgs e = new EventArgs();
            if (this.gameData.mapSize == MapSize.SMALL) {
                if (((this.gameData.rounds * 2)) == (10 * 10)) {
                    this.gameData.winner = CrossCircle.NOTHING;
                    GameOver_Event(this.gameData, e);
                    return;
                }
            }
            else if (this.gameData.mapSize == MapSize.MEDIUM) {
                if (this.gameData.mapSize == MapSize.SMALL) {
                    if (((this.gameData.rounds * 2)) == (20 * 20)) {
                        this.gameData.winner = CrossCircle.NOTHING;
                        GameOver_Event(this.gameData, e);
                        return;
                    }
                }
            }
            else if (this.gameData.mapSize == MapSize.BIG) {
                if (this.gameData.mapSize == MapSize.SMALL) {
                    if (((this.gameData.rounds * 2)) == (30 * 30)) {
                        this.gameData.winner = CrossCircle.NOTHING;
                        GameOver_Event(this.gameData, e);
                        return;
                    }
                }
            }
            else {
                if (this.gameData.mapSize == MapSize.SMALL) {
                    if (((this.gameData.rounds * 2)) == (50 * 50)) {
                        this.gameData.winner = CrossCircle.NOTHING;
                        GameOver_Event(this.gameData, e);
                        return;
                    }
                }
            }
        }

        private void AIPlay() {
            AIPlayer aIPlayer;
            if (this.gameData.player == CrossCircle.CIRCLE) {
                aIPlayer = new AIPlayer(Game_Grid.CloneGameTable(), this.gameData, CrossCircle.CROSS, this.gameData.gameType);
            }
            else {
                aIPlayer = new AIPlayer(Game_Grid.CloneGameTable(), this.gameData, CrossCircle.CIRCLE, this.gameData.gameType);
            }
            (int x, int y) = aIPlayer.ReturnCorrds();
            this.Game_Grid.Play(x, y, aIPlayer.IAm);
        }

    }
}
