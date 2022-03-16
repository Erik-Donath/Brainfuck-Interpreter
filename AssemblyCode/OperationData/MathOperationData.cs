using System;
using System.Collections.Generic;

namespace brainfuck.AssemblyCode.OperationData {
    public class MathOperationData {
        public readonly int count;
        
        public MathOperationData(int count) {
            this.count = count;
        }

        public override string ToString() {
            return String.Format("Count: {0}", count);
        }
    }
}
