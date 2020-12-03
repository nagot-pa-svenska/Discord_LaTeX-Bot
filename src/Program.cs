using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;

using Octave.NET;

namespace Discord_LaTeX_Bot
{
    class Program
    {
        private static DiscordClient Client;

        static void Main(string[] args)
        {
            if (!File.Exists("id.txt"))
                throw new FileNotFoundException("Missing \"id.txt\" from current directory, unable to continue.");

            Client = new DiscordClient(new DiscordConfiguration { Token = File.ReadAllText("id.txt") });

            Client.Ready += (ReadyEventArgs e) =>
            {
                Console.WriteLine("Ready!");
                return Task.CompletedTask;
            };

            Client.MessageCreated += (MessageCreateEventArgs e) =>
            {
                if (e.Author.IsBot)
                    return Task.CompletedTask;

                var m = e.Message;
                string mess = m.Content;

                if (mess.ToLower() == "pog" || mess.ToLower() == "poggers")
                {
                    DiscordEmoji lyra = DiscordEmoji.FromName(Client, ":lyrapog:");
                    DiscordEmoji paige = DiscordEmoji.FromName(Client, ":paigepog:");
                    DiscordEmoji fractal = DiscordEmoji.FromName(Client, ":fractalpog:");
                    DiscordEmoji ikea = DiscordEmoji.FromName(Client, ":ikeapog:");

                    m.Channel.SendMessageAsync(lyra + paige + fractal + ikea);
                }
                else if (!mess.StartsWith("!latex"))
                    return Task.CompletedTask;

                mess = mess.Substring("!latex ".Length);

                char[] fourFunctions = { '+', '-', 'x', '*', '/' };
                //LinkedList<double> nums = new LinkedList<double>();
                //LinkedList<string> ops = new LinkedList<string>();

                var nums = mess.Split(fourFunctions);
                // minus one on length to avoid OutOfBoundsException caused by grabbing the next entry
                string[] ops = new string[nums.Length - 1];

                string equation = "";

                for (int i = 0; i < ops.Length; i++)
                {
                    ops[i] = ReturnInbetween(mess, nums[i], nums[i + 1]);
                    equation += nums[i] + ops[i] + nums[i + 1];
                }

                m.Channel.SendMessageAsync(new OctaveContext().Execute(equation).AsScalar().ToString());

                for (int i = 0; i < mess.Length; i++)
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

        /// <summary>
        /// Finds a string between two given strings.
        /// Used when you don't know what will be in-between two strings, but know what strings are around it.
        /// </summary>
        /// <param name="excerpt">The string you wish to search within.</param>
        /// <param name="before">The string located before the string you want.</param>
        /// <param name="after">The string located after the string you want.</param>
        private static string ReturnInbetween(string excerpt, string before, string after)
        {
            return excerpt.Substring(excerpt.IndexOf(before) + 1, excerpt.IndexOf(after, excerpt.IndexOf(before) + 1) - (excerpt.IndexOf(before) + 1));
        }
    }
}
