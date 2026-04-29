using System;
using Moq;
using Xunit;
using VistaForge.Widgets.SystemMonitorWidget;

namespace VistaForge.Widgets.Tests.SystemMonitorWidget
{
    public class SystemMonitorWidgetTests
    {
        [Fact]
        public void SystemMonitorWidget_Name_IsCorrect()
        {
            var mockProvider = new Mock<ISystemMetricsProvider>();
            var widget = new VistaForge.Widgets.SystemMonitorWidget.SystemMonitorWidget(mockProvider.Object);
            Assert.Equal("System Monitor", widget.Name);
        }

        [Fact]
        public void Update_FetchesMetricsFromProvider()
        {
            // Arrange
            var mockProvider = new Mock<ISystemMetricsProvider>();
            mockProvider.Setup(p => p.GetCpuUsage()).Returns(50f);
            mockProvider.Setup(p => p.GetRamUsage()).Returns(60f);
            mockProvider.Setup(p => p.GetDiskUsage()).Returns(70f);

            var widget = new VistaForge.Widgets.SystemMonitorWidget.SystemMonitorWidget(mockProvider.Object);
            widget.Render(); // Initialize control

            // Act
            widget.Update();

            // Assert
            mockProvider.Verify(p => p.GetCpuUsage(), Times.AtLeastOnce);
            mockProvider.Verify(p => p.GetRamUsage(), Times.AtLeastOnce);
            mockProvider.Verify(p => p.GetDiskUsage(), Times.AtLeastOnce);
        }
    }
}
