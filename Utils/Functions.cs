using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static System.Linq.Expressions.ExpressionType;
using ExpressionToString;

namespace Utils {
    public static class Functions {
        public static List<(string name, Expression expr)> ParseFields<TSource, TResult>(Expression<Func<TSource, TResult>> fieldsExpression, string language = "C#") {
            var prm = fieldsExpression.Parameters[0];
            var body = fieldsExpression.Body;
            switch (body) {
                case NewArrayExpression newArrayExpr when body.NodeType == NewArrayInit:
                    // an array with members
                    return newArrayExpr.Expressions.Select((x, index) => (getName(x), x)).ToList();
                case NewExpression newExpr:
                    if (body.Type.IsAnonymous()) {
                        // anonymous type
                        return newExpr.Constructor.GetParameters().Select(x => x.Name).Zip(newExpr.Arguments).ToList();
                    }
                    throw new InvalidOperationException("Unhandled expression type");
                case Expression _ when body.Type.IsScalar():
                    // scalar value -- single column
                    return new List<(string name, Expression expr)> {
                        (getName(body), body)
                    };

                // TODO handle formatted string -- $"{p.FirstName} is {(DateTime.Now-p.DateOfBirth):yy} years old

                default:
                    throw new InvalidOperationException("Unhandled expression type");
            }

            string getName(Expression expr) => expr.ToString(language);
        }
    }
}
