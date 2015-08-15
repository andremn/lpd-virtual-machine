using static LPD.VirtualMachine.Engine.InstructionSet;

namespace LPD.VirtualMachine.Engine.Instructions
{
    [Instruction(RETURN)]
    class RETURNInstruction : JumpableInstruction
    {
        protected override int SpecificExecute(ExecutionContext context, int[] parameters)
        {
            Stack stack = context.Memory.StackRegion;
            int first = stack.Load();
            stack.Down();
            return first;
        }
    }
}
