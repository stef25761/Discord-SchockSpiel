using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.VisualBasic;
using SchockBot.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Game = SchockBot.Data.Game;

namespace SchockBot.Core.Commands
{
    public class SchockCommands : ModuleBase<SocketCommandContext>
    {
        private const int Pasch = 3;
        private const int Street = 2;
        private const int Whore = 10;
        /// <summary>
        /// Add 2 caps to the player.
        /// </summary>
        /// <returns></returns>
        [RequireContext(ContextType.Guild)]
        [Command("street"), Summary("Fügt 2 Deckel hinzu")]
        public async Task AddStreet()
        {
            var user = Context.User as SocketUser;

            await Context.Channel.SendMessageAsync(Utils.GameMechanic.AddCaps(user, Street));
        }

        /// <summary>
        /// Add the player 3 caps.
        /// </summary>
        /// <returns></returns>
        [RequireContext(ContextType.Guild)]
        [Command("pasch"), Summary("fügt dem Spieler 3 Deckel hinzu")]
        public async Task AddPasch()
        {
            var user = Context.User as SocketUser;

            await Context.Channel.SendMessageAsync(Utils.GameMechanic.AddCaps(user, Pasch));
        }

        /// <summary>
        /// Add a amount of caps to the user.
        /// </summary>
        /// <param name="amount">how much caps should be add.</param>
        /// <returns></returns>
        [RequireContext(ContextType.Guild)]
        [Command("add"), Summary("fügt dem Spieler 1-6  Deckel hinzu")]
        public async Task AddCaps(int amount)
        {
            var user = Context.User as SocketUser;

            await Context.Channel.SendMessageAsync(Utils.GameMechanic.AddCaps(user, amount));
        }

        [RequireContext(ContextType.Guild)]
        [Command("d"), Summary("fügt dem Spieler 1-6  Deckel hinzu")]
        public async Task AddCaps(int amount, SocketUser to)
        {
            var user = Context.User as SocketUser;

            await Context.Channel.SendMessageAsync(Utils.GameMechanic.RemoveCaps(user, to, amount));
        }
        /// <summary>
        /// Return the count of caps in the game.
        /// </summary>
        /// <returns></returns>
        [RequireContext(ContextType.Guild)]
        [Command("caps"), Summary("Gibt die Anzahl der Deckel aus")]
        public async Task GetCaps()
        {
            await Context.Channel.SendMessageAsync(Game.NumberOfCaps.ToString());
        }
        /// <summary>
        /// Returns the number of the player caps.
        /// </summary>
        /// <returns></returns>
        [RequireContext(ContextType.Guild)]
        [Command("mycaps"), Summary("Gibt die Anzahl deiner Deckel aus")]
        public async Task GetMyCaps()
        {
            var user = Context.User as SocketUser;
            string msg = string.Empty;
            if (Game.Players != null)
            {
                foreach (var player in Game.Players)
                {
                    if (string.Equals(user.Id, player.User.Id))
                    {
                        msg = string.Format("{0} besitzt {1} Deckel", user.Username, player.Caps);
                    }
                }
            }
            await Context.Channel.SendMessageAsync(msg);
        }

        /// <summary>
        /// Set 10 caps to the player.
        /// </summary>
        /// <returns></returns>
        [RequireContext(ContextType.Guild)]
        [Command("nutte"), Summary("fügt dem Spieler 10 Deckel zu")]
        public async Task AddWhore()
        {
            var user = Context.User as SocketUser;

            await Context.Channel.SendMessageAsync(Utils.GameMechanic.AddCaps(user,Whore));
        }
        /// <summary>
        /// Reg a new player to a new game.
        /// </summary>
        /// <returns></returns>
        [RequireContext(ContextType.Guild)]
        [Command("play"), Summary("Registriert dich als neuer Spieler")]
        public async Task AddPlayer()
        {
            var user = Context.User as SocketUser;
            string msg = string.Format("User: {0} ist dem Spiel beigetreten", user.Username);

            var player = new Player()
            {
                Caps = 0,
                Round = 0,

                User = user,
            };
            if (Game.Players == null)
            {
                await Context.Channel.SendMessageAsync("Spiel wird erstellt");
                Game.Players = new List<Player>();

                Game.Players.Add(player);
            }
            else
            {
                Game.Players.Add(player);

            }


            await Context.Channel.SendMessageAsync(msg);
        }

        /// <summary>
        /// Game is finished
        /// </summary>
        /// <returns></returns>
        [RequireContext(ContextType.Guild)]
        [Command("finish"), Summary("Stopt das aktuelle Spiel")]
        public async Task GameStop()
        {
            string msg = string.Empty;

            if (Game.Players.Count == 0 && Game.Players == null)
            {
                msg = "Es sind keine Spieler vorhanden";
            }
            else
            {
                msg = "Spiel wird beendet";
                Game.Players.Clear();
                Game.Players = null;
                Game.NumberOfCaps = 13;
            }

            await Context.Channel.SendMessageAsync(msg);
        }
    }
}
