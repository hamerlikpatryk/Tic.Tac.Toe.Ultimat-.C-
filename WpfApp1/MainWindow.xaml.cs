using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApp1

{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class mainWindow1 : Window1
    {

    }
    public partial class MainWindow : Window
    {
        //Button[] btn = new Button[]; 

       #region Private Members

        private Algorithm_Rules[] mResults; // Holds activated cells in game

        private bool Player1Turn; //True if it's player 1 turn (X) or player 2 turn (O) 

        private bool GameEnded; //Tru if game has ended

        private AiMove[] mMove;

        #endregion
  

         #region Constructor


        // Default constructor

        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }


        #endregion
         

        // starts new game and clear last one
        private void NewGame()
        {
            mResults = new Algorithm_Rules[81]; //Create a new blank array of free cells

            for (var i = 0; i < mResults.Length; i++)
                mResults[i] = Algorithm_Rules.Free;

            //Be sure Player 1 starts game
            Player1Turn = true;

            Board.Children.Cast<Button>().ToList().ForEach(button => //iterate every bottom of the grid (Loop)
            {
                button.Content = string.Empty; //Clean all buttons
                button.Background = Brushes.BurlyWood; //Clean Background            
            });
          
        }
       /// <summary>
       /// Handles a button click event
       /// </summary>
       /// <param name="sender"> The button tha was clicked </param>
       /// <param name="e"> The events of the click </param>
       /// 
        private void BClick(object sender, RoutedEventArgs e)
        { 
            if (GameEnded) //starts new game after the end of previous
            {
                NewGame();
                    return;
            }
            // Cast a sender to a button
            var button = (Button) sender;

            //Find the buttons position in the array
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column +(row * 9);

            //Block if button is already clicked 
            if (mResults[index] != Algorithm_Rules.Free)
                return;

            //Set value based on which player turn is            
            mResults[index] = Player1Turn ? Algorithm_Rules.Cross : Algorithm_Rules.Nought ; // If it is Player1 turn (true), use Cross if not (false) use Nought ((abc ? X : O))

            //Set button tekst to the result
            button.Content = Player1Turn ? "X" : "O";

            // Toggle the players turns
            Player1Turn ^= true;
            //Check for the winner
            CheckForWinner();

            //AI
           /* if  (Player1Turn != true)
            {
                AiMove;
            } 
            */
            ///<summary>
            ///Check if there is a winner of 3 line straight 
            /// </summary>


            void CheckForWinner()  //Winning Algorithm 
        {
        #region horizontal win
            //check for horizontal win

            // B0 Row 0, left 
            if (mResults[0] != Algorithm_Rules.Free && (mResults[0] & mResults[1] & mResults[2]) == mResults[0])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B0_0_0.Background = B0_1_0.Background = B0_2_0.Background = Brushes.Purple;
            }
            // B0 Row 1 left
            if (mResults[9] != Algorithm_Rules.Free && (mResults[9] & mResults[10] & mResults[11]) == mResults[9])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B0_0_1.Background = B0_1_1.Background = B0_2_1.Background = Brushes.Purple;
            }
            // B0 Row 2 left
            if (mResults[18] != Algorithm_Rules.Free && (mResults[18] & mResults[19] & mResults[20]) == mResults[18])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B0_0_2.Background = B0_1_2.Background = B0_2_2.Background = Brushes.Purple;
            }          
            
            // B1 Row 0 middle
            if (mResults[3] != Algorithm_Rules.Free && (mResults[3] & mResults[4] & mResults[5]) == mResults[3])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B1_3_0.Background = B1_4_0.Background = B1_5_0.Background = Brushes.Purple;
            } 
            // B1 Row 1 middle
            if (mResults[12] != Algorithm_Rules.Free && (mResults[12] & mResults[13] & mResults[14]) == mResults[12])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B1_3_1.Background = B1_4_1.Background = B1_5_1.Background = Brushes.Purple;
            }
            // B1 Row 2 middle
            if (mResults[21] != Algorithm_Rules.Free && (mResults[21] & mResults[22] & mResults[23]) == mResults[21])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B1_3_2.Background = B1_4_2.Background = B1_5_2.Background = Brushes.Purple;
            }

            // B2 Row 0 right
            if (mResults[6] != Algorithm_Rules.Free && (mResults[6] & mResults[7] & mResults[8]) == mResults[6])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B2_6_0.Background = B2_7_0.Background = B2_8_0.Background = Brushes.Purple;
            }
            // B2 Row 1 right
            if (mResults[15] != Algorithm_Rules.Free && (mResults[15] & mResults[16] & mResults[17]) == mResults[15])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B2_6_1.Background = B2_7_1.Background = B2_8_1.Background = Brushes.Purple;
            }
            // B2 Row 2 right
            if (mResults[24] != Algorithm_Rules.Free && (mResults[24] & mResults[25] & mResults[26]) == mResults[24])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B2_6_2.Background = B2_7_2.Background = B2_8_2.Background = Brushes.Purple;
            }

            // B3 Row 3 left
            if (mResults[27] != Algorithm_Rules.Free && (mResults[27] & mResults[28] & mResults[29]) == mResults[27])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B3_0_3.Background = B3_1_3.Background = B3_2_3.Background = Brushes.Purple;
            }
            // B3 Row 4 left
            if (mResults[36] != Algorithm_Rules.Free && (mResults[36] & mResults[37] & mResults[38]) == mResults[36])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B3_0_4.Background = B3_1_4.Background = B3_2_4.Background = Brushes.Purple;
            }
            // B3 Row 5 left
            if (mResults[45] != Algorithm_Rules.Free && (mResults[45] & mResults[46] & mResults[47]) == mResults[45])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B3_0_5.Background = B3_1_5.Background = B3_2_5.Background = Brushes.Purple;

            }

            //B4 Row 3 middle
            if (mResults[30] != Algorithm_Rules.Free && (mResults[30] & mResults[31] & mResults[32]) == mResults[30])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B4_3_3.Background = B4_4_3.Background = B4_5_3.Background = Brushes.Purple;
            }
            //B4 Row 4 middle
            if (mResults[39] != Algorithm_Rules.Free && (mResults[39] & mResults[40] & mResults[41]) == mResults[39])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B4_3_4.Background = B4_4_4.Background = B4_5_4.Background = Brushes.Purple;
            }
            //B4 Row 5 middle
            if (mResults[48] != Algorithm_Rules.Free && (mResults[48] & mResults[49] & mResults[50]) == mResults[48])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B4_3_5.Background = B4_4_5.Background = B4_5_5.Background = Brushes.Purple;

            }
      
            //B5 Row 3 right
            if (mResults[33] != Algorithm_Rules.Free && (mResults[33] & mResults[34] & mResults[35]) == mResults[33])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B5_6_3.Background = B5_7_3.Background = B5_8_3.Background = Brushes.Purple;
            }
            //B5 Row 4 right
            if (mResults[42] != Algorithm_Rules.Free && (mResults[42] & mResults[43] & mResults[44]) == mResults[42])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B5_6_4.Background = B5_7_4.Background = B5_8_4.Background = Brushes.Purple;

            }
            //B5 Row 5 right
            if (mResults[51] != Algorithm_Rules.Free && (mResults[51] & mResults[52] & mResults[53]) == mResults[51])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B5_6_5.Background = B5_7_5.Background = B5_8_5.Background = Brushes.Purple;

            }

            //B6 Row 6 left
            if (mResults[54] != Algorithm_Rules.Free && (mResults[54] & mResults[55] & mResults[56]) == mResults[54])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B6_0_6.Background = B6_1_6.Background = B6_2_6.Background = Brushes.Purple;

            }
            //B6 Row 7 left
            if (mResults[63] != Algorithm_Rules.Free && (mResults[63] & mResults[64] & mResults[65]) == mResults[63])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B6_0_7.Background = B6_1_7.Background = B6_2_7.Background = Brushes.Purple;

            }
             //B6 Row 8 left
            if (mResults[72] != Algorithm_Rules.Free && (mResults[72] & mResults[73] & mResults[74]) == mResults[72])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B6_0_8.Background = B6_1_8.Background = B6_2_8.Background = Brushes.Purple;

            }
            
            //B7 Row 6 middle
            if (mResults[57] != Algorithm_Rules.Free && (mResults[57] & mResults[58] & mResults[59]) == mResults[57])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B7_3_6.Background = B7_4_6.Background = B7_5_6.Background = Brushes.Purple;

            }
            //B7 Row 7 middle
            if (mResults[66] != Algorithm_Rules.Free && (mResults[66] & mResults[67] & mResults[68]) == mResults[66])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B7_3_7.Background = B7_4_7.Background = B7_5_7.Background = Brushes.Purple;

            }
            //B7 Row 8 middle
            if (mResults[75] != Algorithm_Rules.Free && (mResults[75] & mResults[76] & mResults[77]) == mResults[75])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B7_3_8.Background = B7_4_8.Background = B7_5_8.Background = Brushes.Purple;

            }

            //B8 Row 6 right
            if (mResults[60] != Algorithm_Rules.Free && (mResults[60] & mResults[61] & mResults[62]) == mResults[60])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B8_6_6.Background = B8_7_6.Background = B8_8_6.Background = Brushes.Purple;

            }
            //B8 Row 7 right
            if (mResults[69] != Algorithm_Rules.Free && (mResults[69] & mResults[70] & mResults[71]) == mResults[69])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B8_6_7.Background = B8_7_7.Background = B8_8_7.Background = Brushes.Purple;

            }
            //B8 Row 8 right
            if (mResults[78] != Algorithm_Rules.Free && (mResults[78] & mResults[79] & mResults[80]) == mResults[78])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B8_6_8.Background = B8_7_8.Background = B8_8_8.Background = Brushes.Purple;

            }
            #endregion

        #region vertical win
            //check for vertical win

            // B0 Column 0, up 
            if (mResults[0] != Algorithm_Rules.Free && (mResults[0] & mResults[9] & mResults[18]) == mResults[0])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B0_0_0.Background = B0_0_1.Background = B0_0_2.Background = Brushes.Purple;
            }
            // B0 Column 1 up
            if (mResults[1] != Algorithm_Rules.Free && (mResults[1] & mResults[10] & mResults[19]) == mResults[1])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B0_1_0.Background = B0_1_1.Background = B0_1_2.Background = Brushes.Purple;
            }
            // B0 Column 2 up
            if (mResults[2] != Algorithm_Rules.Free && (mResults[2] & mResults[11] & mResults[20]) == mResults[2])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B0_2_0.Background = B0_2_1.Background = B0_2_2.Background = Brushes.Purple;
            }

            // B1 Column 3 up
            if (mResults[3] != Algorithm_Rules.Free && (mResults[3] & mResults[12] & mResults[21]) == mResults[3])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B1_3_0.Background = B1_3_1.Background = B1_3_2.Background = Brushes.Purple;
            }
            // B1 Column 4 up
            if (mResults[4] != Algorithm_Rules.Free && (mResults[4] & mResults[13] & mResults[22]) == mResults[4])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B1_4_0.Background = B1_4_1.Background = B1_4_2.Background = Brushes.Purple;
            }
            // B1 Column 5 up
            if (mResults[5] != Algorithm_Rules.Free && (mResults[5] & mResults[14] & mResults[23]) == mResults[5])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B1_5_0.Background = B1_5_1.Background = B1_5_2.Background = Brushes.Purple;
            }

            // B2 Column 6 up
            if (mResults[6] != Algorithm_Rules.Free && (mResults[6] & mResults[15] & mResults[24]) == mResults[6])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B2_6_0.Background = B2_6_1.Background = B2_6_2.Background = Brushes.Purple;
            }
            // B2 Column 7 up
            if (mResults[7] != Algorithm_Rules.Free && (mResults[7] & mResults[16] & mResults[25]) == mResults[7])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B2_7_0.Background = B2_7_1.Background = B2_7_2.Background = Brushes.Purple;
            }
            // B2 Column 8 up
            if (mResults[8] != Algorithm_Rules.Free && (mResults[8] & mResults[17] & mResults[26]) == mResults[8])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B2_8_0.Background = B2_8_1.Background = B2_8_2.Background = Brushes.Purple;
            }

            // B3 Column 0 middle
            if (mResults[27] != Algorithm_Rules.Free && (mResults[27] & mResults[36] & mResults[45]) == mResults[27])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B3_0_3.Background = B3_0_4.Background = B3_0_5.Background = Brushes.Purple;
            }
            // B3 Column 1 middle
            if (mResults[28] != Algorithm_Rules.Free && (mResults[28] & mResults[37] & mResults[46]) == mResults[28])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B3_1_3.Background = B3_1_4.Background = B3_1_5.Background = Brushes.Purple;
            }
            // B3 Column 2 middle
            if (mResults[29] != Algorithm_Rules.Free && (mResults[29] & mResults[38] & mResults[47]) == mResults[29])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B3_2_3.Background = B3_2_4.Background = B3_2_5.Background = Brushes.Purple;

            }

            //B4 Column 3 middle
            if (mResults[30] != Algorithm_Rules.Free && (mResults[30] & mResults[39] & mResults[48]) == mResults[30])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B4_3_3.Background = B4_3_4.Background = B4_3_5.Background = Brushes.Purple;
            }
            //B4 Column 4 middle
            if (mResults[31] != Algorithm_Rules.Free && (mResults[31] & mResults[40] & mResults[49]) == mResults[31])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B4_4_3.Background = B4_4_4.Background = B4_4_5.Background = Brushes.Purple;
            }
            //B4 Column 5 middle
            if (mResults[32] != Algorithm_Rules.Free && (mResults[32] & mResults[41] & mResults[50]) == mResults[32])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B4_5_3.Background = B4_5_4.Background = B4_5_5.Background = Brushes.Purple;

            }

            //B5 Column 6 middle
            if (mResults[33] != Algorithm_Rules.Free && (mResults[33] & mResults[42] & mResults[51]) == mResults[33])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B5_6_3.Background = B5_6_4.Background = B5_6_5.Background = Brushes.Purple;
            }
            //B5 Column 7 middle
            if (mResults[34] != Algorithm_Rules.Free && (mResults[34] & mResults[43] & mResults[52]) == mResults[34])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B5_7_3.Background = B5_7_4.Background = B5_7_5.Background = Brushes.Purple;

            }
            //B5 Column 8 middle
            if (mResults[35] != Algorithm_Rules.Free && (mResults[35] & mResults[44] & mResults[53]) == mResults[35])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B5_8_3.Background = B5_8_4.Background = B5_8_5.Background = Brushes.Purple;

            }

            //B6 Column 0 down
            if (mResults[54] != Algorithm_Rules.Free && (mResults[54] & mResults[63] & mResults[72]) == mResults[54])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B6_0_6.Background = B6_0_7.Background = B6_0_8.Background = Brushes.Purple;

            }
            //B6 Column 1 down
            if (mResults[55] != Algorithm_Rules.Free && (mResults[55] & mResults[64] & mResults[73]) == mResults[5])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B6_1_6.Background = B6_1_7.Background = B6_1_8.Background = Brushes.Purple;

            }
            //B6 Column 2 down
            if (mResults[56] != Algorithm_Rules.Free && (mResults[56] & mResults[65] & mResults[74]) == mResults[56])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B6_2_6.Background = B6_2_7.Background = B6_2_8.Background = Brushes.Purple;

            }

            //B7 Column 3 down
            if (mResults[57] != Algorithm_Rules.Free && (mResults[57] & mResults[66] & mResults[75]) == mResults[57])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B7_3_6.Background = B7_3_7.Background = B7_3_8.Background = Brushes.Purple;

            }
            //B7 Column 4 down
            if (mResults[58] != Algorithm_Rules.Free && (mResults[58] & mResults[67] & mResults[76]) == mResults[58])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B7_4_6.Background = B7_4_7.Background = B7_4_8.Background = Brushes.Purple;

            }
            //B7 Column 5 down
            if (mResults[59] != Algorithm_Rules.Free && (mResults[59] & mResults[68] & mResults[77]) == mResults[59])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B7_5_6.Background = B7_5_7.Background = B7_5_8.Background = Brushes.Purple;

            }

            //B8 Column 6 down
            if (mResults[60] != Algorithm_Rules.Free && (mResults[60] & mResults[69] & mResults[78]) == mResults[60])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B8_6_6.Background = B8_6_7.Background = B8_6_8.Background = Brushes.Purple;

            }
            //B8 Column 7 down
            if (mResults[61] != Algorithm_Rules.Free && (mResults[61] & mResults[70] & mResults[79]) == mResults[61])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B8_7_6.Background = B8_7_7.Background = B8_7_8.Background = Brushes.Purple;

            }
            //B8 Column 8 down
            if (mResults[62] != Algorithm_Rules.Free && (mResults[62] & mResults[71] & mResults[80]) == mResults[62])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B8_8_6.Background = B8_8_7.Background = B8_8_8.Background = Brushes.Purple;

            }
            #endregion

        #region diagonal winner
            //check for diagonal win                       // all diagonal wins are set from 1 (left-up to right-down) to 2 (left-down to right-up)

            // B0 diagonal 1
            if (mResults[0] != Algorithm_Rules.Free && (mResults[0] & mResults[10] & mResults[20]) == mResults[0])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B0_0_0.Background = B0_1_1.Background = B0_2_2.Background = Brushes.Purple;
            }
            // B0 diagonal 2
            if (mResults[2] != Algorithm_Rules.Free && (mResults[2] & mResults[10] & mResults[18]) == mResults[2])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B0_2_0.Background = B0_1_1.Background = B0_0_2.Background = Brushes.Purple;
            }

            // B1 diagonal 1
            if (mResults[3] != Algorithm_Rules.Free && (mResults[3] & mResults[13] & mResults[23]) == mResults[3])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B1_3_0.Background = B1_4_1.Background = B1_5_2.Background = Brushes.Purple;
            }
            // B1 diagonal 2
            if (mResults[5] != Algorithm_Rules.Free && (mResults[5] & mResults[13] & mResults[21]) == mResults[5])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B1_5_0.Background = B1_4_1.Background = B1_3_2.Background = Brushes.Purple;
            }

            // B2 diagonal 1
            if (mResults[6] != Algorithm_Rules.Free && (mResults[6] & mResults[16] & mResults[26]) == mResults[6])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B2_6_0.Background = B2_7_1.Background = B2_8_2.Background = Brushes.Purple;
            }
            // B2 diagonal 2
            if (mResults[8] != Algorithm_Rules.Free && (mResults[8] & mResults[16] & mResults[24]) == mResults[8])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B2_8_0.Background = B2_7_1.Background = B2_6_2.Background = Brushes.Purple;
            }

            // B3 diagonal 1
            if (mResults[27] != Algorithm_Rules.Free && (mResults[27] & mResults[37] & mResults[47]) == mResults[27])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B3_0_3.Background = B3_1_4.Background = B3_2_5.Background = Brushes.Purple;
            }
            // B3 diagonal 2
            if (mResults[29] != Algorithm_Rules.Free && (mResults[29] & mResults[37] & mResults[45]) == mResults[29])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B3_2_3.Background = B3_1_4.Background = B3_0_5.Background = Brushes.Purple;
            }
            // B4 diagonal 1
            if (mResults[30] != Algorithm_Rules.Free && (mResults[30] & mResults[40] & mResults[50]) == mResults[30])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B4_3_3.Background = B4_4_4.Background = B4_5_5.Background = Brushes.Purple;
            }
            // B4 diagonal 2
            if (mResults[32] != Algorithm_Rules.Free && (mResults[32] & mResults[40] & mResults[48]) == mResults[32])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B4_5_3.Background = B4_4_4.Background = B4_3_5.Background = Brushes.Purple;
            }

            // B5 diagonal 1
            if (mResults[33] != Algorithm_Rules.Free && (mResults[33] & mResults[43] & mResults[53]) == mResults[33])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B5_6_3.Background = B5_7_4.Background = B5_8_5.Background = Brushes.Purple;
            }
            // B5 diagonal 2
            if (mResults[35] != Algorithm_Rules.Free && (mResults[35] & mResults[43] & mResults[51]) == mResults[35])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B5_8_3.Background = B5_7_4.Background = B5_6_5.Background = Brushes.Purple;
            }
            // B6 diagonal 1
            if (mResults[54] != Algorithm_Rules.Free && (mResults[54] & mResults[64] & mResults[74]) == mResults[54])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B6_0_6.Background = B6_1_7.Background = B6_2_8.Background = Brushes.Purple;
            }
            // B6 diagonal 2
            if (mResults[56] != Algorithm_Rules.Free && (mResults[56] & mResults[64] & mResults[72]) == mResults[56])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B6_2_6.Background = B6_1_7.Background = B6_0_8.Background = Brushes.Purple;
            }

            // B7 diagonal 1
            if (mResults[57] != Algorithm_Rules.Free && (mResults[57] & mResults[67] & mResults[77]) == mResults[57])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B7_3_6.Background = B7_4_7.Background = B7_5_8.Background = Brushes.Purple;
            }
            // B7 diagonal 2
            if (mResults[59] != Algorithm_Rules.Free && (mResults[59] & mResults[67] & mResults[75]) == mResults[59])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B7_5_6.Background = B7_4_7.Background = B7_3_8.Background = Brushes.Purple;
            }
            // B8 diagonal 1
            if (mResults[60] != Algorithm_Rules.Free && (mResults[60] & mResults[70] & mResults[80]) == mResults[60])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B8_6_6.Background = B8_7_7.Background = B8_8_8.Background = Brushes.Purple;
            }
            // B8 diagonal 2
            if (mResults[62] != Algorithm_Rules.Free && (mResults[62] & mResults[70] & mResults[78]) == mResults[62])
            {
                GameEnded = true;

                //Winnig buttons turn red
                B8_8_6.Background = B8_7_7.Background = B8_6_8.Background = Brushes.Purple;
            }

           
            #endregion

        #region No Winner
            if (!mResults.Any(f => f == Algorithm_Rules.Free)) //if no one wins and board is full
            {
                GameEnded = true;
           
                Board.Children.Cast<Button>().ToList().ForEach(Button => 
                {
                    button.Background = Brushes.Gray; //if no one wins paint board gray
                });


            }
#endregion
        } 

        //Comments:B0 stands for Board 0 (3x3),then B0_0_0 stands for Button on board 0 with on position 0_0
        // Definitely there is a better solution for example: making all buttons a group of 9 boards 3x3, and desactivate all boards, then activate the one where is next turn, but it is above my level of skills right now
    }



    // blokowanie pól 


            /* What i want to add :                     
             if (Player1Turn B4_3_3clicked )
                                                                                        
            {
                                                                                                    
            (FirstMove = B4_3_3 || B4_3_3 || B4_5_3 || B4_3_4 || B4_4_4 || B4_5_4 || B4_3_5 || B4_4_5 || B4_5_5);  
                                                                                                     
            private void FirstMove(object sender, RoutedEventArgs e)
            {

            if (NewGame)
            {
                Player1Turn = true;
                return;
            }
            var firstclick = FirstMove
                click button B4_3_3 || B4_3_3 || B4_5_3 || B4_3_4 || B4_4_4 || B4_5_4 || B4_3_5 || B4_4_5 || B4_5_5);

            else
            return



    */
    }
        
             
                        
} 
