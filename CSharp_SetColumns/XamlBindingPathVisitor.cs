using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Shared;

namespace CSharp_SetColumns {
    public class XamlBindingPathVisitor : ExpressionVisitor {
        private static readonly HashSet<MethodInfo> stringFormats = typeof(string).GetMethods()
            .Where(x => {
                if (x.Name != "Format") { return false; }
                var parameters = x.GetParameters();
                return parameters.Length == 2 && parameters.First().ParameterType == typeof(string);
            })
            .ToHashSet();

        private bool isInitialized;
        private (string path, string stringFormat) current;
        public (string Path, string StringFormat) Last;

        private bool isBindablePath = true;

        public override Expression Visit(Expression node) {
            if (node is LambdaExpression lexpr) { return Visit(lexpr.Body); }

            // https://stackoverflow.com/a/34794447/111794
            bool firstVisit = !isInitialized;
            if (firstVisit) {
                current.path = "";
                isInitialized = true;
            }

            // A MethodCallExpression representing a static method call will have its Expression property set to null
            // Also, String.Format may be taking a ConstantExpression
            if (node != null && !(node is ConstantExpression)) { 
                isBindablePath =
                    node is MemberExpression ||
                    node is ParameterExpression || 
                    (node is UnaryExpression && node.NodeType.In(ExpressionType.Convert, ExpressionType.ConvertChecked));
                // TODO how can we separate between any ParameterExpression and the ParameterExpression defined by the current lambda?

                if (firstVisit) {
                    isBindablePath = isBindablePath ||
                        (node is MethodCallExpression mcexpr && stringFormats.Contains(mcexpr.Method));
                }
            }

            // https://stackoverflow.com/questions/36737463/stop-traversal-with-expressionvisitor
            if (!isBindablePath && !firstVisit) { return node; }

            var ret = base.Visit(node);

            if (firstVisit) {
                if (isBindablePath) {
                    Last = (current.path.Substring(1), current.stringFormat);
                } else {
                    Last = (null, null);
                }
                current = (null, null);
                isBindablePath = true;
                isInitialized = false;
            }
            return ret;
        }

        protected override Expression VisitMember(MemberExpression node) {
            var ret = base.VisitMember(node); ;
            current.path += "." + node.Member.Name;
            return ret;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node) {
            var ret = base.VisitMethodCall(node);

            // We're assuming this is a call to string.Format; all others should be excluded in the Visit override

            if (isBindablePath && node.Arguments.Count==2 && node.Arguments[0] is ConstantExpression cexpr) {
                current.stringFormat = cexpr.Value as string;
            } else {
                isBindablePath = false;
            }

            return ret;
        }
    }
}
