using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Gaming.Input;
using Windows.UI.Core;
using System.Threading.Tasks;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GameController
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        RawGameController Controller2;
        public MainPage()
        {
            this.InitializeComponent();

            RawGameController.RawGameControllerAdded += RawGameController_RawGameControllerAdded;
            RawGameController.RawGameControllerRemoved += RawGameController_RawGameControllerRemoved;
            
            DispatcherTimer t = new DispatcherTimer();
            t.Interval = new TimeSpan(0, 0, 0, 0, 1);
            t.Tick += T_Tick1;
            t.Start();

        }

        private async void RawGameController_RawGameControllerRemoved(object sender, RawGameController e)
        {
            Console.WriteLine("removed");
            await Device_Name("Device Removed.");
        }

        private async void RawGameController_RawGameControllerAdded(object sender, RawGameController e)
        {
            Console.WriteLine("added");
            RawGameController[] devices = RawGameController.RawGameControllers.ToArray();

            string device_name = "";

            for (int i = 0; i < devices.Count(); i++)
            {
                if (devices[i].DisplayName == "Sony DualShock4 Gamepad")
                { 
                    device_name = devices[i].DisplayName;
                }
            }
            await Device_Name(device_name);
        }

        private async void T_Tick1(object sender, object e)
        {
            if (RawGameController.RawGameControllers.Count > 0)
            {

                RawGameController[] devices = RawGameController.RawGameControllers.ToArray();
                bool correct_device = Correct_Device_Connected(devices);

                if (correct_device)
                {
                    for (int i = 0; i < devices.Count(); i++)
                    {
                        if (devices[i].DisplayName == "Sony DualShock4 Gamepad")
                        {
                            Controller2 =  devices[i];
                        }
                    }
                    //Controller2 = RawGameController.RawGameControllers[0];

                    int switchCount = Controller2.ButtonCount;
                    bool[] buttonArray = new bool[switchCount];

                    GameControllerSwitchPosition[] switchArray = new GameControllerSwitchPosition[switchCount];


                    double[] axisArray = new double[Controller2.AxisCount];

                    Controller2.GetCurrentReading(buttonArray, switchArray, axisArray);

                    await Device_Reading_Throttle(axisArray[4].ToString(), axisArray[3].ToString());
                }
            }
        }


        private bool Correct_Device_Connected(RawGameController[] devices)
        {
            for (int i = 0; i < devices.Count(); i++)
            {
                if (devices[i].DisplayName == "Sony DualShock4 Gamepad")
                {
                    return true;
                }
            }
            return false;
         }

        private async Task Device_Name(string device_name)
        {
            Task t = Task.Run(() => Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
              {
                  txtGameControllerName.Text = device_name;
              }
             ));
            await t;
        
        }

        private async Task Device_Reading_Throttle(string throttle_reading, string brake_reading)
        {
            Task t = Task.Run(() => Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                txtThrottleMin.Text = throttle_reading;
                txtBrakeMax.Text = brake_reading;
            }
             ));
            await t;

        }

    }
}
