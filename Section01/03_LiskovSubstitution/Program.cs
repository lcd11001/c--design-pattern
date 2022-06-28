namespace _03_LiskovSubstitution;

public class Rectangle
{
    public Rectangle()
    {
    }

    public Rectangle(int width, int height)
    {
        Width = width;
        Height = height;
    }

    // public int Width { get; set; }
    // public int Height { get; set; }

    public virtual int Width { get; set; }
    public virtual int Height { get; set; }

    public override string ToString()
    {
        return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
    }
}

public class Square : Rectangle
{
    // public new int Width
    public override int Width
    {
        set { base.Width = base.Height = value; }
    }

    // public new int Height
    public override int Height
    {
        set { base.Width = base.Height = value; }
    }
}


class Program
{
    static public int Area(Rectangle r) => r.Width * r.Height;

    static void Main(string[] args)
    {
        Console.WriteLine("Liskov Substitution");
        Console.WriteLine("");

        Rectangle rect = new Rectangle(10, 20);
        Console.WriteLine($"{rect} has erea {Area(rect)}");

        // Square square = new Square();
        Rectangle square = new Square();
        square.Width = 5;
        Console.WriteLine($"{square} has erea {Area(square)}");
    }
}
