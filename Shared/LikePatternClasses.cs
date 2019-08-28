using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Shared {
    public enum Wildcards {
        SingleCharacter,
        MultipleCharacter
    }

    // Single characters within a group are represented by a group with the same start and end character
    public class PatternGroup : List<(char start, char end)> {
        public PatternGroup(bool isPositive = true) => IsPositive = isPositive;
        public bool IsPositive { get; set; }
    }

    public class LikePattern : List<OneOfBase<char, Wildcards, PatternGroup>> {
        public static LikePattern ParseVBLike(string pattern) {
            var ret = new LikePattern();

            int pos = -1;
            int lastPos = pattern.Length - 1;

            PatternGroup currentGroup = null;

            char ch;
            while (pos < lastPos) {
                advanceChar();

                if (currentGroup == null) {

                    if (ch == '?') {
                        ret.Add(Wildcards.SingleCharacter);
                    } else if (ch == '*') {
                        ret.Add(Wildcards.MultipleCharacter);
                    } else if (ch == '#') {
                        ret.Add(new PatternGroup() {
                            {'0','9' }
                        });
                    } else if (ch == '[') {
                        currentGroup = new PatternGroup();
                        if (nextChar() == '!') {
                            advanceChar();
                            currentGroup.IsPositive = false;
                        }
                    } else {
                        ret.Add(ch);
                    }

                } else {

                    var start = ch;
                    if (ch == ']') {
                        ret.Add(currentGroup);
                        currentGroup = null;
                    } else if (nextChar() == '-' && nextChar(2) != ']') {
                        advanceChar();
                        advanceChar();
                        currentGroup.Add(start, ch);
                    } else {
                        currentGroup.Add(ch, ch);
                    }

                }
            }

            if (currentGroup != null) {
                throw new ArgumentException("Missing group end.");
            }

            return ret;

            void advanceChar(bool ignoreEnd = false) {
                pos += 1;
                if (pos <= lastPos) {
                    ch = pattern[pos];
                } else if (ignoreEnd) {
                    ch = '\x0';
                } else {
                    throw new ArgumentException("Unexpected end of text");
                }
            }

            char nextChar(int offset = 1) => pos + offset > lastPos ? '\x0' : pattern[pos + offset];
        }

        private static string MapSqlSpecialCharacters(char ch) =>
                ch.In('%', '_', '[') ?
                    "[" + ch + "]" :
                    ch.ToString();

        public static string GetSQLLike(LikePattern pattern) => pattern.Joined("", x => x.Match(
            ch => MapSqlSpecialCharacters(ch),
            wildcard => (wildcard == Wildcards.SingleCharacter ? '_' : '%').ToString(),
            patternGroup => {
                string ret = "";
                if (patternGroup.IsPositive) { ret += '^'; }
                return ret +
                    patternGroup.Joined("", range =>
                        range.start == range.end ? 
                            $"{range.start}" :
                            $"{range.start}-{range.end}");
            }
        ));
    }
}
