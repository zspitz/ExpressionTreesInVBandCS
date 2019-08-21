using Shared;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Shared.Globals;

namespace _InsertDataAndConnectionTest {
    class Program {
        static void Main(string[] args) {
            var conn = new SQLiteConnection($"Data Source={DbPath}");
            var ctx = new PeopleContext(conn);
            var qry = ctx.Persons.Where(x => x.LastName.StartsWith("A"));
            foreach (var p in qry) {
                p.Write();
            }
        }
    }
}
