using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tic_tac_toe.Models.Enums;

namespace Tic_tac_toe.Models
{
    public class AIScoreTable
    {
        private GameTable gameTable;

        private List<int> points = new List<int>();

        private CrossCircle iAm;

        public AIScoreTable(GameTable gameTable, CrossCircle iAm) {
            this.gameTable = gameTable;
            this.iAm = iAm;
            SetFirstPoints();
        }

        public (int, int) GetCoords() {
            SetPoints();
            int max = -1;
            int max_x = -1;
            int max_y = -1;
            for (int i = 0; i < this.gameTable.X_count; i++) {
                for (int j = 0; j < this.gameTable.Y_count; j++) {
                    if (this[i, j] > max) {
                        max = this[i, j];
                        max_x = i;
                        max_y = j;
                    }
                }
            }
            return (max_x, max_y);
        }

        private void SetFirstPoints() {
            for (int i = 0; i < this.gameTable.X_count; i++) {
                for (int j = 0; j < this.gameTable.Y_count; j++) {
                    if (this.gameTable[i, j] == CrossCircle.NOTHING) {
                        points.Add(-1);
                    }
                    else {
                        points.Add(-2);
                    }
                }
            }
        }

        private void SetPoints() {
            for (int i = 0; i < this.gameTable.X_count; i++) {
                for (int j = 0; j < this.gameTable.Y_count; j++) {
                    if (this[i, j] == -1) {
                        List<(int, int)> directions = this.GetDirections(i, j);
                        this[i, j] = GetPoints(i, j, directions);
                    }
                }
            }
        }

        private List<(int, int)> GetDirections(int x, int y) {
            List<(int, int)> directions = new List<(int, int)>();

            if (x == 0) {
                if (y == 0) {
                    if (this[x + 1, y + 1] == -2) {
                        directions.Add((1, 1));
                    }
                }
                else if (y == (gameTable.Y_count - 1)) {
                    if (this[x + 1, y - 1] == -2) {
                        directions.Add((1, -1));
                    }
                }
                else {
                    if (this[x + 1, y + 1] == -2) {
                        directions.Add((1, 1));
                    }
                    if (this[x + 1, y] == -2) {
                        directions.Add((1, 0));
                    }
                    if (this[x + 1, y - 1] == -2) {
                        directions.Add((1, -1));
                    }
                }
            }
            else if (x == (gameTable.X_count - 1)) {
                if (y == 0) {
                    if (this[x - 1, y + 1] == -2) {
                        directions.Add((-1, 1));
                    }
                }
                else if (y == (gameTable.Y_count - 1)) {
                    if (this[x - 1, y - 1] == -2) {
                        directions.Add((-1, -1));
                    }
                }
                else {
                    if (this[x - 1, y + 1] == -2) {
                        directions.Add((-1, 1));
                    }
                    if (this[x - 1, y] == -2) {
                        directions.Add((-1, 0));
                    }
                    if (this[x - 1, y - 1] == -2) {
                        directions.Add((-1, -1));
                    }
                }
            }
            else {
                if (y == 0) {
                    if (this[x - 1, y + 1] == -2) {
                        directions.Add((-1, +1));
                    }
                    if (this[x, y + 1] == -2) {
                        directions.Add((0, +1));
                    }
                    if (this[x + 1, y + 1] == -2) {
                        directions.Add((+1, +1));
                    }
                }
                else if (y == (gameTable.Y_count - 1)) {
                    if (this[x - 1, y - 1] == -2) {
                        directions.Add((-1, -1));
                    }
                    if (this[x, y - 1] == -2) {
                        directions.Add((0, -1));
                    }
                    if (this[x + 1, y - 1] == -2) {
                        directions.Add((+1, -1));
                    }
                }
                else {
                    if (this[x - 1, y - 1] == -2) {
                        directions.Add((-1, -1));
                    }
                    if (this[x, y - 1] == -2) {
                        directions.Add((0, -1));
                    }
                    if (this[x + 1, y - 1] == -2) {
                        directions.Add((+1, -1));
                    }
                    if (this[x + 1, y] == -2) {
                        directions.Add((+1, 0));
                    }
                    if (this[x + 1, y + 1] == -2) {
                        directions.Add((+1, +1));
                    }
                    if (this[x, y + 1] == -2) {
                        directions.Add((0, +1));
                    }
                    if (this[x - 1, y + 1] == -2) {
                        directions.Add((-1, +1));
                    }
                    if (this[x - 1, y] == -2) {
                        directions.Add((-1, 0));
                    }
                }
            }

            return directions;
        }

        private int GetPoints(int x, int y, List<(int, int)> directions) {
            if (directions.Count == 0) {
                return 0;
            }

            int sum = 0;
            for (int d = 0; d < directions.Count; d++) {
                (int dx, int dy) = directions[d];
                CrossCircle firstTouched = CrossCircle.NOTHING;
                int success = 0;
                int i = 1;
                while (true) {
                    if (i == 1) {
                        firstTouched = this.gameTable[x + dx, y + dy];
                        success++;
                    }
                    else {
                        if (x + (i * dx) == this.gameTable.X_count || y + (i * dy) == this.gameTable.Y_count ||
                            x + (i * dx) < 0 || y + (i * dy) < 0) {
                            //Is Blocked
                            if (success == 1) {

                            }
                            else if (success == 2) {

                            }
                            else if (success == 3) {

                            }
                            else if (success == 4) {
                                if (firstTouched == iAm) {
                                    success = 2000;
                                }
                                else {
                                    success = 1999;
                                }
                            }
                            //END Is Blocked
                            break;
                        }
                        if (this.gameTable[x + (i * dx), y + (i * dy)] == firstTouched) {
                            success++;
                        }
                        else {
                            if (this.gameTable[x + (i * dx), y + (i * dy)] == CrossCircle.NOTHING) {
                                //Not Blocked
                                if (success == 1) {
                                    success = success * 2;
                                }
                                else if (success == 2) {
                                    success = success * 2;
                                }
                                else if (success == 3) {
                                    if (firstTouched == iAm) {
                                        success = 1900;
                                    }
                                    else {
                                        success = 1899;
                                    }

                                }
                                else if (success == 4) {
                                    if (firstTouched == iAm) {
                                        success = 2000;
                                    }
                                    else {
                                        success = 1999;
                                    }
                                }
                                //END Not Blocked
                            }
                            else {
                                //Is Blocked
                                if (success == 1) {

                                }
                                else if (success == 2) {

                                }
                                else if (success == 3) {

                                }
                                else if (success == 4) {
                                    if (firstTouched == iAm) {
                                        success = 2000;
                                    }
                                    else {
                                        success = 1999;
                                    }
                                }
                                //END Is Blocked
                            }
                            break;
                        }
                    }
                    i++;
                }
                sum += success;
            }

            return sum;
        }


        public int this[int x, int y] {
            get { return points[(x * this.gameTable.Y_count) + y]; }
            set { points[(x * this.gameTable.Y_count) + y] = value; }
        }

    }
}
