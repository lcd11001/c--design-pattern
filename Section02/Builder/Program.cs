using System.Text;

namespace Builder;

public class CodeField
{
    public string Name, Type;

    public CodeField()
    {
    }

    public CodeField(string name, string type1)
    {
        Name = name;
        Type = type1;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.Append($"public {this.Type} {this.Name};");

        return sb.ToString();
    }
}

public class Code
{
    public string ClassName;
    public List<CodeField> Fields = new List<CodeField>();
    private const int FIELD_INDENT = 2;

    public Code()
    {
    }

    public void Add(string name, string type)
    {
        Fields.Add(new CodeField(name, type));
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();

        sb.AppendLine("public class " + this.ClassName);
        sb.AppendLine("{");

        var indent = new string(' ', FIELD_INDENT);
        foreach (var field in Fields)
        {
            sb.AppendLine($"{indent}{field.ToString()}");
        }


        sb.AppendLine("}");

        return sb.ToString();
    }
}

public class CodeBuilder
{
    private Code code = new Code();

    public CodeBuilder(string className)
    {
        this.code.ClassName = className;
    }

    public CodeBuilder AddField(string fieldName, string fieldType)
    {
        this.code.Add(fieldName, fieldType);
        return this;
    }

    public override string ToString()
    {
        return this.code.ToString();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Builder");
        Console.WriteLine("");

        var cb = new CodeBuilder("Person")
        .AddField("Name", "string")
        .AddField("Age", "int");
        Console.WriteLine(cb);
    }
}
