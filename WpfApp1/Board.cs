using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class Board
    {
        // All mini-games in the game board
        public List<char[,]> MiniGames;

        // Main (big) game board
        public char[,] Main;

        /// <summary>
        ///     default constructor
        /// </summary>
        public Board()
        {
            this.MiniGames = new List<char[,]>();
            this.Main = new char[3, 3];
            this.InitBoards();
        }

        /// <summary>
        ///     Copy contructor
        /// </summary>
        /// <param name="b"></param>
        public Board(Board b)
        {
            this.MiniGames = new List<char[,]>();
            this.Main = new char[3, 3];
            this.InitBoards();

            for (short x = 0; x < 9; x++)
                for (short y = 0; y < 3; y++)
                    for (short z = 0; z < 3; z++)
                    {
                        this.MiniGames[x][y, z] = b.MiniGames[x][y, z];
                        if (x == 8)
                        {
                            this.Main[y, z] = b.Main[y, z];
                        }
                    }
        }

        /// <summary>
        ///    Makes every space on the board a blank space
        /// </summary>
        private void InitBoards()
        {
            for (short x = 0; x < 9; x++)
            {
                this.MiniGames.Add(new char[3, 3]);
                for (short y = 0; y < 3; y++)
                    for (short z = 0; z < 3; z++)
                    {
                        this.MiniGames[x][y, z] = 'B';
                        this.Main[y, z] = 'B';
                    }
            }
        }


        /// <summary>
        ///     Check if a win is in the right diagonal
        /// </summary>
        /// <param name="board">
        /// 
        ///     Board to be evaluated
        /// 
        /// </param>
        /// <returns>
        /// 
        ///     X, O or N, for X win, O win, or No winner
        /// 
        /// </returns>
        private char CheckRightDiaganolWin(char[,] board)
        {
            short xPlayer = 0;
            short oPlayer = 0;

            for (short row = 0; row < 3; row++)
                for (short col = 0; col < 3; col++)
                {
                    if (row == col && board[row, col] == 'X')
                        xPlayer++;
                    else if (row == col && board[row, col] == 'O')
                        oPlayer++;

                    if (xPlayer == 3)
                        return 'X';
                    else if (oPlayer == 3)
                        return 'O';
                }

            return 'N';
        }


        /// <summary>
        ///     
        ///     Determines if a win is in the left diagonal
        /// 
        /// </summary>
        /// <param name="board">
        /// 
        ///     board to be evaluated
        /// 
        /// </param>
        /// <returns>
        /// 
        ///     X, O or N, for X win, O win, or No winner
        /// 
        /// </returns>
        private char CheckLeftDiaganolWin(char[,] board)
        {
            short xPlayer = 0;
            short oPlayer = 0;

            for (short row = 0; row < 3; row++)
                for (short col = 0; col < 3; col++)
                {
                    if (row + col == 2 && board[row, col] == 'X')
                        xPlayer++;
                    else if (row + col == 2 && board[row, col] == 'O')
                        oPlayer++;

                    if (xPlayer == 3)
                        return 'X';
                    else if (oPlayer == 3)
                        return 'O';
                }

            return 'N';
        }


        /// <summary>
        ///     
        ///     Checks for a win recursively in every row
        /// 
        /// </summary>
        /// <param name="board">
        /// 
        ///     board to be evaluated
        /// 
        /// </param>
        /// <param name="row">
        /// 
        ///     row in the board to be incremented, for recursive calls
        /// 
        /// </param>
        /// <returns>
        /// 
        ///     X, O or N, for X win, O win, or No winner
        /// 
        /// </returns>
        private char CheckRowWin(char[,] board, short row)
        {
            short xPlayer = 0;
            short oPlayer = 0;

            for (short col = 0; col < 3; col++)
            {
                if (board[row, col] == 'X')
                    xPlayer++;
                else if (board[row, col] == 'O')
                    oPlayer++;
            }

            if (xPlayer == 3)
                return 'X';
            else if (oPlayer == 3)
                return 'O';
            else if (row < 2)
                return CheckRowWin(board, ++row);
            else
                return 'N';
        }


        /// <summary>
        ///     
        ///     Checks for a win recursively in every column
        /// 
        /// </summary>
        /// <param name="board">
        /// 
        ///     board to be evaluated
        /// 
        /// </param>
        /// <param name="col">
        /// 
        ///     column in the board to be incremented, for recursive calls
        /// 
        /// </param>
        /// <returns>
        /// 
        ///     X, O or N, for X win, O win, or No winner
        /// 
        /// </returns>
        private char CheckColWin(char[,] board, short col)
        {
            short xPlayer = 0;
            short oPlayer = 0;

            for (short row = 0; row < 3; row++)
            {
                if (board[row, col] == 'X')
                    xPlayer++;
                else if (board[row, col] == 'O')
                    oPlayer++;
            }

            if (xPlayer == 3)
                return 'X';
            else if (oPlayer == 3)
                return 'O';
            else if (col < 2)
                return CheckColWin(board, ++col);
            else
                return 'N';
        }


        /// <summary>
        ///     
        ///     Checks for a tie in the given board
        /// 
        /// </summary>
        /// <param name="board">
        /// 
        ///     Board to be evaluated
        /// 
        /// </param>
        /// <returns>
        /// 
        ///     Tuple containing boolean for if the game is tied or not, and N or T for 
        ///     No win or Tie.
        /// 
        /// </returns>
        private Tuple<bool, char> CheckTie(char[,] board)
        {
            // If a mini game is still in progress then return false, 'N'
            for (short row = 0; row < 3; row++)
                for (short col = 0; col < 3; col++)
                    if (board[row, col] == 'B')
                        return new Tuple<bool, char>(false, 'N');

            return new Tuple<bool, char>(true, 'T');
        }

        /// <summary>
        /// 
        ///     Returns true/false if game is won and a char for who won, O or X.
        ///     Otherwise, checks for a tie.
        /// 
        /// </summary>
        /// <param name="board">
        /// 
        ///     Board to be evaluated
        /// 
        /// </param>
        /// <returns>
        /// 
        ///     Tuple containing boolean for if the game is tied or not, and X or O for 
        ///     X win or O win.
        /// 
        /// </returns>
        public Tuple<bool, char> BoardComplete(char[,] board)
        {
            List<char> results = new List<char>();

            results.Add(this.CheckRightDiaganolWin(board));
            results.Add(this.CheckLeftDiaganolWin(board));
            results.Add(this.CheckRowWin(board, 0));
            results.Add(this.CheckColWin(board, 0));

            if (results.Find(x => x == 'X') == 'X')
                return new Tuple<bool, char>(true, 'X');
            else if (results.Find(x => x == 'O') == 'O')
                return new Tuple<bool, char>(true, 'O');
            else
                return CheckTie(board);

        }


        /// <summary>
        /// 
        ///  Returns all open moves for a given move.
        /// 
        /// </summary>
        /// <param name="board">
        /// 
        ///     Given board to evaluate
        /// 
        /// </param>
        /// <param name="boardNum">
        /// 
        ///     0 | 1 | 2
        ///   --------------
        ///     3 | 4 | 5
        ///   --------------
        ///     6 | 7 | 8
        /// 
        /// </param>
        /// <returns>
        /// 
        ///     List of all open moves contained in the given board.
        ///     
        ///     short1: Row of move
        ///     short2: Column of move
        ///     short3: Board number of move (see chart above)
        /// 
        /// </returns>
        public List<Tuple<short, short, short>> GetOpenMoves(char[,] board, short boardNum)
        {
            List<Tuple<short, short, short>> moves = new List<Tuple<short, short, short>>();
            for (short i = 0; i < 3; i++)
                for (short j = 0; j < 3; j++)
                {
                    if (board[i, j] == 'B')
                        moves.Add(new Tuple<short, short, short>(i, j, boardNum));
                }

            return moves;
        }

        /// <summary>
        /// 
        ///     Returns (Row, Col) that equals the 0-8 boardNum move.
        ///     
        /// 
        ///     00 | 01 | 02
        ///   --------------
        ///     10 | 11 | 12
        ///   --------------
        ///     20 | 21 | 22
        /// 
        ///     ==>
        /// 
        ///     0 | 1 | 2
        ///   --------------
        ///     3 | 4 | 5
        ///   --------------
        ///     6 | 7 | 8
        /// 
        /// </summary>
        /// <param name="boardNum">
        /// 
        ///     Board number (0-8)
        /// 
        /// </param>
        /// <returns>
        /// 
        ///     (Row, Col) representing boardNum
        /// 
        /// </returns>
        public Tuple<short, short> BoardCoord(short boardNum)
        {
            switch (boardNum)
            {
                case 0: return new Tuple<short, short>(0, 0);
                case 1: return new Tuple<short, short>(0, 1);
                case 2: return new Tuple<short, short>(0, 2);
                case 3: return new Tuple<short, short>(1, 0);
                case 4: return new Tuple<short, short>(1, 1);
                case 5: return new Tuple<short, short>(1, 2);
                case 6: return new Tuple<short, short>(2, 0);
                case 7: return new Tuple<short, short>(2, 1);
                default: return new Tuple<short, short>(2, 2);
            }
        }


        /// <summary>
        /// 
        ///     Returns board number that equals the (row, col) move.
        /// 
        ///     00 | 01 | 02
        ///   --------------
        ///     10 | 11 | 12   (Mini-game)
        ///   --------------
        ///     20 | 21 | 22
        /// 
        ///     ==>
        /// 
        ///     0 | 1 | 2
        ///   --------------  (Overall game)
        ///     3 | 4 | 5
        ///   --------------
        ///     6 | 7 | 8
        ///     
        /// 
        /// </summary>
        /// <param name="row">
        /// 
        ///     0, 1, or 2
        ///     
        ///     Row from 3x3 board
        /// 
        /// </param>
        /// <param name="col">
        /// 
        ///     0, 1, or 2
        ///     
        ///     Column from 3x3 board
        /// 
        /// </param>
        /// <returns></returns>
        public short MainBoardCoord(short row, short col)
        {
            if (row == 0 && col == 0)
                return 0;
            else if (row == 0 && col == 1)
                return 1;
            else if (row == 0 && col == 2)
                return 2;
            else if (row == 1 && col == 0)
                return 3;
            else if (row == 1 && col == 1)
                return 4;
            else if (row == 1 && col == 2)
                return 5;
            else if (row == 2 && col == 0)
                return 6;
            else if (row == 2 && col == 1)
                return 7;
            else
                return 8;
        }
    }
}
}
