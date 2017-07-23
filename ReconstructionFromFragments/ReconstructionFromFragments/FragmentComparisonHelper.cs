using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReconstructionFromFragments
{
    public class FragmentComparisonHelper
    {
        private class StringLengthComparer : IComparer<string>
        {
            public int Compare(string a, string b)
            {
                if (a != null && b != null)
                    return a.Length - b.Length;
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Returns comparison information between two strings
        /// </summary>
        /// <param name="first">First string value</param>
        /// <param name="second">Second string value</param>
        /// <returns>Returns comparison value</returns>
        public static FragmentComparison GetFragmentComparison(string first, string second)
        {
            if (first.Equals(second))
                return new FragmentComparison(first, second, ComparisonType.ExactMatch, first.Length);

            var comparisonType = ComparisonType.NoMatch;
            var maxMatchedLength = 0;

            if (first.Length > second.Length && first.Contains(second))
            {
                comparisonType = ComparisonType.FirstContainsSecond;
                maxMatchedLength = second.Length;
            }
            else if (second.Length > first.Length && second.Contains(first))
            {
                comparisonType = ComparisonType.SecondContainsFirst;
                maxMatchedLength = first.Length;
            }

            var overlapLength = GetMatchedLength(first, second);
            if (overlapLength > maxMatchedLength)
            {
                comparisonType = ComparisonType.FirstPreceedsSecond;
                maxMatchedLength = overlapLength;
            }

            overlapLength = GetMatchedLength(second, first);
            if (overlapLength > maxMatchedLength)
            {
                comparisonType = ComparisonType.SecondPreceedsFirst;
                maxMatchedLength = overlapLength;
            }

            if (maxMatchedLength == 0)
                return new FragmentComparison(first, second, ComparisonType.NoMatch, 0);
            else
                return new FragmentComparison(first, second, comparisonType, maxMatchedLength);

        }

        /// <summary>
        /// Returns matched length between two strings
        /// </summary>
        /// <param name="begin">First string value</param>
        /// <param name="end">Second string value</param>
        /// <returns>Returns matched length between two strings</returns>
        private static int GetMatchedLength(string begin, string end)
        {
            var suffixes = GetSuffixes(begin);
            var prefixes = GetPrefixes(end);

            suffixes = suffixes.Intersect(prefixes).ToList();

            if (!suffixes.Any())
                return 0;

            var overlappedSequences = new SortedSet<string>(new StringLengthComparer());
            overlappedSequences.UnionWith(suffixes);
            
            return overlappedSequences.Last().Length;
        }

        private static List<string> GetPrefixes(string fragment)
        {
            var prefixes = new List<string>();

            var stringBuilder = new StringBuilder(fragment.Length);

            for (var i = 0; i < fragment.Length - 1; i++)
            {
                stringBuilder.Append(fragment.ToCharArray()[i]);
                prefixes.Add(stringBuilder.ToString());
            }

            return prefixes;
        }

        private static List<string> GetSuffixes(string fragment)
        {

            var suffixes = new List<string>();

            var stringBuilder = new StringBuilder(fragment.Length);

            for (var i = fragment.Length - 1; i >= 1; i--)
            {
                stringBuilder.Insert(0, fragment.ToCharArray()[i]);
                suffixes.Add(stringBuilder.ToString());
            }

            return suffixes;
        }
    }
}