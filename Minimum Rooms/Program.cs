using Minimum_Rooms;
using System.Text.RegularExpressions;

#region Variables
int start, end, min, max, counter;
double rooms;
bool isMinSet, error;
string? classPeriod;
List<ClassPeriod> my_Classes = new();
#endregion

while (true)
{
    my_Classes.Clear();
    min = 0;
    max = 0;
    counter = 1;
    rooms = 0;
    isMinSet = false;
    error = false;
    Console.Title = "Minimum Class Rooms";
    Console.CursorVisible = false;
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.Write("  \"Please type in your class periods and double press enter when done\"");
    Console.ReadKey(); // Jump to receiving inputs
    Console.Write("\n");

    Console.CursorVisible = true;
    while (true)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
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

    for (int i = min; i < max; i++)
    {
        counter = 0;
        foreach (var x in my_Classes)
            if (i >= x.start && i < x.end)
                counter++;
        if (counter > rooms)
            rooms = counter + 0.2;
    } // Evaluate the maximum used time of the day

    Console.ForegroundColor = ConsoleColor.Blue;
    Console.Write($"\n  --> a minimum number of {Convert.ToInt32(rooms)} rooms are needed");

what_to_do:
    Console.ForegroundColor = ConsoleColor.Green;
    Console.Write("\n\n  R: restart   |   ESC: exit\n");
    bool isRestart = false;
    while (true) // Check if restart or exit the app
    {
        switch (Console.ReadKey().Key)
        {
            case ConsoleKey.R:
                Console.Clear();
                isRestart = true;
                break;
            case ConsoleKey.Escape:
                return;
            default:
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write(' ');
                Console.SetCursorPosition(0, Console.CursorTop);
                break;
        }
        if (isRestart) break;
    }
}