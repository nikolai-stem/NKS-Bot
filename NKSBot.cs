using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nks_discord_bot
{
    public class NKSBot
    {
        private readonly DiscordSocketClient _client;
        private readonly BotBuilder _builder;

        public NKSBot(DiscordSocketClient client, BotBuilder builder)
        {
            _client = client;
            _builder = builder;
        }

        public async Task Run()
        {
            await _builder.Init();

            var token = _builder.GetToken();
            await _client.LoginAsync(Discord.TokenType.Bot, token);
            await _client.StartAsync();

            await Task.Delay(Timeout.Infinite);
        }
    }
}
