using static LPD.VirtualMachine.Engine.InstructionSet;

namespace LPD.VirtualMachine.Engine.Instructions
{
    /// <summary>
    /// The LDV instruction.
    /// </summary>
    [Instruction(LDV)]
    public class LDVInstruction : IncrementalInstruction
    {
        /// <summary>
        /// Stores a value to the stack.
        /// </summary>
        /// <param name="context">The execution context.</param>
        /// <param name="parameters">The index of the value to be stored.</param>
        protected override void SpecificExecute(ExecutionContext context, int[] parameters)
        {            
            int index = parameters[0];
            Stack stack = context.Memory.StackRegion;
            
            stack.Up();
            stack.Store(stack.LoadFrom(index));
        }
    }
}
