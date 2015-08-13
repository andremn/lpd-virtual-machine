using static LPD.VirtualMachine.Engine.InstructionSet;

namespace LPD.VirtualMachine.Engine.Instructions
{
    [Instruction(ALLOC)]
    class ALLOCInstruction : IncrementalInstruction
    {
        protected override void SpecificExecute(ExecutionContext context, int[] parameters)
        {
            int k;
            int address = parameters[0];            
            Stack stack = context.Memory.StackRegion;

            for (k = 0;k < address - 1; k++)
            {
                stack.Up(); stack.Store(address+k);
            }
        }
    }
}
