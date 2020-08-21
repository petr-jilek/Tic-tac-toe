using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Tic_tac_toe.Models.Enums;

namespace Tic_tac_toe.Views.UserControls
{
    public class GameGrid : Grid
    {
        private int x_count;
        private int y_count;

        private List<CrossCircle> gameTable;

        public EventHandler GridButton_Click_Event;

        public GameGrid(int x_count, int y_count) {
            this.x_count = x_count;
            this.y_count = y_count;
            GenerateGameGrid();
        }

        private void GenerateGameGrid() {
            for (int i = 0; i < x_count; i++) {
                this.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star), });
            }
            for (int i = 0; i < y_count; i++) {
                this.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star), });
            }
            this.gameTable = new List<CrossCircle>();
            for (int i = 0; i < x_count; i++) {
                for (int j = 0; j < y_count; j++) {
                    GameGridButton Grid_Button = new GameGridButton(i, j);
                    Grid_Button.Click += GridButton_Click;
                    Grid.SetColumn(Grid_Button, i);
                    Grid.SetRow(Grid_Button, j);
                    this.Children.Add(Grid_Button);
                    gameTable.Add(CrossCircle.NOTHING);
                }
            }
        }

        private void GridButton_Click(object sender, RoutedEventArgs e) {
            GridButton_Click_Event(sender, e);
        }

        public void Play(int x, int y, CrossCircle crossCircle) {
            gameTable[(x * this.y_count) + y] = crossCircle;
            (this.Children[(x * this.y_count) + y] as GameGridButton).Change(crossCircle);
        }

        public bool CheckWin(CrossCircle crossCircle) {
            for (int j = 0; j < y_count; j++) {
                int success = 0;
                for (int i = 0; i < x_count; i++) {
                    if (this[i, j] == crossCircle) {
                        success++;
                        if (success == 5) {
                            return true;
                        }
                    }
                    else {
                        success = 0;
                    }
                }
            }

            for (int i = 0; i < x_count; i++) {
                int success = 0;
                for (int j = 0; j < y_count; j++) {
                    if (this[i, j] == crossCircle) {
                        success++;
                        if (success == 5) {
                            return true;
                        }
                    }
                    else {
                        success = 0;
                    }
                }
            }

            for (int i = 0; i < y_count; i++) {
                int success = 0;
                for (int j = 0; j < (i + 1); j++) {
                    if (this[j, y_count - i + j - 1] == crossCircle) {
                        success++;
                        if (success == 5) {
                            return true;
                        }
                    }
                    else {
                        success = 0;
                    }
                }
            }

            for (int i = 0; i < x_count; i++) {
                if (i == 0) continue;
                int success = 0;
                for (int j = 0; j < (y_count - i); j++) {
                    if (this[i + j, j] == crossCircle) {
                        success++;
                        if (success == 5) {
                            return true;
                        }
                    }
                    else {
                        success = 0;
                    }
                }
            }

            for (int i = 0; i < y_count; i++) {
                int success = 0;
                for (int j = 0; j < (x_count - i); j++) {
                    if (this[j, y_count - i - j - 1] == crossCircle) {
                        success++;
                        if (success == 5) {
                            return true;
                        }
                    }
                    else {
                        success = 0;
                    }
                }
            }

            for (int i = 0; i < x_count; i++) {
                if (i == 0) continue;
                int success = 0;
                for (int j = 0; j < (y_count - i); j++) {
                    if (this[i + j, y_count - j - 1] == crossCircle) {
                        success++;
                        if (success == 5) {
                            return true;
                        }
                    }
                    else {
                        success = 0;
                    }
                }
            }
            return false;
        }

        public CrossCircle this[int x, int y] {
            get { return gameTable[(x * y_count) + y]; }
            private set { }
        }

    }
}
