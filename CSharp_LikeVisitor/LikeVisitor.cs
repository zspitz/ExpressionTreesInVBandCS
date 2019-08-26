using Microsoft.VisualBasic.CompilerServices;
using Shared;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Reflection;
using static System.Linq.Expressions.Expression;

namespace CSharp_LikeVisitor {
    public class LikeVisitor : ExpressionVisitor {
        static readonly MethodInfo[] likeMethods = new[] {
            typeof(LikeOperator).GetMethod("LikeString"),
            typeof(LikeOperator).GetMethod("LikeObject")
        };

        static readonly MethodInfo dbfunctionsLike = typeof(DbFunctions).GetMethod("Like", new[] { typeof(string), typeof(string) });

        protected override Expression VisitMethodCall(MethodCallExpression node) {
            // Is this node using the LikeString or LikeObject method? If not, leave it alone.
            if (node.Method.NotIn(likeMethods)) { return base.VisitMethodCall(node); }

            var argExpression = node.Arguments[1];

            // Visual Basic inserts a conversion node whenever the types don't exactly match, but rather one inherits from  / implements the other
            // This will happen here if the LikeObject method -- which takes an object as its' second parameter -- is used.
            var (hasConversion, patternExpression) = argExpression.SansDerivedConvert();

            // We can only map the syntax from VB Like to SQL Like if the pattern is a constant expressions
            // If the pattern is not a string, say a number or date, we're not going to replace
            if (patternExpression is ConstantExpression cexpr && patternExpression.Type == typeof(string)) {
                var oldPattern = (string)cexpr.Value;
                var newPattern = oldPattern.Replace("*", "%");

                // TODO implement full replacement here

                // ? -- any single character
                // * -- Zero or more characters
                // # -- Any sngle digit
                // [charlist] -- Any single character in charlist
                // [!charlist] -- Any single character not in charlist
                // [a-z] range of characters
                // hyphen out of range matches itself
                // special characters -- wrap in [] to match ("]" can't be used within a group, only outside a group)
                // digraph handling?



                patternExpression = Constant(newPattern);
                argExpression =
                    hasConversion ? Convert(patternExpression, argExpression.Type) :
                    patternExpression;
            }

            return Call(
                dbfunctionsLike,
                Visit(node.Arguments[0]),
                Visit(argExpression)
            );
        }
    }
}
