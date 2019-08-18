using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_SetColumns {
    public class XamlBindingPathVisitor : ExpressionVisitor {
        private string currenPath;
        private bool isBindablePath = true;

        public string LastPath { get; private set; }

        public override Expression Visit(Expression node) {
            // https://stackoverflow.com/a/34794447/111794
            bool firstVisit = currenPath == null;
            if (firstVisit) {
                currenPath= "";
            }

            isBindablePath = node is MemberExpression || node is LambdaExpression || node is ParameterExpression;

            // https://stackoverflow.com/questions/36737463/stop-traversal-with-expressionvisitor
            if (!isBindablePath && !firstVisit) { return node; }

            var ret = base.Visit(node);

            if (firstVisit) {
                if (isBindablePath) {
                    LastPath = currenPath.Substring(1);
                } else {
                    LastPath = null;
                }
                currenPath = null;
                isBindablePath = true;
            }
            return ret;
        }

        protected override Expression VisitMember(MemberExpression node) {
            var ret = base.VisitMember(node); ;
            currenPath += "." + node.Member.Name;
            return ret;
        }

        public override string ToString() => LastPath;
    }
}
