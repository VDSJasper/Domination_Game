using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Domination_Game
{
    internal class Board
    {
        private Block[,] _cells = new Block[8,8];
        private Rectangle _rect;

        public int BoardWidth { get; private set; }
        public int BoardHeight { get; private set; }
        public int BoardMargin { get; private set; }
        public Block[,] BoardBlocks { get => _cells; }

        public Board(int margin, double width, double height) 
        {
            BoardWidth = (int) (width - (margin * 2));
            BoardHeight = (int) (height - (margin * 2));
            BoardMargin = margin;

            _rect = new Rectangle();
            _rect.Margin = new Thickness(margin, margin, 0, 0);
            _rect.Width = BoardWidth;
            _rect.Height = BoardHeight;
            _rect.Stroke = new SolidColorBrush(Colors.Black);
            CreateField();
        }
        private void CreateField()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++) 
                {
                    Block block = new Block(i,j, this);
                    _cells[j,i] = block;
                }
            }
        }
        public void PlaceTile(Block block1, Block block2, Color fillColor) 
        {
            block1.FillBlock(fillColor);
            block2.FillBlock(fillColor);
        }

        public Rectangle GetBoard(out Block[,] cells)
        {
            cells = _cells;
            return _rect;
        }
    }
}
