using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace cybersecurityawarenessbot
{
    public class CybersecurityBot
    {
        // Fields declaration
        private string userName;
        private Dictionary<string, TopicInfo> topicResponses;
        private HashSet<string> ignoreWords;
        private List<string> conversationHistory;
        private Random random;

        // Inner class to store topic information
        private class TopicInfo
        {
            public string MainResponse { get; set; }
            public List<string> Keywords { get; set; }
            public List<string> FollowUpQuestions { get; set; }

            public TopicInfo(string response, List<string> keywords, List<string> followUps)
            {
                MainResponse = response;
                Keywords = keywords;
                FollowUpQuestions = followUps;
            }
        }
        public CybersecurityBot()
        {
            // Initialize collections
            topicResponses = new Dictionary<string, TopicInfo>();
            ignoreWords = new HashSet<string>();
            conversationHistory = new List<string>();
            random = new Random();

            // Initialize data
            InitializeTopicResponses();
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
            Console.WriteLine("Type 'help' to see available commands or 'exit' to quit the program.\n");

            // Get user name
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Please enter your name: ");
            Console.ResetColor();
            userName = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(userName))
            {
                userName = "User";
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("ChatBot:-> ");
            TypeWriterEffect($"Hello {userName}! I'm your Cybersecurity Awareness Bot. Ask me about any of these topics:", ConsoleColor.Green);

            // Display available topics
            DisplayAvailableTopics();
            Console.WriteLine();

            // Start the main interaction loop
            MainInteractionLoop();
        }

        private void DisplayAvailableTopics()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (string topic in topicResponses.Keys)
            {
                Console.WriteLine($"• {char.ToUpper(topic[0]) + topic.Substring(1)}");
            }
            Console.ResetColor();
        }

        private void MainInteractionLoop()
        {
            string userInput = string.Empty;

            do
            {
                // Prompt for user input with their name
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write($"{userName}:-> ");
                Console.ResetColor();
                userInput = Console.ReadLine()?.Trim() ?? string.Empty;

                // Add to conversation history
                if (!string.IsNullOrWhiteSpace(userInput))
                {
                    conversationHistory.Add(userInput.ToLower());
                }

                // Process the user's input if it's not "exit"
                if (userInput.ToLower() == "exit")
                {
                    break;
                }
                else if (userInput.ToLower() == "help")
                {
                    DisplayHelpCommands();
                }
                else if (userInput.ToLower() == "topics")
                {
                    DisplayAvailableTopics();
                }
                else if (userInput.ToLower() == "clear")
                {
                    Console.Clear();
                    DisplayWelcomeBack();
                }
                else
                {
                    // Process and respond to the user's question
                    ProcessUserInput(userInput);
                }

            } while (true); // Continue until user types "exit"

            // Farewell message
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("ChatBot:-> ");
            TypeWriterEffect($"Thank you for using the Cybersecurity Awareness Bot, {userName}! Stay safe online!", ConsoleColor.Green);
            Console.ResetColor();

            // Slight delay before exiting
            Thread.Sleep(1500);
        }

        private void DisplayWelcomeBack()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===================================================");
            Console.WriteLine("  CYBERSECURITY AWARENESS CHATBOT   ");
            Console.WriteLine("===================================================");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("ChatBot:-> ");
            TypeWriterEffect($"Welcome back, {userName}! How else can I help you with cybersecurity?", ConsoleColor.Green);
            Console.WriteLine();
        }

        private void DisplayHelpCommands()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nAvailable Commands:");
            Console.WriteLine("• help    - Display this help message");
            Console.WriteLine("• topics  - Show all available cybersecurity topics");
            Console.WriteLine("• clear   - Clear the console screen");
            Console.WriteLine("• exit    - Exit the application");
            Console.WriteLine("\nOr simply ask me about any cybersecurity topic you're interested in!");
            Console.ResetColor();
        }

        private void ProcessUserInput(string input)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("ChatBot:-> ");

            // Check for basic conversation starters
            if (Regex.IsMatch(input.ToLower(), @"\b(how are you|how('|a)re you doing)\b"))
            {
                TypeWriterEffect("I'm functioning optimally and ready to help you with cybersecurity questions! What would you like to know about?", ConsoleColor.Green);
                return;
            }
            else if (Regex.IsMatch(input.ToLower(), @"\b(what('|')s your purpose|what can you do|who are you)\b"))
            {
                TypeWriterEffect("My purpose is to help raise awareness about cybersecurity topics and answer your questions about online safety! Try asking me about specific topics like password management or phishing.", ConsoleColor.Green);
                return;
            }
            else if (Regex.IsMatch(input.ToLower(), @"\b(thank you|thanks)\b"))
            {
                TypeWriterEffect($"You're welcome, {userName}! I'm glad I could help. Is there anything else you'd like to know about cybersecurity?", ConsoleColor.Green);
                return;
            }

            // Split the question into words for processing
            string[] words = input.Split(new char[] { ' ', '.', ',', '?', '!', ':', ';', '-', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);

            // Filter out ignore words
            List<string> filteredWords = words
                .Where(word => !ignoreWords.Contains(word.ToLower()))
                .Select(word => word.ToLower())
                .ToList();

            // Look for matches in security topics
            string bestMatchTopic = FindBestMatchingTopic(filteredWords);

            if (bestMatchTopic != null)
            {
                // Display the main response for the topic
                TopicInfo topicInfo = topicResponses[bestMatchTopic];
                TypeWriterEffect(topicInfo.MainResponse, ConsoleColor.Green);

                // Suggest a follow-up question if available
                if (topicInfo.FollowUpQuestions.Count > 0)
                {
                    Thread.Sleep(500);
                    string followUp = topicInfo.FollowUpQuestions[random.Next(topicInfo.FollowUpQuestions.Count)];
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine($"\nYou might also want to ask: \"{followUp}\"");
                    Console.ResetColor();
                }
            }
            else
            {
                // Check if the input might be a follow-up question to a previous topic
                string contextTopic = DetermineContextFromHistory();
                if (contextTopic != null)
                {
                    TypeWriterEffect($"Regarding {contextTopic}, {topicResponses[contextTopic].MainResponse}", ConsoleColor.Green);
                }
                else
                {
                    // Default response if no matching topics found
                    TypeWriterEffect("I didn't quite understand that. Could you rephrase your question or type 'topics' to see what I can help you with?", ConsoleColor.Green);
                }
            }
        }

        private string FindBestMatchingTopic(List<string> words)
        {
            string bestMatch = null;
            int highestMatchScore = 0;

            foreach (var topicEntry in topicResponses)
            {
                string topic = topicEntry.Key;
                List<string> keywords = topicEntry.Value.Keywords;

                int matchScore = words.Sum(word =>
                {
                    // Check if word matches the topic or any of its keywords
                    if (word == topic) return 3;  // Exact match with topic name
                    if (keywords.Contains(word)) return 2;  // Match with a keyword
                    if (keywords.Any(k => k.Contains(word) || word.Contains(k))) return 1;  // Partial match
                    return 0;
                });

                if (matchScore > highestMatchScore)
                {
                    highestMatchScore = matchScore;
                    bestMatch = topic;
                }
            }

            // Only return a match if the score is above a threshold
            return highestMatchScore >= 1 ? bestMatch : null;
        }

        private string DetermineContextFromHistory()
        {
            // Look at the last few conversations to determine context
            if (conversationHistory.Count <= 1) return null;

            // Check the previous messages for topic mentions
            for (int i = conversationHistory.Count - 2; i >= Math.Max(0, conversationHistory.Count - 4); i--)
            {
                string previousMessage = conversationHistory[i];
                foreach (var topic in topicResponses.Keys)
                {
                    if (previousMessage.Contains(topic) ||
                        topicResponses[topic].Keywords.Any(k => previousMessage.Contains(k)))
                    {
                        return topic;
                    }
                }
            }

            return null;
        }

        private void TypeWriterEffect(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(10); // Faster typing speed
            }
            Console.WriteLine();
            Console.ResetColor();
        }

        private void InitializeTopicResponses()
        {
            // Password security
            topicResponses.Add("password", new TopicInfo(
                "Password Safety Tips:\n" +
                "• Use complex passwords with at least 12 characters including letters, numbers, and symbols\n" +
                "• Never share your passwords with anyone\n" +
                "• Use different passwords for different accounts\n" +
                "• Consider using a password manager like LastPass, 1Password, or Bitwarden\n" +
                "• Enable two-factor authentication (2FA) whenever possible",
                new List<string> { "password", "passwords", "passphrase", "authentication", "login", "credentials", "2fa", "mfa", "two-factor" },
                new List<string> { "What is two-factor authentication?", "How do password managers work?", "How often should I change my passwords?" }
            ));

            // Phishing
            topicResponses.Add("phishing", new TopicInfo(
                "Phishing Awareness:\n" +
                "• Be suspicious of unexpected emails requesting personal information or urgent action\n" +
                "• Check email sender addresses carefully (hover over the name to see the actual email)\n" +
                "• Don't click on suspicious links - hover first to see the actual URL\n" +
                "• Look for grammatical errors or unusual formatting which are common in phishing attempts\n" +
                "• When in doubt, contact the company directly through their official website",
                new List<string> { "phishing", "scam", "scams", "email", "emails", "spam", "fraud", "fraudulent", "fake" },
                new List<string> { "What are signs of a phishing email?", "How do I report phishing attempts?", "What is spear phishing?" }
            ));

            // Safe browsing
            topicResponses.Add("browsing", new TopicInfo(
                "Safe Browsing Tips:\n" +
                "• Always check for HTTPS (padlock icon) in the URL before entering sensitive information\n" +
                "• Keep your browser and extensions updated to the latest version\n" +
                "• Use ad-blockers and anti-tracking extensions\n" +
                "• Clear your browsing history and cookies regularly\n" +
                "• Be careful what you download - verify the source is legitimate",
                new List<string> { "browsing", "browser", "surf", "surfing", "internet", "web", "website", "online", "https", "ssl" },
                new List<string> { "What browser extensions improve security?", "What does HTTPS actually mean?", "How do I check if a website is secure?" }
            ));

            // Malware protection
            topicResponses.Add("malware", new TopicInfo(
                "Malware Protection:\n" +
                "• Install and regularly update reputable antivirus/anti-malware software\n" +
                "• Don't download files or click links from untrusted sources\n" +
                "• Regularly scan your computer for threats\n" +
                "• Keep your operating system and all software updated with security patches\n" +
                "• Be cautious with email attachments, even from known senders",
                new List<string> { "malware", "virus", "viruses", "trojan", "ransomware", "spyware", "adware", "antivirus", "infection" },
                new List<string> { "What's the difference between viruses and ransomware?", "How often should I scan my computer?", "How can I tell if my computer is infected?" }
            ));

            // Social media safety
            topicResponses.Add("social", new TopicInfo(
                "Social Media Safety:\n" +
                "• Limit the personal information you share on your profiles\n" +
                "• Use strong privacy settings to control who sees your posts\n" +
                "• Be wary of friend/connection requests from unknown people\n" +
                "• Regularly review your privacy settings as platforms often update them\n" +
                "• Think twice before sharing your location or travel plans publicly",
                new List<string> { "social", "media", "facebook", "twitter", "instagram", "linkedin", "tiktok", "snapchat", "privacy", "profile" },
                new List<string> { "How do I review my social media privacy settings?", "What information should I never share on social media?", "How do hackers use social media to gather information?" }
            ));

            // Public Wi-Fi safety
            topicResponses.Add("wifi", new TopicInfo(
                "Public Wi-Fi Safety:\n" +
                "• Avoid accessing sensitive accounts (banking, email) on public Wi-Fi\n" +
                "• Use a VPN (Virtual Private Network) when connecting to public networks\n" +
                "• Ensure the network you're connecting to is legitimate - verify the name\n" +
                "• Turn off file sharing and AirDrop when on public networks\n" +
                "• Set your device to 'forget' public networks after using them",
                new List<string> { "wifi", "wi-fi", "wireless", "hotspot", "network", "public", "vpn", "connection", "connect" },
                new List<string> { "How does a VPN protect me?", "What are evil twin Wi-Fi networks?", "Is it safe to use hotel Wi-Fi?" }
            ));

            // Data backups
            topicResponses.Add("backup", new TopicInfo(
                "Data Backup Best Practices:\n" +
                "• Follow the 3-2-1 backup rule: 3 copies, 2 different media types, 1 offsite\n" +
                "• Regularly backup important data - automate if possible\n" +
                "• Test your backups occasionally to ensure they can be restored\n" +
                "• Encrypt sensitive backups for additional protection\n" +
                "• Consider cloud backup services with strong security features",
                new List<string> { "backup", "backups", "copy", "copies", "restore", "recovery", "cloud", "storage", "data" },
                new List<string> { "What's the 3-2-1 backup rule?", "How often should I backup my data?", "What cloud backup services are recommended?" }
            ));

            // Mobile device security
            topicResponses.Add("mobile", new TopicInfo(
                "Mobile Device Security:\n" +
                "• Set a strong PIN/password and enable biometric security if available\n" +
                "• Keep your device's operating system and apps updated\n" +
                "• Only download apps from official app stores (Google Play, App Store)\n" +
                "• Review app permissions carefully - limit access to location, contacts, etc.\n" +
                "• Enable remote tracking and wiping features in case of loss or theft",
                new List<string> { "mobile", "phone", "smartphone", "tablet", "android", "iphone", "ios", "app", "apps" },
                new List<string> { "How do I secure my smartphone?", "Should I use a screen lock?", "How can I protect my personal data on my phone?" }
            ));
        }

        private void InitializeIgnoreWords()
        {
            // Common words to ignore in processing
            string[] words = { "a", "an", "the", "is", "are", "was", "were", "be", "being", "been",
                "and", "or", "but", "if", "then", "else", "when", "what", "where", "how",
                "why", "who", "which", "that", "tell", "me", "about", "can", "you", "please",
                "could", "would", "should", "i", "we", "they", "he", "she", "it", "this", "these",
                "those", "do", "does", "did", "will", "have", "has", "had", "get", "getting", "got",
                "want", "need", "know", "think", "good", "bad", "information", "info", "things", "way",
                "my", "your", "their", "our", "to", "from", "with", "without", "for", "of", "in", "on",
                "at", "by", "up", "down", "over", "under", "again", "further", "more", "most", "some",
                "such", "no", "nor", "not", "only", "own", "same", "so", "than", "too", "very" };

            foreach (string word in words)
            {
                ignoreWords.Add(word);
            }
        }
    }
}