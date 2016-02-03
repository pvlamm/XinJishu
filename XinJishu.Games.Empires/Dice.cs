﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace XinJishu.Games.Empires
{
    public class Dice
    {
        private static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();

        // Just want a quick and dirty version of this for Int's. I freely admit I listed it from below and will change 
        // it out when I get around to it.
        // -------------------------------------------------------------------------------------------------------------
        // http://msdn.microsoft.com/en-us/library/system.security.cryptography.rngcryptoserviceprovider(v=vs.110).aspx
        // -------------------------------------------------------------------------------------------------------------


        // This method simulates a roll of the dice. The input parameter is the 
        // number of sides of the dice. 

        public static Int32 RollDice(byte numberSides)
        {
            if (numberSides <= 0)
                throw new ArgumentOutOfRangeException("numberSides");

            // Create a byte array to hold the random value. 
            byte[] randomNumber = new byte[1];
            do
            {
                // Fill the array with a random value.
                rngCsp.GetBytes(randomNumber);
            }
            while (!IsFairRoll(randomNumber[0], numberSides));
            // Return the random number mod the number 
            // of sides.  The possible values are zero- 
            // based, so we add one. 
            return Convert.ToInt32(((randomNumber[0] % numberSides) + 1)); // (byte)((randomNumber[0] % numberSides) + 1);
        }

        private static bool IsFairRoll(byte roll, byte numSides)
        {
            // There are MaxValue / numSides full sets of numbers that can come up 
            // in a single byte.  For instance, if we have a 6 sided die, there are 
            // 42 full sets of 1-6 that come up.  The 43rd set is incomplete. 
            int fullSetsOfValues = Byte.MaxValue / numSides;

            // If the roll is within this range of fair values, then we let it continue. 
            // In the 6 sided die case, a roll between 0 and 251 is allowed.  (We use 
            // < rather than <= since the = portion allows through an extra 0 value). 
            // 252 through 255 would provide an extra 0, 1, 2, 3 so they are not fair 
            // to use. 
            return roll < numSides * fullSetsOfValues;
        }
    }
}
