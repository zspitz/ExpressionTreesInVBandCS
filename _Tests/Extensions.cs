using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests {
    public static class Extensions {
        public static TheoryData<T1, T2> ToTheoryData<T1, T2>(this IEnumerable<ValueTuple<T1, T2>> src) {
            var ret = new TheoryData<T1, T2>();
            foreach (var (a, b) in src) {
                ret.Add(a, b);
            }
            return ret;
        }
    }
}
