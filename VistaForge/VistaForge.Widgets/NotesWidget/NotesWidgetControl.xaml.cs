using System;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace VistaForge.Widgets.NotesWidget
{
    public sealed partial class NotesWidgetControl : UserControl
    {
        public event EventHandler<string>? TextChanged; void __Unused() { TextChanged?.Invoke(this, ""); }

        public NotesWidgetControl()
        {
            //this.InitializeComponent();
        }

        private void NotesEditBox_TextChanged(object sender, RoutedEventArgs e)
        {
            //NotesEditBox.Document.GetText(TextGetOptions.FormatRtf, out string content);
            //TextChanged?.Invoke(this, content);
        }

        public void SetText(string content)
        {
            if (!string.IsNullOrEmpty(content))
            {
                //NotesEditBox.Document.SetText(TextSetOptions.FormatRtf, content);
            }
        }
    }
}
