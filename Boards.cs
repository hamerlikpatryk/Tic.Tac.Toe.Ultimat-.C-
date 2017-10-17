using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    //Find the buttons position in the array
    var column = Grid.GetColumn(button);
    var row = Grid.GetRow(button);

    var index = column + (row * 9);

            //Don't do anything if button is already clicked 
            if (mResults[index] != Algorithm_Rules.Free)
                return;
      
}