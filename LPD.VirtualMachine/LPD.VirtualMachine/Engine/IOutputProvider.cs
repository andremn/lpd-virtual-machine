namespace LPD.VirtualMachine.Engine
{
    /// <summary>
    /// Contract for output providers.
    /// </summary>
    public interface IOutputProvider
    {
        /// <summary>
        /// Prints a value.
        /// </summary>
        /// <returns></returns>
        void Print(int value);
    }
}
