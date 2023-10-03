using Crosslight.Core;

{
    using(var config = new Config())
    {
        var a = config.GetString("A");
        Console.WriteLine($"Value: {a}");

        var bRes = config.SetString("B", "B");
        var b = config.GetString("B");
        Console.WriteLine($"Value: {b}");

        var cRes = config.SetString("C", null);
        var c = config.GetString("C");
        Console.WriteLine($"Value: {c}");
    }
}
GC.Collect();
GC.WaitForPendingFinalizers();
