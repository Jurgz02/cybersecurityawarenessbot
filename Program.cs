using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace cybersecurityawarenessbot
{
    public class Program
    {
        static void Main(string[] args)
        {
            //create instance for voice_greeting
            new voice_greeting() { };
            //create instance for logo_design
            new logo_design() { };
            //create instance for welcome_message
            new welcome_message() { };
            // Create and start the bot
            try
            {
                CybersecurityBot bot = new CybersecurityBot();
                bot.Start(); // Make sure this matches the method name in CybersecurityBot class
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"An error occurred: {ex.Message}");
                Console.WriteLine("The application will now exit.");
                Console.ResetColor();
                Thread.Sleep(3000);
                //create instance for keyword_recognition
                new Keyword_recognition() { };
                //create instance for random_responses
                new Random_responses() { };
                //create instance for conversation_flow
                new conversation_flow() { };
                //create instance for memory_recall
                new memory_recall() { };
                //create instance for sentiment_detection
                new sentiment_detection() { };
            }
        }
    }
}