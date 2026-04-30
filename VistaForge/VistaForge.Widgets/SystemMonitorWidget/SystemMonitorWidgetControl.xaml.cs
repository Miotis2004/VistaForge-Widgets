using System;
using Microsoft.UI.Xaml.Controls;

namespace VistaForge.Widgets.SystemMonitorWidget
{
    public sealed partial class SystemMonitorWidgetControl : UserControl
    {
        public SystemMonitorWidgetControl()
        {
            //this.InitializeComponent();
        }

        public void UpdateMetrics(float cpu, float ram, float disk)
        {
            //CpuProgress.Value = cpu;
            //CpuText.Text = $"{(int)cpu}%";

            //RamProgress.Value = ram;
            //RamText.Text = $"{(int)ram}%";

            //DiskProgress.Value = disk;
            //DiskText.Text = $"{(int)disk}%";
        }
    }
}
