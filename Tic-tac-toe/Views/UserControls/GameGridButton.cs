using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Tic_tac_toe.Models.Enums;

namespace Tic_tac_toe.Views.UserControls
{
    public class GameGridButton : Button
    {
        private int x_corrd;
        private int y_corrd;

        public int X { get => this.x_corrd; private set { } }
        public int Y { get => this.y_corrd; private set { } }

        private CrossCircle crossCircle;

        public CrossCircle CrossCircle { get => this.crossCircle; private set { } }

        public GameGridButton(int x_corrd, int y_corrd) {
            this.x_corrd = x_corrd;
            this.y_corrd = y_corrd;
            Grid.SetColumn(this, this.x_corrd);
            Grid.SetRow(this, this.y_corrd);
            this.crossCircle = CrossCircle.NOTHING;
            SetContent();
            this.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            this.IsEnabledChanged += GameOver;
        }

        private void SetContent() {
            switch (this.crossCircle) {
                case CrossCircle.NOTHING:
                this.Content = "";
                break;
                case CrossCircle.CROSS:
                this.Content = "X";
                this.Background = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                break;
                case CrossCircle.CIRCLE:
                this.Content = "O";
                this.Background = new SolidColorBrush(Color.FromRgb(20, 189, 172));
                break;
            }
            this.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            this.FontWeight = FontWeights.Bold;
            this.BorderThickness = new Thickness(0.2, 0.2, 0.2, 0.2);
        }

        public void Change(CrossCircle crossCircle) {
            if (this.crossCircle == CrossCircle.NOTHING) {
                this.crossCircle = crossCircle;
            }
            SetContent();
        }

        private void GameOver(object sender, DependencyPropertyChangedEventArgs e) {
            if (this.crossCircle == CrossCircle.CIRCLE) {
                this.Foreground = new SolidColorBrush(Color.FromRgb(20, 189, 172));
            }
            else {
                this.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            }
        }

    }
}
