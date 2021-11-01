using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
namespace Lab3Q1
{
    public class WordCountTester
    {
        static int Main()
        {
            int startIdx = 0, count = 0;
            try {


                //=================================================
                // Implement your tests here. Check all the edge case scenarios.
                // Create a large list which iterates over WCTester
                //=================================================

                var test_list = new List<string>
                {
                    " ",                                // string with 1 empty character test
                    "123",                              // string with number test
                    " letter",                          // string start with space test
                    "letter ",                          // string end with space test
                    " letter 123 ",                     // string mix with letter and number and spaces
                    " !  ?    ",                        // string with special characters and space
                    "  letter    another letter ",      // strong with odd spacing
                };
                var expectedResults_list = new List<int>
                {
                    0,
                    1,
                    1,
                    1,
                    2,
                    2,
                    3
                };
                for (int i = 0; i < test_list.Count; i++)
                {
                    WCTester(test_list[i], startIdx, expectedResults_list[i]);
                }
                

            }
            catch(UnitTestException e)
            {
                Console.WriteLine(e);
            }
            return count;
        }


        /**
         * Tests word_count for the given line and starting index
         * @param line line in which to search for words
         * @param start_idx starting index in line to search for words
         * @param expected expected answer
         * @throws UnitTestException if the test fails
         */
        static void WCTester(string line, int start_idx, int expected) {

            //=================================================
            // Implement: comparison between the expected and
            // the actual word counter results
            //=================================================
            int result = HelperFunctions.WordCount(ref line, start_idx);
            Console.WriteLine("String: {0}", line);
            Console.WriteLine("Result: {0}, Expected: {1}, Match?: {2}", result, expected, result == expected ? "Yes" : "No");
            if (result != expected) {
                throw new Lab3Q1.UnitTestException(ref line, start_idx, result, expected, String.Format("UnitTestFailed: result:{0} expected:{1}, line: {2} starting from index {3}", result, expected, line, start_idx));
            }

        }
    }
}
