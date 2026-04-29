using System;
using Microsoft.UI.Xaml;
using VistaForge.SDK;

namespace VistaForge.Widgets.SystemMonitorWidget
{
    public class SystemMonitorWidget : IWidget
    {
        private SystemMonitorWidgetControl? _control;
        private readonly ISystemMetricsProvider _metricsProvider;
        private DispatcherTimer? _timer;

        public string Name => "System Monitor";

        public SystemMonitorWidget(ISystemMetricsProvider metricsProvider)
        {
            _metricsProvider = metricsProvider;
        }

        public SystemMonitorWidget() : this(new SystemMetricsProvider())
        {
        }

        public UIElement Render()
        {
            if (_control == null)
            {
                _control = new SystemMonitorWidgetControl();
            }
            return _control;
        }

        public void Initialize()
        {
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(2);
            _timer.Tick += Timer_Tick;
            _timer.Start();
            UpdateMetrics();
        }

        public void Update()
        {
            UpdateMetrics();
        }

        private void Timer_Tick(object? sender, object e)
        {
            UpdateMetrics();
        }

        private void UpdateMetrics()
        {
            if (_control != null)
            {
                float cpu = _metricsProvider.GetCpuUsage();
                float ram = _metricsProvider.GetRamUsage();
                float disk = _metricsProvider.GetDiskUsage();

                _control.UpdateMetrics(cpu, ram, disk);
            }
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
