using System;
using System.Collections.Generic;

namespace brainfuck.AssemblyCode.OperationData {
    public class LoopOperationData {
        public readonly int loopArrayIndex;
        
        public LoopOperationData(int loopArrayIndex) {
            this.loopArrayIndex= loopArrayIndex;
        }

        public override string ToString() {
            return String.Format("LoopIndex: {0}", loopArrayIndex);
        }
    }
}
