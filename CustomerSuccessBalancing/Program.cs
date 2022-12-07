// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

Dictionary<int, int> result = new Dictionary<int, int>();

result[5] = 5;
result[6] = 6;

Console.WriteLine(result.GetValueOrDefault(1));
Console.WriteLine(result.GetValueOrDefault(5));
Console.WriteLine(result.GetValueOrDefault(6));
Console.WriteLine(result.GetValueOrDefault(70));