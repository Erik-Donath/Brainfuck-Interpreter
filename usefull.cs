using System;
using System.Collections.Generic;
using System.IO;

namespace brainfuck {

    public static class usefull {

        public static bool isCharInCharList(char ch, char[] ls) {
            foreach(char c in ls) {
                if (ch == c)
                    return true;
            }
            return false;
        }

        public static bool ContainsStringCharFromCharList(string s, char[] ls) {
            foreach (char c in ls) {
                if (s.Contains(c))
                    return true;
            }
            return false;
        }

        public static string ReadFile(string path) {
            try {
                return File.ReadAllText(path);
            }
            catch {
                return "\0";
            }
        }
    }
}
