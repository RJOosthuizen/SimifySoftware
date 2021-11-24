using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Gaming.Input;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Simify
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        // initialize raw game controller class

        RawGameController Controller;
        public MainPage() // main function
        {
            this.InitializeComponent();


            // set up game controller added + removed
            RawGameController.RawGameControllerAdded += RawGameController_RawGameControllerAdded; // triggers when contoller connected
            RawGameController.RawGameControllerRemoved += RawGameController_RawGameControllerRemoved; // triggers when contoller disconnected

            // starts timer to continuously read values from raw game controller
            DispatcherTimer t = new DispatcherTimer();
            t.Interval = new TimeSpan(0, 0, 0, 0, 1); // function to read values will run every 100ms
            t.Tick += T_Tick; // function name that is called to read values
            t.Start(); // start function and run as per timespan parameters
        }



        // code that runs when a controller is connected
        private async void RawGameController_RawGameControllerAdded(object sender, RawGameController e)
        {
            
            RawGameController[] devices = RawGameController.RawGameControllers.ToArray(); // get list of all devices

            string device_name = ""; // create device name variable to store display name of device

            for (int i = 0; i < devices.Count(); i++) // loop through device list
            {
                if (devices[i].DisplayName == "Sony DualShock4 Gamepad") // identifies correct device to be displayed // TODO: this needs to be changed to simify pro pedals name
                {
                    device_name = devices[i].DisplayName; // set variable to correct device name
                    //Console.WriteLine(device_name);
                    await Display_Device_Name(device_name, true); // call function to update label with correct device name that has been added
                }
            }
        }

        // code that runs when a controller is disconnected
        private async void RawGameController_RawGameControllerRemoved(object sender, RawGameController e)
        {
            //Console.WriteLine("Device Removed.");
            await Display_Device_Name("Device Removed", false); // call function to update label that a device has been removed
        }

        // display device text
        private async Task Display_Device_Name(string device_name, bool connected)
        {
            Task t = Task.Run(() => Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                txtDeviceName.Text = device_name; // changes label to specific device text
                if (connected) // if device connected then change text to green, else red
                {
                    txtDeviceName.Foreground = new SolidColorBrush(Colors.Green);
                }
                else
                {
                    txtDeviceName.Foreground = new SolidColorBrush(Colors.Red);
                }
            }
             ));
            await t;

        }

        // main function that reads values from controller
        private async void T_Tick(object sender, object e)
        {
            if (RawGameController.RawGameControllers.Count > 0) // run only if there are devices connected
            {

                RawGameController[] devices = RawGameController.RawGameControllers.ToArray(); // get all devices and store in array
                bool correct_device = Correct_Device_Connected(devices); // run function to detect if correct device is connected for value reading

                if (correct_device) // if correct device connected then run
                {
                    for (int i = 0; i < devices.Count(); i++) // loop through device list
                    {
                        if (devices[i].DisplayName == "Sony DualShock4 Gamepad") // get correct device // TODO: this needs to be changed to simify pro pedals name
                        {
                            Controller = devices[i]; // set controller to correct device
                        }
                    }

                    int switchCount = Controller.ButtonCount; // get button count for correct connected device
                    bool[] buttonArray = new bool[switchCount]; // create bool array with correct amount of button mappings for correct connected device
                    
                    GameControllerSwitchPosition[] switchArray = new GameControllerSwitchPosition[switchCount]; // create switcharray with correct amount of switch positions that will be updated with the relevant switch positions for correct connected device
                    
                    double[] axisArray = new double[Controller.AxisCount]; // create axis array with amount of axis for the correct connected device

                    Controller.GetCurrentReading(buttonArray, switchArray, axisArray); // get device current reading and populate relevant arrays

                    await Device_Display_Throttle_Reading(axisArray[4].ToString()); // send throttle reading to current throttle reading label // TODO: update with simify throttle reading in array
                    await Device_Display_Brake_Reading(axisArray[3].ToString()); // send brake reading to current brake reading label // TODO: update with simify brake reading in array
                    await Device_Display_Clutch_Reading(axisArray[2].ToString()); // send clutch reading to current clutch reading label // TODO: update with simify clutch reading in array


                }
            }
        }

        // function to detect if correct device is connected for value reading
        private bool Correct_Device_Connected(RawGameController[] devices)
        {
            for (int i = 0; i < devices.Count(); i++) // loop through device list
            {
                if (devices[i].DisplayName == "Sony DualShock4 Gamepad") // check for correct device // TODO: this needs to be changed to simify pro pedals name
                {
                    return true; // if correct device connected return true
                }
            }
            return false; // if correct device not connected return false
        }

        // function to update label with current throttle reading
        private async Task Device_Display_Throttle_Reading(string throttle_reading)
        {
            Task t = Task.Run(() => Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                txtThrottleReading.Text = "Current Reading: " + throttle_reading; // set current throttle reading label
                pbThrottle.Value = Convert.ToDouble(throttle_reading); // set value for progress bar
            }
             ));
            await t;

        }

        // function to update label with current brake reading
        private async Task Device_Display_Brake_Reading(string brake_reading)
        {
            Task t = Task.Run(() => Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                txtBrakeReading.Text = "Current Reading: " +  brake_reading; // set current brake reading label
            }
             ));
            await t;

        }

        // function to update label with current clutch reading
        private async Task Device_Display_Clutch_Reading(string clutch_reading)
        {
            Task t = Task.Run(() => Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                txtClutchReading.Text = "Current Reading: " +  clutch_reading; // set current clutch reading label
            }
             ));
            await t;

        }


        // THROTTLE
        // function to start throttle calibration proccess
       
        private void btnCalibrateThrottle_Click(object sender, RoutedEventArgs e)
        {
            txtThrottleInstructions.Text = "RELEASE the throttle pedal fully and click Capture Min Value."; // update label with first set of instructions
            
            btnCalibrateThrottle.IsEnabled = false; // disable calibrate start button
            btnCaptureThrottleMin.IsEnabled = true; // enable min value capture button

            txtThrottleReading.Visibility = Visibility.Visible; // show current reading label
            pbThrottle.Visibility = Visibility.Visible; // show progress bar

            btnCaptureThrottleMin.Background = new SolidColorBrush(Colors.DarkCyan); //update button color
        }

        // function to capture throttle min value
        private void btnCaptureThrottleMin_Click(object sender, RoutedEventArgs e)
        {
            txtThrottleInstructions.Text = "PRESS the throttle pedal fully and click Capture Max Value."; // update label with second set of instructions

            btnCaptureThrottleMin.IsEnabled = false; // disable capture min value button
            btnCaptureThrottleMax.IsEnabled = true; // enable max value capture button

            List<String> listStrLineElements = txtThrottleReading.Text.Split(' ').ToList(); // get current reading and split into list
            txtThrottleMin.Text = listStrLineElements[2]; // update min value label with 3rd item in list which is the raw value

            btnCaptureThrottleMin.Background = new SolidColorBrush(Colors.Gray); //update button color
            btnCaptureThrottleMax.Background = new SolidColorBrush(Colors.DarkCyan); //update button color

        }

        // function to capture throttle max value
       
        private void btnCaptureThrottleMax_Click(object sender, RoutedEventArgs e)
        {
            txtThrottleInstructions.Text = ""; // clear instructions label text

            btnCaptureThrottleMax.IsEnabled = false; // disable max value capture button
            btnCalibrateThrottle.IsEnabled = true; // enable calibrate start button

            List<String> listStrLineElements = txtThrottleReading.Text.Split(' ').ToList(); // get current reading and split into list
            txtThrottleMax.Text = listStrLineElements[2]; // update max value label with 3rd item in list which is the raw value

            
            txtThrottleReading.Visibility = Visibility.Collapsed; // hide current reading label
            pbThrottle.Visibility = Visibility.Collapsed; // hide progress bar

            btnCaptureThrottleMax.Background = new SolidColorBrush(Colors.Gray); //update button color

        }


        // BRAKE
        // function to start brake calibration proccess
        private void btnCalibrateBrake_Click(object sender, RoutedEventArgs e)
        {
            txtBrakeInstructions.Text = "RELEASE the brake pedal fully and click Capture Min Value."; // update label with first set of instructions

            btnCalibrateBrake.IsEnabled = false; // disable calibrate start button
            btnCaptureBrakeMin.IsEnabled = true; // enable min value capture button

            txtBrakeReading.Visibility = Visibility.Visible; // show current reading label
            pbBrake.Visibility = Visibility.Visible; // show progress bar

            btnCaptureBrakeMin.Background = new SolidColorBrush(Colors.DarkCyan); //update button color
        }

        // function to capture brake min value
        private void btnCaptureBrakeMin_Click(object sender, RoutedEventArgs e)
        {
            txtBrakeInstructions.Text = "PRESS the brake pedal fully and click Capture Max Value."; // update label with second set of instructions

            btnCaptureBrakeMin.IsEnabled = false; // disable capture min value button
            btnCaptureBrakeMax.IsEnabled = true; // enable max value capture button

            List<String> listStrLineElements = txtBrakeReading.Text.Split(' ').ToList(); // get current reading and split into list
            txtBrakeMin.Text = listStrLineElements[2]; // update min value label with 3rd item in list which is the raw value

            btnCaptureBrakeMin.Background = new SolidColorBrush(Colors.Gray); //update button color
            btnCaptureBrakeMax.Background = new SolidColorBrush(Colors.DarkCyan); //update button color

        }

        // function to capture brake max value

        private void btnCaptureBrakeMax_Click(object sender, RoutedEventArgs e)
        {
            txtBrakeInstructions.Text = ""; // clear instructions label text

            btnCaptureBrakeMax.IsEnabled = false; // disable max value capture button
            btnCalibrateBrake.IsEnabled = true; // enable calibrate start button

            List<String> listStrLineElements = txtBrakeReading.Text.Split(' ').ToList(); // get current reading and split into list
            txtBrakeMax.Text = listStrLineElements[2]; // update max value label with 3rd item in list which is the raw value


            txtBrakeReading.Visibility = Visibility.Collapsed; // hide current reading label
            pbBrake.Visibility = Visibility.Collapsed; // hide progress bar

            btnCaptureBrakeMax.Background = new SolidColorBrush(Colors.Gray); //update button color

        }


        // CLUTCH
        // function to start clutch calibration proccess
        private void btnCalibrateClutch_Click(object sender, RoutedEventArgs e)
        {
            txtClutchInstructions.Text = "RELEASE the clutch pedal fully and click Capture Min Value."; // update label with first set of instructions

            btnCalibrateClutch.IsEnabled = false; // disable calibrate start button
            btnCaptureClutchMin.IsEnabled = true; // enable min value capture button

            txtClutchReading.Visibility = Visibility.Visible; // show current reading label
            pbClutch.Visibility = Visibility.Visible; // show progress bar

            btnCaptureClutchMin.Background = new SolidColorBrush(Colors.DarkCyan); //update button color
        }

        // function to capture clutch min value
        private void btnCaptureClutchMin_Click(object sender, RoutedEventArgs e)
        {
            txtClutchInstructions.Text = "PRESS the clutch pedal fully and click Capture Max Value."; // update label with second set of instructions

            btnCaptureClutchMin.IsEnabled = false; // disable capture min value button
            btnCaptureClutchMax.IsEnabled = true; // enable max value capture button

            List<String> listStrLineElements = txtClutchReading.Text.Split(' ').ToList(); // get current reading and split into list
            txtClutchMin.Text = listStrLineElements[2]; // update min value label with 3rd item in list which is the raw value

            btnCaptureClutchMin.Background = new SolidColorBrush(Colors.Gray); //update button color
            btnCaptureClutchMax.Background = new SolidColorBrush(Colors.DarkCyan); //update button color

        }

        // function to capture clutch max value

        private void btnCaptureClutchMax_Click(object sender, RoutedEventArgs e)
        {
            txtClutchInstructions.Text = ""; // clear instructions label text

            btnCaptureClutchMax.IsEnabled = false; // disable max value capture button
            btnCalibrateClutch.IsEnabled = true; // enable calibrate start button

            List<String> listStrLineElements = txtClutchReading.Text.Split(' ').ToList(); // get current reading and split into list
            txtClutchMax.Text = listStrLineElements[2]; // update max value label with 3rd item in list which is the raw value


            txtClutchReading.Visibility = Visibility.Collapsed; // hide current reading label
            pbClutch.Visibility = Visibility.Collapsed; // hide progress bar

            btnCaptureClutchMax.Background = new SolidColorBrush(Colors.Gray); //update button color

        }
    }
}
