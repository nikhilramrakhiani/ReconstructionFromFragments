using System;
using System.Text;

namespace ReconstructionFromFragments
{
    public class FragmentComparison : IComparable<FragmentComparison>
    {
        public string First { get; }
        public string Second { get; }
        public ComparisonType ComparisonType { get; }
        public int OverlappingLength { get; }

        public FragmentComparison(ComparisonType comparisonType)
        {
            ComparisonType = comparisonType;
        }

        public FragmentComparison(string first, string second, ComparisonType comparisonType, int overlappingLength)
        {
            First = first;
            Second = second;
            ComparisonType = comparisonType;
            OverlappingLength = overlappingLength;
        }

        public int CompareTo(FragmentComparison other)
        {
            return OverlappingLength - other.OverlappingLength;
        }

        public string GetMatchedFragments()
        {
            switch (ComparisonType)
            {
                case ComparisonType.NoMatch:
                    return First + Second;
                case ComparisonType.ExactMatch:
                    return First;
                case ComparisonType.FirstPreceedsSecond:
                    return FragmentResultHelper(First, Second, OverlappingLength);
                case ComparisonType.SecondPreceedsFirst:
                    return FragmentResultHelper(Second, First, OverlappingLength);
                case ComparisonType.FirstContainsSecond:
                    return First;
                case ComparisonType.SecondContainsFirst:
                    return Second;
            }


            return string.Empty;
        }

        protected string FragmentResultHelper(string begin, string end, int overlappingLength)
        {
            var builder = new StringBuilder(begin.Length + end.Length - overlappingLength);
            builder.Append(begin);
            builder.Append(end.Substring(overlappingLength));
            return builder.ToString();
        }
    }

    public enum ComparisonType
    {
        NoMatch = 0,
        ExactMatch = 1,
        FirstContainsSecond = 2,
        SecondContainsFirst = 3,
        FirstPreceedsSecond = 4,
        SecondPreceedsFirst = 5
    }
}