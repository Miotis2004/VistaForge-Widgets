using System;
using Xunit;
using VistaForge.Widgets.ClockWidget;

namespace VistaForge.Widgets.Tests.ClockWidget
{
    public class ClockWidgetTests
    {
        [Fact]
        public void ClockWidget_Name_IsCorrect()
        {
            var widget = new VistaForge.Widgets.ClockWidget.ClockWidget();
            Assert.Equal("Clock", widget.Name);
        }
    }
}
