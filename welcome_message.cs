using System;
using System.IO;
using System.Media;

namespace cybersecurityawarenessbot
{
    public class welcome_message
    {
        public welcome_message()
        {
            //set color for border
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╔══════════════════════════════════════════════════════════════╗");
            //set color for text
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("║                Welcome to the Cybersecurity                  ║");
            Console.WriteLine("║                     Awareness Bot                            ║");
            //set color for border
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("╚══════════════════════════════════════════════════════════════╝");
            //set color back to white
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();

        }
    }
}