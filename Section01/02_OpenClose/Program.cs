namespace _02_OpenClose;

public enum Color
{
    Red, Green, Blue
}

public enum Size
{
    Small, Medium, Large, Huge
}

public class Product
{
    public string name;
    public Color color;
    public Size size;

    public Product(string name, Color color, Size size)
    {
        this.name = name;
        this.color = color;
        this.size = size;
    }
}

public class ProductFilter
{
    public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
    {
        foreach (var p in products)
        {
            if (p.size == size)
            {
                yield return p;
            }
        }
    }

    public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
    {
        foreach (var p in products)
        {
            if (p.color == color)
            {
                yield return p;
            }
        }
    }

    public IEnumerable<Product> FilterByColorAndSize(IEnumerable<Product> products, Color color, Size size)
    {
        foreach (var p in products)
        {
            if (p.color == color && p.size == size)
            {
                yield return p;
            }
        }
    }
}

public interface ISpecification<T>
{
    bool IsSatisfied(T t);
}

public interface IFilter<T>
{
    IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
}

public class ColorSpecification : ISpecification<Product>
{
    private Color color;

    public ColorSpecification(Color color)
    {
        this.color = color;
    }

    public bool IsSatisfied(Product p)
    {
        return p.color == this.color;
    }
}

public class SizeSpecification : ISpecification<Product>
{
    private Size size;

    public SizeSpecification(Size size)
    {
        this.size = size;
    }

    public bool IsSatisfied(Product p)
    {
        return p.size == this.size;
    }
}

public class MultiSpecification<T> : ISpecification<T>
{
    private ISpecification<T>[] specs;
    public MultiSpecification(params ISpecification<T>[] specs)
    {
        this.specs = specs;
    }

    public bool IsSatisfied(T t)
    {
        for (int i = 0, len = this.specs.Length; i < len; i++)
        {
            if (!this.specs[i].IsSatisfied(t))
            {
                return false;
            }
        }
        return true;
    }
}

public class BetterFilter : IFilter<Product>
{
    public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
    {
        foreach (var item in items)
        {
            if (spec.IsSatisfied(item))
            {
                yield return item;
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Open Close");

        var apple = new Product("apple", Color.Green, Size.Small);
        var tree = new Product("tree", Color.Green, Size.Large);
        var house = new Product("house", Color.Blue, Size.Large);

        Product[] products = { apple, tree, house };

        var pf = new ProductFilter();
        Console.WriteLine("");

        Console.WriteLine("Small products (old): ");
        var filterBySize = pf.FilterBySize(products, Size.Small);
        foreach (var item in filterBySize)
        {
            Console.WriteLine($"  - {item.name}");
        }

        Console.WriteLine("Green products (old): ");
        var filterByColor = pf.FilterByColor(products, Color.Green);
        foreach (var item in filterByColor)
        {
            Console.WriteLine($"  - {item.name}");
        }

        Console.WriteLine("Blue + Large products (old): ");
        var filterByColorAndSize = pf.FilterByColorAndSize(products, Color.Blue, Size.Large);
        foreach (var item in filterByColorAndSize)
        {
            Console.WriteLine($"  - {item.name}");
        }

        var bf = new BetterFilter();
        Console.WriteLine("");

        Console.WriteLine("Small products (new): ");
        var newfilterBySize = bf.Filter(products, new SizeSpecification(Size.Small));
        foreach (var item in newfilterBySize)
        {
            Console.WriteLine($"  - {item.name}");
        }

        Console.WriteLine("Green products (new): ");
        var newFilterByColor = bf.Filter(products, new ColorSpecification(Color.Green));
        foreach (var item in newFilterByColor)
        {
            Console.WriteLine($"  - {item.name}");
        }

        Console.WriteLine("Blue + Large products (new): ");
        var newFilterByColorAndSize = bf.Filter(products, new MultiSpecification<Product>(
            new ColorSpecification(Color.Blue),
            new SizeSpecification(Size.Large)
        ));
        foreach (var item in newFilterByColorAndSize)
        {
            Console.WriteLine($"  - {item.name}");
        }
    }
}
