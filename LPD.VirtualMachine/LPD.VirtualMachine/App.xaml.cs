using LPD.VirtualMachine.Properties;
using System.Windows;

namespace LPD.VirtualMachine
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// The key for the memory seeting.
        /// </summary>
        public const string SystemMemorySettingKey = "SystemMemory";

        /// <summary>
        /// The default virtual machine memory.
        /// </summary>
        private const uint DefaultMemory = 16;

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            int savedMemory = (int)Settings.Default[SystemMemorySettingKey];

            if (savedMemory == 0)
            {
                Settings.Default[SystemMemorySettingKey] = (int)DefaultMemory;
                Settings.Default.Save();
            }
        }
    }
}
