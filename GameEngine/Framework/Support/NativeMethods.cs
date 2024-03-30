namespace GameEngine.Framework.Support
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.ComponentModel;
    using System.Reflection;

    public static class NativeMethods
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct Message
        {
            public IntPtr hWnd;
            public uint msg;
            public IntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public System.Drawing.Point p;
        }

        public const uint WM_LBUTTONDOWN = 0x0201;
        public const uint WM_LBUTTONUP = 0x0202;
        public const uint WM_MOUSEMOVE = 0x0200;
        public const uint WM_MOUSEWHEEL = 0x020A;
        public const uint WM_RBUTTONDOWN = 0x0204;
        public const uint WM_RBUTTONUP = 0x0205;

        public static Message msg;

        private const int SM_CXSCREEN = 0;
        private const int SM_CYSCREEN = 1;
        private static IntPtr HWND_TOP = IntPtr.Zero;
        private const int SWP_SHOWWINDOW = 64; // 0×0040

        [System.Security.SuppressUnmanagedCodeSecurity] // We won't use this maliciously
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool PeekMessage(out Message msg, IntPtr hWnd, uint messageFilterMin, uint messageFilterMax, uint flags);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr LoadCursorFromFile(string path);

        [DllImport("user32.dll", EntryPoint = "GetSystemMetrics")]
        public static extern int GetSystemMetrics(int which);

        [DllImport("user32.dll")]
        public static extern void SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter, int X, int Y, int width, int height, uint flags);

        [DllImport("user32.dll")]
        public static extern int GetKeyboardState(byte[] keystate);

        public static bool AppStillIdle
        {
            get
            {
                return !PeekMessage(out msg, IntPtr.Zero, 0, 0, 0);
            }
        }
        
        public static Cursor LoadCustomCursor(string path)   
        {
            // http://allenwp.com/blog/2011/04/04/changing-the-windows-mouse-cursor-in-xna/
            // http://stackoverflow.com/questions/4305800/using-custom-colored-cursors-in-a-c-sharp-windows-application
            // http://www.rw-designer.com/gallery?search=game+cursors
            // http://www.rw-designer.com/gallery?search=crosshair&by=
            IntPtr hCurs = LoadCursorFromFile(path);        
            if (hCurs == IntPtr.Zero) 
                throw new Win32Exception();        
            var curs = new Cursor(hCurs);        
            // Note: force the cursor to own the handle so it gets released properly        
            var fi = typeof(Cursor).GetField("ownHandle", BindingFlags.NonPublic | BindingFlags.Instance);        
            fi.SetValue(curs, true);        
            return curs;    
        }      

        public static int ScreenX
        {
            get 
            { 
                return GetSystemMetrics(SM_CXSCREEN);
            }
        }

        public static int ScreenY
        {
            get 
            { 
                return GetSystemMetrics(SM_CYSCREEN);
            }
        }

        public static void SetWinFullScreen(IntPtr hwnd)
        {
            // http://www.codeproject.com/Articles/16618/How-To-Make-a-Windows-Form-App-Truly-Full-Screen-a
            SetWindowPos(hwnd, HWND_TOP, 0, 0, ScreenX, ScreenY, SWP_SHOWWINDOW);
        }

    }
}
