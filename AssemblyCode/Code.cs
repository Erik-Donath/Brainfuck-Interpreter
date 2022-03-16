using System;
using System.Collections.Generic;

namespace brainfuck.AssemblyCode {
    public class Code {

        //Assembly Code Variables
        #region Assembly Code
        public readonly string originalCode;

        public List<Tuple<SyntaxType, Object>> operations {
            private set; get;
        }

        public List<Tuple<int, int>> loops {
            private set; get;
        }
        #endregion

        //Code Execution Variables
        #region Code Execution
        public Byte[] stack {
            private set; get;
        }

        public int ptr {
            private set; get;
        }

        public Byte Current {
            private set {
                stack[ptr] = value;
            }
            get {
                return stack[ptr];
            }
        }

        public int Pointer {
            private set {
                if(value < 0)
                    ptr = stack.Length - (int)MathF.Abs(value);
                else if(value >= stack.Length) 
                    ptr = 0 + (value - stack.Length);
                else
                    ptr = value;
            }
            get {
                return ptr;
            }
        }
        #endregion

        //Gloable Variables
        #region Gloable
        public int exitCode {
            private set; get;
        }
        public bool compiled {
            private set; get;
        }
        public bool runnable {
            private set; get;
        }

        #endregion

        //Funktions

        public Code(string originalCode, bool run = false, int stackSize = 30000) {
            this.originalCode = originalCode;
            operations = new List<Tuple<SyntaxType, object>>();
            loops = new List<Tuple<int, int>>();
            stack = new Byte[stackSize];

            if (run)
                Run();
        }

        public void Compile() {
            (operations, loops, runnable) = Compiler.Compile(originalCode);
        }

        public int Run() {
            // Compile Code Automatikly
            if (!compiled)
                Compile();

            // Returns if this code can't execute
            if(!runnable) {
                exitCode = 100;
                return exitCode;
            }

            for(int i = 0; i < operations.Count; i++) {

                switch(operations[i]) {
                    case (SyntaxType a, OperationData.MathOperationData b) when a == SyntaxType.addition:
                        Current += Convert.ToByte(b.count);
                        break;
                    case (SyntaxType a, OperationData.MathOperationData b) when a == SyntaxType.subtraction:
                        Current -= Convert.ToByte(b.count);
                        break;
                    case (SyntaxType a, OperationData.PointerOperationData b) when a == SyntaxType.pointerUp:
                        Pointer += b.offset;
                        break;
                    case (SyntaxType a, OperationData.PointerOperationData b) when a == SyntaxType.pointerDown:
                        Pointer -= b.offset;
                        break;
                    case (SyntaxType a, null) when a == SyntaxType.input:
                        Current = Convert.ToByte(Console.ReadKey().KeyChar);
                        break;
                    case (SyntaxType a, null) when a == SyntaxType.output:
                        Console.Write(Convert.ToChar(Current));
                        break;
                    case (SyntaxType a, OperationData.LoopOperationData b) when a == SyntaxType.startLoop:
                        if (Current == 0)
                            i = loops[b.loopArrayIndex].Item2;
                        break;
                    case (SyntaxType a, OperationData.LoopOperationData b) when a == SyntaxType.endLoop:
                        if (Current != 0)
                            i = loops[b.loopArrayIndex].Item1;
                        break;
                    default:
                        break;
                }

            }

            exitCode = Convert.ToInt32(Current);
            return exitCode;
        }

    }
}
