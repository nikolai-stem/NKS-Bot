using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using nks_discord_bot;

var discordConfig = new DiscordSocketConfig()
{
    GatewayIntents = Discord.GatewayIntents.AllUnprivileged | Discord.GatewayIntents.MessageContent
};

var serviceCollection = new ServiceCollection()
    .AddSingleton<NKSBot>()
    .AddSingleton<DiscordSocketConfig>(discordConfig)
    .AddSingleton<DiscordSocketClient>()
    .AddSingleton<EventLogger>()
    .AddScoped<IConfigurationBuilder, ConfigurationBuilder>()
    .AddScoped<BotBuilder>()
    .AddScoped<CommandHandler>()
    .AddScoped<CommandService>();

var provider = serviceCollection.BuildServiceProvider();
var bot = provider.GetService<NKSBot>();
await bot.Run();