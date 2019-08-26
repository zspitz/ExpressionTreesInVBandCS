using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Shared;
using static System.Linq.Expressions.Expression;

namespace CSharp_SetColumns {
    public class Functions {
        /// <summary>
        /// Parses an expression into names and subexpressions.<br/>
        /// The following expressions are handled:<br/>
        /// * an array; the individual elements are the subexpressions<br/>
        /// * an anonymous type; the initializers are the subexpressions, and the names are the initialized members<br/>
        /// * a scalar-returning expression<br/>
        /// * the parameter itself; creates a property name / subexpression pair for each property
        /// If the expression is an anonymous type, the corresponding name for each expression is the initialized member. For other expressions, the name is a string rendering of the expression, based on the language parameter.
        /// </summary>
        /// <param name="language">Can be <code>"C#"</code> or <code>"Visual Basic"</code></param>
        public static List<(string name, Expression expr)> ParseFields<TSource, TResult>(Expression<Func<TSource, TResult>> fieldsExpression, string language = "C#") {
            var formatter =
                language == "Visual Basic" ? language :
                "C#";

            var body = fieldsExpression.Body;
            switch (body) {

                // an array with elements
                case NewArrayExpression newArrayExpr when body.NodeType == ExpressionType.NewArrayInit:
                    return newArrayExpr.Expressions.Select(x => ("", x)).ToList();

                // anonymous type
                case NewExpression newExpr when newExpr.Type.IsAnonymous():
                    return newExpr.Constructor.GetParameters().Select(x => x.Name).Zip(newExpr.Arguments).ToList();

                // the parameter itself
                case Expression _ when body == fieldsExpression.Parameters[0]:
                    return body.Type.GetProperties().Select(x => (x.Name, PropertyOrField(body, x.Name) as Expression)).ToList();

                /// single property
                case Expression _ when body.Type.IsScalar():
                    return new List<(string name, Expression expr)> {
                        ("", body)
                    };

                default:
                    throw new ArgumentException("Unhandled expression.");
            }
        }
    }
}
