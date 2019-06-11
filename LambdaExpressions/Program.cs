using System;
using System.Linq;
using System.Linq.Expressions;

//https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/statements-expressions-operators/lambda-expressions

//A Lambda expression is a block of code that is teated as an object and can be passed as an argument or returned as
//from method call. Lambda expressions are code that can be represented as delegates, or epxression threes that
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
            //used in Action delegates and function delegates
            Action<string> PrintMessage = msg => Console.WriteLine(msg); //Action delegate
            PrintMessage("This message was printed using PrintMessage Action");
            
            Func<int, int, int> AddNumbers = (x, y) => x+y; //Function delegate
            Console.WriteLine("AddNumbers(5,3) is: " + AddNumbers(5,3));
            
            Expression<Func<int, int>> LinqSquareExpression = x=> x*x; //LINQ expression
            Console.WriteLine("\nAddNumbersExpression is: " + LinqSquareExpression);
            // using LinqSquareExpression
            int[] someNumbers = {2, 4, 6, 8};
            var squaredNumbers = someNumbers.Select(x => x*x);
            Console.WriteLine("Squared numbers are: " + string.Join(", ", squaredNumbers));

            Console.WriteLine("Lambda expressions examples done! Enter any key to terminate: ");
            Console.ReadLine();
            
        }
    }
}
