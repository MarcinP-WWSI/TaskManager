using Avalonia;
using Avalonia.ReactiveUI;
using System;
using TaskManager;

internal class Program
{
    [STAThread]
    public static void Main(string[] args) =>
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);

    public static AppBuilder BuildAvaloniaApp()
    => AppBuilder.Configure<App>()
        .UsePlatformDetect()
        .UseReactiveUI()
        .LogToTrace();
}