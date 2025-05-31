using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;

namespace Domination_Game
{
    internal class Block
    {
        private SolidColorBrush _brush = new SolidColorBrush(Colors.Black);
        private Rectangle _rect;
        public Rectangle BlockImage { get => _rect; }
        public Point BlockPosition { get; private set; }
        public int CellSize { get; private set; }
        public bool Occupied { get; private set; }
        public Block(int xLocation, int yLocation, Board board) 
        {
            CellSize = board.BoardWidth / 8;
            int leftCorner = board.BoardMargin + (xLocation * CellSize);
            int upperCorner = board.BoardMargin + (yLocation * CellSize);
            BlockPosition = new Point(leftCorner, upperCorner);

            _rect = new Rectangle()
            {
                Margin = new Thickness(leftCorner, upperCorner, 0, 0),
                Width = CellSize,
                Height = CellSize,
                Stroke = _brush,
                StrokeThickness = 0.3
            };
        }

        public void FillBlock(Color fillColor) 
        {
            Occupied = true;
            _rect.Fill = new SolidColorBrush(fillColor);
        }
    }
}
