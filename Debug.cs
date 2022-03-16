using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace brainfuck {
    public static class Debug {

        public static bool isDebugMode = false;

        public static void PrintAssemlyCode(List<Tuple<AssemblyCode.SyntaxType, Object>> operartions) {

            if (!isDebugMode)
                return;

            if (operartions.Count != 0) {
                for (int i = 0; i < operartions.Count; i++) {

                    Console.Write($"{i}: {{ Type: \"{Enum.GetName(operartions[i].Item1)}\"");
                    if (operartions[i].Item2 != null)
                        Console.WriteLine($", {operartions[i].Item2.ToString()} }}");
                    else
                        Console.WriteLine(", NULL }");

                }
            }
            else
                Console.WriteLine("-");

        }

        public static void PrintAssemlyLoops(List<Tuple<int, int>> loops) {

            if (!isDebugMode)
                return;

            if (loops.Count != 0) {
                for (int i = 0; i < loops.Count; i++) {
                    Console.WriteLine($"{i}: {{ start: {loops[i].Item1}, end: {loops[i].Item2} }}");
                }
            }
            else
                Console.WriteLine("-");
        }

        public static void WriteLine(string s) {
            if (!isDebugMode)
                return;

            Console.WriteLine(s);
        }

        public static void Write(string s) {
            if (!isDebugMode)
                return;

            Console.Write(s);
        }

    }
}
