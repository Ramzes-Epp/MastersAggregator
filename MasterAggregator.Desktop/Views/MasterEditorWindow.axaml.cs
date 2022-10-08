using Avalonia.Controls;

namespace MasterAggregator.Desktop.Views
{
    public partial class MasterEditorWindow : Window
    {
        public MasterEditorWindow()
        {
            InitializeComponent();
        } 
        public object DialogResult { get; internal set; }
    }
}
