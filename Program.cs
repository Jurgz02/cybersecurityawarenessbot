using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
            new CybersecurityBot(){};
            CybersecurityBot bot = new CybersecurityBot();
            bot.Start();
            //create instance for keyword_recognition
            new keyword_recognition() { };
            //create instance for random_responses
            new random_responses() { };
        }
    }
}
