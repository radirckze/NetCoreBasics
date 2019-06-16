using System;
using System.Linq;
using System.Linq.Expressions;

//https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/lambda-expressions

//A Lambda expression is a block of code that is teated as an object and can be passed as an argument or returned
//from method call. Lambda expressions are code that can be represented as delegates, or epxression trees that
//compile into delegates. If they don't return a value they correspond to an Action delegate (1., Action<T1, T2,..>). 
//If they return a value they corespond to a function delegate (i.e., Func<T1, T2, ..., T.Result>).
//Primarily used for creating Tasks (i.e., Task.Run(...)), writing LINQ queries and creating expression trees.

//General format: <arguments> => <expression> (e.g., x => x * X;). => is the Lambda expression operator

namespace LambdaExpressions
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting lambda expression examples ...\n");
            //Lambda expressions can be respresented using Action delegates, function delegates or expressions
            int AddIntegersActionResult = 0;
            Action<int, int> AddInegersAction = (a,  b) => {AddIntegersActionResult = a + b;}; //Action delegate
            AddInegersAction(5,3);
            Console.WriteLine(String.Format("AddIntegersActionResult = {0}", AddIntegersActionResult));
            
            Func<int, int, int> AddNumbersFunction = (x, y) => x+y; //Function delegate
            Console.WriteLine("AddNumbersFunction(5,3) is: " + AddNumbersFunction(5,3));
            
            Expression<Func<int, int>> LinqSquareExpression = x=> x*x; //LINQ expression
            Console.WriteLine("LinqSquareExpression is: " + LinqSquareExpression);
            // using LinqSquareExpression
            int[] someNumbers = {2, 4, 6, 8};
            var squaredNumbers = someNumbers.Select(x => x*x);
            Console.WriteLine("Using LinqSquareExpression for {2,4,6,8} : " + string.Join(", ", squaredNumbers));

            //Expression Lambdas and Statement Labdas

            //Expression Lambda example:
            Func<int, int, int> ExpessionLambdaExample = (x, y) => x+y; 

            //Statement Lambda Example (i.e., statements are enclosed in {}):
            Action<int, int> StatementLambdaExample = (a,  b) => {AddIntegersActionResult = a + b;};

            // See Async Lamdas (no examples provided)

            // Lambda expressions and Tuples (Tupple support in C# 7+)
            Func<(int, int, int), (int, int, int), (int, int, int)> AddTuppleValues = (tuple1, tuple2) => (
                tuple1.Item1 + tuple2.Item1, tuple1.Item2 + tuple2.Item2, tuple1.Item3 + tuple2.Item3);
            var var1 = (1,2,3);
            var var2 = (4,5,6);
            var sumOfValues = AddTuppleValues(var1, var2);
            Console.WriteLine($"Lambda Expressions and Tuples - {var1} + {var2} = {sumOfValues}");

            //Using Lambdas with query operators (applied to each element in a list)
            int[] numbers = {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
            var evenNumbers = numbers.Where(x => x%2 == 0);
            Console.WriteLine($"Lambda as query operator. Even numbers = {string.Join(", ", evenNumbers)}");

            Console.WriteLine("Lambda expressions examples done! Enter any key to terminate: ");
            Console.ReadLine();
            
        }
    }
}
