using static LPD.VirtualMachine.Engine.InstructionSet;

namespace LPD.VirtualMachine.Engine.Instructions
{
    [Instruction(NEG)]
    class NEGInstruction : IncrementalInstruction
    {
        protected override void SpecificExecute(ExecutionContext context, string[] parameters)
        {
            int first;
            Stack stack = context.Memory.StackRegion;
            first = stack.Load();
            stack.Store(1-first);
        }
    }
}
