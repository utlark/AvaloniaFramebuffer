using System;
using System.Linq;
using System.Threading;
using Avalonia;
using Avalonia.Media;

namespace AvaloniaFramebuffer;

internal class Program
{
    [STAThread]
    public static int Main(string[] args)
    {
        var builder = BuildAvaloniaApp();

        if (args.Contains("--fbdev"))
        {
            SilenceConsole();
            return builder.StartLinuxFbDev(args);
        }

        if (args.Contains("--drm"))
        {
            SilenceConsole();
            return builder.StartLinuxDrm(args, null, 1);
        }

        return builder.StartWithClassicDesktopLifetime(args);
    }

    private static void SilenceConsole() =>
        new Thread(() =>
            {
                Console.CursorVisible = false;
                while (true)
                    Console.ReadKey(true);
            })
            { IsBackground = true }.Start();

    public static AppBuilder BuildAvaloniaApp()
    {
        return AppBuilder.Configure<App>()
            .With(new FontManagerOptions
            {
                DefaultFamilyName = "avares://AvaloniaFramebuffer/NotoSans-Regular.ttf#Noto Sans"
            })
            .UsePlatformDetect()
            .LogToTrace();
    }
}