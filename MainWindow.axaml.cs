using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml.Linq;

namespace AvaloniaApplication1
{
    public partial class MainWindow : Window
    {
        [LibraryImport(@"libs\SDL2_mixer.dll", EntryPoint = "Mix_Volume")]
        internal static partial int MixVolume(int channel, int volume);

        [DllImport(@"libs\SDL2.dll", EntryPoint = "SDL_GetAudioDriver")]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public extern static string GetAudioDriver(int index);

        [LibraryImport(@"libs\SDL2.dll", EntryPoint = "SDL_GetNumAudioDevices")]
        internal static partial int GetNumAudioDevices(int iscapture);

        [DllImport(@"libs\SDL2.dll", EntryPoint = "SDL_GetAudioDeviceName")]
        public extern static IntPtr GetAudioDeviceName(int index, int iscapture);

        [DllImport(@"libs\SDL2.dll", EntryPoint = "SDL_Init")]
        public extern static int SDLInit(uint flags);

        [Flags]
        enum SDLInitFlags : uint
        {
            Timer = 0x00000001u,
            Audio = 0x00000010u,
            Video = 0x00000020u,  /**< SDL_INIT_VIDEO implies SDL_INIT_EVENTS */
            Joystick = 0x00000200u,  /**< SDL_INIT_JOYSTICK implies SDL_INIT_EVENTS */
            Haptic = 0x00001000u,
            GameController = 0x00002000u,  /**< SDL_INIT_GAMECONTROLLER implies SDL_INIT_JOYSTICK */
            Events = 0x00004000u,
            Sensor = 0x00008000u,
            NoParachute = 0x00100000u,  /**< compatibility; this flag is ignored. */
        }

        public MainWindow()
        {
            InitializeComponent();
            var res = SDLInit((uint)(SDLInitFlags.Audio | SDLInitFlags.Events));

            int outputDeviceCount = GetNumAudioDevices(0);
            int inputDeviceCount = GetNumAudioDevices(1);

            Console.WriteLine($"output ({outputDeviceCount}):");
            for (int i = 0; i < outputDeviceCount; i++)
            {
                IntPtr driver = GetAudioDeviceName(i, 0);
                string? driverName = Marshal.PtrToStringAnsi(driver);
                Console.WriteLine($"driver: {driverName}");
            }

            Console.WriteLine($"intput ({inputDeviceCount}):");
            for (int i = 0; i < outputDeviceCount; i++)
            {
                IntPtr driver = GetAudioDeviceName(0, 1);
                string? driverName = Marshal.PtrToStringAnsi(driver);
                Console.WriteLine($"driver: {driverName}");
            }
        }

        private void PointerPressedHandler(object sender, PointerPressedEventArgs args)
        {
            PointerPoint point = args.GetCurrentPoint(sender as Control);
            double x = point.Position.X;
            double y = point.Position.Y;
            string msg = $"Pointer press at {x}, {y} relative to sender.";
            if (point.Properties.IsLeftButtonPressed)
            {
                msg += " Left button pressed.";
            }
            if (point.Properties.IsRightButtonPressed)
            {
                msg += " Right button pressed.";
            }
            Console.WriteLine(msg);
        }

        private void LoginButton_Click(object sender, RoutedEventArgs args)
        {
            Console.WriteLine("Button clicked!");
            var loginWindow = new LoginWindow();
            loginWindow.Show();
        }
    }
}