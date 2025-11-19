
/*
Nathan Hillhouse
Lab 11 - Mad Libs 2
11/18/2025
*/


class Program {
    static void Main(string[] filenames) {
        foreach (string filename in filenames) {
            Console.WriteLine(filename);

            string storytext = File.ReadAllText(filename);
            story(storytext);
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
    static void story(string originalStory)
    {
        Console.Clear();

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
                Console.Write($"Please choose {prefix} {word}: ");
                word = Console.ReadLine();
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
}


