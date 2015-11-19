using System.ComponentModel;

namespace LPD.VirtualMachine.ViewModel
{
    /// <summary>
    /// Represents the view model for a instruction.
    /// </summary>
    public class InstructionViewModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InstructionViewModel"/> class.
        /// </summary>
        public InstructionViewModel()
        {
        }

        /// <summary>
        /// Gets or sets instruction line number.
        /// </summary>
        [DisplayName("Linha")]
        public uint LineNumber { get; set; }

        /// <summary>
        /// Gets or sets instruction content.
        /// </summary>
        [DisplayName("Instrução")]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets instruction comment.
        /// </summary>
        [DisplayName("Argumentos")]
        public string Arguments { get; set; }

        /// <summary>
        /// Gets or sets if the instruction has a breakpoint.
        /// </summary>
        [DisplayName("Breakpoint")]
        public bool HasBreakpoint { get; set; }
    }
}
