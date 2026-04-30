using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace VistaForge.Widgets.ClockWidget
{
    public sealed partial class ClockWidgetControl : UserControl
    {
        public ClockWidgetControl()
        {
            //this.InitializeComponent();
        }

        private void ModeToggle_Toggled(object sender, RoutedEventArgs e)
        {
            if (false /* ModeToggle.IsOn */)
            {
                //DigitalText.Visibility = Visibility.Collapsed;
                //AnalogGrid.Visibility = Visibility.Visible;
            }
            else
            {
                //DigitalText.Visibility = Visibility.Visible;
                //AnalogGrid.Visibility = Visibility.Collapsed;
            }
        }

        public void UpdateTime(DateTime time)
        {
            // Update Digital
            //DigitalText.Text = time.ToString("HH:mm:ss");

            // Update Analog
            //SecondRotate.Angle = time.Second * 6;
            //MinuteRotate.Angle = (time.Minute * 6) + (time.Second * 0.1);
            //HourRotate.Angle = (time.Hour * 30) + (time.Minute * 0.5);
        }
    }
}
