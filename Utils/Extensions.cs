using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Utils {
    public static class Extensions {
        public static void Add(this List<Person> lst, int ID, string lastname, string firstname) => lst.Add(new Person {
            ID = ID,
            LastName = lastname,
            FirstName = firstname
        });

        public static bool HasAttribute<TAttribute>(this MemberInfo mi, bool inherit = false) where TAttribute : Attribute =>
            mi.GetCustomAttributes(typeof(TAttribute), inherit).Any();

        public static bool IsAnonymous(this Type type) =>
            type.HasAttribute<CompilerGeneratedAttribute>() && type.Name.Contains("Anonymous") && 
            (type.Name.Contains("<>") || type.Name.Contains("VB$"));

        public static Type UnderlyingIfNullable(this Type type) => Nullable.GetUnderlyingType(type) ?? type;

        public static bool IsScalar(this Type t) {
            t = t.UnderlyingIfNullable();
            return t.IsPrimitive || t == typeof(string) || t == typeof(DateTime) || t == typeof(TimeSpan);
        }

        public static IEnumerable<(TFirst, TSecond)> Zip<TFirst, TSecond>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second) => first.Zip(second, (x, y) => (x, y));
    }
}
