using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using System.Drawing;
using System.Threading.Tasks;

namespace VistaForge.App.UITests
{
    public class BasicUITests : IDisposable
    {
        private const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
        private const string AppId = "VistaForge"; // Usually PackageFamilyName!AppId for packaged apps
        private WindowsDriver<WindowsElement>? _driver;

        public BasicUITests()
        {
            // Note: Since this is run on Linux during testing/CI, we skip actual setup
            // This is just a compilation check for the UITest suite structure
        }

        [Fact(Skip = "Requires Windows UI Environment and WinAppDriver running")]
        public void Verify_Widget_DragAndDrop()
        {
            // Arrange
            var appOptions = new AppiumOptions();
            appOptions.AddAdditionalCapability("app", AppId);
            appOptions.AddAdditionalCapability("platformName", "Windows");

            _driver = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appOptions);

            var widgetContainer = _driver.FindElementByAccessibilityId("MockWidgetContainer");

            var initialLocation = widgetContainer.Location;

            // Act
            Actions action = new Actions(_driver);
            action.DragAndDropToOffset(widgetContainer, 50, 50).Perform();

            // Assert
            var newLocation = widgetContainer.Location;
            Assert.Equal(initialLocation.X + 50, newLocation.X);
            Assert.Equal(initialLocation.Y + 50, newLocation.Y);
        }

        public void Dispose()
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver = null;
            }
        }
    }
}
