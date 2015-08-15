using LPD.VirtualMachine.Properties;
using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Controls;

namespace LPD.VirtualMachine.View
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : MetroWindow
    {
        /// <summary>
        /// The minimum virtual machine memory.
        /// </summary>
        private const double MinimumMemory = 128;
        
        /// <summary>
        /// The maximum virtual machine memory.
        /// </summary>
        private const double MaximumMemory = 512;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsWindow"/> class.
        /// </summary>
        public SettingsWindow()
        {
            InitializeComponent();
            MinimumSliderValueTextBlock.Text = MinimumMemory.ToString();
            MaximumSliderValueTextBlock.Text = MaximumMemory.ToString();
            MemorySlider.Minimum = MinimumMemory;
            MemorySlider.Maximum = MaximumMemory;
            MemorySlider.Value = (int)Settings.Default[App.SystemMemorySettingKey];
        }

        /// <summary>
        /// Saves the memory setting.
        /// </summary>
        private void SaveMemorySetting()
        {
            Settings.Default[App.SystemMemorySettingKey] = (int)MemorySlider.Value;
            Settings.Default.Save();
        }

        /// <summary>
        /// Occurs when the <see cref="MemorySlider"/> control change its value.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The info of the event.</param>
        private void OnMemorySliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MemoryTextBox.Text = e.NewValue.ToString("##");
        }

        /// <summary>
        /// Occurs when the <see cref="MemoryTextBox"/> control change its text.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The info of the event.</param>
        private void OnMemoryTextBoxTextChanged(object sender, TextChangedEventArgs e)
        {
            double value;

            if (!double.TryParse(MemoryTextBox.Text, out value))
            {
                return;
            }

            if (value < MinimumMemory)
            {
                value = MinimumMemory;
            }
            else if (value > MaximumMemory)
            {
                value = MaximumMemory;
            }

            MemorySlider.Value = value;
        }

        /// <summary>
        /// Occurs when the button "Aplicar" is clicked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The info of the event.</param>
        private void OnApplyButtonClick(object sender, RoutedEventArgs e)
        {
            SaveMemorySetting();
            Close();
        }
    }
}
