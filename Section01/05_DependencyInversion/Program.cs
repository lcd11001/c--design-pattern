namespace _05_DependencyInversion;

public enum Relationship
{
    Parent,
    Child,
    Sibling
}

public class Person
{
    public string Name;
    public DateTime DateOfBirth;
    // ....
}

public interface IRelationshipBrowser
{
    IEnumerable<Person> FindAllRelationshipOf(string name, Relationship relationship);
}

// low-level
public class Relationships : IRelationshipBrowser
{
    private List<(Person, Relationship, Person)> relations = new List<(Person, Relationship, Person)>();

    public void AddParentAndChild(Person parent, Person child)
    {
        relations.Add((parent, Relationship.Parent, child));
        relations.Add((child, Relationship.Child, parent));
    }

    // bad design due to can not change the way store relationship information
    public List<(Person, Relationship, Person)> Relations => relations;

    public IEnumerable<Person> FindAllRelationshipOf(string name, Relationship relationship)
    {
        // foreach (var r in this.relations.Where(x => x.Item1.Name == name && x.Item2 == relationship))
        // {
        //     yield return r.Item3;
        // }
        return this.relations
        .Where(x => x.Item1.Name == name && x.Item2 == relationship)
        .Select(r => r.Item3);
    }
}

// High-level
public class Research
{
    private List<(Person, Relationship, Person)> relations;
    public Research(Relationships relationships)
    {
        this.relations = relationships.Relations;
    }

    public void Report(string name, Relationship relationship, string description)
    {
        // bad design: access low-level data
        foreach (var r in this.relations.Where(x => x.Item1.Name == name && x.Item2 == relationship))
        {
            Console.WriteLine($"{name} has {description} called {r.Item3.Name}");
        }
    }
}

// high-level
public class NewResearch
{
    private IRelationshipBrowser browser;
    public NewResearch(IRelationshipBrowser browser)
    {
        this.browser = browser;
    }

    public IEnumerable<Person> Report(string name, Relationship relationship)
    {
        // good design: don't access low-level data
        return this.browser.FindAllRelationshipOf(name, relationship);
    }
}

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Dependency Inversion");
        Console.WriteLine("");

        var dad = new Person { Name = "LCD" };
        var mom = new Person { Name = "Duong" };
        var boy = new Person { Name = "Hecquyn" };
        var girl = new Person { Name = "Nice" };

        var relationships = new Relationships();
        relationships.AddParentAndChild(dad, boy);
        relationships.AddParentAndChild(dad, girl);

        // relationships.AddParentAndChild(mom, boy);
        relationships.AddParentAndChild(mom, girl);

        var r = new Research(relationships);
        r.Report(dad.Name, Relationship.Parent, "a child");

        Console.WriteLine("");
        r.Report(mom.Name, Relationship.Parent, "a child");

        Console.WriteLine("");
        r.Report(girl.Name, Relationship.Child, "parent");

        var nr = new NewResearch(relationships);

        Console.WriteLine("");
        Console.WriteLine($"All children of {dad.Name}:");
        var children = nr.Report(dad.Name, Relationship.Parent);
        foreach (var child in children)
        {
            Console.WriteLine($"  - {child.Name}");
        }

        Console.WriteLine("");
        Console.WriteLine($"All parent of {girl.Name}:");
        var parent = nr.Report(girl.Name, Relationship.Child);
        foreach (var p in parent)
        {
            Console.WriteLine($"  - {p.Name}");
        }
    }
}
