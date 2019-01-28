using System;

// https://blogs.msdn.microsoft.com/brunoterkaly/2012/03/02/c-delegates-actions-funcs-lambdaskeeping-it-super-simple/

namespace Delegates
{

    public delegate string changeCaps(string input);

    class Program
    {

        static void Main(string[] args)
        {
            DelegateExample delegateEg = new DelegateExample();

            changeCaps toUpper = new changeCaps(delegateEg.ToUpperMethod);

            changeCaps toLower = new changeCaps(delegateEg.ToLowerMethod);

            string testString = "Text in MiXED case";
            Console.WriteLine(String.Format("Oroginal string: {0}", testString));
            Console.WriteLine(String.Format("Afer toUpper: {0}", toUpper(testString)));
            Console.WriteLine(String.Format("Afer toLower: {0}", toLower(testString)));
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
        }


    }

}
