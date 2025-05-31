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
        public bool EndOfGame { get; private set; }

        public Game(int margin, double width, double height) 
        {
            EndOfGame = false;
            _board = new Board(margin, width, height);
            CurrentPlayer = Players.Red;
        }

        public bool TryPlaceTile(int x, int y) 
        {
            if (HasMovesLeft())
            {
                Block block1 = _board.BoardBlocks[y, x];
                Block block2;

                if (!block1.Occupied)
                {
                    if (CurrentPlayer == Players.Red)
                    {
                        if ((y + 1 < _board.BlocksPerRow) && !(_board.BoardBlocks[y + 1, x].Occupied))
                        {
                            block2 = _board.BoardBlocks[y + 1, x];
                            _board.PlaceTile(block1, block2, Colors.Red);
                            return true;
                        }
                    }
                    if (CurrentPlayer == Players.Blue)
                    {
                        if ((x + 1 < _board.BlocksPerRow) && !(_board.BoardBlocks[y, x + 1].Occupied))
                        {
                            block2 = _board.BoardBlocks[y, x + 1];
                            _board.PlaceTile(block1, block2, Colors.Blue);                           
                            return true;
                        }
                    }
                }
                throw new DominationException("Can't place this tile here.");                
            }
            else 
            {
                EndOfGame = true;
            }
            return false;
        }

        public bool HasMovesLeft() 
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
            EndOfGame = true;
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
    }    
}

