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
        private CrossCircle player;
        private GameType gameType;

        public CrossCircle Player { get => this.player; private set { } }
        public GameType GameType { get => this.gameType; private set { } }

        public GameData(CrossCircle player, GameType gameType) {
            this.player = player;
            this.gameType = gameType;
        }

    }
}