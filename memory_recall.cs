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
            _conversationHistory.Add($"User: {input}");

            // Try to extract user information
            ExtractUserInfo(input);

            // Save the updated memory
            SaveMemory();
        }

        public void StoreResponse(string response)
        {
            // Add bot responses to conversation history too
            _conversationHistory.Add($"Bot: {response}");

            // Save after each update
            SaveMemory();
        }

        public bool HasUserInformation()
        {
            return _userInformation.Count > 0;
        }

        public string GetUserInfo(string key)
        {
            if (_userInformation.ContainsKey(key))
            {
                return _userInformation[key];
            }
            return null;
        }

        public Dictionary<string, string> GetAllUserInfo()
        {
            return new Dictionary<string, string>(_userInformation);
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
            // Try to extract name with more flexible pattern
            Match nameMatch = Regex.Match(input, @"(?:my|I'm|I am|call me)(?:'s| is| am)? (?:name(?:'s| is)? )?(\w+)", RegexOptions.IgnoreCase);
            if (nameMatch.Success)
            {
                _userInformation["name"] = nameMatch.Groups[1].Value;
            }

            // Try to extract interests with more flexible pattern
            Match interestMatch = Regex.Match(input, @"(?:interested in|like|enjoy|love) (\w+)", RegexOptions.IgnoreCase);
            if (interestMatch.Success)
            {
                _userInformation["interest"] = interestMatch.Groups[1].Value;
            }

            // Additional extraction for role or job
            Match roleMatch = Regex.Match(input, @"(?:I|I'm|I am)(?: a| an)? (\w+ ?(?:developer|engineer|manager|admin|specialist|professional|student))", RegexOptions.IgnoreCase);
            if (roleMatch.Success)
            {
                _userInformation["role"] = roleMatch.Groups[1].Value;
            }
        }

        private void LoadMemory()
        {
            try
            {
                if (File.Exists(_memoryFilePath))
                {
                    string[] lines = File.ReadAllLines(_memoryFilePath);
                    bool inUserInfo = true;

                    foreach (string line in lines)
                    {
                        // Check for section separators
                        if (line == "--- USER INFO ---")
                        {
                            inUserInfo = true;
                            continue;
                        }
                        else if (line == "--- CONVERSATION HISTORY ---")
                        {
                            inUserInfo = false;
                            continue;
                        }

                        // Process based on section
                        if (inUserInfo)
                        {
                            if (line.Contains(":"))
                            {
                                string[] parts = line.Split(new char[] { ':' }, 2);
                                if (parts.Length == 2)
                                {
                                    _userInformation[parts[0].Trim()] = parts[1].Trim();
                                }
                            }
                        }
                        else
                        {
                            _conversationHistory.Add(line);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                Console.WriteLine($"Error loading memory: {ex.Message}");
                // Silently continue execution - memory functions are non-critical
            }
        }

        private void SaveMemory()
        {
            try
            {
                List<string> lines = new List<string>();

                // Save user information section
                lines.Add("--- USER INFO ---");
                foreach (var kvp in _userInformation)
                {
                    lines.Add($"{kvp.Key}: {kvp.Value}");
                }

                // Save conversation history section
                lines.Add("--- CONVERSATION HISTORY ---");
                // Limit history to most recent 50 exchanges to prevent file from growing too large
                int startIndex = Math.Max(0, _conversationHistory.Count - 50);
                for (int i = startIndex; i < _conversationHistory.Count; i++)
                {
                    lines.Add(_conversationHistory[i]);
                }

                File.WriteAllLines(_memoryFilePath, lines);
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                Console.WriteLine($"Error saving memory: {ex.Message}");
                // Silently continue execution - memory functions are non-critical
            }
        }

        // Method to clear memory (useful for testing or resetting)
        public void ClearMemory()
        {
            _userInformation.Clear();
            _conversationHistory.Clear();

            try
            {
                if (File.Exists(_memoryFilePath))
                {
                    File.Delete(_memoryFilePath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error clearing memory file: {ex.Message}");
            }
        }
    }
}