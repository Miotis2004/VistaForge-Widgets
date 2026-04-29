using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;
using VistaForge.SDK;

namespace VistaForge.Widgets.ClockWidget
{
    public class ClockWidget : IWidget
    {
        private ClockWidgetControl? _control;
        private DispatcherTimer? _timer;

        public string Name => "Clock";

        public UIElement Render()
        {
            if (_control == null)
            {
                _control = new ClockWidgetControl();
            }
            return _control;
        }

        public void Initialize()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += Timer_Tick;
            _timer.Start();
            UpdateControlTime(); // Initial update
        }

        public void Update()
        {
            // Explicit update requested
            UpdateControlTime();
        }

        private void Timer_Tick(object? sender, object e)
        {
            UpdateControlTime();
        }

        private void UpdateControlTime()
        {
            _control?.UpdateTime(DateTime.Now);
        }

        public void Dispose()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Tick -= Timer_Tick;
                _timer = null;
            }
        }
    }
}
