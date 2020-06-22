using Discord.WebSocket;
using Microsoft.VisualBasic.CompilerServices;
using SchockBot.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchockBot.Core.Commands.Utils
{
    public class GameMechanic
    {
        /// <summary>
        /// Add caps to the player.
        /// </summary>
        /// <param name="user">player of the game.</param>
        /// <param name="amount">amount of caps for the player.</param>
        /// <returns>returns succeed msg.</returns>
        public static string AddCaps(SocketUser user, int amount)
        {
            string msg = string.Empty;
            if (Game.Players != null)
            {

                foreach (var player in Game.Players)
                {

                    if (string.Equals(player.User.Id, user.Id))
                    {
                        if (amount <= 13 && player.Caps <= 13 && Game.NumberOfCaps >= 0 && Game.NumberOfCaps - amount >= 0)
                        {
                            player.Caps += amount;
                            msg = string.Format("{0}: {1} ", user.Username, player.Caps);
                            Game.NumberOfCaps -= amount;
                        }
                        else
                        {
                            msg = string.Format("{0} konnte keine Deckel hinzugefügt werden, weil er zu viele hat oder es keiner mehr auf dem Stapel gibt.", user.Username);
                        }


                    }
                   
                }
            }
            else
            {
                msg = string.Format("Es ist noch kein Spieler hinzugefügt worden.");
            }

            return msg;
        }

        /// <summary>
        /// Remove caps from the User.
        /// </summary>
        /// <param name="from">from the player of the game.</param>
        /// <param name="to">to the player of the game.</param>
        /// <param name="amount">amount of caps.</param>
        /// <returns></returns>
        public static string RemoveCaps(SocketUser from, SocketUser to, int amount)
        {
            string msg = string.Empty;
            if (Game.Players != null)
            {
                var userToSendCaps = Game.Players.Where(x => string.Equals(x.User.Id,to.Id)).Select(y => y).FirstOrDefault();
                
               
                foreach (var player in Game.Players)
                {

                    if (string.Equals(player.User.Id, from.Id))
                    {

                        if (amount <= 13 && player.Caps <= 13 && Game.NumberOfCaps >= 0 && player.Caps - amount >= 0)
                        {

                            player.Caps -= amount;
                            userToSendCaps.Caps += amount;

                            msg = string.Format("{0}: {1} , {2}: {3} ", from.Username, player.Caps, userToSendCaps.User.Username, userToSendCaps.Caps);

                        }
                        else
                        {
                            msg = string.Format("{0} konnte keine Deckel hinzugefügt werden, weil er zu viele hat oder es keiner mehr auf dem Stapel gibt.", from.Username);
                        }


                    }
                   
                }
            }
            else
            {
                msg = string.Format("Es ist noch kein Spieler hinzugefügt worden.");
            }

            return msg;
        }
    }
}
