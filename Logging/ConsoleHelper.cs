using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Library.Logging
{
    public partial class ConsoleHelper
    {

        [LibraryImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool AllocConsole();

        [LibraryImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool FreeConsole();

        [LibraryImport("kernel32.dll", SetLastError = true)]
        public static partial IntPtr GetConsoleWindow();

        [LibraryImport("kernel32.dll", SetLastError = true)]
        public static partial IntPtr GetStdHandle(int nStdHandle);

        [LibraryImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool SetStdHandle(int nStdHandle, IntPtr handle);

        [LibraryImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;
        private const int STD_OUTPUT_HANDLE = -11;

        public static void CreateNewConsole()
        {
            FreeConsole();
            AllocConsole();

            IntPtr consoleHandle = GetConsoleWindow();
            ShowWindow(consoleHandle, SW_SHOW);
        }
    }
}
