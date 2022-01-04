using System;
using System.Collections.Generic;

namespace brainfuck.AssemblyCode.OperationData {
    public class PointerOperationData {
        public readonly int offset;
        
        public PointerOperationData(int offset) {
            this.offset= offset;
        }

        public override string ToString() {
            return String.Format("Offset: {0}", offset);
        }
    }
}
