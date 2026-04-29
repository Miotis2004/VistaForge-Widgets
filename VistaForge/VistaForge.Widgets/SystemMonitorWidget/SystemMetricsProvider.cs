using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace VistaForge.Widgets.SystemMonitorWidget
{
    public class SystemMetricsProvider : ISystemMetricsProvider
    {
        private PerformanceCounter? _cpuCounter;
        private PerformanceCounter? _ramCounter;
        private PerformanceCounter? _diskCounter;
        private bool _isWindows;

        public SystemMetricsProvider()
        {
            _isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

            if (_isWindows)
            {
                try
                {
                    _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                    _ramCounter = new PerformanceCounter("Memory", "% Committed Bytes In Use");
                    _diskCounter = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
                }
                catch (PlatformNotSupportedException)
                {
                    _isWindows = false;
                }
                catch (Exception)
                {
                    // Fallback if counters aren't available
                    _isWindows = false;
                }
            }
        }

        public float GetCpuUsage()
        {
            if (!_isWindows || _cpuCounter == null) return 0f;
            try
            {
                return _cpuCounter.NextValue();
            }
            catch
            {
                return 0f;
            }
        }

        public float GetRamUsage()
        {
            if (!_isWindows || _ramCounter == null) return 0f;
            try
            {
                return _ramCounter.NextValue();
            }
            catch
            {
                return 0f;
            }
        }

        public float GetDiskUsage()
        {
            if (!_isWindows || _diskCounter == null) return 0f;
            try
            {
                return _diskCounter.NextValue();
            }
            catch
            {
                return 0f;
            }
        }
    }
}
