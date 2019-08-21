using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.SQLite.EF6;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.EF6 {
    public class PeopleContextConfiguration : DbConfiguration {
        public PeopleContextConfiguration() {
            AddDependencyResolver(new SQLiteDbDependencyResolver());
        }
    }

    // https://stackoverflow.com/a/43688403/111794
    public class SQLiteProviderInvariantName : IProviderInvariantName {
        public static readonly SQLiteProviderInvariantName Instance = new SQLiteProviderInvariantName();
        private SQLiteProviderInvariantName() { }
        public const string ProviderName = "System.Data.SQLite.EF6";
        public string Name { get { return ProviderName; } }
    }

    class SQLiteDbDependencyResolver : IDbDependencyResolver {
        public object GetService(Type type, object key) {
            if (type == typeof(IProviderInvariantName)) return SQLiteProviderInvariantName.Instance;
            if (type == typeof(DbProviderFactory)) return SQLiteProviderFactory.Instance;
            return SQLiteProviderFactory.Instance.GetService(type);
        }

        public IEnumerable<object> GetServices(Type type, object key) {
            var service = GetService(type, key);
            if (service != null) yield return service;
        }
    }
}
