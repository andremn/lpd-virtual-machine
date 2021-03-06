﻿using static LPD.VirtualMachine.Engine.InstructionSet;

namespace LPD.VirtualMachine.Engine.Instructions
{
    /// <summary>
    /// The SUB instruction.
    /// </summary>
    [Instruction(SUB)]
    class SUBInstruction: IncrementalInstruction
    {
        /// <summary>
        /// Substracts the value on the top of the stack with the value just below it.
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
            stack.Store(second - first);
        }
    }
}
