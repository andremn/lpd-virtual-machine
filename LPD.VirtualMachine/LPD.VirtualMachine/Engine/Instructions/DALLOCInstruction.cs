using static LPD.VirtualMachine.Engine.InstructionSet;

namespace LPD.VirtualMachine.Engine.Instructions
{
    [Instruction(DALLOC)]
    class DALLOCInstruction : IncrementalInstruction
    {
        protected override void SpecificExecute(ExecutionContext context, int[] parameters)
        {
            int k;
            int address = parameters[0];
            Stack stack = context.Memory.StackRegion;

            for (k = address - 1; k > 0; k--)
            {
                stack.Store(address + k); stack.Down(); 
            }
        }
    }
}
