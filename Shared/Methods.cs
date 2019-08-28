using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shared {
    public static class Methods {
        public static void RunSample(Expression<Action> expr) {
            var body = expr.Body;
            if (!(body is MethodCallExpression mexpr)) {
                throw new ArgumentException("Expression is something other than a method call.");
            }
            var name = mexpr.Method.Name;
            Console.WriteLine();
            Console.WriteLine($"---- {name} {new string('-', 50)}");
            Console.WriteLine();
            expr.Compile().Invoke();
            Console.WriteLine();
            Console.WriteLine("Press any key to proceed...");
            Console.ReadKey(true);
        }

        /// <summary>
        /// This is a workaround for Visual Basic, which currently doesn't support pattern matching.<br/>
        /// Returns True if the match is successful, and False if not.<br/>
        /// The match is considered successful only if the test object is of type T, and the passed-in delegate returns True or Nothing.
        /// </summary>
        public static bool ExecIfType<T>(object toTest, Func<T, bool?> fn) {
            if (!(toTest is T matched)) { return false; }
            var ret = fn(matched);
            if (ret.HasValue && !ret.Value) { return false; }
            return true;
        }
    }
}
