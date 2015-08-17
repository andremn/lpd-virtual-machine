using static LPD.VirtualMachine.Engine.InstructionSet;

namespace LPD.VirtualMachine.Engine.Instructions
{
    /// <summary>
    /// Represents the PRN instruction.
    /// </summary>
    [Instruction(PRN)]
    public class PRNInstruction : IncrementalInstruction
    {
        /// <summary>
        /// Writes the value on the top of the stack to the output.
        /// </summary>
        /// <param name="context">The execution context.</param>
        /// <param name="parameters">Not used.</param>
        protected override void SpecificExecute(ExecutionContext context, int[] parameters)
        {
            Stack stack = context.Memory.StackRegion;
            int item;

            item = stack.Load();
            context.OutputProvider.Print(item);
            stack.Down();
        }
    }
}
