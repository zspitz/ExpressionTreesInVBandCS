using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using static Utils.Functions;
using Utils;
using System.Windows.Data;

namespace CSharp_SetColumns {
    public static class Extensions {
        // We need the TResult parameter so the compiler can recognize the second parameter as an expression tree
        public static void SetColumns<TSource, TResult>(this DataGrid dg, Expression<Func<TSource, TResult>> selector) {
            dg.Columns.Clear();

            // call parse fields
            var fields = ParseFields(selector);

            var prm = selector.Parameters[0];

            var visitor = new XamlBindingPathVisitor();

            // TODO validate expressions
            // verify that each expression can be translated into a binding
            // it has to be a MemberAccessExpression, of possibly multiple levels, returning a scalar value
            // the innermost item in the MemberAccessExpression has to be the parameter of type TSource of the original expression
            foreach (var (name,expr) in fields) {
                if (!expr.Type.IsScalar()) {
                    throw new InvalidOperationException("Expression's return type is non-bindable");
                }

                visitor.Visit(expr);
                if (visitor.LastPath == null) {
                    throw new InvalidOperationException("Unable to generate binding path from expression");
                }

                dg.Columns.Add(new DataGridTextColumn {
                    Header = name,
                    Binding = new Binding(visitor.LastPath)
                });
            }
        }
    }
}