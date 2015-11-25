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
        /// Gets the file's path passed from the desktop, if any.
        /// </summary>
        public static string FileFromArgument { get; private set; }

        /// <summary>
        /// Gets the name of the program passed from the compiler, if any.
        /// </summary>
        public static string ProgramNameFromArgument { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
#if DEBUG
            System.Diagnostics.Debugger.Launch();
#endif
        }

        /// <summary>
        /// Occurs when the application is started.
        /// </summary>
        /// <param name="e">The program arguments.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            FileFromArgument = e.Args.Length > 0 ? e.Args[0] : null;
            ProgramNameFromArgument = e.Args.Length > 1 ? e.Args[1] : null;
            base.OnStartup(e);
        }
    }
}
