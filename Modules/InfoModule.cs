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

        [Command("ns-emojicounter")]
        public async Task EmoteCounterCommand(IUser user, string searchEmote = "")
        {
            Task.Run(async () => await EmoteCounter(user, searchEmote));
            await ReplyAsync($"Fetching emoji statistics for {user.Username}. This may take a few moments, please wait...");
        }

        private async Task EmoteCounter(IUser user, string searchEmote)
        {
            var emoteText = searchEmote;
            if (!string.IsNullOrEmpty(searchEmote) && searchEmote.Contains(":"))
            {
                emoteText = searchEmote.Split(':')[1].ToLower();
            }
            var emoteCounter = new Dictionary<string, int>();

            var bearsCall = Context.Client.Guilds.Where(g => g.Name.ToLower().Contains("salt")).First();
            foreach (var channel in bearsCall.TextChannels)
            {
                // loop current messages
                await foreach (var msgCollection in channel.GetMessagesAsync())
                {
                    foreach (var message in msgCollection)
                    {
                        if (message.Author.Username == user.Username && !message.Content.ToLower().Contains("!ns-emojicounter"))
                        {
                            if (!string.IsNullOrEmpty(emoteText))
                            {
                                if (message.Content.ToLower().Contains(emoteText))
                                {
                                    int res;
                                    if (!emoteCounter.TryGetValue(emoteText, out res))
                                    {
                                        emoteCounter.Add(emoteText, 0);
                                    }
                                    emoteCounter[emoteText]++;
                                }
                            }
                            else
                            {
                                await ReplyAsync("This feature is not implemented yet! You need to search for a specific emoji! :smile: ");
                                return;
                            }
                        }
                    }
                }

                // loop cached messages?
            }

            // Format response
            var builder = new StringBuilder();
            builder.AppendLine($"Emoji statistics for {user.Username}:");
            builder.AppendLine("");
            if (emoteCounter.Count == 0)
            {
                builder.AppendLine($"{user.Username} has not used the queried emoji!");
            }
            else
            {
                foreach (var kv in emoteCounter)
                {
                    builder.AppendLine($"{kv.Key}\t\t\t{kv.Value} time{(kv.Value > 1 ? "s" : "" )}");
                }
            }

            await ReplyAsync(Format.Code(builder.ToString()));
        }
    }
}
