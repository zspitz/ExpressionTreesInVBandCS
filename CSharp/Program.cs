using System;
using System.Linq;
using static System.Linq.Expressions.Expression;
using System.Linq.Expressions;
using ExpressionToString;
using Utils;
using static Utils.Methods;
using Simple.OData.Client;
using System.Threading.Tasks;

namespace CSharp {
    class Program {
        static void Main(string[] args) {
            SimpleODataClient();
            DrawLine();

            HourMessage();
            DrawLine();
        }


        /**
         * <summary>
         * Constructing OData web requests using expression trees and the Simple.OData.Client library
         * </summary>
         */
        public static void SimpleODataClient() {
            var client = new ODataClient("https://services.odata.org/v4/TripPinServiceRW/");
            
            var command = client.For<Person>()
                .Filter(x => x.Trips.Any(y => y.Budget > 3000))
                .Top(2)
                .Select(x => new { x.FirstName, x.LastName });

            var commandText = Task.Run(() => command.GetCommandTextAsync()).Result;
            // generated relative URL
            Console.WriteLine(commandText);
            Console.WriteLine();

            // results
            var people = Task.Run(() => command.FindEntriesAsync()).Result;
            foreach (var p in people) {
                p.Write();
            }
        }



        /**
         * <summary>
         * Builds the expression tree of a lambda with the following code<br/><br/>
         * <code>
         * string msg = "Hello";<br/>
         * if (DateTime.Now.Hour > 18) { msg = "Good night"; }<br/>
         * Console.WriteLine(msg);<br/>
         * </code>
         * </summary>
         */
        public static void HourMessage() {

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


    }
}
