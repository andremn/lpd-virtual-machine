using System;
using static LPD.VirtualMachine.Engine.InstructionSet;

namespace LPD.VirtualMachine.Engine.Instructions
{
    [Instruction(JMP)]
    class JMPInstruction : JumpableInstruction
    {
        public override int SpecificExecute(ExecutionContext context, int[] parameters)
        {
            int address = parameters[0];

            return address;
        }
    }
}
