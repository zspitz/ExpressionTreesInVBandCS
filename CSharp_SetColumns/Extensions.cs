using System;
using System.Linq.Expressions;
using System.Windows.Controls;
using System.Windows.Data;
using static CSharp_SetColumns.Functions;
using Shared;

namespace CSharp_SetColumns {
    public static class Extensions {
        // We need the TResult type parameter so the compiler can recognize the second parameter as an expression tree
        public static void SetColumns<TSource, TResult>(this DataGrid dg, Expression<Func<TSource, TResult>> selector) {
            dg.Columns.Clear();

            var fields = ParseFields(selector);

            var visitor = new XamlBindingPathVisitor();

            foreach (var (name,expr) in fields) {
                visitor.Visit(expr);
                if (visitor.Last.Path == null) {
                    throw new InvalidOperationException("Unable to generate binding path from expression");
                }

                dg.Columns.Add(new DataGridTextColumn {
                    Header = name.IsNullOrWhitespace() ? visitor.Last.Path : name,
                    Binding = new Binding(visitor.Last.Path) {
                        StringFormat = visitor.Last.StringFormat
                    }
                });
            }
        }
    }
}