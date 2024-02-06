using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using CSSC.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSSC_Tests
{
    public class CSSCContextFixture : IDisposable
    {
        public CSSCContext DbContext { get; private set; }

        public CSSCContextFixture() 
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder<CSSCContext>()
                    .UseSqlite(connection)
                    .Options;
            DbContext = new CSSCContext(options);

            DbContext.Database.EnsureCreated();

            DbContext.SaveChanges();
        }

        public void Dispose() => DbContext.Dispose();
    }
}
