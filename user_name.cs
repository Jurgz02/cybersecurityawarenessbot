using System;

namespace cybersecurityawarenessbot
{
    public class user_name
        
    {
        // Variable declarations
        private string userName = string.Empty;
        public user_name()
        {
            // Prompt for user name with colored text
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("ChatBot:-> ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Please enter your name.");

            // Get user input with different color
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("You:-> ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            userName = Console.ReadLine();

            // Welcome the user by name
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("ChatBot:-> ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Hey {userName}, I'm here to help you stay safe online! What would you like to know about cybersecurity?");
            Console.ResetColor();
        }
    }
}