using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Windows.Foundation;
using VistaForge.SDK;

namespace VistaForge.Core
{
    public class WidgetContainer : ContentControl
    {
        private readonly IWidget _widget;
        private TranslateTransform _transform;

        private const double MinWidthBounds = 100;
        private const double MinHeightBounds = 100;

        public WidgetContainer(IWidget widget)
        {
            _widget = widget;
            Content = widget.Render();

            _transform = new TranslateTransform();
            RenderTransform = _transform;

            ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateY | ManipulationModes.Scale;
            ManipulationDelta += OnManipulationDelta;
        }

        private void OnManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            // Handle resizing if it's a pinch/scale (or we could add resize handles later)
            // For now, handle simple drag/translate
            if (e.Delta.Translation.X != 0 || e.Delta.Translation.Y != 0)
            {
                double newX = _transform.X + e.Delta.Translation.X;
                double newY = _transform.Y + e.Delta.Translation.Y;

                // Enforce simple constraints (greater than 0) - more complex logic handled by host/parent usually
                _transform.X = Math.Max(0, newX);
                _transform.Y = Math.Max(0, newY);
            }

            // Simple scale logic for resizing constraints test
            if (e.Delta.Scale != 1.0)
            {
                double newWidth = ActualWidth * e.Delta.Scale;
                double newHeight = ActualHeight * e.Delta.Scale;

                if (newWidth >= MinWidthBounds && newHeight >= MinHeightBounds)
                {
                    Width = newWidth;
                    Height = newHeight;
                }
            }

            e.Handled = true;
        }

        public IWidget Widget => _widget;
    }
}
