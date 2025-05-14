using System.Collections.Generic;
using System;

namespace cybersecurityawarenessbot
{
    public class random_responses
    {


        // Dictionary to store multiple responses for each topic
        private Dictionary<string, List<string>> _topicResponses;
        private Random random;
        private Random _random;

        public random_responses()
        {
            random = new Random();

            // Initialize with multiple responses for each topic
            _topicResponses = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
            {
                { "phishing", new List<string>
                    {
                        "Be cautious of emails asking for personal information. Scammers often disguise themselves as trusted organizations.",
                        "Don't click on links in suspicious emails. Instead, type the website address directly in your browser.",
                        "Check for spelling errors and unusual email addresses - these are common signs of phishing attempts.",
                        "If an email creates urgency or threatens negative consequences, be extra skeptical - this is a common phishing tactic.",
                        "Legitimate organizations won't ask for sensitive information via email. When in doubt, contact the organization directly through official channels."
                    }
                },
                { "password", new List<string>
                    {
                        "Use a different password for each important account. This prevents hackers from accessing multiple accounts if one password is compromised.",
                        "Create passwords with at least 12 characters that include uppercase letters, lowercase letters, numbers, and symbols.",
                        "Consider using a password manager to generate and store strong, unique passwords securely.",
                        "Never share your passwords with others or store them in plain text on your devices.",
                        "Change your passwords regularly, especially for important accounts like banking and email."
                    }
                },
                { "malware", new List<string>
                    {
                        "Keep your operating system and software updated to protect against known security vulnerabilities.",
                        "Only download software from official sources and app stores to reduce the risk of malware.",
                        "Use reputable antivirus software and keep it updated to detect and remove malware.",
                        "Be careful when opening email attachments, even from known senders, as they might contain malware.",
                        "Regularly scan your computer for malware and suspicious programs."
                    }
                },
                { "privacy", new List<string>
                    {
                        "Regularly review and update the privacy settings on your social media accounts to control what information is visible.",
                        "Be mindful of what you share online - once information is posted, it can be difficult to completely remove.",
                        "Use a VPN when connected to public Wi-Fi to protect your data from potential eavesdroppers.",
                        "Consider using privacy-focused browsers and search engines that don't track your online activities.",
                        "Regularly clear your browsing history, cookies, and cached data to minimize tracking."
                    }
                },
                { "scam", new List<string>
                    {
                        "If something seems too good to be true, it probably is. Be skeptical of amazing offers and unexpected winnings.",
                        "Never send money or provide financial information to someone you've only met online.",
                        "Research unfamiliar websites and companies before making purchases or providing personal information.",
                        "Be wary of unsolicited calls claiming to be from tech support, government agencies, or banks.",
                        "Take your time when making decisions. Scammers often create urgency to pressure victims into acting quickly."
                    }
                }
            };
        }

        public bool HasMultipleResponses(string topic)
        {
            return _topicResponses.ContainsKey(topic);
        }

        public string GetRandomResponse(string topic, string sentiment = "")
        {
            if (!_topicResponses.ContainsKey(topic))
                return null;

            // Get a random response for the topic
            List<string> responses = _topicResponses[topic];
            int randomIndex = _random.Next(0, responses.Count);

            // Add sentiment-based prefix if sentiment is detected
            if (!string.IsNullOrEmpty(sentiment))
            {
                switch (sentiment)
                {
                    case "worried":
                        return "I understand you're concerned. " + responses[randomIndex];
                    case "curious":
                        return "Great question! " + responses[randomIndex];
                    case "frustrated":
                        return "I see you're having some difficulty with this. " + responses[randomIndex];
                    default:
                        return responses[randomIndex];
                }
            }

            return responses[randomIndex];
        }
    }
}