using System;
using System.Collections.Generic;
using System.Threading;

namespace cybersecurityawarenessbot
{
    public class CybersecurityBot
    {
        // Fields declaration
        private string userName;
        private Dictionary<string, string> responses;
        private HashSet<string> ignoreWords;
        public CybersecurityBot()
        {
            // Initialize collections
            responses = new Dictionary<string, string>();
            ignoreWords = new HashSet<string>();

            // Initialize data
            InitializeResponses();
            InitializeIgnoreWords();
        }
        // Application startup method
        public void Start()
        {
            // Display welcome message and get user name
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===================================================");
            Console.WriteLine("  WELCOME TO THE CYBERSECURITY AWARENESS CHATBOT   ");
            Console.WriteLine("===================================================");
            Console.ResetColor();

            Console.WriteLine("\nThis bot will help you learn about various cybersecurity topics.");
            Console.WriteLine("Type 'exit' at any time to quit the program.\n");

            // Get user name
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Please enter your name: ");
            Console.ResetColor();
            userName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userName))
            {
                userName = "User";
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("ChatBot:-> ");
            TypeWriterEffect($"Hello {userName}! I'm your Cybersecurity Awareness Bot. Ask me about password safety, phishing, safe browsing, malware, social media safety, or public Wi-Fi!", ConsoleColor.Green);
            Console.WriteLine();

            // Start the main interaction loop
            MainInteractionLoop();
        }

        private void MainInteractionLoop()
        {
            string userQuestion = string.Empty;

            do
            {
                // Prompt for user input with their name
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write($"{userName}:-> ");
                Console.ForegroundColor = ConsoleColor.Magenta;
                userQuestion = Console.ReadLine();

                // Process the user's question if it's not "exit"
                if (userQuestion.ToLower() != "exit")
                {
                    // Process and respond to the user's question
                    ProcessUserQuestion(userQuestion);
                }

            } while (userQuestion.ToLower() != "exit"); // Continue until user types "exit"

            // Farewell message
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("ChatBot:-> ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Thank you for using the Cybersecurity Awareness Bot, {userName}! Stay safe online!");
            Console.ResetColor();

            // Slight delay before exiting
            Thread.Sleep(2000);
        }

        private void ProcessUserQuestion(string question)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("ChatBot:-> ");

            // Check for basic conversation starters
            if (question.ToLower().Contains("how are you"))
            {
                TypeWriterEffect("I'm functioning optimally and ready to help you with cybersecurity questions!", ConsoleColor.Green);
                return;
            }
            else if (question.ToLower().Contains("what's your purpose") || question.ToLower().Contains("what can you do"))
            {
                TypeWriterEffect("My purpose is to help raise awareness about cybersecurity topics like password safety, phishing attacks, and safe browsing habits!", ConsoleColor.Green);
                return;
            }

            // Split the question into words for processing
            string[] words = question.Split(new char[] { ' ', '.', ',', '?', '!' }, StringSplitOptions.RemoveEmptyEntries);

            // Filter out ignore words
            List<string> filteredWords = new List<string>();
            foreach (string word in words)
            {
                if (!ignoreWords.Contains(word.ToLower()))
                {
                    filteredWords.Add(word.ToLower());
                }
            }

            // Look for matches in security topics
            bool foundResponse = false;
            foreach (string word in filteredWords)
            {
                foreach (string topic in responses.Keys)
                {
                    if (topic.Contains(word) || word.Contains(topic))
                    {
                        TypeWriterEffect(responses[topic], ConsoleColor.Green);
                        foundResponse = true;
                        break;
                    }
                }
                if (foundResponse) break;
            }

            // Default response if no matching topics found
            if (!foundResponse)
            {
                TypeWriterEffect("I didn't quite understand that. Could you rephrase your question about password safety, phishing, or safe browsing?", ConsoleColor.Green);
            }
        }

        private void TypeWriterEffect(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(20); // Adjust for desired typing speed
            }
            Console.WriteLine();
            Console.ResetColor();
        }

        private void InitializeResponses()
        {
            // Add cybersecurity-related responses
            responses.Add("password", "Password Safety Tips:\n- Use complex passwords with a mix of letters, numbers, and symbols\n- Never share your passwords with anyone\n- Use different passwords for different accounts\n- Consider using a password manager");

            responses.Add("phishing", "Phishing Awareness:\n- Be suspicious of unexpected emails asking for personal information\n- Check email sender addresses carefully\n- Don't click on suspicious links\n- Look for grammatical errors which are common in phishing attempts");

            responses.Add("browsing", "Safe Browsing Tips:\n- Always check for HTTPS in the URL before entering sensitive information\n- Keep your browser updated\n- Use ad-blockers and anti-malware extensions\n- Be careful what you download");

            responses.Add("malware", "Malware Protection:\n- Install and regularly update antivirus software\n- Don't download files from untrusted sources\n- Regularly scan your computer for threats\n- Keep your operating system updated");

            responses.Add("social", "Social Media Safety:\n- Limit the personal information you share online\n- Use privacy settings to control who sees your posts\n- Be wary of friend requests from unknown people\n- Regularly review your privacy settings");

            responses.Add("wifi", "Public Wi-Fi Safety:\n- Avoid accessing sensitive accounts on public Wi-Fi\n- Use a VPN when connecting to public networks\n- Ensure the network you're connecting to is legitimate\n- Turn off file sharing when on public networks");
        }

        private void InitializeIgnoreWords()
        {
            // Common words to ignore in processing
            string[] words = { "a", "an", "the", "is", "are", "was", "were", "be", "being", "been",
                              "and", "or", "but", "if", "then", "else", "when", "what", "where", "how",
                              "why", "who", "which", "that", "tell", "me", "about", "can", "you", "please",
                              "could", "would", "should", "i", "we", "they", "he", "she", "it", "this", "these",
                              "those", "do", "does", "did", "will", "have", "has", "had" };

            foreach (string word in words)
            {
                ignoreWords.Add(word);
            }
        }
    }
}