using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using DSharpPlus;
using DSharpPlus.EventArgs;
using DSharpPlus.Entities;

using Octave.NET;

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

                if (mess.ToLower() == "pog" || mess.ToLower() == "poggers")
                {
                    DiscordEmoji lyra = DiscordEmoji.FromName(Client,":lyrapog:");
                    DiscordEmoji paige = DiscordEmoji.FromName(Client, ":paigepog:");
                    DiscordEmoji fractal = DiscordEmoji.FromName(Client, ":fractalpog:");
                    DiscordEmoji ikea = DiscordEmoji.FromName(Client, ":ikeapog:");
                    m.Channel.SendMessageAsync(lyra + paige + fractal + ikea);
                }

                if (mess == "2+2")
                {
                    using (var octave = new OctaveContext())
                    {
                        var scalarResult = octave
                           .Execute(mess)
                           .AsScalar();
                           
                        m.Channel.SendMessageAsync(scalarResult.ToString());
                    }
                }

                char[] fourFunction = { '+', '-', 'x', '*', '/' };
                LinkedList <String> nums = new LinkedList<String>();
                LinkedList<String> ops = new LinkedList<String>();
                
                for(int i = 0; i < mess.Length; i++)
                {
                    // TODO: use regex.split to tokenize message input into strings->double in nums, and operators into ops.
                    // use stringbuilder to "build" a number until it hits a delimiter? seems workaround and slow. does c# have stringbuilder?
                    // stringbuild until hit delimiter, unload to linked list, getchar, repeat until endchar
                    
                }

                return Task.CompletedTask;
            };


            Client.ConnectAsync();

            SpinWait.SpinUntil(() => false); // this is a hack to stop the program from self closing
        }
    }
}
