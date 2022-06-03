using System.Windows;
using System.Windows.Input;

namespace MT.Client.WPF;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Border_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ChangedButton == MouseButton.Left)
        {
            DragMove();
        }
    }

    private void MinimizeCommand(object sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }

    private void MaximizeCommand(object sender, RoutedEventArgs e)
    {
        WindowState ^= WindowState.Maximized;
    }

    private void CloseCommand(object sender, RoutedEventArgs e)
    {
        Close();
    }
}
