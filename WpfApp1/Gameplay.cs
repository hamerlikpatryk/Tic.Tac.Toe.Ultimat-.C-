using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace WpfApp1
    {
        class Gameplay
        {
            private Board board;
            private Move aMove;
            private AlphaBeta AI;
            private short boardNumberToPlayOn;
            private short boardNumberPlayedOn;
            private bool isMoveUnlimited;
            private bool xTurn;

            /// <summary>
            /// Default Constructor
            /// </summary>
            public Game()
            {
                xTurn = true;           // Player will always be X and go first
                isMoveUnlimited = true; // Player may choose any move at the beginning of the game
                board = new Board();
                aMove = new Move();
                AI = new AlphaBeta();
            }

            /// <summary>
            /// This function handles the AI function calls and return values. Then returns the values that the controller
            ///     needs to update the GUI.
            /// </summary>
            /// 
            /// <returns>
            ///     Tuple (char1, char2, short1, short2, short3)
            ///     {
            ///         char1 = either 'O' for AI's move, or game status (see MadeMove code for reference)
            ///         char2 = only used if game is over or mini game is complete (see MadeMove code for reference)
            ///         short1 = the board number the AI played on
            ///         short2 = the AI's move row number
            ///         short3 = the AI's move col number
            ///     }
            /// </returns>
            public Tuple<char, char, short, short, short> MakeAIMove()
            {
                // The second paramter of the AI.MakeAIMove() call is the hard coded value for the AI algorithm's depth
                Node AINode = new Node(AI.MakeAIMove(new Node(boardNumberPlayedOn, boardNumberToPlayOn, aMove.row, aMove.col), 7, false));
                Tuple<bool, char, char> moveResults = MadeMove(AINode.BoardNumberPlayedOn, AINode.Row, AINode.Col);
                return new Tuple<char, char, short, short, short>(moveResults.Item2, moveResults.Item3, AINode.BoardNumberPlayedOn, AINode.Row, AINode.Col);
            }

            /// <summary>
            /// The majority of the model's work is done in this function. It validates the move, calls the 
            ///     update function, processes the move, and returns a tuple that represents the move's result in the game.
            /// </summary>
            /// <param name="boardNum">Number of the board that was played on</param>
            /// <param name="moveRow">The row number (0-2) of the move within a mini game</param>
            /// <param name="moveCol">The column number (0-2) of the move within a mini game</param>
            /// <returns>
            ///     Bool for valid move,
            ///     char #1 for who moved (if valid) or for the mini or main game state,
            ///     char #2 for winner, tie, or just regular move.</returns>
            public Tuple<bool, char, char> MadeMove(short boardNum, short moveRow, short moveCol)
            {
                boardNumberPlayedOn = boardNum;
                aMove = new Move(moveRow, moveCol);

                if (IsValidMove())
                {
                    if (!xTurn)
                        UpdateBoard();

                    boardNumberToPlayOn = board.MainBoardCoord(moveRow, moveCol);

                    if (board.BoardComplete(board.MiniGames[boardNumberToPlayOn]).Item1)
                        isMoveUnlimited = true;
                    else
                        isMoveUnlimited = false;

                    UpdateBoard();

                    Tuple<bool, char> moveResult = IsMiniGameOver(boardNum);

                    if (moveResult.Item1)
                    {
                        // Mini Game is complete
                        Tuple<short, short> boardCoords = board.BoardCoord(boardNumberPlayedOn);
                        board.Main[boardCoords.Item1, boardCoords.Item2] = moveResult.Item2;

                        Tuple<bool, char> gameState = IsGameOver();

                        if (gameState.Item1)
                            return new Tuple<bool, char, char>(true, 'G', gameState.Item2);     // Game over (item2 has winner/tie)
                        else
                        {
                            TurnManager();
                            return new Tuple<bool, char, char>(true, 'M', moveResult.Item2);    // Mini game is complete (item2 has winner/tie)
                        }
                    }
                    else if (xTurn)
                    {
                        TurnManager();
                        return new Tuple<bool, char, char>(true, 'X', 'B');     // Valid move, no win/tie
                    }
                    else
                    {
                        TurnManager();
                        return new Tuple<bool, char, char>(true, 'O', 'B');     // Valid move, no win/tie
                    }
                }
                else
                    return new Tuple<bool, char, char>(false, 'B', 'B');         // Not a valid move
            }

            /// <summary>
            /// This boolean function determines if the move was valid. The move is previously stored in the Move object called 'aMove'.
            /// </summary>
            /// <returns>A bool (True/False)</returns>
            private bool IsValidMove()
            {
                if (!xTurn)
                    return true;

                if (isMoveUnlimited)
                {
                    if (board.BoardComplete(board.MiniGames[boardNumberPlayedOn]).Item1)
                        return false;       // Not a valid move
                    else if (board.MiniGames[boardNumberPlayedOn][aMove.row, aMove.col] == 'B')
                        return true;        // Valid move
                }
                else
                    if (!(boardNumberPlayedOn == boardNumberToPlayOn))  // Check if desired move is located in the required mini game
                    return false;       // Not a valid move

                // If mini game has been finished, do not allow move within mini game
                if (!board.BoardComplete(board.MiniGames[boardNumberToPlayOn]).Item1)
                {
                    // If the player may move anywhere and move is available; then move is valid
                    if (board.MiniGames[boardNumberPlayedOn][aMove.row, aMove.col] == 'B')
                        return true;       // Valid move
                    else
                        return false;      // Not a valid move
                }
                else
                    return false;          // Not a valid move
            }

            /// <summary>
            /// This function updates the game board. It uses global variables to update the game board appropriately.
            /// </summary>
            private void UpdateBoard()
            {
                if (xTurn)
                    board.MiniGames[boardNumberPlayedOn][aMove.row, aMove.col] = 'X';
                else
                    board.MiniGames[boardNumberPlayedOn][aMove.row, aMove.col] = 'O';

                Tuple<bool, char> boardState = board.BoardComplete(board.MiniGames[boardNumberPlayedOn]);

                if (boardState.Item1)
                {
                    Tuple<short, short> boardCoords = board.BoardCoord(boardNumberPlayedOn);
                    board.Main[boardCoords.Item1, boardCoords.Item2] = boardState.Item2;
                }
            }

            /// <summary>
            /// This function checks the game board for an overall win (a big game win).
            /// </summary>
            /// <returns>  
            /// A tuple with a boolean and char. If the game is over, the boolean is 
            ///     true and the char is used to determine who won or if the game is a tie.
            ///     Otherwise, the boolean is just false and the char is not used.
            /// </returns>
            public Tuple<bool, char> IsGameOver()
            {
                return board.BoardComplete(board.Main);
            }

            /// <summary>
            /// This function checks the mini game board for a win or tie state.
            /// </summary>
            /// <param name="boardNum">The number of a mini game board</param>
            /// <returns>
            /// A tuple with a boolean and char. If the mini game is over, the boolean is 
            ///     true and the char is used to determine who won or if the mini game is a tie.
            ///     Otherwise, the boolean is just false and the char is not used.
            /// </returns>
            private Tuple<bool, char> IsMiniGameOver(short boardNum)
            {
                return board.BoardComplete(board.MiniGames[boardNum]);
            }

            /// <summary>
            /// This function reduces the repetitive code that flips the xTurn boolean. This is 
            ///     important for the model to determine if it's the player's turn or AI's move.
            ///     The AI's move is treated slightly differently than the player's move.
            /// </summary>
            private void TurnManager()
            {
                if (xTurn)
                    xTurn = false;
                else
                    xTurn = true;
            }

            /// <summary>
            /// This function gets the Board Number To Play On (shorthand is BNTPO) if the move is limited to a 
            ///     specific mini game. 
            /// </summary>
            /// <returns>
            /// -1 if the move may be placed anywhere in the game (determined via isMoveUnlimited boolean) OR
            ///     the boardNumberToPlayOn value.
            /// </returns>
            public short GetBNTPO()
            {
                if (isMoveUnlimited)
                    return (short)-1;
                else
                    return (short)boardNumberToPlayOn;
            }

            /// <summary>
            /// Gets the value of the boolean private member 'xTurn'
            /// </summary>
            /// <returns>The value of the private boolean called 'xTurn'</returns>
            public bool isXturn()
            {
                return xTurn;
            }
        }
    }
}
}
