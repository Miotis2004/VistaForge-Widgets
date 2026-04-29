using System;

namespace VistaForge.Widgets.SystemMonitorWidget
{
    public interface ISystemMetricsProvider
    {
        float GetCpuUsage();
        float GetRamUsage();
        float GetDiskUsage();
    }
}
