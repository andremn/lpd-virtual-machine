namespace LPD.VirtualMachine.Engine
{
    /// <summary>
    /// Represents the program counter.
    /// </summary>
    public class ProgramCounter
    {
        /// <summary>
        /// The internal program counter.
        /// </summary>
        private int _pc;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramCounter"/> class with an initial value.
        /// </summary>
        /// <param name="initialValue">The program counter initial value.</param>
        public ProgramCounter(int initialValue)
        {
            _pc = initialValue;
        }

        /// <summary>
        /// The current position of the program counter.
        /// </summary>
        public int Current
        {
            get { return _pc; }
        }

        /// <summary>
        /// Increments the program counter by one.
        /// </summary>
        public void Increment()
        {
            _pc++;
        }

        /// <summary>
        /// Sets the program counter to a specific position.
        /// </summary>
        /// <param name="value">The position to set the program counter to.</param>
        public void Jump(int value)
        {
            _pc = value;
        }
    }
}
