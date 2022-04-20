using HashingText;

Console.WriteLine("Hello, World!");
string input = Console.ReadLine();

string output = HashUtil.Compute(input);
Console.WriteLine("Hashed:" + output);