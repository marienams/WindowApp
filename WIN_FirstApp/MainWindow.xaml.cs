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
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics;
using Windows.Storage;
using Windows.UI.Core.AnimationMetrics;
using System.Windows;
using System.Windows.Input;



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
            PopulateExpanderWithLog();

        }

        


        private void Btn_Animation(object sender, RoutedEventArgs e)
        {
            //This is the Button to animate
            //Click to start animation and click again to bring it back down
            // Animation for scaling the bar height
            DoubleAnimation scaleAnimation = new DoubleAnimation
            {
                From = isBarScaled ? 1 : 0,
                To = isBarScaled ? 0 : 1,
                Duration = new Duration(TimeSpan.FromSeconds(1)),
                AutoReverse = false
            };

            // Update the scale state tracking
            isBarScaled = !isBarScaled;

            // Storyboard for the animation
            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(scaleAnimation);

            // Target pripety for scale transform
            Storyboard.SetTarget(scaleAnimation, BlueBarTransform);
            Storyboard.SetTargetProperty(scaleAnimation, "ScaleY");

            // Start the animation
            storyboard.Begin();
        }
        private void Logging_Access()
        {
            //Log the date and time everytime the app initializes
            // Get the local app data folder
            string localFolderPath = ApplicationData.Current.LocalFolder.Path;

            
            // Check your AppData under your user folder to view AppLog.txt file
            string logFilePath = Path.Combine(localFolderPath, "AppLog.txt");

            // Log entry to write
            string logEntry = $"App opened at: {DateTime.Now}\n";

            // Append log entry to the file
            File.AppendAllText(logFilePath, logEntry);
        }

        private void PopulateExpanderWithLog()
        {
            //display the data logged inside the view in the expander
            // Get the local app data folder
            string localFolderPath = ApplicationData.Current.LocalFolder.Path;

            // Full path for the log file inside the local app data folder
            string logFilePath = Path.Combine(localFolderPath, "AppLog.txt");

            if (File.Exists(logFilePath))
            {
                
                var lines = File.ReadAllLines(logFilePath);

                // Get the last three entries
                var latestEntries = lines.Reverse().Take(3);

                // Clear existing items before adding new entries
                List_Expander.Items.Clear();

                // Add each of the latest entries as an individual item
                foreach (var entry in latestEntries)
                {
                    List_Expander.Items.Add(entry);
                }
            }
            else
            {
                List_Expander.Items.Add("Log file not found.");
            }
            
        }


        //THE LONG PRESS BUTTON FUNCTIONS: NOT WORKING

        //private void Button_PointerPressed(object sender, PointerRoutedEventArgs e)
        //{
        //    //StartScaleAnimation(0, 1);
        //    var scaleYAnimation = new DoubleAnimation
        //    {
        //        From = 0,
        //        To = 2,
        //        Duration = new Duration(TimeSpan.FromSeconds(0.5)), // Adjust the duration as needed
        //        EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut }
        //    };

        //    var storyboard = new Storyboard();
        //    storyboard.Children.Add(scaleYAnimation);

        //    Storyboard.SetTarget(scaleYAnimation, BlueBarTransform);
        //    Storyboard.SetTargetProperty(scaleYAnimation, "ScaleY");

        //    storyboard.Begin();
        //}

        //private void Button_PointerReleased(object sender, PointerRoutedEventArgs e)
        //{

        //    //StartScaleAnimation(1, 0);
        //    var scaleYAnimation = new DoubleAnimation
        //    {
        //        From = 2,
        //        To = 0,
        //        Duration = new Duration(TimeSpan.FromSeconds(0.5)), // Adjust the duration as needed
        //        EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut }
        //    };

        //    var storyboard = new Storyboard();
        //    storyboard.Children.Add(scaleYAnimation);

        //    Storyboard.SetTarget(scaleYAnimation, BlueBarTransform);
        //    Storyboard.SetTargetProperty(scaleYAnimation, "ScaleY");

        //    storyboard.Begin();
        //}


        //private void StartScaleAnimation(double from, double to)
        //{
        //    var scaleYAnimation = new DoubleAnimation
        //    {
        //        From = from,
        //        To = to,
        //        Duration = new Duration(TimeSpan.FromSeconds(0.5)), // Adjust the duration as needed
        //        EasingFunction = new SineEase { EasingMode = EasingMode.EaseInOut }
        //    };

        //    var storyboard = new Storyboard();
        //    storyboard.Children.Add(scaleYAnimation);

        //    Storyboard.SetTarget(scaleYAnimation, BlueBarTransform);
        //    Storyboard.SetTargetProperty(scaleYAnimation, "ScaleY");

        //    storyboard.Begin();
        //}



    }
}
