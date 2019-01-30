using System;

// Delegates, functions, actions and lambdas.
// https://blogs.msdn.microsoft.com/brunoterkaly/2012/03/02/c-delegates-actions-funcs-lambdaskeeping-it-super-simple/

namespace Delegates
{

    // Delegate is a pointer to a method that can be passed to another method.
    // Its the genral form.
    public delegate string changeCaps(string input);

    class Program
    {

        static void Main(string[] args)
        {

            // Delegate example
            DelegateExample delegateEg = new DelegateExample();
            changeCaps toUpper = new changeCaps(delegateEg.ToUpperMethod);
            changeCaps toLower = new changeCaps(delegateEg.ToLowerMethod);

            string testString = "Text in MiXED case";
            Console.WriteLine(String.Format("Oroginal string: {0}", testString));
            Console.WriteLine(String.Format("Afer toUpper Delegate: {0}", toUpper(testString)));
            Console.WriteLine(String.Format("Afer toLower Delegate: {0}", toLower(testString)));

            // Func<...> is similar. Difference is in the way you declare it. The last arg is the reutnr type
            Func<string, string> toUpperFunc = str => str.ToUpper(); 
            //Func<string, string> toUpperFunc = delegateEg.ToUpperMethod; // same result as above
            Console.WriteLine(String.Format("After toUpper Func: {0}", toUpperFunc(testString)));

            // An action is a delegate with no return type
            Action<string, string> concatAndPrint = (str1, str2) => 
            {
                Console.WriteLine(String.Format("Concat string Action result: {0}{1}",str1, str2 ));
            };
            Action<string, string> concatAndPrintToo = new Action<string, string>(delegateEg.ConcatAndPrint);

            concatAndPrint("FirstString", "SecondString");


        }

        public class DelegateExample
        {
            public string ToUpperMethod (string anyString)
            {
                return anyString.ToUpper();
            }

            public string ToLowerMethod(string anyString)
            {
                return anyString.ToLower();
            }

            public void ConcatAndPrint(string str1, string str2)
            {
                Console.WriteLine(String.Format("Concated string: {0}{1}",str1, str2 ));
            }
        }


    }

}
