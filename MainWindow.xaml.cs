using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;


namespace Domination_Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _boardMargin = 5;
        private Point _mouseLocation;
        private int _x;
        private int _y;
        private Block? _selectedBlock;
        Game? _game;

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem? menuItem = sender as MenuItem;
            if (menuItem != null)
            {
                switch (menuItem.Name)
                {
                    case "start":
                        canvas.Children.Clear();
                        _game = new Game(_boardMargin, canvas);
                        break;
                    case "moves":
                        ShowMoves();
                        break;
                    case "exit":
                        this.Close();
                        break;
                }
            }
        }



        private void ShowMoves() 
        {
            // Read File Stuff
        }

        private void AddMoves()
        {
            // WriteStream Stuff
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            _mouseLocation = e.GetPosition(canvas);
            positionX.Content = "X: " + (int) _mouseLocation.X;
            positionY.Content = "Y: " + (int) _mouseLocation.Y;

            if (_game != null)
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if ((_mouseLocation.Y > _game.GameBoard.BoardMargin + (_game.GameBoard.BoardBlocks[i, j].CellSize * i))
                            && (_mouseLocation.Y < _game.GameBoard.BoardMargin + (_game.GameBoard.BoardBlocks[i, j].CellSize * (i + 1)))) 
                        {
                            gridY.Content = "Y: " + i;
                        }
                        if    (_mouseLocation.X > _game.GameBoard.BoardMargin + (_game.GameBoard.BoardBlocks[i, j].CellSize * j)
                            && (_mouseLocation.X < _game.GameBoard.BoardMargin + (_game.GameBoard.BoardBlocks[i, j].CellSize * (j + 1))))
                        {
                            gridX.Content = "X: " + j;
                        }
                    }
                }
            }
        }

        private void canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_game != null) 
            {
                _selectedBlock = _game.GameBoard.BoardBlocks[_x,_y];
                _game.TryPlaceTile(_selectedBlock);
            }
        }
    }
}