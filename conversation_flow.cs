using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace cybersecurityawarenessbot
{
    public class conversation_flow
    {
        private string _currentTopic = "";
        private Dictionary<string, List<string>> _followUpResponses;

        public conversation_flow()
        {
            // Initialize follow-up responses for each topic
            _followUpResponses = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
            {
                { "password", new List<string>
                    {
                        "Would you like to know how to create a strong password?",
                        "Did you know that using a password manager can help you maintain unique passwords for all your accounts?",
                        "Another important aspect of password security is changing them regularly, especially for sensitive accounts."
                    }
                },
                { "phishing", new List<string>
                    {
                        "Would you like more specific tips on how to identify phishing emails?",
                        "Many phishing attempts also occur through text messages and social media. Would you like to learn about those?",
                        "Organizations can implement technical safeguards against phishing. Would you like to know about those?"
                    }
                },
                { "privacy", new List<string>
                    {
                        "As someone interested in privacy, you might want to review the security settings on your accounts.",
                        "Have you considered using privacy-focused browsers and search engines?",
                        "Would you like to know more about how to protect your privacy on social media specifically?"
                    }
                },
                { "scam", new List<string>
                    {
                        "Would you like to learn about the most common types of online scams currently circulating?",
                        "Do you have any specific questions about how to recognize or report scams?",
                        "Would you like to know what to do if you think you've already fallen for a scam?"
                    }
                },
                { "malware", new List<string>
                    {
                        "Would you like to know the warning signs that your device might be infected with malware?",
                        "Do you have questions about specific types of malware like ransomware or spyware?",
                        "Would you like recommendations for reliable antimalware software?"
                    }
                }
            };
        }

        public void SetCurrentTopic(string topic)
        {
            _currentTopic = topic;
        }

        public bool IsFollowUpQuestion(string input)
        {
            if (string.IsNullOrEmpty(_currentTopic))
                return false;

            // Check if this is a follow-up question to the current topic
            string[] followUpIndicators = { "more", "tell me more", "explain", "elaborate", "details", "yes", "sure", "okay", "examples", "how" };
            return followUpIndicators.Any(indicator =>
                Regex.IsMatch(input, $@"\b{Regex.Escape(indicator)}\b", RegexOptions.IgnoreCase));
        }

        public string HandleFollowUp(string input)
        {
            if (string.IsNullOrEmpty(_currentTopic) || !_followUpResponses.ContainsKey(_currentTopic))
                return "Could you please clarify what you'd like to know more about?";

            // Get a relevant follow-up response
            List<string> responses = _followUpResponses[_currentTopic];
            Random rand = new Random();
            return responses[rand.Next(0, responses.Count)];
        }
    }
}