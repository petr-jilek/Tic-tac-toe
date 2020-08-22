using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Tic_tac_toe.Models;
using Tic_tac_toe.Models.Enums;
using Tic_tac_toe.Static;

namespace Tic_tac_toe.Views.UserControls
{
    public class GameGrid : Grid
    {
        private int x_count;
        private int y_count;

        private GameTable gameTable;

        public EventHandler GridButton_Click_Event;

        public GameGrid(int x_count, int y_count) {
            this.x_count = x_count;
            this.y_count = y_count;
            this.gameTable = new GameTable(x_count, y_count);
            GenerateGameGrid();
        }

        private void GenerateGameGrid() {
            for (int i = 0; i < x_count; i++) {
                this.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star), });
            }
            for (int i = 0; i < y_count; i++) {
                this.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star), });
            }
            for (int i = 0; i < x_count; i++) {
                for (int j = 0; j < y_count; j++) {
                    GameGridButton Grid_Button = new GameGridButton(i, j);
                    Grid_Button.Click += GridButton_Click;
                    Grid.SetColumn(Grid_Button, i);
                    Grid.SetRow(Grid_Button, j);
                    this.Children.Add(Grid_Button);
                    this.gameTable.Add(CrossCircle.NOTHING);
                }
            }
        }

        private void GridButton_Click(object sender, RoutedEventArgs e) {
            GridButton_Click_Event(sender, e);
        }

        public void Play(int x, int y, CrossCircle crossCircle) {
            gameTable[x, y] = crossCircle;
            (this.Children[(x * this.y_count) + y] as GameGridButton).Change(crossCircle);
        }

        public bool CheckWin(CrossCircle crossCircle) {
            return this.gameTable.CheckWin(crossCircle, 5);
        }

        public GameTable CloneGameTable() {
            return (GameTable)DeepClone.DeepCopy<GameTable>(this.gameTable);
        }

        public CrossCircle this[int x, int y] {
            get { return this.gameTable[x, y]; }
            private set { }
        }

    }
}
