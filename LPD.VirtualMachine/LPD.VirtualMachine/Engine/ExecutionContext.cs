using LPD.VirtualMachine.Engine.HAL;

namespace LPD.VirtualMachine.Engine
{
    /// <summary>
    /// Represents the context of a program execution.
    /// </summary>
    public class ExecutionContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExecutionContext"/> class.
        /// </summary>
        public ExecutionContext()
        {
        }

        /// <summary>
        /// Gets or sets the program counter (PC).
        /// </summary>
        public ProgramCounter ProgramCounter { get; set; }

        /// <summary>
        /// Gets or sets the memory used by the current execution context.
        /// </summary>
        public Memory Memory { get; set; }

        /// <summary>
        /// Gets or sets the input provider used by the current execution context.
        /// </summary>
        public IInputProvider InputProvider { get; set; }

        /// <summary>
        /// Gets or sets the output provider used by the current execution context.
        /// </summary>
        public IOutputProvider OutputProvider { get; set; }
    }
}
