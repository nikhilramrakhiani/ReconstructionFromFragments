using System;
using System.Collections.Generic;

namespace ReconstructionFromFragments
{
    public class Program
    {
        static void Main()
        {
            var stringFragments = new List<string> {"all is well", "ell that en", "hat end", "t ends well"};

            var response = ReconstructFromFragments(stringFragments);

            Console.WriteLine(response);
            Console.ReadLine();
        }

        /// <summary>
        /// Reads text fragments and attempts to reconstruct the original document out of the fragments
        /// </summary>
        /// <param name="stringFragments">Array of string fragments</param>
        /// <returns>Final reconstructed string value</returns>
        public static string ReconstructFromFragments(List<string> stringFragments)
        {
            ValidateInput(stringFragments);

            if (stringFragments.Count == 0)
                return string.Empty;

            while (stringFragments.Count > 1)
            {
                MatchAndMergeFragments(stringFragments);
            }

            return stringFragments[0];
        }

        private static void MatchAndMergeFragments(IList<string> fragments)
        {
            var maxFragmentComparisonInfo = new FragmentComparison(ComparisonType.NoMatch);

            var firstMatchedIndex = 0;
            var secondMatchedIndex = 0;

            //loop through each item in the list and compare with other items in the list
            for (var i = 0; i < fragments.Count - 1; i++)
            {
                var firstFragment = fragments[i];

                //Check overlap match with the rest of the list
                for (var j = i + 1; j < fragments.Count; j++)
                {
                    var secondFragment = fragments[j];

                    var currentFragmentComparisonInfo = FragmentComparisonHelper.GetFragmentComparison(firstFragment, secondFragment);

                    maxFragmentComparisonInfo = currentFragmentComparisonInfo;
                    firstMatchedIndex = i;
                    secondMatchedIndex = j;
                }
            }

            var matchedFragmentResultValue = maxFragmentComparisonInfo.GetMatchedFragments();

            //replace the first fragment with the result of the overlapping
            fragments[firstMatchedIndex] = matchedFragmentResultValue;

            //remove the second fragment
            fragments.RemoveAt(secondMatchedIndex);
        }

        private static void ValidateInput(List<string> input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));
        }
    }
}
