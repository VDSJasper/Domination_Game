using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Domination_Game
{
    internal class Game
    {
        private Board _board;
        public Board GameBoard { get => _board; }
        public Game(int margin, Canvas canvas) 
        {
            _board = new Board(margin, canvas);
        }

        public void TryPlaceTile(Block block) 
        {
            // Do a check if placement is possible
        }
    }
}
