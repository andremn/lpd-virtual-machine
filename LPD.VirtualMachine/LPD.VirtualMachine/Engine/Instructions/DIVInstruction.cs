namespace LPD.VirtualMachine.Engine.Instructions
{
    [Instruction(DIV)]
    class DIVInstruction : IncrementalInstruction
    {
        protected override void SpecificExecute(ExecutionContext context, string[] parameters)
        {
            int first;
            int second;
            Stack stack = context.Memory.StackRegion;

            first = stack.Load();
            stack.Down();
            second = stack.Load();
            stack.Store(first / second);
        }
    }
}
