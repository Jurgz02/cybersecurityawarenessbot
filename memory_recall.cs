using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace cybersecurityawarenessbot
{
    public class memory_recall
    {
        private Dictionary<string, string> _userInformation = new Dictionary<string, string>();
        private List<string> _conversationHistory = new List<string>();
        private string _memoryFilePath;

        public memory_recall()
        {
            // Set up the path for storing memory
            string full_path = AppDomain.CurrentDomain.BaseDirectory;
            string new_path = full_path.Replace("bin\\Debug\\", "");
            _memoryFilePath = Path.Combine(new_path, "memory.txt");

            // Load any existing memory
            LoadMemory();
        }

        public void StoreInput(string input)
        {
            // Add to conversation history
            _conversationHistory.Add(input);

            // Try to extract user information
            ExtractUserInfo(input);

            // Save the updated memory
            SaveMemory();
        }

        public bool HasUserInformation()
        {
            return _userInformation.Count > 0;
        }

        public string PersonalizeResponse(string response)
        {
            // Personalize response based on stored user information
            if (_userInformation.ContainsKey("name"))
            {
                // If we know the user's name, occasionally use it
                if (new Random().Next(0, 3) == 0) // 1 in 3 chance
                {
                    return $"{response} (And {_userInformation["name"]}, remember that cybersecurity is everyone's responsibility!)";
                }
            }

            // If we know their interests
            if (_userInformation.ContainsKey("interest"))
            {
                string interest = _userInformation["interest"];
                if (response.ToLower().Contains(interest.ToLower()))
                {
                    return $"As someone interested in {interest}, {response}";
                }
            }

            return response;
        }

        private void ExtractUserInfo(string input)
        {
            // Try to extract name
            Match nameMatch = Regex.Match(input, @"my name is (\w+)", RegexOptions.IgnoreCase);
            if (nameMatch.Success)
            {
                _userInformation["name"] = nameMatch.Groups[1].Value;
            }

            // Try to extract interests
            Match interestMatch = Regex.Match(input, @"interested in (\w+)", RegexOptions.IgnoreCase);
            if (interestMatch.Success)
            {
                _userInformation["interest"] = interestMatch.Groups[1].Value;
            }
        }

        private void LoadMemory()
        {
            try
            {
                if (File.Exists(_memoryFilePath))
                {
                    string[] lines = File.ReadAllLines(_memoryFilePath);
                    foreach (string line in lines)
                    {
                        if (line.Contains(":"))
                        {
                            string[] parts = line.Split(':');
                            if (parts.Length == 2)
                            {
                                _userInformation[parts[0].Trim()] = parts[1].Trim();
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Silently fail - memory functions are non-critical
            }
        }

        private void SaveMemory()
        {
            try
            {
                List<string> lines = new List<string>();
                foreach (var kvp in _userInformation)
                {
                    lines.Add($"{kvp.Key}: {kvp.Value}");
                }

                File.WriteAllLines(_memoryFilePath, lines);
            }
            catch (Exception)
            {
                // Silently fail - memory functions are non-critical
            }
        }
    }
}