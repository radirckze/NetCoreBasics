using System;

// A nice / concise histroy of delegates 
// https://itnext.io/delegates-anonymous-methods-and-lambda-expressions-5ea4e56bbd05

namespace Delegates
{

    delegate int BinaryOperation(int first, int second);

    delegate void BinaryOperationPrintRes(int first, int second);

    public class DelegateEvolution
    {

        public void Run()
        {

            Console.WriteLine("C# delegates - understanding the evolution of delegates");

            Console.WriteLine("C# 1.0 - basic delegates ...");
            BinaryOperation sumInts = AddOperation;
            Console.WriteLine("The result of 3 + 2 is {0}\n", sumInts(3,2).ToString());

            Console.WriteLine("Multicast delegation example ...");
            BinaryOperationPrintRes binaryOperationPR = AddOperationPrintRes;
            binaryOperationPR += SubOperationPrintRes;
            binaryOperationPR(3,2);

            Console.WriteLine("\nC# 2.0) - Anonymous methods ...");
            // Anonymous methods are inline, so do not require a separate method to be defined. 

            BinaryOperation sumIntsAnonymous = delegate (int first, int second)
            {
                return first + second;
            };
            Console.WriteLine("The result of 3 + 2 is {0}", sumIntsAnonymous(3,2).ToString());

            Console.WriteLine("\nC# 3.0 - Lambda Expressions ...");

            BinaryOperationPrintRes sumLambdaOpPrintRes = (int first, int second) => 
            {
                Console.WriteLine("The result of {0} + {1} is: {2}", first, second,  first+second);
            };
            sumLambdaOpPrintRes(3,5);
            
            Console.WriteLine();
            BinaryOperation sumLambdaOpReturnRes = (first, second) => 
            {
                return first + second;
            };
            Console.WriteLine("The result of 3 + 5 (using Lambda operation that returns a result) is: {0}", 
                sumLambdaOpReturnRes(3,5)); 

            Console.WriteLine("\nC# 3.0 - Action and Func Delegates ...");

            // Note, C# 3.0 introduced built in generic delegate types Func and Action so we don't need to
            // declare custom delegates. These types take upto 16 input parameters. 
            // Note, Action returns void and Func returns a value

             Action<int, int> sumPrintResAction = (int first, int second) => 
            {
                Console.WriteLine("The result of {0} + {1} (using Action<int, int>) is: {2}", first, second,  first+second);
            };
            sumPrintResAction(4,4);

            Console.WriteLine();
            Func<int, int, int> sumReturnResFunc = (first, second) => 
            {
                return first + second;
            };
            Console.WriteLine("The result of 3 + 5 (using Func<int, int, int>) is: {0}", 
                sumReturnResFunc(4,5)); 
   

            Console.WriteLine("\nDelegate Evolution sample - done!");

        }

        public int AddOperation(int first, int second)
        {
            return first + second;
        }

        public int SubOperation(int first, int second)
        {
            return first - second;
        }

        public void AddOperationPrintRes(int first, int second)
        {
            Console.WriteLine("The result of {0} + {1} is {2}", first, second, first+second);
        }

        public void SubOperationPrintRes(int first, int second)
        {
            Console.WriteLine("The result of {0} - {1} is {2}", first, second, first-second);
        }


    }

}