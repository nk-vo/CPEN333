using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;

namespace Lab3Q1
{
    public class HelperFunctions
    {
        /**
         * Counts number of words, separated by spaces, in a line.
         * @param line string in which to count words
         * @param start_idx starting index to search for words
         * @return number of words in the line
         */
        public static int WordCount(ref string line, int start_idx)
        {
            // YOUR IMPLEMENTATION HERE
            int count = 0;
            bool is_space = false;

            if (line.Length >= 2)
            {
                for (int i = start_idx + 1; i < line.Length; i++)
                {
                    
                    if (char.IsWhiteSpace(line[i - 1]) && is_space)
                    {
                        if (char.IsLetterOrDigit(line[i]) || char.IsPunctuation(line[i]))
                            count++;
                    }
                    
                    else if (char.IsLetterOrDigit(line[i]) || char.IsPunctuation(line[i]))
                        is_space = true;
                }
                count++;
            }
            if (line.Length < 2)
            {
                if (char.IsLetterOrDigit(line[start_idx]) || char.IsPunctuation(line[start_idx]))
                    count = 1;
                else
                    count = 0;
            }


            return count;
        }


        /**
        * Reads a file to count the number of words each actor speaks.
        *
        * @param filename file to open
        * @param mutex mutex for protected access to the shared wcounts map
        * @param wcounts a shared map from character -> word count
        */
        public static void CountCharacterWords(string filename,
                                 Mutex mutex,
                                 Dictionary<string, int> wcounts)
        {

        //===============================================
        //  IMPLEMENT THIS METHOD INCLUDING THREAD SAFETY
        //===============================================

            string line;  // for storing each line read from the file
            string character = "";  // empty character to start
            System.IO.StreamReader file = new System.IO.StreamReader(filename);

            int count;

            while ((line = file.ReadLine()) != null)
            {
                //=================================================
                // YOUR JOB TO ADD WORD COUNT INFORMATION TO MAP
                //=================================================

                // Is the line a dialogueLine?
                //    If yes, get the index and the character name.
                //      if index > 0 and character not empty
                //        get the word counts
                //          if the key exists, update the word counts
                //          else add a new key-value to the dictionary
                //    reset the character
                int dialogue_index = IsDialogueLine(line, ref character);

                if (dialogue_index != -1)
                {
                    mutex.WaitOne();
                    if (dialogue_index > 0 && character != string.Empty)
                    {
                        count = HelperFunctions.WordCount(ref line, dialogue_index);
                        if(wcounts.ContainsKey(character))
                        {
                            wcounts[character] += count;
                        }
                        else
                        {
                            wcounts.Add(character, count);
                        }
                    }
                    mutex.ReleaseMutex();
                }
            }
            // Close the file
            file.Close();
        }



        /**
         * Checks if the line specifies a character's dialogue, returning
         * the index of the start of the dialogue.  If the
         * line specifies a new character is speaking, then extracts the
         * character's name.
         *
         * Assumptions: (doesn't have to be perfect)
         *     Line that starts with exactly two spaces has
         *       CHARACTER. <dialogue>
         *     Line that starts with exactly four spaces
         *       continues the dialogue of previous character
         *
         * @param line line to check
         * @param character extracted character name if new character,
         *        otherwise leaves character unmodified
         * @return index of start of dialogue if a dialogue line,
         *      -1 if not a dialogue line
         */
        static int IsDialogueLine(string line, ref string character)
        {

            // new character
            if (line.Length >= 3 && line[0] == ' '
                && line[1] == ' ' && line[2] != ' ')
            {
                // extract character name

                int start_idx = 2;
                int end_idx = 3;
                while (end_idx <= line.Length && line[end_idx - 1] != '.')
                {
                    ++end_idx;
                }

                // no name found
                if (end_idx >= line.Length)
                {
                    return 0;
                }

                // extract character's name
                character = line.Substring(start_idx, end_idx - start_idx - 1);
                return end_idx;
            }

            // previous character
            if (line.Length >= 5 && line[0] == ' '
                && line[1] == ' ' && line[2] == ' '
                && line[3] == ' ' && line[4] != ' ')
            {
                // continuation
                return 4;
            }

            return 0;
        }

        /**
         * Sorts characters in descending order by word count
         *
         * @param wcounts a map of character -> word count
         * @return sorted vector of {character, word count} pairs
         */
        public static List<Tuple<int, string>> SortCharactersByWordcount(Dictionary<string, int> wordcount)
        {

            // Implement sorting by word count here
            var sortedDictionary = wordcount.OrderByDescending(x => x.Value).ThenBy(x => x.Key);
            List<Tuple<int, string>> sortedByValueList = new List<Tuple<int, string>>();
            int count = 0;

            foreach (KeyValuePair<string, int> pairItem in sortedDictionary)
            {
                Tuple<int, string> item = new Tuple<int, string>(pairItem.Value, pairItem.Key);
                sortedByValueList.Add(item);
                count++;
            }
            return sortedByValueList;

        }


        /**
         * Prints the List of Tuple<int, string>
         *
         * @param sortedList
         * @return Nothing
         */
        public static void PrintListofTuples(List<Tuple<int, string>> sortedList)
        {

            // Implement printing here
            foreach (var tuple in sortedList)
            {
                Console.WriteLine("{0} - {1}", tuple.Item1.ToString(), tuple.Item2);
            }
        }
    }
}
