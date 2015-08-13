using static LPD.VirtualMachine.Engine.InstructionSet;

namespace LPD.VirtualMachine.Engine.Instructions
{
    [Instruction(OR)]
    class ORInstruction: IncrementalInstruction
    {
        protected override void SpecificExecute(ExecutionContext context, string[] parameters)
        {
            int first;
            int second;
            Stack stack = context.Memory.StackRegion;

            first = stack.Load();
            stack.Down();
            second = stack.Load();
            if (first == 1 && second == 1)
            {
                second = 1;
            }
            else
            {
                second = 0;
            }
            stack.Store(second);
        }
    }
}
