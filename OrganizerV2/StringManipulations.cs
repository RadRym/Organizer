using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizerV2
{
    public static class StringManipulations
    {
        public static string MoveThirdLetterToEnd(string input)
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
        public static string InsertLastCharacterToThirdPosition(string input)
        {
            if (input != null && input.StartsWith("HE") && input.Length >= 3)
            {
                char lastCharacter = input[input.Length - 1];
                string firstTwoCharacters = input.Substring(0, 2);
                string remainingCharacters = input.Substring(2, input.Length - 3);
                return firstTwoCharacters + lastCharacter + remainingCharacters;
            }
            else
            {
                return input;
            }
        }
    }
}
