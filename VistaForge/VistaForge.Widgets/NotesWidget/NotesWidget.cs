using System;
using Microsoft.UI.Xaml;
using VistaForge.SDK;

namespace VistaForge.Widgets.NotesWidget
{
    public class NotesWidget : IWidget
    {
        private NotesWidgetControl? _control;
        private string _textContent = "";

        public string Name => "Sticky Notes";

        public UIElement Render()
        {
            if (_control == null)
            {
                _control = new NotesWidgetControl();
                _control.SetText(_textContent);
                _control.TextChanged += Control_TextChanged;
            }
            return _control;
        }

        private void Control_TextChanged(object? sender, string e)
        {
            _textContent = e;
        }

        public void Initialize()
        {
            // Initialized
        }

        public void Update()
        {
            // Update requested
        }

        public void Dispose()
        {
            if (_control != null)
            {
                _control.TextChanged -= Control_TextChanged;
                _control = null;
            }
        }
    }
}
