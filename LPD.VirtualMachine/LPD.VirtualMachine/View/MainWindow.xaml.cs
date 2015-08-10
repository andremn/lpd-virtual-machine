using LPD.VirtualMachine.Engine;
using LPD.VirtualMachine.Engine.HAL;
using LPD.VirtualMachine.Properties;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using static System.IO.Path;

namespace LPD.VirtualMachine.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private const string OpenDialogDefaultFilter = "Arquivo de assembly|*.asmd|Todos os arquivos|*.*";

        private string _selectedFilePath;
        private Window _executionWindow;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Starts and completes the animation for the StartButton control.
        /// </summary>
        private void DoStartButtonAnimation()
        {
            Storyboard sb = FindResource("StartButtonStoryboard") as Storyboard;

            sb.Completed += delegate
            {
                StartButton.RenderTransform = new ScaleTransform();
                StartButton.Margin = new Thickness(50, 13, 0, 0);
                StartButton.Width = 250d;
                sb.Stop();
            };
            sb.Begin();
        }

        /// <summary>
        /// Occurs when the button "Carregar" is clicked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The info of the event.</param>
        private async void OnLoadButtonClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = OpenDialogDefaultFilter
            };

            bool? result = openFileDialog.ShowDialog();

            if (!result.HasValue || !result.Value)
            {
                return; 
            }

            _executionWindow = new ExecutionWindow(GetFileNameWithoutExtension(_selectedFilePath));

            int virtualMachineSize = (int)Settings.Default[App.SystemMemorySettingKey];
            InstructionSet instructionsCollection = await InstructionSet.CreateFromFileAsync(openFileDialog.FileName);
            Memory memory = Memory.CreateMemory(virtualMachineSize, instructionsCollection);
            ExecutionContext context = new ExecutionContext()
            {
                ProgramCounter = new ProgramCounter(CPU.InitialProgramCounter),
                InputProvider = (IInputProvider)_executionWindow,
                OutputProvider = (IOutputProvider)_executionWindow,
                Memory = memory
            };

            CPU.Instance.Initialize(context);
            DoStartButtonAnimation();
            _selectedFilePath = openFileDialog.FileName;
        }

        /// <summary>
        /// Occurs when the button "Iniciar" is clicked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The info of the event.</param>
        private void OnStartButtonClick(object sender, RoutedEventArgs e)
        {
            _executionWindow.ShowDialog();
        }

        /// <summary>
        /// Occurs when the button "Configurações" is clicked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The info of the event.</param>
        private void OnSettingsButtonClick(object sender, RoutedEventArgs e)
        {
            new SettingsWindow().ShowDialog();
        }
    }
}
