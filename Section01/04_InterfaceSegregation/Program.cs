namespace _04_InterfaceSegregation;

public class Document
{

}

// bad design: don't put too much into an interface; split into seperate interfaces
public interface IMachine
{
    void Print(Document d);
    void Scan(Document d);
    void Fax(Document d);
}

public class MultiFunctionPrinter : IMachine
{
    public void Fax(Document d)
    {
        Console.WriteLine("Fax");
    }

    public void Print(Document d)
    {
        Console.WriteLine("Print");
    }

    public void Scan(Document d)
    {
        Console.WriteLine("Scan");
    }
}

public class OldPrinter1 : IMachine
{
    public void Fax(Document d)
    {
        // Console.WriteLine("Fax not working");
    }

    public void Print(Document d)
    {
        Console.WriteLine("Print");
    }

    public void Scan(Document d)
    {
        // Console.WriteLine("Scan not working");
    }
}

public interface IPrinter
{
    void Print(Document d);
}

public interface IScan
{
    void Scan(Document d);
}

public interface IFax
{
    void Fax(Document d);
}

public class OldPrinter2 : IPrinter, IScan
{
    public void Print(Document d)
    {
        Console.WriteLine("Print");
    }

    public void Scan(Document d)
    {
        Console.WriteLine("Scan");
    }
}

public interface IMultiFunctionDevice: IPrinter, IScan
{

}

public class OldPrinter3 : IMultiFunctionDevice
{
    IPrinter iPrinter;
    IScan iScan;

    public OldPrinter3(IPrinter iPrinter, IScan iScan)
    {
        this.iPrinter = iPrinter;
        this.iScan = iScan;
    }

    // delegate
    public void Print(Document d)
    {
        iPrinter.Print(d);
    }

    // delegate
    public void Scan(Document d)
    {
        iScan.Scan(d);
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Interface Segregation");
        Console.WriteLine("");


    }
}
