using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;


namespace TaskKiller.Views
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Page
    {
        private static readonly string _documentationUrl = "https://github.com/joeiannone/TaskKiller";
        
        /// <summary>
        /// 
        /// </summary>
        public About()
        {
            InitializeComponent();
            TextBlock_AboutVersion.Text = $"Task Killer, Product Version {Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion}";
            DocumentationHyperlink_TextBlock.Text = _documentationUrl;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DocumentationHyperlink_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = _documentationUrl,
                UseShellExecute = true
            });
        }
    }
}
