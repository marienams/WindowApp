using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace WIN_FirstApp
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public bool isBarScaled;
        public MainWindow()
        {
            this.InitializeComponent();

            int width = 500;
            int height = 870;
            var windowAPP = this.AppWindow;
            windowAPP.Resize(new SizeInt32(width, height));
            Logging_Access();
            LoadLogContent();
        }



        private void AnimateBar_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            // Scale the bar up when the button is pressed
            StartScaleAnimation(2, 0);  // Adjust the second parameter to the desired scale
        }

        private void AnimateBar_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            // Scale the bar back down when the button is released
            StartScaleAnimation(0, 2); // Reverse the scale
        }

        private void StartScaleAnimation(double from, double to)
        {
            var scaleYAnimation = new DoubleAnimation
            {
                From = from,
                To = to,
                Duration = new Duration(TimeSpan.FromSeconds(0.5)), // Adjust the duration as needed
                EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut }
            };

            var storyboard = new Storyboard();
            storyboard.Children.Add(scaleYAnimation);

            Storyboard.SetTarget(scaleYAnimation, BarScaleTransform);
            Storyboard.SetTargetProperty(scaleYAnimation, "ScaleY");

            storyboard.Begin();
        }
        private void Btn_Animation(object sender, RoutedEventArgs e)
        {
            // Create the animation for scaling the bar height
            DoubleAnimation scaleAnimation = new DoubleAnimation
            {
                From = isBarScaled ? 2 : 0,
                To = isBarScaled ? 0 : 2,
                Duration = new Duration(TimeSpan.FromSeconds(1)),
                AutoReverse = false
            };

            // Update the scale state tracking
            isBarScaled = !isBarScaled;

            // Create the storyboard to contain the animation
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(scaleAnimation);

            // Set the target property for the animation
            Storyboard.SetTarget(scaleAnimation, BarScaleTransform);
            Storyboard.SetTargetProperty(scaleAnimation, "ScaleY");

            // Start the animation
            storyboard.Begin();
        }
        private void Logging_Access()
        {
            string logFilePath = "AppLog.txt";
            string logEntry = $"App opened at: {DateTime.Now}\n";
            File.AppendAllText(logFilePath, logEntry);
        }
        private void LoadLogContent()
        {
            string logFilePath = "AppLog.txt";

            if (File.Exists(logFilePath))
            {
                string logContent = File.ReadAllText(logFilePath);
                LogTextBox.Text = logContent; // Display the log content in the TextBox
            }
            else
            {
                LogTextBox.Text = "Log file not found."; // Handle if the log file doesn't exist
            }
        }
    }
}
