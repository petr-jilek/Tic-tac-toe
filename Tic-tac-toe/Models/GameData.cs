using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tic_tac_toe.Models.Enums;

namespace Tic_tac_toe.Models
{
    public class GameData
    {
        public GameType gameType;
        public MapSize mapSize;
        public CrossCircle player;

        public int rounds;
        public CrossCircle winner;

        public GameData(GameType gameType) {
            this.gameType = gameType;
            rounds = 0;
        }

    }
}