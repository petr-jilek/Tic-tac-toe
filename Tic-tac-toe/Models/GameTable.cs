using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tic_tac_toe.Models.Enums;

namespace Tic_tac_toe.Models
{
    [Serializable]
    public class GameTable
    {
        private int x_count;
        private int y_count;

        public int X_count { get => x_count; private set { } }
        public int Y_count { get => y_count; private set { } }

        private List<CrossCircle> table = new List<CrossCircle>();

        public int Count { get => table.Count; private set { } }

        public GameTable(int x_count, int y_count) {
            this.x_count = x_count;
            this.y_count = y_count;
        }


        public bool CheckWin(CrossCircle crossCircle, int index) {
            for (int j = 0; j < y_count; j++) {
                int success = 0;
                for (int i = 0; i < x_count; i++) {
                    if (this[i, j] == crossCircle) {
                        success++;
                        if (success == index) {
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
                        if (success == index) {
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
                        if (success == index) {
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
                        if (success == index) {
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
                        if (success == index) {
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
                        if (success == index) {
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

        public void Add(CrossCircle crossCircle) {
            this.table.Add(crossCircle);
        }

        public CrossCircle this[int x, int y] {
            get { return table[(x * y_count) + y]; }
            set { table[(x * y_count) + y] = value; }
        }

    }
}
