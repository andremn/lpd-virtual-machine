using static LPD.VirtualMachine.Engine.InstructionSet;

namespace LPD.VirtualMachine.Engine.Instructions
{
    /// <summary>
    /// The AND instruction.
    /// </summary>
    [Instruction(AND)]
    class ANDInstruction: IncrementalInstruction
    {
        /// <summary>
        /// Realizes an logical AND operation on the first two values in the stack.
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

            if ((first != 1 && first != 0) || (second != 1 && second != 0))
            {
                throw new InvalidInstructionException("Operação OR precisa ser com 0 ou 1");
            }

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
