using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.EventArgs;

namespace Discord_LaTeX_Bot
{
    class Program
    {
        private static DiscordClient Client;

        static void Main(string[] args)
        {
            if (!File.Exists("id.txt"))
                throw new Exception("Bruh.");

            Client = new DiscordClient(new DiscordConfiguration { Token = File.ReadAllText("id.txt") });

            Client.MessageCreated += (MessageCreateEventArgs e) =>
            {
                if (e.Author.IsBot)
                    return Task.CompletedTask;

                var m = e.Message;
                string mess = m.Content;

                if (mess == "BRUH")
                    m.Channel.SendMessageAsync("TOAST");

                return Task.CompletedTask;
            };

            Client.ConnectAsync();

            SpinWait.SpinUntil(() => false); // this is a hack to stop the program from self closing
        }
    }
}
