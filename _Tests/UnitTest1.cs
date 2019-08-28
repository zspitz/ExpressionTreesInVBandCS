using System;
using Xunit;
using Shared;
using static Shared.LikePattern;
using static Shared.Globals;
using System.Collections.Generic;
using System.Linq;

namespace Tests {
    public class VBLikePatternParsing {
        [Theory]
        [MemberData(nameof(TestData))]
        public static void RunTest(string pattern, LikePattern expected) {
            var actual = ParseVBLike(pattern);
            Assert.Equal(expected.Count, actual.Count);
            expected.Zip(actual).ForEachT((expected1, actual1) => {
                if (expected1.IsT2 && actual1.IsT2) {
                    Assert.Equal(expected1.AsT2.IsPositive, actual1.AsT2.IsPositive);
                    Assert.Equal(expected1.AsT2.ToList(), actual1.AsT2.ToList());
                } else {
                    Assert.Equal(expected1, actual1);
                }
            });
        }

        public static TheoryData<string, LikePattern> TestData = IIFE(() => {
            var data = new List<(string pattern, LikePattern expected)>() {
                { "abcd", new LikePattern {
                    'a','b','c','d'
                } },
                { "ab?cd", new LikePattern {
                    'a', 'b', Wildcards.SingleCharacter, 'c', 'd'
                } },
                { "ab*cd", new LikePattern {
                    'a', 'b', Wildcards.MultipleCharacter, 'c', 'd'
                } },
                { "ab]cd", new LikePattern {
                    'a', 'b', ']', 'c', 'd'
                } },
                { "a-d", new LikePattern {
                    'a', '-', 'd'
                } },
                { "[a-d]", new LikePattern {
                    new PatternGroup {
                        {'a','d' }
                    }
                } },
                { "[!a-d]", new LikePattern {
                    new PatternGroup(false) {
                        {'a','d' }
                    }
                } },
                { "#", new LikePattern {
                    new PatternGroup {
                        {'0','9' }
                    }
                } },
            };

            return data.ToTheoryData();
        });

        [Fact]
        public void Test1() {
            var pattern = "a[L-P]#[!c-e]";
            var result = ParseVBLike(pattern);
            Assert.Collection(
                result,
                item => Assert.Equal('a', item),
                item => {
                    PatternGroup grp = item.AsT2;
                    Assert.True(grp.IsPositive);
                    Assert.Equal(
                        new List<(char,char)> { ('L', 'P') },
                        grp.ToList()
                    );
                },
                item => {
                    PatternGroup grp = item.AsT2;
                    Assert.True(grp.IsPositive);
                    Assert.Equal(
                        new List<(char, char)> { ('0', '9') },
                        grp.ToList()
                    );
                },
                item => {
                    PatternGroup grp = item.AsT2;
                    Assert.False(grp.IsPositive);
                    Assert.Equal(
                        new List<(char, char)> { ('c', 'e') },
                        grp.ToList()
                    );
                }
            );
        }

        [Fact]
        public void TestWithAsterisk() {
            var pattern = "a*b";
            var result = ParseVBLike(pattern);
            Assert.Collection(
                result,
                item => Assert.Equal('a', item),
                item => Assert.Equal(Wildcards.MultipleCharacter, item),
                item => Assert.Equal('b', item)
            );
        }


    }
}
