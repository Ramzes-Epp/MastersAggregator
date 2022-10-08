using Avalonia.Controls;

namespace MasterAggregator.Desktop.Views
{
    public partial class UserEditorWindow : Window
    {
        public UserEditorWindow()
        {
            InitializeComponent();
        }

        public object DialogResult { get; internal set; }
    }
}
