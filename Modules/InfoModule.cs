using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nks_discord_bot.Modules
{
    public class InfoModule : ModuleBase<SocketCommandContext>
    {
        [Command("ns-greeting")]
        public async Task InfoAsync() => await ReplyAsync("Hello, I am NKSBot!");

        [Command("ns-emotecounter")]
        public async Task EmoteCounterCommand(IUser user, string searchEmote = "")
        {
            Task.Run(async () => await EmoteCounter(user, searchEmote));
            await ReplyAsync($"Fetching emoji statistics for {user.Username}. This may take a few moments, please wait...");
        }

        private async Task EmoteCounter(IUser user, string searchEmote)
        {
            var result = "";
            var counter = 0;
            var bearsCall = Context.Client.Guilds.Where(g => g.Name.ToLower().Contains("salt")).First();
            foreach (var channel in bearsCall.TextChannels)
            {
                // loop current messages
                await foreach (var msgCollection in channel.GetMessagesAsync())
                {
                    foreach (var message in msgCollection)
                    {
                        if (message.Author.Username == user.Username)
                        {
                            Console.WriteLine(message.Content);
                        }
                    }
                }
            }

            await ReplyAsync("Done!");
        }
    }
}
