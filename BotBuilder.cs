using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nks_discord_bot
{
    public class BotBuilder
    {
        private readonly IConfigurationRoot _config;
        private readonly DiscordSocketClient _client;
        private readonly EventLogger _eventLogger;
        private readonly CommandHandler _commandHandler;

        public BotBuilder(DiscordSocketClient client, IConfigurationBuilder configBuilder, CommandHandler commandHandler, EventLogger eventLogger)
        {
            configBuilder.AddJsonFile($"appsettings.json");
            _config = configBuilder.Build();

            _client = client;
            _eventLogger = eventLogger;
            _commandHandler = commandHandler;
        }

        public async Task Init()
        {
            _client.Log += _eventLogger.Log;

            await _commandHandler.InstallCommandsAsync();
        }

        public string GetToken()
        {
            return _config.GetSection("botToken").Value ?? "";
        }
    }
}
