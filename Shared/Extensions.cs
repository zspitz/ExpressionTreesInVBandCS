﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Shared {
    public static class Extensions {
        /// <summary>Enables using a collection initializer with a List&lt;Person&gt;</summary>
        public static void Add(this List<Person> lst, int id, string firstname, string lastname, string email, string birthdate) => lst.Add(new Person {
            ID = id,
            LastName = lastname,
            FirstName = firstname,
            Email = email,
            DateOfBirth = DateTime.Parse(birthdate)
        });

        public static bool HasAttribute<TAttribute>(this MemberInfo mi, bool inherit = false) where TAttribute : Attribute =>
            mi.GetCustomAttributes(typeof(TAttribute), inherit).Any();

        public static bool IsAnonymous(this Type type) =>
            type.HasAttribute<CompilerGeneratedAttribute>() && type.Name.Contains("Anonymous") &&
            (type.Name.Contains("<>") || type.Name.Contains("VB$"));

        /// <summary>If the type is Nullable&lt;T&gt;, returns a System.Type representing T. Otherwise, returns the original type.</summary>
        public static Type UnderlyingIfNullable(this Type type) => Nullable.GetUnderlyingType(type) ?? type;

        /// <summary>Is primitive, System.DateTime, System.TimeSpan, System.String?</summary>
        public static bool IsScalar(this Type t) {
            t = t.UnderlyingIfNullable();
            return t.IsPrimitive || t == typeof(string) || t == typeof(DateTime) || t == typeof(TimeSpan);
        }

        /// <summary>Produces a value tuple for each pair of corresponding elements in two sequences.</summary>
        public static IEnumerable<(TFirst, TSecond)> Zip<TFirst, TSecond>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second) => first.Zip(second, (x, y) => (x, y));

        public static bool IsNullOrWhitespace(this string s) => string.IsNullOrWhiteSpace(s);

        /// <summary>Returns True if the specified value is in the IEnumerable&lt;T&gt;</summary>
        public static bool In<T>(this T val, IEnumerable<T> vals) => vals.Contains(val);

        /// <summary>Returns True if the specified value matches any of the passed-in values</summary>
        public static bool In<T>(this T val, params T[] vals) => vals.Contains(val);

        /// <summary>Returns True if the specified value doesn't match any of the passed-in values</summary>
        public static bool NotIn<T>(this T val, params T[] vals) => !vals.Contains(val);

        /// <summary>Visual Basic wraps a conversion around an innder node, when an inner node type inherits from / implements the expected node type.<br />This function returns a tuple of True and the inner node if that is the case; otherwise it returns False and the original node</summary>
        // For example. see https://github.com/simple-odata-client/Simple.OData.Client/issues/638
        public static (bool unwrapped, Expression final) SansDerivedConvert(this Expression expr) {
            switch (expr) {
                case UnaryExpression uexpr when expr.NodeType == ExpressionType.Convert && expr.Type.IsAssignableFrom(uexpr.Operand.Type):
                    return (true, uexpr.Operand);
            }
            return (false, expr);
        }

        /// <summary>VB.NET currently doesn't support tuple unpacking. This is a workaround, and should only be used from VB.NET</summary>
        public static void Unpack<T, T1>(this ValueTuple<T, T1> t, ref T item1, ref T1 item2) => (item1, item2) = t;

        /// <summary>VB.NET currently doesn't support tuple unpacking. This replaces tuple unpacking in a For Each loop, and should only be used from VB.NET</summary>
        public static void ForEachT<T1, T2>(this IEnumerable<(T1, T2)> src, Action<T1, T2> action) {
            foreach (var (item1, item2) in src) {
                action(item1, item2);
            }
        }

        public static void Add<T1, T2>(this ICollection<(T1, T2)> lst, T1 t1, T2 t2) => lst.Add((t1, t2));

        public static bool All<T1, T2>(this IEnumerable<(T1, T2)> src, Func<T1, T2, bool> predicate) => src.All(x => predicate(x.Item1, x.Item2));

        public static string Joined<T>(this IEnumerable<T> source, string delimiter = ",", Func<T, string> selector = null) {
            if (source == null) { return ""; }
            if (selector == null) { return string.Join(delimiter, source); }
            return string.Join(delimiter, source.Select(selector));
        }
    }
}
