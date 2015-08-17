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

        protected override void OnStartup(StartupEventArgs e)
        {
            //TODO - corrigir apropriadamente ?? 
            try
            {
                _desktopFilePath = e.Args[0];
            }
            catch (System.Exception)
            {
                return;
            }
            base.OnStartup(e);
        }
    }
}
