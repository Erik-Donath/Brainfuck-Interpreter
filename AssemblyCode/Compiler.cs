using System;
using System.Linq;
using System.Collections.Generic;

namespace brainfuck.AssemblyCode {

    public static class Compiler {

        public static (List<Tuple<SyntaxType, Object>>, List<Tuple<int, int>>, bool) Compile(string original) {

            List<Tuple<SyntaxType, Object>> operations = new List<Tuple<SyntaxType, object>>();
            List<Tuple<int, int>> loops = new List<Tuple<int, int>>();
            List<int> openLoops = new List<int>();

            Tuple<SyntaxType, Object> operation;

            bool runnable = true;
            int loopIndex = 0;
            int higestLoopIndex = 0;
            int codeIndex = 0;
            int start = 0;
            int count = 0;
            char c = '\0';

            for (int i = 0; i < original.Length; i++) {
                
                if (!usefull.isCharInCharList(original[i], new Char[] { '+', '-', '>', '<', '.', ',', '[', ']' }))
                    continue;

                c = original[i];
                if (usefull.isCharInCharList(original[i], new Char[] { '+', '-', '>', '<' })) {
                    start = i;

                    while (i < original.Length && original[i] == c)
                        i++;

                    i--;
                    count = i - start + 1;
                }

                switch(c) {
                    case '+':
                        operation = new Tuple<SyntaxType, object>(SyntaxType.addition, new OperationData.MathOperationData(count));
                        break;
                    case '-':
                        operation = new Tuple<SyntaxType, object>(SyntaxType.subtraction, new OperationData.MathOperationData(count));
                        break;
                    case '>':
                        operation = new Tuple<SyntaxType, object>(SyntaxType.pointerUp, new OperationData.PointerOperationData(count));
                        break;
                    case '<':
                        operation = new Tuple<SyntaxType, object>(SyntaxType.pointerDown, new OperationData.PointerOperationData(count));
                        break;
                    case ',':
                        operation = new Tuple<SyntaxType, object>(SyntaxType.input, null);
                        break;
                    case '.':
                        operation = new Tuple<SyntaxType, object>(SyntaxType.output, null);
                        break;
                    case '[':
                        if (loopIndex < higestLoopIndex)
                            loopIndex = higestLoopIndex;

                        operation = new Tuple<SyntaxType, object>(SyntaxType.startLoop, new OperationData.LoopOperationData(loopIndex));
                        openLoops.Add(loopIndex);
                        loops.Add(new Tuple<int, int>(codeIndex, 404));

                        loopIndex++;
                        if (higestLoopIndex < loopIndex)
                            higestLoopIndex = loopIndex;
                        break;
                    case ']':
                        runnable = runnable && openLoops.Count != 0;

                        if (!runnable)
                            continue;

                        loopIndex--;
                        int last = openLoops.Last();

                        operation = new Tuple<SyntaxType, object>(SyntaxType.endLoop, new OperationData.LoopOperationData(last));
                        loops[last] = new Tuple<int, int>(loops[last].Item1, codeIndex);
                        openLoops.Remove(last);
                        break;
                    default:
                        operation = null;
                        break;
                }
                operations.Add(operation);

                codeIndex++;
            }

            runnable = runnable && openLoops.Count == 0;

            return (operations, loops, runnable);
        }

    }
}
