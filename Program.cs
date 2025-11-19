
/*
Nathan Hillhouse
Lab 11 - Mad Libs 2
11/18/2025
*/


class Program {
    static void Main(string[] filenames) {
        Console.Clear();
        Dictionary<string, string[]> wordbank = CreateDict("WordBank.txt");
        foreach (string filename in filenames) {
            string storytext = File.ReadAllText(filename);
            story(storytext, wordbank);
            //string[] splittext = storytext.Split(" ", "::");
            //story(filename);
        }
    }

    static string PickRandom(Dictionary<string, string[]> dict, string key)
    {
        Random rand = new Random();
        int item = rand.Next(dict[key].Count());
        return dict[key][item];
    }
    static void story(string originalStory, Dictionary<string, string[]> wordbank)
    {
        Random rand = new Random();
        char[] vowels = ['a', 'e', 'i', 'o', 'u'];
        string[] splitStory = originalStory.Split(" ");
        List<string> story = new List<string>();


        for (int item = 0; item < splitStory.Length; item++)
        {
            string word = "";
            if (splitStory[item].Contains("::"))
            {
                word += splitStory[item];

                string prefix = "a";
                bool end_sentence = false;
                if (word.Contains('.')) end_sentence = true;
                word = word.Replace("(", "").Replace(")", "").Replace(".", "");
                bool changed = false;

                foreach (char x in vowels)
                {
                    if (word.StartsWith(x)) 
                    {
                        prefix = "an";
                        break;
                    }
                    else prefix = "a";
                }
                //Console.Write($"Please choose {prefix} {word}: ");
                string[] newword = word.Split("::");
                word = wordbank[newword[1]][rand.Next(0,wordbank[newword[1]].Length)];
                word = newword[0];
                if (end_sentence) word += '.';

                if (splitStory[item - 1].Contains("/"))
                {
                    foreach (char vowel in vowels) if (splitStory[item].StartsWith(vowel)) // FIX TO CHANGE THE PREVIOUS WORD TO A OR AN
                        {
                            //word = "an";
                            changed = true;
                        }
                    if (!changed) story[story.Count-1] = "an";
                    else story[story.Count-1] = "a";
                    Console.WriteLine(story[story.Count-1]);
                }
            }
            else word = splitStory[item];
            story.Add(word);
        }

        foreach (string item in story)
        {
            Console.Write(item + " ");
        }
    }

    static Dictionary<string, string[]> CreateDict(string file)
    {
        string[] words = File.ReadAllLines(file);
        Dictionary<string, string[]> wordDict = new Dictionary<string, string[]>(); 
        for  (int i = 0; i < words.Length; i++) 
        {
            string item = words[i];
            string[] itemlist = item.Split(',');
            //foreach (string x in itemlist) Console.WriteLine(x); 
            wordDict[itemlist[0].ToLower()] = itemlist[1..itemlist.Length];
            //foreach (string x in wordDict[itemlist[i]]) Console.WriteLine(x);
            
        }

        return wordDict;
    }
}


