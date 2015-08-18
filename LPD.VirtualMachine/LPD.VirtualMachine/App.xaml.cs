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

        private static string _desktopFilePath = null;

        /// <summary>
        /// Gets the file's path passed from the desktop, if any.
        /// </summary>
        public static string DesktopFilePath
        {
            get { return _desktopFilePath; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            
        }

        /// <summary>
        /// Occurs when the application is started.
        /// </summary>
        /// <param name="e">The program arguments.</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            _desktopFilePath = e.Args.Length > 0 ? e.Args[0] : null;
            base.OnStartup(e);
        }
    }
}
