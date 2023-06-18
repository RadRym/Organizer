using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizerV2
{
    public class StringManipulations
    {
        public string MoveThirdLetterToEnd(string input)
        {
            if (input != null && input.StartsWith("HE") && input.Length >= 3)
            {
                string thirdLetter = input.Substring(2, 1);
                string remainingLetters = input.Substring(0, 2) + input.Substring(3);
                return remainingLetters + thirdLetter;
            }
            else
            {
                return input;
            }
        }
    }
}
