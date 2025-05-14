using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace cybersecurityawarenessbot
{
    public class sentiment_detection
    {
        private Dictionary<string, List<string>> _sentimentKeywords;

        public sentiment_detection()
        {
            // Initialize sentiment keywords with more comprehensive lists
            _sentimentKeywords = new Dictionary<string, List<string>>
            {
                { "worried", new List<string> {
                    "worried", "concern", "concerned", "anxiety", "anxious", "scared", "afraid",
                    "fear", "nervous", "stress", "stressed", "frightened", "alarmed", "uneasy",
                    "apprehensive", "dread", "panic", "terrified", "paranoid", "vulnerability",
                    "vulnerable", "threat", "risk", "danger", "dangerous", "insecure", "unsafe"
                }},

                { "curious", new List<string> {
                    "curious", "interest", "interested", "fascinated", "learn", "wonder",
                    "how", "what", "why", "tell me", "explain", "help me understand",
                    "knowledge", "information", "details", "specifics", "more about",
                    "would like to know", "discover", "find out", "teach me"
                }},

                { "frustrated", new List<string> {
                    "frustrated", "annoyed", "upset", "confused", "struggling", "difficult",
                    "complicated", "hard", "complex", "can't", "cannot", "don't understand",
                    "stuck", "lost", "overwhelmed", "fail", "failure", "giving up", "too much",
                    "impossible", "challenging", "trouble", "problems with", "issue", "issues"
                }}
            };
        }

        public string DetectSentiment(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "";

            // Convert input to lowercase for case-insensitive matching
            input = input.ToLower();

            // Track sentiment scores instead of just first match
            Dictionary<string, int> sentimentScores = new Dictionary<string, int>();
            foreach (var sentiment in _sentimentKeywords.Keys)
            {
                sentimentScores[sentiment] = 0;
            }

            // Count occurrences of each sentiment's keywords
            foreach (var sentimentPair in _sentimentKeywords)
            {
                string sentiment = sentimentPair.Key;
                List<string> keywords = sentimentPair.Value;

                foreach (var keyword in keywords)
                {
                    // Handle multi-word keywords differently
                    if (keyword.Contains(" "))
                    {
                        if (input.Contains(keyword))
                        {
                            sentimentScores[sentiment]++;
                        }
                    }
                    else
                    {
                        // Use regex to match whole words only
                        MatchCollection matches = Regex.Matches(input, $@"\b{Regex.Escape(keyword)}\b", RegexOptions.IgnoreCase);
                        sentimentScores[sentiment] += matches.Count;
                    }
                }
            }

            // Find sentiment with highest score
            string dominantSentiment = "";
            int highestScore = 0;

            foreach (var score in sentimentScores)
            {
                if (score.Value > highestScore)
                {
                    highestScore = score.Value;
                    dominantSentiment = score.Key;
                }
            }

            // Return the dominant sentiment (if any was detected)
            return highestScore > 0 ? dominantSentiment : "";
        }

        // Optional: Method to get all detected sentiments with their scores
        public Dictionary<string, int> GetAllSentimentScores(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return new Dictionary<string, int>();

            // Convert input to lowercase for case-insensitive matching
            input = input.ToLower();

            Dictionary<string, int> sentimentScores = new Dictionary<string, int>();
            foreach (var sentiment in _sentimentKeywords.Keys)
            {
                sentimentScores[sentiment] = 0;
            }

            // Count occurrences of each sentiment's keywords
            foreach (var sentimentPair in _sentimentKeywords)
            {
                string sentiment = sentimentPair.Key;
                List<string> keywords = sentimentPair.Value;

                foreach (var keyword in keywords)
                {
                    if (keyword.Contains(" "))
                    {
                        if (input.Contains(keyword))
                        {
                            sentimentScores[sentiment]++;
                        }
                    }
                    else
                    {
                        MatchCollection matches = Regex.Matches(input, $@"\b{Regex.Escape(keyword)}\b", RegexOptions.IgnoreCase);
                        sentimentScores[sentiment] += matches.Count;
                    }
                }
            }

            return sentimentScores;
        }
    }
}