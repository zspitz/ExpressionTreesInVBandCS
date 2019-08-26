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

        /// <summary>This is a workaround for Visual Basic, which currently doesn't support pattern matching. Return False if the match has failed; if the action returns True or null, the match is considered to have succeeded.</summary>
        public static bool ExecIfType<T>(object toTest, Func<T, bool?> action) {
            if (!(toTest is T matched)) { return false; }
            var ret = action(matched);
            if (ret.HasValue && !ret.Value) { return false; }
            return true;
        }
    }
}
