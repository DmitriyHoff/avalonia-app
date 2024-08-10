using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using System;
using System.Runtime.InteropServices;
using libs;

namespace AvaloniaApplication1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs args)
        {
            Console.WriteLine("Button clicked!");
            var loginWindow = new LoginWindow();
            loginWindow.Show();
        }
    }
}