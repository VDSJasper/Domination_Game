using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml.Linq;

namespace Domination_Game
{
    internal class Game
    {
        private Board _board;
        public Board GameBoard { get => _board; }
        public Players CurrentPlayer { get; private set; }

        public MainWindow? GameWindow { get; set; }
        public Game(int margin, Canvas canvas) 
        {
            _board = new Board(margin, canvas);
            CurrentPlayer = Players.Red;
        }

        public bool TryPlaceTile(int x, int y) 
        {
            if (HasMovesLeft())
            {
                Block block1 = _board.BoardBlocks[y, x];
                Block block2;
                bool possible = false;
                if (!block1.Occupied)
                {
                    if (CurrentPlayer == Players.Red)
                    {
                        if ((y + 1 < 8) && !(_board.BoardBlocks[y + 1, x].Occupied))
                        {
                            block2 = _board.BoardBlocks[y + 1, x];
                            _board.PlaceTile(block1, block2, Colors.Red);
                            possible = true;
                            return possible;
                        }
                    }
                    if (CurrentPlayer == Players.Blue)
                    {
                        if ((x + 1 < 8) && !(_board.BoardBlocks[y, x + 1].Occupied))
                        {
                            block2 = _board.BoardBlocks[y, x + 1];
                            _board.PlaceTile(block1, block2, Colors.Blue);                           
                            possible = true;
                            return possible;
                        }
                    }
                }
                if (!possible)
                {
                    throw new DominationException("Can't place this tile here.");
                }
            }
            else 
            {
                EndOfGame();
            }
            return false;
        }

        private bool HasMovesLeft() 
        {
            int rowCounter;
            int colCounter;
            for (int i = 0; i < _board.BoardBlocks.GetLength(0); i++)
            {
                rowCounter = 0;
                colCounter = 0;
                for (int j = 0; j < _board.BoardBlocks.GetLength(0); j++)
                {
                    if (!(_board.BoardBlocks[i, j].Occupied))
                    {
                        rowCounter++;
                    }
                    else 
                    {
                        rowCounter = 0;
                    }

                    if (!(_board.BoardBlocks[j, i].Occupied))
                    {
                        colCounter++;
                    }
                    else
                    {
                        colCounter = 0;
                    }

                    if (CurrentPlayer == Players.Blue && rowCounter > 1)
                    { 
                        return true;
                    }
                    if (CurrentPlayer == Players.Red && colCounter > 1)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void NextPlayer() 
        {
            if (CurrentPlayer == Players.Red)
            {
                CurrentPlayer = Players.Blue;
            }
            else
            {
                CurrentPlayer = Players.Red;
            }
        }

        private void EndOfGame() 
        {
            GameWindow?.NoRunningGame();
            MessageBox.Show($"{CurrentPlayer} has no more moves. Game over");
        }
    }    
}

