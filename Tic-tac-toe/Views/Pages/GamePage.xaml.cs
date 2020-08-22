﻿using System;
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

            if (this.gameData.player == CrossCircle.CROSS) {
                AIPlay();
                SwichPlayer();
            }
        }

        private void Play(object sender, EventArgs e) {
            GameGridButton gameGridButton = (sender as GameGridButton);

            if (this.Game_Grid[gameGridButton.X, gameGridButton.Y] == CrossCircle.NOTHING) {
                this.Game_Grid.Play(gameGridButton.X, gameGridButton.Y, this.playerOnMove);
                CheckWin(sender, e);
                if (this.playerOnMove == CrossCircle.CROSS) {
                    this.gameData.rounds++;
                }
                SwichPlayer();

                if (this.gameData.gameType == GameType.ONE_PLAYER_EASY ||
                    this.gameData.gameType == GameType.ONE_PLAYER_MEDIUM ||
                    this.gameData.gameType == GameType.ONE_PLAYER_HARD) {
                    AIPlay();
                    CheckWin(sender, e);
                    SwichPlayer();
                    this.gameData.rounds++;
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
            if (this.playerOnMove == CrossCircle.CIRCLE) this.playerOnMove = CrossCircle.CROSS;
            else this.playerOnMove = CrossCircle.CIRCLE;
        }

        private void AIPlay() {
            AIPlayer aIPlayer;
            if (this.gameData.player == CrossCircle.CIRCLE) {
                aIPlayer = new AIPlayer(Game_Grid.CloneGameTable(), this.gameData, CrossCircle.CROSS);
            }
            else {
                aIPlayer = new AIPlayer(Game_Grid.CloneGameTable(), this.gameData, CrossCircle.CIRCLE);
            }
            (int x, int y) = aIPlayer.ReturnCorrds();
            this.Game_Grid.Play(x, y, aIPlayer.IAm);
        }

    }
}
