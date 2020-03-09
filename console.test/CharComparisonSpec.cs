using System;
using Xunit;

namespace console.test
{
    public class CharComparisonSpec
    {
        public class InvariantCultureIgnoreCaseComparison
        {
            [Fact]
            public void IdenticalLowercaseCharactersAreEqual()
            {
                Assert.True(CharComparison.InvariantCultureIgnoreCase.Equals('a', 'a'));
            }

            [Fact]
            public void IdenticalUppercaseCharactersAreEqual()
            {
                Assert.True(CharComparison.InvariantCultureIgnoreCase.Equals('A', 'A'));
            }

            [Fact]
            public void SameCharacterDifferentCaseAreEqual()
            {
                Assert.True(CharComparison.InvariantCultureIgnoreCase.Equals('A', 'a'));
                Assert.True(CharComparison.InvariantCultureIgnoreCase.Equals('a', 'A'));
            }

            [Fact]
            public void DifferentCharacterSameCaseAreNotEqual()
            {
                Assert.False(CharComparison.InvariantCultureIgnoreCase.Equals('a', 'b'));
                Assert.False(CharComparison.InvariantCultureIgnoreCase.Equals('b', 'a'));
            }

            [Fact]
            public void DifferentCharacterDifferentCaseAreNotEqual()
            {
                Assert.False(CharComparison.InvariantCultureIgnoreCase.Equals('A', 'b'));
                Assert.False(CharComparison.InvariantCultureIgnoreCase.Equals('a', 'B'));
            }
        }
    }
}
