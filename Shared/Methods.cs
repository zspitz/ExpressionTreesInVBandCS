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
    }
}
