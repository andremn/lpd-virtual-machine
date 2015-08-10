namespace LPD.VirtualMachine.Engine
{
    /// <summary>
    /// Contract for input providers.
    /// </summary>
    public interface IInputProvider
    {
        /// <summary>
        /// Reads the next input value.
        /// </summary>
        /// <returns></returns>
        int ReadInputValue();
    }
}
