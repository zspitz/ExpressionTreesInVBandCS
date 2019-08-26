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
        public static void Main() {
            var conn = new SQLiteConnection($"Data Source={DbPath}");
            using (var ctx = new PeopleContext(conn)) {
                //ctx.Persons.RemoveRange(ctx.Persons);

                //ctx.Persons.AddRange(PersonList);
                //ctx.SaveChanges();

                foreach (var p in ctx.Persons.Where(x => x.DateOfBirth < new DateTime(1930, 1, 1))) {
                    p.Write();
                }
                Console.ReadKey(true);
            }
        }
    }
}
