using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReconstructionFromFragments;

namespace ReconstructionFromFragmentsTests
{
    [TestClass]
    public class Tests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ProcessInput_NullInput_ThrowsException()
        {
            Program.ReconstructFromFragments(null);
        }

        [TestMethod]
        public void ProcessInput_EmptyInput_ThrowsException()
        {
            var response = Program.ReconstructFromFragments(new List<string>());
            Assert.AreEqual(string.Empty, response);
        }

        [TestMethod]
        public void ProcessInput_OneInputEmpty_ReturnsResponse()
        {
            var response = Program.ReconstructFromFragments(new List<string> { "ab", "" });

            Assert.AreEqual("ab", response);
        }

        [TestMethod]
        public void ProcessInput_NoOverlap_ReturnsResponse()
        {
            var response = Program.ReconstructFromFragments(new List<string>{"ab", "cd"});

            Assert.AreEqual("abcd", response);
        }

        [TestMethod]
        public void ProcessInput_OneOverlap_ReturnsResponse()
        {
            var response = Program.ReconstructFromFragments(new List<string> { "abcd", "defg" });

            Assert.AreEqual("abcdefg", response);
        }

        [TestMethod]
        public void ProcessInput_MultiOverlaps_ReturnsResponse()
        {
            var response = Program.ReconstructFromFragments(new List<string> { "all is well", "ell that en", "hat end", "t ends well" });

            Assert.AreEqual("all is well that ends well", response);
        }

        [TestMethod]
        public void ProcessInput_PreserveLeadingAndTrailingSpaces_ReturnsResponse()
        {
            var response = Program.ReconstructFromFragments(new List<string> { " all is very ", "very well " });

            Assert.AreEqual(" all is very well ", response);
        }

        [TestMethod]
        public void ProcessInput_EntireFragmentInsideOne_ReturnsResponse()
        {
            var response = Program.ReconstructFromFragments(new List<string> { "all is well", "well" });

            Assert.AreEqual("all is well", response);
        }
    }
}
