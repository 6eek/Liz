// See https://aka.ms/new-console-template for more information
using Lizthux;
using Spectre.Console;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System;
using System.Windows;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using Octokit;
using Microsoft.Extensions.Configuration;
using System.Net.NetworkInformation;
using System.Net.Http.Headers;

namespace Lizthux // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        public static Thread TE = new Thread(new ThreadStart(exit));
        public static async void clear()
        {
            Console.Clear();
            AnsiConsole.MarkupLine($"{Settings.Logo}");
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2 ) + ("===========================MOTD===========================".Length / 2)) + "}", "===========================MOTD==========================="));
            Console.WriteLine("");
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2 ) + ($"[ {Settings.MOTD} ]".Length / 2)) + "}", $"[ {Settings.MOTD} ]"));
            Console.WriteLine("");
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2 ) + ("===========================MOTD===========================".Length / 2)) + "}", "===========================MOTD==========================="));
            Console.WriteLine("");
            Console.WriteLine("");
        }
        public static async void exit()
        {
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + ("Exiting LIZTHUX".Length / 2)) + "}", "Exiting LIZTHUX"));
            Thread.Sleep(1500);
            System.Environment.Exit(-420420420);
        }
        public static async void yourblacklisted()
        {
            clear();
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + ("[!] KEY BLACKLISTED [!]".Length / 2)) + "}", "[!] KEY BLACKLISTED [!]"));
            Thread.Sleep(1500);
            System.Environment.Exit(-420420420);
        }
        public static bool yes = false;
        public static bool _IsBlacklisted = false;
        public static bool _IsAllowed = false;
        public static string key = "";
        public static async void execblacklist()
        {
            string[] linefrompastebin = new string[2500];
            string useridstring = key;
            int i = 0;
            int maxLines = 0;

            var url = "http://raw.githubusercontent.com/KemoThara/dwabtit/main/Blacklist.txt";
            var client = new WebClient();

            using (var stream = client.OpenRead(url))
            using (var reader = new StreamReader(stream))
            {
                linefrompastebin[0] = "";

                //Store lines from HTML into string
                while ((linefrompastebin[i] = reader.ReadLine()) != null)
                {
                    i++;
                }
                maxLines = i;
            }

            //do some line processing - compare user with whitelist
            for (i = 0; i < maxLines; i++)
            {
                //Console.WriteLine(linefrompastebin[i]);

                if (linefrompastebin[i] == useridstring)
                {
                    _IsBlacklisted = true;
                }

            }

            linefrompastebin = null;
            if (_IsBlacklisted == true)
            {
                yourblacklisted();
            }
        }
        public static async void whitelistkey()
        {
            string[] linefrompastebin = new string[2500];
            string useridstring = key;
            int i = 0;
            int maxLines = 0;

            var url = "http://raw.githubusercontent.com/KemoThara/dwabtit/main/Whitelist.txt";
            var client = new WebClient();

            using (var stream = client.OpenRead(url))
            using (var reader = new StreamReader(stream))
            {
                linefrompastebin[0] = "";

                //Store lines from HTML into string
                while ((linefrompastebin[i] = reader.ReadLine()) != null)
                {
                    i++;
                }
                maxLines = i;
            }

            //do some line processing - compare user with whitelist
            for (i = 0; i < maxLines; i++)
            {
                //Console.WriteLine(linefrompastebin[i]);
                if (linefrompastebin[i] == useridstring)
                {
                    _IsAllowed = true;
                }

            }

            linefrompastebin = null;
            if (_IsAllowed == true)
            {
                if (_IsBlacklisted == false)
                {
                    clear();
                    Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + ("[+] Success [+]".Length / 2)) + "}", "[+] Success [+]"));
                    Thread.Sleep(1000);
                    clear();
                }
            }
            if (_IsAllowed == false)
            {
                Console.WriteLine("");
                Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + ("[!] Invalid Key [!]".Length / 2)) + "}", "[!] Invalid Key [!]"));
                keyverif();
            }
        }
        public static async void keyverif()
        {
            yes = true;
            AnsiConsole.Markup("KEY:[rgb(255,0,0)]> [/]");
            key = User();
            if (key.ToLower() == "exit")
            {
                clear();
                exit();
            }
            execblacklist();
            whitelistkey();
        }
        public static async void blacklistakey()
        {
            AnsiConsole.Markup("KEY:[rgb(255,0,0)]> [/]");
            string blacklistkey = Console.ReadLine();
            var url2 = "http://raw.githubusercontent.com/KemoThara/dwabtit/main/Blacklist.txt";
            WebRequest request2 = WebRequest.Create(url2);
            WebResponse response2 = request2.GetResponse();
            Stream dataStream2 = response2.GetResponseStream();
            StreamReader reader2 = new StreamReader(dataStream2);
            string rawcontent2 = reader2.ReadToEnd();
            var newreplace2 = new string(rawcontent2 + "\n" + blacklistkey);
            var ghClient2 = new GitHubClient(new Octokit.ProductHeaderValue("Octokit-Test"));
            ghClient2.Credentials = new Credentials("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");

            // github variables
            var owner2 = "KemoThara";
            var repo2 = "dwabtit";
            var branch2 = "main";

            var targetFile2 = "Blacklist.txt";

            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + ($"Key {blacklistkey} has been blacklisted".Length / 2)) + "}", $"Key {blacklistkey} has been blacklisted"));
            OpenClipboard(IntPtr.Zero);
            var yourString = $"{blacklistkey}";
            var ptr = Marshal.StringToHGlobalUni(yourString);
            SetClipboardData(13, ptr);
            CloseClipboard();
            Marshal.FreeHGlobal(ptr);
                // try to get the file (and with the file the last commit sha)
            var existingFile2 = await ghClient2.Repository.Content.GetAllContentsByRef(owner2, repo2, targetFile2, branch2);

                // update the file
            var updateChangeSet2 = await ghClient2.Repository.Content.UpdateFile(owner2, repo2, targetFile2,
               new UpdateFileRequest("API File update", $"{newreplace2}", existingFile2.First().Sha, branch2));
        }
        public static async void createnewkey()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_=+)(*&^%$#@![{]},./";
            var stringChars = new char[40];
            var random = new Random();
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            var finalString = new String(stringChars);

            var url = "http://raw.githubusercontent.com/KemoThara/dwabtit/main/Whitelist.txt";
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string rawcontent = reader.ReadToEnd();
            var newreplace = new string(rawcontent + "\n" + finalString);
            var ghClient = new GitHubClient(new Octokit.ProductHeaderValue("Octokit-Test"));
            ghClient.Credentials = new Credentials("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");

            // github variables
            var owner = "KemoThara";
            var repo = "dwabtit";
            var branch = "main";

            var targetFile = "Whitelist.txt";

            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + ($"Key {finalString} has been created".Length / 2)) + "}", $"Key {finalString} has been created"));
            OpenClipboard(IntPtr.Zero);
            var yourString = $"{finalString}";
            var ptr = Marshal.StringToHGlobalUni(yourString);
            SetClipboardData(13, ptr);
            CloseClipboard();
            Marshal.FreeHGlobal(ptr);
            // try to get the file (and with the file the last commit sha)
            var existingFile = await ghClient.Repository.Content.GetAllContentsByRef(owner, repo, targetFile, branch);

            // update the file
            var updateChangeSet = await ghClient.Repository.Content.UpdateFile(owner, repo, targetFile,
               new UpdateFileRequest("API File update", $"{newreplace}", existingFile.First().Sha, branch));
        }
        public static string adminkey = "rtWbPbOMI]VlGn7vv3PeJdqp,XM0JXT%r^p{JEd.";


        [STAThread]
        public static async void inputloop()
        {
            while (_IsBlacklisted == false)
            {
                AnsiConsole.Markup(":[rgb(255,0,0)]> [/]");
                string userResponse = Console.ReadLine();
                if (userResponse.ToLower() == "clear")
                {
                    clear();
                }
                else if (userResponse.ToLower() == "exit")
                {
                    clear();
                    exit();
                }
                else if (userResponse.ToLower() == "commands")
                {
                    if (key == adminkey)
                    {
                        //Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + ("changeuser - Change current user".Length / 2)) + "}", "changeuser - Change current user"));
                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + ("Clear - Clears the terminal".Length / 2)) + "}", "Clear - Clears the terminal"));
                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + ("Admincomds - Admin Commands".Length / 2)) + "}", "Admincomds - Admin Commands"));
                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + ("Exit - Exits the terminal".Length / 2)) + "}", "Exit - Exits the terminal"));
                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + ("Commands - Brings up this".Length / 2)) + "}", "Commands - Brings up this"));
                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + ("IPLookup - Lookup an ip".Length / 2)) + "}", "IPLookup - Lookup an ip"));
                    }
                    else
                    {
                        //Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + ("changeuser - Change current user".Length / 2)) + "}", "changeuser - Change current user"));
                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + ("Clear - Clears the terminal".Length / 2)) + "}", "Clear - Clears the terminal"));
                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + ("Exit - Exits the terminal".Length / 2)) + "}", "Exit - Exits the terminal"));
                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + ("Commands - Brings up this".Length / 2)) + "}", "Commands - Brings up this"));
                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + ("IPLookup - Lookup an ip".Length / 2)) + "}", "IPLookup - Lookup an ip"));
                    }
                }
                else if (userResponse.ToLower() == "admincomds")
                {
                    if (key == adminkey)
                    {
                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + ("Blacklist - Blacklist an existing key".Length / 2)) + "}", "Blacklist - Blacklist an existing key"));
                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + ("Newkey - Generates a key".Length / 2)) + "}", "Newkey - Generates a key"));
                    }
                    else
                    {
                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + ("[!] Unknown Command [!]".Length / 2)) + "}", "[!] Unknown Command [!]"));
                    }
                }
                else if (userResponse.ToLower() == "newkey")
                {
                    if (key == adminkey)
                    {
                        try
                        {
                            createnewkey();
                        }
                        catch (Exception e3)
                        {
                            Console.WriteLine(e3);
                        }

                    }
                    else
                    {
                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + ("[!] Unknown Command [!]".Length / 2)) + "}", "[!] Unknown Command [!]"));
                    }

                }
                else if (userResponse.ToLower() == "blacklist")
                {
                    if (key == adminkey)
                    {
                        try
                        {
                            blacklistakey();
                        }
                        catch (Exception e2)
                        {
                            Console.WriteLine(e2);
                        }

                    }
                    else
                    {
                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + ("[!] Unknown Command [!]".Length / 2)) + "}", "[!] Unknown Command [!]"));
                    }

                }
                else if (userResponse.ToLower() == "iplookup")
                {
                    AnsiConsole.Markup("IP:[rgb(255,0,0)]> [/]");
                    string iptol = Console.ReadLine();
                    var url = $"http://proxycheck.io/v2/{iptol}/?risk=1?asn=1?vpn=1";
                    WebRequest request = WebRequest.Create(url);
                    WebResponse response = request.GetResponse();
                    Stream dataStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(dataStream);
                    string rawcontent = reader.ReadToEnd();
                    Console.Clear();
                    AnsiConsole.MarkupLine($"{Settings.Logo}");
                    Console.WriteLine(rawcontent);
                }
                else
                {
                    Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + ("[!] Unknown Command [!]".Length / 2)) + "}", "[!] Unknown Command [!]"));
                }
                if (_IsBlacklisted == true)
                {
                    yourblacklisted();
                }
            }
        }

        [DllImport("user32.dll")]
        internal static extern bool OpenClipboard(IntPtr hWndNewOwner);

        [DllImport("user32.dll")]
        internal static extern bool CloseClipboard();

        [DllImport("user32.dll")]
        internal static extern bool SetClipboardData(uint uFormat, IntPtr data);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        [DllImport("kernel32.dll", ExactSpelling = true)]

        private static extern IntPtr GetConsoleWindow();
        private static IntPtr ThisConsole = GetConsoleWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]

        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        private const int HIDE = 0;
        private const int MAXIMIZE = 3;
        private const int UNMAXIMIZE = 9;
        private const int MINIMIZE = 6;
        private const int RESTORE = 9;
        private const int NORMAL = 1;


        private const int MF_BYCOMMAND = 0x00000000;
        public const int SC_CLOSE = 0xF060;
        public const int SC_MINIMIZE = 0xF020;
        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_SIZE = 0xF000;

        [DllImport("user32.dll")]
        public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

        [DllImport("user32.dll")]
        private static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool IsIconic(IntPtr hWnd);
        [DllImport("user32.dll")]
        static extern bool IsZoomed(IntPtr hWnd);

        public const int SW_RESTORE = 9;

        [DllImport("user32.dll")]
        public static extern int SetForegroundWindow(IntPtr handle);
        public static void resizeloop()
        {
            while (true)
            {
                try
                {
                    while (true)
                    {
                        Process process = Process.GetCurrentProcess();
                        Console.Title = Settings.WinName;
                        Console.SetWindowSize(Settings.WinSize1, Settings.WinSize2);
                        Console.BufferHeight = Settings.BufHeight;
                        Console.BufferWidth = Settings.BufWidth;
                        DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_MAXIMIZE, MF_BYCOMMAND);
                        DeleteMenu(GetSystemMenu(GetConsoleWindow(), false), SC_SIZE, MF_BYCOMMAND);
                        if (Console.WindowWidth > Settings.WinSize1)
                        {
                            Console.SetWindowPosition(0, 0);
                        }
                        if (Console.WindowHeight > Settings.WinSize2)
                        {
                            Console.SetWindowPosition(0, 0);
                        }
                        if (Console.WindowWidth < Settings.WinSize1)
                        {
                            Console.SetWindowPosition(0, 0);
                        }
                        if (Console.WindowHeight < Settings.WinSize2)
                        {
                            Console.SetWindowPosition(0, 0);
                        }
                        if (IsZoomed(ThisConsole))
                        {
                            ShowWindow(ThisConsole, NORMAL);
                        }
                    }
                }
                catch
                {
                    continue;
                }
            }
        }
        public static class ConsoleWindow
        {
            private static class NativeFunctions
            {
                public enum StdHandle : int
                {
                    STD_INPUT_HANDLE = -10,
                    STD_OUTPUT_HANDLE = -11,
                    STD_ERROR_HANDLE = -12,
                }

                [DllImport("kernel32.dll", SetLastError = true)]
                public static extern IntPtr GetStdHandle(int nStdHandle); //returns Handle

                public enum ConsoleMode : uint
                {
                    ENABLE_ECHO_INPUT = 0x0004,
                    ENABLE_EXTENDED_FLAGS = 0x0080,
                    ENABLE_INSERT_MODE = 0x0020,
                    ENABLE_LINE_INPUT = 0x0002,
                    ENABLE_MOUSE_INPUT = 0x0010,
                    ENABLE_PROCESSED_INPUT = 0x0001,
                    ENABLE_QUICK_EDIT_MODE = 0x0040,
                    ENABLE_WINDOW_INPUT = 0x0008,
                    ENABLE_VIRTUAL_TERMINAL_INPUT = 0x0200,

                    //screen buffer handle
                    ENABLE_PROCESSED_OUTPUT = 0x0001,
                    ENABLE_WRAP_AT_EOL_OUTPUT = 0x0002,
                    ENABLE_VIRTUAL_TERMINAL_PROCESSING = 0x0004,
                    DISABLE_NEWLINE_AUTO_RETURN = 0x0008,
                    ENABLE_LVB_GRID_WORLDWIDE = 0x0010
                }

                [DllImport("kernel32.dll", SetLastError = true)]
                public static extern bool GetConsoleMode(IntPtr hConsoleHandle, out uint lpMode);

                [DllImport("kernel32.dll", SetLastError = true)]
                public static extern bool SetConsoleMode(IntPtr hConsoleHandle, uint dwMode);
            }

            public static void QuickEditMode(bool Enable)
            {
                //QuickEdit lets the user select text in the console window with the mouse, to copy to the windows clipboard.
                //But selecting text stops the console process (e.g. unzipping). This may not be always wanted.
                IntPtr consoleHandle = NativeFunctions.GetStdHandle((int)NativeFunctions.StdHandle.STD_INPUT_HANDLE);
                UInt32 consoleMode;

                NativeFunctions.GetConsoleMode(consoleHandle, out consoleMode);
                if (Enable)
                    consoleMode |= ((uint)NativeFunctions.ConsoleMode.ENABLE_QUICK_EDIT_MODE);
                else
                    consoleMode &= ~((uint)NativeFunctions.ConsoleMode.ENABLE_QUICK_EDIT_MODE);

                consoleMode |= ((uint)NativeFunctions.ConsoleMode.ENABLE_EXTENDED_FLAGS);

                NativeFunctions.SetConsoleMode(consoleHandle, consoleMode);
            }
        }
        public static string User()
        {
            while (yes == true)
            {
                yes = false;
                StringBuilder input = new StringBuilder();
                while (true)
                {
                    var key = Console.ReadKey(true);
                    if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                    if (key.Key == ConsoleKey.Backspace && input.Length > 0)
                    {
                        input.Remove(input.Length - 1, 1);
                        Console.Write("\b \b");
                    }
                    else if (key.Key != ConsoleKey.Backspace)
                    {
                        input.Append(key.KeyChar);
                        Console.Write("*");
                    }
                }
                return $"{input.ToString()}";
            }
            return $"";
        }
        static void Main(string[] args)
        {
            string CurVersion = "1.0.2\n";
            //var url2 = "http://raw.githubusercontent.com/Lizthux/Lizthux/main/Version.txt";
            //WebRequest request2 = WebRequest.Create(url2);
            //WebResponse response2 = request2.GetResponse();
            //Stream dataStream2 = response2.GetResponseStream();
            //StreamReader reader2 = new StreamReader(dataStream2);
            //string rawcontent2 = reader2.ReadToEnd();
            //if (rawcontent2 != CurVersion)
            if (CurVersion != CurVersion)
            {
                Thread T2 = new Thread(new ThreadStart(resizeloop));
                T2.Start();
                ConsoleWindow.QuickEditMode(false);
                clear();
                Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + ("[!] Please Update [!]".Length / 2)) + "}", "[!] Please Update [!]"));
            }
            else
            {
                Thread T1 = new Thread(new ThreadStart(inputloop));
                Thread T2 = new Thread(new ThreadStart(resizeloop));
                Thread TBL = new Thread(new ThreadStart(execblacklist));
                Thread TWL = new Thread(new ThreadStart(whitelistkey));
                T2.Start();
                clear();
                ConsoleWindow.QuickEditMode(false);
                keyverif();
                T1.Start();
                while (true)
                {
                    execblacklist();
                }
            }
        }
    }
}