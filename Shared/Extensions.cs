﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Shared {
    public static class Extensions {
        /// <summary>Enables using a collection initializer with a List&lt;Person&gt;</summary>
        public static void Add(this List<Person> lst, int ID, string firstname, string lastname, string email, string birthdate) => lst.Add(new Person {
            ID = ID,
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
    }
}
