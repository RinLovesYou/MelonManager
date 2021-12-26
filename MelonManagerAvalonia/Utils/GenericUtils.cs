using System.Diagnostics;
using System.Runtime.InteropServices;

namespace MelonManagerAvalonia.Utils;

public class GenericUtils
{
    public static void OpenBrowser(string url)
    {
        try
        {
            Process.Start(url);
        }
        catch
        {
            // hack because of this: https://github.com/dotnet/corefx/issues/10361
            if (Program.CurrentPlatform == OSPlatform.Windows)
            {
                url = url.Replace("&", "^&");
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
            }
            else if (Program.CurrentPlatform == OSPlatform.Linux)
            {
                Process.Start("xdg-open", url);
            }
            else if (Program.CurrentPlatform == OSPlatform.OSX)
            {
                Process.Start("open", url);
            }
            else
            {
                throw;
            }

        }
    }
}