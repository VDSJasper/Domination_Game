﻿using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Domination_Game
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int _boardMargin = 5;
        private Point _mouseLocation;
        private int _x;
        private int _y;
        private string _path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private string _filePath;
        Game? _game;
        MenuItem _movesItem;

        public MainWindow()
        {
            InitializeComponent();
            _filePath = System.IO.Path.Combine(_path, "domination.txt");
            using StreamWriter writer = File.CreateText(_filePath);
            writer.WriteLine("All moves made this game:");
            _movesItem = (MenuItem)menuBar.FindName("moves");
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem? menuItem = sender as MenuItem;
            if (menuItem != null)
            {
                switch (menuItem.Name)
                {
                    case "start":
                        StartGame();
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

        private void StartGame()
        {
            canvas.Children.Clear();
            _game = new Game(_boardMargin, canvas.Width, canvas.Height);
            Rectangle board = _game.GameBoard.GetBoard(out Block[,] cells);
            canvas.Children.Add(board);
            foreach (Block block in cells)
            {
                canvas.Children.Add(block.BlockImage);
            }
            playerToPlay.Content = Convert.ToString(_game.CurrentPlayer);
            _movesItem.IsEnabled = false;
        }

        private void ShowMoves() 
        {
            MovesWindow _movesWindow = new MovesWindow(_filePath);
            _movesWindow.Show();
        }

        private void AddMoves(int x, int y, Players player)
        {
            int x2 = x;
            int y2 = y;
            if (player == Players.Red)
            {
                y2 = y++;
            }
            else if (player == Players.Blue)
            {
                x2 = x++;
            }
            using StreamWriter writer = File.AppendText(_filePath);
            writer.WriteLine($"{player}: X{x} Y{y} And X{x2} Y{y2}");
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            _mouseLocation = e.GetPosition(canvas);
            positionX.Content = "X: " + (int) _mouseLocation.X;
            positionY.Content = "Y: " + (int) _mouseLocation.Y;

            if (_game != null)
            {
                for (int i = 0; i < _game.GameBoard.BlocksPerRow; i++)
                {
                    for (int j = 0; j < _game.GameBoard.BlocksPerRow; j++)
                    {
                        if ((_mouseLocation.Y > _game.GameBoard.BoardMargin + (_game.GameBoard.BoardBlocks[i, j].CellSize * i))
                            && (_mouseLocation.Y < _game.GameBoard.BoardMargin + (_game.GameBoard.BoardBlocks[i, j].CellSize * (i + 1)))) 
                        {
                            _y = i;
                            gridY.Content = "Y: " + i;
                        }
                        if    (_mouseLocation.X > _game.GameBoard.BoardMargin + (_game.GameBoard.BoardBlocks[i, j].CellSize * j)
                            && (_mouseLocation.X < _game.GameBoard.BoardMargin + (_game.GameBoard.BoardBlocks[i, j].CellSize * (j + 1))))
                        {
                            _x = j;
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
                try
                {
                    bool tilePlaced = _game.TryPlaceTile(_x, _y);
                    playerToPlay.Content = Convert.ToString(_game.CurrentPlayer);
                    if (tilePlaced)
                    {
                        AddMoves(_x, _y, _game.CurrentPlayer);
                        _game.NextPlayer();
                        if (!_game.HasMovesLeft())
                        {
                            GameOver();
                        }
                    }
                }
                catch (DominationException moveException) 
                {
                    MessageBox.Show(moveException.Message);
                }
            }
        }

        private void GameOver()
        {
            if (_game.EndOfGame)
            {
                MessageBox.Show($"{_game.CurrentPlayer} has no more moves. Game over");
            }
        }
        public void NoRunningGame() 
        {
            _movesItem.IsEnabled = true;
        }
    }
}