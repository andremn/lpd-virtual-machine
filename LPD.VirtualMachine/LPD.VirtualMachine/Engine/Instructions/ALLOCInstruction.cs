using static LPD.VirtualMachine.Engine.InstructionSet;

namespace LPD.VirtualMachine.Engine.Instructions
{
    [Instruction(ALLOC)]
    class ALLOCInstruction : IncrementalInstruction
    {
        protected override void SpecificExecute(ExecutionContext context, int[] parameters)
        {
            int baseIndex = parameters[0];
            int slots = parameters[1];
            Stack stack = context.Memory.StackRegion;

            for (int i = 0; i < slots; i++)
            {
                stack.Up();
                stack.Store(stack.LoadFrom(baseIndex + i));
            }
        }
    }
}
