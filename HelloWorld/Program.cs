namespace HelloWorld;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        #if DEBUG
        Console.WriteLine("DEBUG mode");
        #else
        Console.WriteLine("RELEASE mode");
        #endif
    }
}
