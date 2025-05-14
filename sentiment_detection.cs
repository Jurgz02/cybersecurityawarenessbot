using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace cybersecurityawarenessbot
{
    public class sentiment_detection
    {
        private Dictionary<string, List<string>> _sentimentKeywords;

        public sentiment_detection()
        {
            // Initialize sentiment keywords
            _sentimentKeywords = new Dictionary<string, List<string>>
            {
                { "worried", new List<string> { "worried", "concerned", "anxious", "scared", "afraid", "fear", "nervous", "stress", "stressed", "frightened", "alarmed" } },
                { "curious", new List<string> { "curious", "interested", "fascinated", "learn", "wonder", "how", "what", "why", "tell me", "explain", "help me understand" } },
                { "frustrated", new List<string> { "frustrated", "annoyed", "upset", "confused", "struggling", "difficult", "complicated", "hard", "complex", "can't", "cannot", "don't understand" } }
            };
        }

        public string DetectSentiment(string input)
        {
            // Check for each sentiment
            foreach (var sentiment in _sentimentKeywords)
            {
                foreach (var keyword in sentiment.Value)
                {
                    if (Regex.IsMatch(input, $@"\b{Regex.Escape(keyword)}\b", RegexOptions.IgnoreCase))
                    {
                        return sentiment.Key;
                    }
                }
            }

            return ""; // No sentiment detected
        }
    }
}