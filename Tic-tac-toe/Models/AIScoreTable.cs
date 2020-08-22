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

        private GameType gameType;

        public AIScoreTable(GameTable gameTable, CrossCircle iAm, GameType gameType) {
            this.gameTable = gameTable;
            this.iAm = iAm;
            this.gameType = gameType;
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
                        if (this.gameType == GameType.ONE_PLAYER_EASY) {
                            this[i, j] = ReturnScoreEasy(i, j, directions);
                        }
                        else if (this.gameType == GameType.ONE_PLAYER_MEDIUM) {
                            this[i, j] = ReturnScoreMedium(i, j, directions);
                        }
                        else {
                            (int sum, List<(int, CrossCircle, bool)> successFirstTouchedBlocked) = GetPoints(i, j, directions);
                            this[i, j] = ReturnScoreHard(sum, successFirstTouchedBlocked, directions);
                        }
                    }
                }
            }
        }

        private int ReturnScoreHard(int sum, List<(int, CrossCircle, bool)> successFirstTouchedBlocked, List<(int, int)> directions) {
            if (directions.Count == 0) {
                return 0;
            }

            int finalSum = 0;
            for (int i = 0; i < successFirstTouchedBlocked.Count; i++) {
                (int score, CrossCircle touched, bool blocked) = successFirstTouchedBlocked[i];
                int scoreOposite = 0;
                CrossCircle touchedOposite = CrossCircle.NOTHING;
                bool blockedOposite = false;
                bool haveOposite = false;
                (int dx, int dy) = directions[i];

                for (int d = 0; d < directions.Count; d++) {
                    (int dxC, int dyC) = directions[d];
                    if ((dx + dxC == 0) && (dy + dyC == 0)) {
                        haveOposite = true;
                        (scoreOposite, touchedOposite, blockedOposite) = successFirstTouchedBlocked[d];
                        break;
                    }
                }

                int finalScore = 0;

                const int BigInfinity = 1000000;
                const int SmallInfinity = 100000;
                const int BigSmallInfinity = 10000;
                const int SmallSmallInfinity = 1000;

                if (haveOposite) {
                    if (touched == touchedOposite) {
                        if (blocked) {
                            if (blockedOposite) {
                                if (touched == iAm) {
                                    //haveOposite, same, blocked, blockedOposite, iAm
                                    if ((score + scoreOposite) >= 4) {
                                        finalScore = BigInfinity;
                                    }
                                    else {
                                        finalScore = 0;
                                    }

                                }
                                else {
                                    //haveOposite, same, blocked, blockedOposite, not iAm
                                    if ((score + scoreOposite) >= 4) {
                                        finalScore = SmallInfinity;
                                    }
                                    else {
                                        finalScore = 0;
                                    }
                                }
                            }
                            else {
                                if (touched == iAm) {
                                    //haveOposite, same, blocked, not blockedOposite, iAm
                                    if ((score + scoreOposite) >= 4) {
                                        finalScore = BigInfinity;
                                    }
                                    else {
                                        finalScore = score + scoreOposite;
                                    }
                                }
                                else {
                                    //haveOposite, same, blocked, not blockedOposite, not iAm
                                    if ((score + scoreOposite) >= 4) {
                                        finalScore = SmallInfinity;
                                    }
                                    else {
                                        finalScore = score + scoreOposite;
                                    }
                                }
                            }
                        }
                        else {
                            if (blockedOposite) {
                                if (touched == iAm) {
                                    //haveOposite, same, not blocked, blockedOposite, iAm
                                    if ((score + scoreOposite) >= 4) {
                                        finalScore = BigInfinity;
                                    }
                                    else {
                                        finalScore = score + scoreOposite;
                                    }
                                }
                                else {
                                    //haveOposite, same, not blocked, blockedOposite, not iAm
                                    if ((score + scoreOposite) >= 4) {
                                        finalScore = SmallInfinity;
                                    }
                                    else {
                                        finalScore = score + scoreOposite;
                                    }
                                }
                            }
                            else {
                                if (touched == iAm) {
                                    //haveOposite, same, not blocked, not blockedOposite, iAm
                                    if ((score + scoreOposite) >= 4) {
                                        finalScore = BigInfinity;
                                    }
                                    else {
                                        finalScore = (score + scoreOposite) * (score + scoreOposite);
                                    }
                                }
                                else {
                                    //haveOposite, same, not blocked, not blockedOposite, not iAm
                                    if ((score + scoreOposite) >= 4) {
                                        finalScore = SmallInfinity;
                                    }
                                    else {
                                        finalScore = (score + scoreOposite) * (score + scoreOposite);
                                    }
                                }
                            }
                        }
                    }
                    else {
                        if (blocked) {
                            if (blockedOposite) {
                                if (touched == iAm) {
                                    //haveOposite, not same, blocked, blockedOposite, iAm
                                    if (score >= 4) {
                                        finalScore = BigInfinity;
                                    }
                                    else if (scoreOposite >= 4) {
                                        finalScore = SmallInfinity;
                                    }
                                    else {
                                        finalScore = score * score;
                                    }
                                }
                                else {
                                    //haveOposite, not same, blocked, blockedOposite, not iAm
                                    if (score >= 4) {
                                        finalScore = SmallInfinity;
                                    }
                                    else if (scoreOposite >= 4) {
                                        finalScore = BigInfinity;
                                    }
                                    else {
                                        finalScore = score * score;
                                    }
                                }
                            }
                            else {
                                if (touched == iAm) {
                                    //haveOposite, not same, blocked, not blockedOposite, iAm
                                    if (score >= 4) {
                                        finalScore = BigInfinity;
                                    }
                                    else if (scoreOposite >= 4) {
                                        finalScore = SmallInfinity;
                                    }
                                    else {
                                        finalScore = 0;
                                    }
                                }
                                else {
                                    //haveOposite, not same, blocked, not blockedOposite, not iAm
                                    if (score >= 4) {
                                        finalScore = BigInfinity;
                                    }
                                    else if (scoreOposite >= 4) {
                                        finalScore = SmallInfinity;
                                    }
                                    else if (scoreOposite >= 3) {
                                        finalScore = SmallSmallInfinity;
                                    }
                                    else {
                                        finalScore = score;
                                    }
                                }
                            }
                        }
                        else {
                            if (blockedOposite) {
                                if (touched == iAm) {
                                    //haveOposite, not same, not blocked, blockedOposite, iAm
                                    if (score >= 4) {
                                        finalScore = BigInfinity;
                                    }
                                    else if (scoreOposite >= 4) {
                                        finalScore = SmallInfinity;
                                    }
                                    else if (score >= 3) {
                                        finalScore = BigSmallInfinity;
                                    }
                                    else {
                                        finalScore = score * score;
                                    }
                                }
                                else {
                                    //haveOposite, not same, not blocked, blockedOposite, not iAm
                                    if (score >= 4) {
                                        finalScore = SmallInfinity;
                                    }
                                    else if (scoreOposite >= 4) {
                                        finalScore = BigInfinity;
                                    }
                                    else if (score >= 3) {
                                        finalScore = SmallSmallInfinity;
                                    }
                                    else {
                                        finalScore = score * score;
                                    }
                                }
                            }
                            else {
                                if (touched == iAm) {
                                    //haveOposite, not same, not blocked, not blockedOposite, iAm
                                    if (score >= 4) {
                                        finalScore = BigInfinity;
                                    }
                                    else if (score >= 3) {
                                        finalScore = BigSmallInfinity;
                                    }
                                    else {
                                        finalScore = score * score;
                                    }
                                }
                                else {
                                    //haveOposite, not same, not blocked, not blockedOposite, not iAm
                                    if (score >= 4) {
                                        finalScore = SmallInfinity;
                                    }
                                    else if (score >= 3) {
                                        finalScore = SmallSmallInfinity;
                                    }
                                    else {
                                        finalScore = score * score;
                                    }
                                }
                            }
                        }
                    }
                }
                else {
                    if (blocked) {
                        if (touched == iAm) {
                            //not haveOposite, blocked, iAm
                            if (score >= 4) {
                                finalScore = BigInfinity;
                            }
                            else {
                                finalScore = score;
                            }
                        }
                        else {
                            //not haveOposite, blocked, not iAm
                            if (score >= 4) {
                                finalScore = SmallInfinity;
                            }
                            else {
                                finalScore = score;
                            }
                        }
                    }
                    else {
                        if (touched == iAm) {
                            //not haveOposite, not blocked, iAm
                            if (score >= 4) {
                                finalScore = BigInfinity;
                            }
                            else if (score >= 3) {
                                finalScore = BigSmallInfinity;
                            }
                            else {
                                finalScore = score * score;
                            }
                        }
                        else {
                            //not haveOposite, not blocked, not iAm
                            if (score >= 4) {
                                finalScore = SmallInfinity;
                            }
                            else if (score >= 3) {
                                finalScore = SmallSmallInfinity;
                            }
                            else {
                                finalScore = score * score;
                            }
                        }
                    }
                }
                finalSum += finalScore;
            }

            return finalSum;
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

        private (int, List<(int, CrossCircle, bool)>) GetPoints(int x, int y, List<(int, int)> directions) {
            if (directions.Count == 0) {
                return (0, null);
            }

            List<(int, CrossCircle, bool)> successFirstTouchedBlocked = new List<(int, CrossCircle, bool)>();

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
                            successFirstTouchedBlocked.Add((success, firstTouched, true));
                            //END Is Blocked
                            break;
                        }
                        if (this.gameTable[x + (i * dx), y + (i * dy)] == firstTouched) {
                            success++;
                        }
                        else {
                            if (this.gameTable[x + (i * dx), y + (i * dy)] == CrossCircle.NOTHING) {
                                //Not Blocked
                                successFirstTouchedBlocked.Add((success, firstTouched, false));
                                //END Not Blocked
                            }
                            else {
                                //Is Blocked
                                successFirstTouchedBlocked.Add((success, firstTouched, true));
                                //END Is Blocked
                            }
                            break;
                        }
                    }
                    i++;
                }
                sum += success;
            }

            return (sum, successFirstTouchedBlocked);
        }

        private int ReturnScoreMedium(int x, int y, List<(int, int)> directions) {
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
                            if (success == 4) {
                                success = success * success;
                            }
                            break;
                        }
                        if (this.gameTable[x + (i * dx), y + (i * dy)] == firstTouched) {
                            success++;
                        }
                        else {
                            if (this.gameTable[x + (i * dx), y + (i * dy)] == CrossCircle.NOTHING) {
                                success = success * success;
                            }
                            else {
                                if (success == 4) {
                                    success = success * success;
                                }
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

        private int ReturnScoreEasy(int x, int y, List<(int, int)> directions) {
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
                        if (this.gameTable[x + (i * dx), y + (i * dy)] == firstTouched) {
                            success++;
                        }
                        else {
                            if (this.gameTable[x + (i * dx), y + (i * dy)] == CrossCircle.NOTHING) {                                
                            }
                            else {
                                if (success == 4) {
                                    success = success * success;
                                }
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
