using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace exsam
{
    public class UserDictionary
    {
        public string Name { get; set; }
        public Dictionary<string, List<string>> Words { get; set; }

        private readonly string _pahtFileNames ;
        public UserDictionary()
        {
            this._pahtFileNames = "NamesFailes.json";
            Console.WriteLine("Enter the name of the new dictionary:");
            string name = Console.ReadLine();
            Name = name;
            Words = GetWords();
            WriteToFileName();

        }

        public void WriteToFileName()
        {
            string fromJson = ReadFromFileName();
            List<string> namesList;

            if (File.Exists(_pahtFileNames))
            {
                namesList = JsonConvert.DeserializeObject<List<string>>(fromJson);
            }
            else
            {
                namesList = new List<string>();
            }

            if (!namesList.Contains(Name))
            {
                namesList.Add(Name);
            }

            string newJson = JsonConvert.SerializeObject(namesList, Formatting.Indented);

            File.WriteAllText(_pahtFileNames, newJson);
        }

        public string ReadFromFileName()
        {
            if(File.Exists(_pahtFileNames))
            {
                return File.ReadAllText(_pahtFileNames);
            }
            else { return "[]"; }
        }

        public Dictionary<string, List<string>> GetWords()
        {
            return JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(ReadJsonFile());
        }

        public string ReadJsonFile()
        {
            try
            {
                if (File.Exists(GetJsonFileName()))
                {
                    return File.ReadAllText(GetJsonFileName());
                }
                else
                {
                    return "{}"; 
                }
            }
            catch (IOException e)
            {
                Console.WriteLine($"An error occurred while reading the file: {e.Message}");
                return "{}"; 
            }
        }

        public string GetJsonFileName()
        {
            return $"{Name}.json";
        }

        public void AddWord()
        {
            Console.WriteLine("Enter word.");
            string word = Console.ReadLine();

            var isWordFound = Words?.ContainsKey(word) ?? false;

            if (!isWordFound)
            {
                List<string> translations = new List<string>();
                string choice ;
                do
                {
                    Console.WriteLine("Enter word-translation.");
                    string translation = Console.ReadLine();

                    translations.Add(translation);

                    Console.WriteLine("continue 1 if you want to finish press 2");
                    choice = Console.ReadLine();
                }
                while (choice == "1");

                Words.Add(word, translations);
                SaveToJsonFile();

            }
            else
            {
                Console.WriteLine("The word exist in dictionary.");
            }
        }

        public void DeleteWord()
        {
            Console.WriteLine("Enter the word you want to delete");
            string word = Console.ReadLine();

            if ( Words?.ContainsKey(word) ?? false)
            {
                Words.Remove(word);
                SaveToJsonFile();
                Console.WriteLine($"The word '{word}' has been removed from the dictionary.");
            }
            else
            {
                Console.WriteLine($"The word '{word}' wasn't found in the dictionary.");
            }

        }

        public void DeleteTranslate()
        {
            Console.WriteLine("Enter the word you want to delete a translation from:");
            string word = Console.ReadLine();

            if (Words.ContainsKey(word))
            {
                Console.WriteLine("Enter the translation you want to delete:");
                string translate = Console.ReadLine();

                if (Words[word].Contains(translate))
                {
                    Words[word].Remove(translate);

                    if (Words[word].Count == 0)
                    {
                        Words.Remove(word);
                        Console.WriteLine($"The word '{word}' has been removed from the dictionary as it had no more translations.");
                    }
                    else
                    {
                        Console.WriteLine($"The translation '{translate}' has been removed from the word '{word}'.");
                    }
                    SaveToJsonFile();
                }
                else
                {
                    Console.WriteLine($"The translation '{translate}' was not found for the word '{word}'.");
                }
            }
            else
            {
                Console.WriteLine($"The word '{word}' was not found in the dictionary.");
            }
        }

        public void AddTranlate()
        {
            Console.WriteLine("Enter word.");
            string word = Console.ReadLine();

            if (!Words.ContainsKey(word))
            {
                Console.WriteLine("The word dosn`t exist in dictionary.");
            }
            else
            {
                string choice;
                do
                {
                    Console.WriteLine("Enter word-translation.");
                    string translation = Console.ReadLine();

                    List<string> translates = new List<string>();
                    bool isFound = Words?.TryGetValue(word, out translates) ?? false;

                    if (translates.Contains(translation))
                    {
                        Console.WriteLine("The translate exist in dictionary.");
                    }

                    if (isFound)
                    {
                        translates.Add(translation);
                    }

                    Console.WriteLine("continue 1 if you want to finish press 2");
                    choice = Console.ReadLine();
                }
                while (choice == "1");

                SaveToJsonFile();
            }
        }

        public void SaveToJsonFile()
        {
            string json = JsonConvert.SerializeObject(Words, Formatting.Indented);

            using (StreamWriter sw = new StreamWriter(GetJsonFileName(), false))
            {
                sw.WriteLine(json);
            }
        }

        public void CheckWord()
        {
            Console.WriteLine("Enter the word you want to find");
            string word = Console.ReadLine();

            if (Words.TryGetValue(word, out List<string> translations))
            {
                Console.WriteLine($"{word} - {string.Join(", ", translations)}");
            }
            else
            {
                Console.WriteLine($"The word '{word}' is not found in the dictionary.");
            }
        }


        public void ShowDictionary()
        {
            foreach (var kvp in Words.OrderBy(kvp => kvp.Key))
            {
                Console.WriteLine($"{kvp.Key}: {string.Join(", ", kvp.Value)}");
            }
        }

        public UserDictionary CreateDictionary()
        {
            return new UserDictionary();
        }

        public  UserDictionary OpenDictionary()
        {
            int count = 0;
            string names = ReadFromFileName();
            List<string> namesArray = JsonConvert.DeserializeObject<List<string>>(names);
            foreach (var item in namesArray)
            {
                ++count;
                Console.WriteLine($"{count}:{item}");
            }
            Console.WriteLine();
            Console.WriteLine("Enter the name of the existing dictionary:");
            string name = Console.ReadLine();
            UserDictionary userDictionary = new UserDictionary();

            if (userDictionary.Words.Count == 0)
            {
                Console.WriteLine($"The dictionary '{name}' is empty or doesn't exist.");
            }
            else
            {
                Console.WriteLine($"The dictionary '{name}' has been successfully opened.");
            }

            return userDictionary;
        }

        public  void UsingDictionary()
        {

            Console.WriteLine("You have opened the 'Dictionary' application. Choose an action:" +
                "\n 1. Create a new dictionary;" +
                "\n 2. Open alredy exist dictionary;");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateDictionary();
                    WorkInDictionary();
                    break;
                case "2":
                    OpenDictionary();
                    WorkInDictionary();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please choose 1 or 2.");
                    UsingDictionary();
                    break;
            }

        }

        public  void WorkInDictionary()
        {
            string choice = "";
            while (choice != "7")
            {
                Console.WriteLine("What do you want to do:" +
                    "\n 1. Add word to the dictionary:" +
                    "\n 2. Add new tanslate-word to the dictionary:" +
                    "\n 3. Delete word to the dictionary:" +
                    "\n 4. Delete tanslate-word to the dictionary:" +
                    "\n 5. Show all the dictionary;" +
                    "\n 6. Check the word in the dictionary;" +
                    "\n 7. Retun to main menu;" +
                    "\n 8. finish working with the dictionary");
                choice = Console.ReadLine();


                switch (choice)
                {
                    case "1":
                        AddWord();
                        break;
                    case "2":
                        AddTranlate();
                        break;
                    case "3":
                        DeleteWord();
                        break;
                    case "4":
                        DeleteTranslate();
                        break;
                    case "5":
                        ShowDictionary();
                        break;
                    case "6":
                        CheckWord();
                        break;
                    case "7":
                        UsingDictionary();
                        break;
                    case "8":
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please choose 1, 2, 3, 4, 5, 6 or 7.");
                        break;
                }

            }
        }

    }
}
