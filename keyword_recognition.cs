using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace cybersecurityawarenessbot
{
    public class Keyword_recognition
    {
        // Dictionary to store keywords and responses
        private Dictionary<string, string> _keywordResponses;
        private string _detectedKeyword = "";

        public Keyword_recognition()
        {
            // Initialize dictionary with cybersecurity keywords and responses
            _keywordResponses = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "password", "Make sure to use strong, unique passwords for each account. Avoid using personal details in your passwords." },
                { "scam", "Be cautious of unsolicited communications asking for personal information. Legitimate organizations won't ask for sensitive information via email or text." },
                { "privacy", "Protect your privacy online by regularly reviewing privacy settings on your accounts, using VPNs when on public networks, and being mindful of what you share on social media." },
                { "phishing", "Phishing attacks attempt to steal sensitive information by disguising as trustworthy entities. Always verify the sender's email address and never click on suspicious links." },
                { "malware", "Malware is malicious software designed to damage or gain unauthorized access to systems. Keep your software updated and use antivirus protection." },
                { "vpn", "A Virtual Private Network (VPN) encrypts your internet connection and masks your IP address. Use a VPN when connected to public Wi-Fi." },
                { "encryption", "Encryption is a security measure that encodes data to protect it from unauthorized access. Use encrypted connections (HTTPS) whenever possible." },
                { "firewall", "A firewall acts as a barrier between your trusted network and untrusted networks. Ensure your firewall is enabled on your devices." },
                { "authentication", "Authentication verifies a user's identity. Use multi-factor authentication (MFA) whenever possible for added security." },
                { "backup", "Regular backups protect your data in case of ransomware attacks or hardware failures. Follow the 3-2-1 rule: 3 copies, 2 different media, 1 off-site." }
            };
        }

        public string ProcessInput(string userInput)
        {
            if (string.IsNullOrWhiteSpace(userInput))
                return null;

            // Normalize the input (trim, remove extra spaces)
            userInput = userInput.Trim();

            // First try: Standard keyword matching (more flexible)
            foreach (var keyword in _keywordResponses.Keys)
            {
                // Check if keyword exists in input (more lenient approach)
                if (userInput.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    _detectedKeyword = keyword;
                    return _keywordResponses[keyword];
                }
            }

            // Second try: Word boundary matching (more precise but potentially stricter)
            foreach (var keyword in _keywordResponses.Keys)
            {
                // Using regex to find the keyword as a whole word
                string pattern = $@"\b{Regex.Escape(keyword)}\b";
                if (Regex.IsMatch(userInput, pattern, RegexOptions.IgnoreCase))
                {
                    _detectedKeyword = keyword;
                    return _keywordResponses[keyword];
                }
            }

            // Third try: Partial matching for longer keywords (like "password protection")
            foreach (var keyword in _keywordResponses.Keys)
            {
                if (keyword.Length > 4) // Only try partial matching for longer keywords
                {
                    string partialPattern = Regex.Escape(keyword);
                    if (Regex.IsMatch(userInput, partialPattern, RegexOptions.IgnoreCase))
                    {
                        _detectedKeyword = keyword;
                        return _keywordResponses[keyword];
                    }
                }
            }

            // Check for general cybersecurity terms
            if (ContainsCybersecurityTerms(userInput))
            {
                return "I notice you're asking about cybersecurity. Could you be more specific? You can ask about passwords, privacy, scams, phishing, malware, or other security topics.";
            }

            return null;
        }

        public string GetDetectedKeyword()
        {
            return _detectedKeyword;
        }

        private bool ContainsCybersecurityTerms(string input)
        {
            string[] generalTerms = { "cyber", "security", "hack", "threat", "data", "breach", "protect", "online", "secure", "risk" };

            // First check for exact terms
            foreach (var term in generalTerms)
            {
                if (input.IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return true;
                }
            }

            return false;
        }

        // Method to list all available keywords for reference
        public List<string> GetAvailableKeywords()
        {
            return _keywordResponses.Keys.ToList();
        }
    }
}