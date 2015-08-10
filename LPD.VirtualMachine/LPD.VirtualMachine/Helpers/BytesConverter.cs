namespace LPD.VirtualMachine.Helpers
{
    /// <summary>
    /// Converts bytes to kilobytes.
    /// </summary>
    public static class BytesConverter
    {
        /// <summary>
        /// Gets the value of a kilobyte.
        /// </summary>
        public const int KiloByte = 1024;

        /// <summary>
        /// Converts kilobytes to bytes.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The value converted, in kilobytes.</returns>
        public static int ConvertKiloBytesToBytes(int value)
        {
            return value * KiloByte;
        }

        /// <summary>
        /// Converts bytes to kilobytes.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The value converted, in bytes.</returns>
        public static int ConvertBytesToKiloBytes(int value)
        {
            return value / KiloByte;
        }
    }
}
