using static LPD.VirtualMachine.Engine.InstructionSet;

namespace LPD.VirtualMachine.Engine.Instructions
{
    [Instruction(CMA)]
    class CMAInstruction: IncrementalInstruction
    {
        /// <summary>
        /// Compares if the second value in the stack is greater than the first value.
        /// The result is loaded to the second value address.
        /// </summary>
        /// <param name="context">The execution context.</param>
        /// <param name="parameters">Not used.</param>
        protected override void SpecificExecute(ExecutionContext context, int[] parameters)
        {
            int first;
            int second;
            Stack stack = context.Memory.StackRegion;

            first = stack.Load();
            stack.Down();
            second = stack.Load();

            if (second > first)
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
