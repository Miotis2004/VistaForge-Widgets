using System;
using Xunit;

namespace VistaForge.Widgets.Tests.NotesWidget
{
    public class NotesWidgetTests
    {
        [Fact]
        public void NotesWidget_Name_IsCorrect()
        {
            var widget = new VistaForge.Widgets.NotesWidget.NotesWidget();
            Assert.Equal("Sticky Notes", widget.Name);
        }
    }
}
