using static LPD.VirtualMachine.Engine.InstructionSet;

namespace LPD.VirtualMachine.Engine.Instructions
{
    /// <summary>
    /// The ADD instruction.
    /// </summary>
    [Instruction(ADD)]
    public class ADDInstruction : IncrementalInstruction
    {
        /// <summary>
        /// Executes the ADD instruction.
        /// </summary>
        /// <param name="context">The execution context.</param>
        /// <param name="parameters">Not used.</param>
        protected override void SpecificExecute(ExecutionContext context, string[] parameters)
        {
            int first;
            int second;
            Stack stack = context.Memory.StackRegion;

            first = stack.Load();
            stack.Down();
            second = stack.Load();
            stack.Store(first + second);
        }
    }
}
