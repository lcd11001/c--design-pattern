namespace _01_SingleResponsibility;

using System;
using System.Diagnostics;

class Journal
{
    private readonly List<string> entries = new List<string>();
    private static int count = 0;

    public int AddEntry(string text)
    {
        entries.Add($"{++count}: {text}");
        return count;
    }

    public void RemoveEntry(int index)
    {
        entries.RemoveAt(index);
    }

    public override string ToString()
    {
        return string.Join(Environment.NewLine, entries);
    }
}

class Persistence<T>
{
    public void SaveToFile(T content, string fileName, bool overwrite = false)
    {
        if (overwrite || !File.Exists(fileName))
        {
            File.WriteAllText(fileName, content!.ToString());
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Single Responsibility");
        Console.WriteLine("");

        var j = new Journal();
        j.AddEntry("I cried today");
        j.AddEntry("I ate a bug");
        Console.WriteLine(j);

        var p = new Persistence<Journal>();
        var fileName = @"journal.txt";
        p.SaveToFile(j, fileName, true);

        using (var pr = new Process())
        {
            pr.StartInfo = new ProcessStartInfo()
            {
                FileName = fileName,
                UseShellExecute = true
            };
            pr.Start();
        }
    }
}
