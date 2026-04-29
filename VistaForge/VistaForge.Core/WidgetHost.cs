using System;
using System.Collections.ObjectModel;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using VistaForge.SDK;

namespace VistaForge.Core
{
    public class WidgetHost : Canvas
    {
        private ObservableCollection<WidgetContainer> _containers = new ObservableCollection<WidgetContainer>();

        public WidgetHost()
        {
            // Simple Canvas that can host widget containers
        }

        public void AddWidget(IWidget widget)
        {
            var container = new WidgetContainer(widget);
            _containers.Add(container);

            // Default position and size
            Canvas.SetLeft(container, 100);
            Canvas.SetTop(container, 100);

            // Set initial Z-Index
            Canvas.SetZIndex(container, _containers.Count);

            container.PointerPressed += (s, e) =>
            {
                // Bring to front on click
                BringToFront((WidgetContainer)s);
            };

            Children.Add(container);
            widget.Initialize();
        }

        public void RemoveWidget(IWidget widget)
        {
            WidgetContainer toRemove = null;
            foreach (var container in _containers)
            {
                if (container.Widget == widget)
                {
                    toRemove = container;
                    break;
                }
            }

            if (toRemove != null)
            {
                Children.Remove(toRemove);
                _containers.Remove(toRemove);
                widget.Dispose();
            }
        }

        public void BringToFront(WidgetContainer container)
        {
            int maxZ = 0;
            foreach (UIElement child in Children)
            {
                int z = Canvas.GetZIndex(child);
                if (z > maxZ) maxZ = z;
            }
            Canvas.SetZIndex(container, maxZ + 1);
        }
    }
}
