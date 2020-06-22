using System;
using System.Collections.Generic;
using System.Text;

namespace SchockBot.Data
{
    /// <summary>
    /// Consists game necessary properties.
    /// </summary>
    public class Game
    {
        //TODO: implementation of a singelton.

        private static int caps = 13;
        /// <summary>
        /// Number of playing Users.
        /// </summary>
        /// 
        public static List<Player> Players { get; set; }
        /// <summary>
        /// Number of all caps in the current game.
        /// </summary>
        public static int NumberOfCaps
        {
            set
            {
                caps = value;
            }

            get
            {
                return caps;
            }
        }
    }
}
