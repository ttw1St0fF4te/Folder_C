using System.Diagnostics;

namespace Folder_C
{
    internal static class knopka
    {
        public static ConsoleKeyInfo clavisha;
    }
    internal class menu
    {
        public static int max;
        public static int min;
        public menu(int a, int b)
        {
            min = a;
            max = b;

        }

        public static int Strelki()
        {
            int position = 0;
            while (true)
            {
                string del = new string(' ', 2);

                if (knopka.clavisha.Key == ConsoleKey.UpArrow)
                {
                    Console.SetCursorPosition(0, position--);
                    if (position < min)
                    {
                        position = min;
                    }
                }
                if (knopka.clavisha.Key == ConsoleKey.DownArrow)
                {
                    Console.SetCursorPosition(0, position++);
                    if (position > max)
                    {
                        position = max;
                    }
                }
                else if (knopka.clavisha.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (knopka.clavisha.Key == ConsoleKey.Escape)
                {
                    return -1;
                }
                Console.Write("\r" + del);
                Console.SetCursorPosition(0, position);
                Console.WriteLine("->");
                knopka.clavisha = Console.ReadKey();
            }
            return position;
        }
    }
    internal static class dop
    {
        public static void DiskInf()
        {
            DriveInfo[] DirAll = DriveInfo.GetDrives();
            int kolvo = DirAll.Length;
            foreach (var item in DirAll)
            {
                Console.WriteLine("  " + item.Name + $" cвободно + {item.TotalFreeSpace / 1073741824} Гб из {item.TotalSize / 1073741824} Гб");
            }
            menu menu = new menu(0, kolvo - 1);

            int pos = menu.Strelki();
            knopka.clavisha = Console.ReadKey();
            Console.Clear();
            DiskCD(DirAll[pos].ToString());
            knopka.clavisha = new ConsoleKeyInfo();
            pos = 0;
        }

        static void DiskCD(string paf)
        {
            int position = 0;
            while (true)
            {
                int pos = 1;
                string[] directories = Directory.GetDirectories(paf);
                string[] allFiles = Directory.GetFiles(paf);
                int kolvo = directories.Length + allFiles.Length;
                menu menu = new menu(0, kolvo - 1);

                foreach (var item in directories)
                {
                    Console.WriteLine("  " + item);
                }
                foreach (var item in allFiles)
                {
                    Console.WriteLine("  " + item);
                }
                foreach (var item in directories)
                {
                    DirectoryInfo inf = new DirectoryInfo(item);
                    string extn = inf.Extension;
                    DateTime tdirs = Directory.GetCreationTime(item);
                    Console.SetCursorPosition(45, position++);
                    Console.Write(tdirs + "                " + extn);
                }
                foreach (var item in allFiles)
                {
                    FileInfo inf = new FileInfo(item);
                    string extn = inf.Extension;
                    DateTime tdirs = File.GetCreationTime(item);
                    Console.SetCursorPosition(45, position++);
                    Console.Write(tdirs + "                " + extn);
                }
                position = 0;
                knopka.clavisha = new ConsoleKeyInfo();
                pos = menu.Strelki();
                Console.Clear();
                if (paf == "C:\\" || paf == "D:\\")
                    if (pos == -1)
                    {
                        pos = 0;
                        Program.Main();
                    }
                if (pos == -1)
                {
                    return;
                }
                else if (pos < directories.Length)
                {
                    DiskCD(directories[pos].ToString());
                }
                else
                {
                    Process.Start(new ProcessStartInfo { FileName = allFiles[pos - directories.Length], UseShellExecute = true });
                }
            }
        }
    }
}