using static LPD.VirtualMachine.Engine.InstructionSet;

namespace LPD.VirtualMachine.Engine.Instructions
{
    [Instruction(NEG)]
    class NEGInstruction : IncrementalInstruction
    {
        protected override void SpecificExecute(ExecutionContext context, int[] parameters)
        {
            int first;
            Stack stack = context.Memory.StackRegion;
            first = stack.Load();
            stack.Store(1-first);
        }
    }
}
