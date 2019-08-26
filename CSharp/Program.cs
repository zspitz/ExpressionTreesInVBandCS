using System;
using System.Linq;
using static System.Linq.Expressions.Expression;
using System.Linq.Expressions;
using ExpressionToString;
using Shared;
using static Shared.Methods;
using Simple.OData.Client;
using System.Threading.Tasks;
using System.Collections.Generic;
using static System.Reflection.BindingFlags;
using System.Reflection;

namespace CSharp {
    class Program {
        static void Main(string[] args) {
            RunSample(() => Figure2_ExpressionsAsObjects());
            RunSample(() => Figure5_BlocksAssignmentsStatements());
            RunSample(() => Figure6_QueryableWhere());
            RunSample(() => Inline_GetMethod());
            RunSample(() => Figure7_SimpleODataClient());
        }

        /**
         * <summary>Shows the objects in the expression tree for n + 42 == 27, using object and collection initializers</summary>
         */
        public static void Figure2_ExpressionsAsObjects() {
            var n = Parameter(typeof(int), "n");
            var expr = Equal(
                Add(
                    n,
                    Constant(42)
                ),
                Constant(27)
            );
            Console.WriteLine(expr.ToString("Object notation"));
        }

        /**
         * <summary>
         * Builds the expression tree of a lambda containing code like the following:<br/><br/>
         * <code>
         * string msg = "Hello";<br/>
         * if (DateTime.Now.Hour > 18) { msg = "Good night"; }<br/>
         * Console.WriteLine(msg);<br/>
         * </code>
         * </summary>
         */
        public static void Figure5_BlocksAssignmentsStatements() {
            // string msg
            var msg = Parameter(typeof(string), "msg");

            // () => { ... }
            var expr = Lambda(

                // the lambda will contain multiple statements, so we need to wrap them in a Block call
                Block(

                    // msg = "Hello"
                    Assign(msg, Constant("Hello")),

                    // if (...) {...}
                    IfThen(

                        // DateTime.Now.Hour > 18
                        GreaterThan(
                            MakeMemberAccess(
                                MakeMemberAccess(
                                    null,
                                    typeof(DateTime).GetMember("Now").Single()
                                ),
                                typeof(DateTime).GetMember("Hour").Single()
                            ),
                            Constant(18)
                        ),

                        // msg = "Good night"
                        Assign(msg, Constant("Good night"))
                    ),

                    // Console.WriteLine(msg)
                    Call(
                        typeof(Console).GetMethod("WriteLine", new[] { typeof(string) }),
                        msg
                    )
                )
            );

            Console.WriteLine(expr.ToString("C#"));
        }

        /**
         * <summary>Shows an expression tree before and after the call to Queryable.Where</summary>
         */
        public static void Figure6_QueryableWhere() {
            Expression<Func<Person, bool>> expressionBeforeWhere = p => p.LastName.StartsWith("D");
            Console.WriteLine(expressionBeforeWhere.ToString("Textual tree"));
            Console.WriteLine();

            var personSource = new List<Person>().AsQueryable();
            var qry = personSource.Where(expressionBeforeWhere);
            var expressionAfterWhere = qry.GetType().GetProperty("Expression", NonPublic | Instance).GetValue(qry) as Expression;
            Console.WriteLine(expressionAfterWhere.ToString("Textual tree"));
        }

        /**
         * <summary>Use compiled expressions to target a specific MethodInfo, instead of reflection</summary>
         */
        public static void Inline_GetMethod() {
            MethodInfo GetMethod(Expression<Action> expr) => (expr.Body as MethodCallExpression)?.Method;

            var mi = GetMethod(() => Console.WriteLine());

            // Without generics, this is simple enoguh to do with reflection:
            Console.WriteLine(mi == 
                typeof(Console).GetMethod("WriteLine", new Type[] { })
            );

            // But once generics are involved, it becomes much more complicated to get hold of a specific closed overload using reflection.
            // You have to find the method whose name is "Where" and whose second parameter's type is Expression<Func<T, bool>>, and not Expression<Func<T, int, bool>>
            // Related: https://github.com/dotnet/corefx/issues/16567 
            mi = GetMethod(() => ((IQueryable<Person>)null).Where(x => true));
            Console.WriteLine(mi ==
                typeof(Queryable).GetMethods().Single(x => {
                    if (x.Name != "Where") { return false; }
                    return x.GetParameters()[1].ParameterType.GetGenericArguments().Single().GetGenericArguments().Length == 2;
                }).MakeGenericMethod(typeof(Person))
            );
        }

        /**
         * <summary>Constructing OData web requests using expression trees and the Simple.OData.Client library</summary>
         */
        public static void Figure7_SimpleODataClient() {
            var client = new ODataClient("https://services.odata.org/v4/TripPinServiceRW/");

            var command = client.For<Person>()
                .Filter(x => x.Trips.Any(y => y.Budget > 3000))
                .Top(2)
                .Select(x => new { x.FirstName, x.LastName });

            var commandText = Task.Run(() => command.GetCommandTextAsync()).Result;
            // Prints the generated relative URL
            Console.WriteLine(commandText);
            Console.WriteLine();

            var people = Task.Run(() => command.FindEntriesAsync()).Result;
            foreach (var p in people) {
                p.Write();
            }
        }


    }
}
