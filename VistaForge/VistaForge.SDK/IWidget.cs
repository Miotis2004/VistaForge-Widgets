using System;
using Microsoft.UI.Xaml;

namespace VistaForge.SDK
{
    public interface IWidget : IDisposable
    {
        string Name { get; }

        /// <summary>
        /// Renders the UI of the widget.
        /// </summary>
        UIElement Render();

        /// <summary>
        /// Initializes the widget.
        /// </summary>
        void Initialize();

        /// <summary>
        /// Updates the widget state.
        /// </summary>
        void Update();
    }
}
