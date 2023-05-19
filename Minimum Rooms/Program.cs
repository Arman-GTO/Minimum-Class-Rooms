using Minimum_Rooms.Classes;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Minimum_Rooms
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            #region Variables
            int start, end, min, max, counter, i;
            double rooms;
            bool isMinSet, error;
            string classPeriod;
            var my_Classes = new List<ClassPeriod>();
            var hourRooms = new List<HourRooms>();
            #endregion

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Console.CursorVisible = false;
            Console.Title = "Minimum Class Rooms";
            while (true)
            {
                my_Classes.Clear();
                hourRooms.Clear();
                min = 0;
                max = 0;
                counter = 1;
                rooms = 0;
                isMinSet = false;
                error = false;
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("  \"Please type in your class periods and double press enter when done\"\n");
                Console.ReadKey(true); // Jump to receiving inputs
                Console.CursorVisible = true;
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"    class {counter}: ");
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    classPeriod = Console.ReadLine() + "";
                    if (classPeriod == "")
                    {
                        Console.SetCursorPosition(4, Console.CursorTop - 1);
                        Console.Write(new string(' ', counter + 7));
                        Console.SetCursorPosition(0, Console.CursorTop);
                        break;
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    try
                    {
                        start = Convert.ToInt32(new Regex(@"^\d+").Match(classPeriod).Value);
                        end = Convert.ToInt32(new Regex(@"\d+$").Match(classPeriod).Value);
                    }
                    catch
                    {
                        Console.Write("\n  Invalid inputs!");
                        error = true;
                        break;
                    }
                    if (start == end)
                    {
                        Console.Write("\n  Start and end of class can't be the same!");
                        error = true;
                        break;
                    }
                    if (start > end)
                    {
                        Console.Write("\n  Wrong order of time!");
                        error = true;
                        break;
                    }
                    if (start < 0 || end < 0)
                    {
                        Console.Write("\n  Time can't be negetive!");
                        error = true;
                        break;
                    }
                    if (!isMinSet) { min = start; isMinSet = true; }
                    else if (start < min) min = start;
                    if (end > max) max = end;

                    my_Classes.Add(new ClassPeriod(start, end));
                    counter++;
                } // Get the class periods and check for any possible errors
                Console.CursorVisible = false;

                if (error) goto what_to_do; // Do not calculate any answer if any error has occurred

                for (i = min; i < max; i++)
                {
                    counter = 0;
                    foreach (var x in my_Classes)
                        if (i >= x.Start && i < x.End)
                            counter++;
                    if (counter > rooms)
                        rooms = counter + 0.2;
                    hourRooms.Add(new HourRooms(i, counter));
                } // Evaluate the maximum used time of the day
                hourRooms.Add(new HourRooms(i, counter));

                rooms = Convert.ToInt32(rooms);
                Console.ForegroundColor = ConsoleColor.Yellow;
                if (rooms == 0) // Show the answer to minimum number of rooms
                    Console.Write("\n  --> no room is needed");
                else
                {
                    Console.Write($"\n  --> a minimum number of {rooms}");
                    Console.Write((rooms > 1) ? " rooms are needed" : " room is needed");
                }

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("\n      (Enter --> chart)");

            Get_Key:
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Enter:
                        var chart = new ChartWindow(hourRooms);
                        Application.Run(chart);
                        break;
                    default: goto Get_Key;
                } // Show the chart for rooms used in every hour
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(new string(' ', 23));

            what_to_do:
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("\n\n  R: restart   |   ESC: exit\n\n");
                bool isRestart = false;
                while (true)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.R:
                            Console.Clear();
                            isRestart = true;
                            break;
                        case ConsoleKey.Escape:
                            Console.ResetColor();
                            return;
                    }
                    if (isRestart) break;
                } // Check if restart or exit the app
            }
        }
    }
}