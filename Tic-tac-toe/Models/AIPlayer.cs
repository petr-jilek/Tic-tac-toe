using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tic_tac_toe.Models.Enums;
using Tic_tac_toe.Static;

namespace Tic_tac_toe.Models
{
    public class AIPlayer
    {
        private GameTable gameTable;

        private GameTable croppedTable;
        private int upCroop;
        private int downCroop;
        private int leftCroop;
        private int rightCroop;

        private readonly GameType gameType;

        private readonly CrossCircle iAm;

        public CrossCircle IAm { get => this.iAm; private set { } }

        private int selectedCroppedX;
        private int selectedCroppedY;

        public AIPlayer(GameTable gameTable, GameType gameType, CrossCircle iAm) {
            this.gameTable = gameTable;
            this.gameType = gameType;
            this.iAm = iAm;
        }

        public (int, int) ReturnCorrds() {
            MakeCroppedTable();
            SelectCroppedCords();
            return (selectedCroppedX + leftCroop, selectedCroppedY + upCroop);
        }

        public void MakeCroppedTable() {
            int up = 0; int down = 0; int left = 0; int right = 0;

            for (int i = 0; i < gameTable.Y_count; i++) {
                int match = 0;
                for (int j = 0; j < gameTable.X_count; j++) {
                    if (gameTable[j, i] != CrossCircle.NOTHING) {
                        match++;
                        break;
                    }
                }
                if (match == 0) up++;
                else break;
            }

            for (int i = 0; i < gameTable.Y_count; i++) {
                int match = 0;
                for (int j = 0; j < gameTable.X_count; j++) {
                    if (gameTable[j, gameTable.Y_count - i - 1] != CrossCircle.NOTHING) {
                        match++;
                        break;
                    }
                }
                if (match == 0) down++;
                else break;
            }

            for (int i = 0; i < gameTable.X_count; i++) {
                int match = 0;
                for (int j = 0; j < gameTable.Y_count; j++) {
                    if (gameTable[i, j] != CrossCircle.NOTHING) {
                        match++;
                        break;
                    }
                }
                if (match == 0) left++;
                else break;
            }

            for (int i = 0; i < gameTable.X_count; i++) {
                int match = 0;
                for (int j = 0; j < gameTable.Y_count; j++) {
                    if (gameTable[gameTable.X_count - i - 1, j] != CrossCircle.NOTHING) {
                        match++;
                        break;
                    }
                }
                if (match == 0) right++;
                else break;
            }

            if (up != 0) up--;
            if (down != 0) down--;
            if (left != 0) left--;
            if (right != 0) right--;

            this.croppedTable = new GameTable(this.gameTable.X_count - left - right, this.gameTable.Y_count - up - down);
            this.upCroop = up;
            this.downCroop = down;
            this.leftCroop = left;
            this.rightCroop = right;


            for (int i = left; i < (croppedTable.X_count + left); i++) {
                for (int j = up; j < (croppedTable.Y_count + up); j++) {
                    croppedTable.Add(gameTable[i, j]);
                }
            }
        }

        private void SelectCroppedCords() {
            if (croppedTable.X_count <= 3 && croppedTable.Y_count <= 3) {
                Random random = new Random();
                int x = random.Next(croppedTable.X_count);
                int y = random.Next(croppedTable.Y_count);
                if (croppedTable[x, y] == CrossCircle.NOTHING) {
                    selectedCroppedX = x;
                    selectedCroppedY = y;
                }
                else {
                    for (int i = 0; i < croppedTable.X_count; i++) {
                        for (int j = 0; j < croppedTable.Y_count; j++) {
                            if (croppedTable[i, j] == CrossCircle.NOTHING) {
                                selectedCroppedX = i;
                                selectedCroppedY = j;
                            }
                        }
                    }
                }
            }
            else {
                AIScoreTable aIScoreTable = new AIScoreTable(this.croppedTable);
                (selectedCroppedX, selectedCroppedY) = aIScoreTable.GetCoords();
            }
            int a = 0;
        }

    }
}
