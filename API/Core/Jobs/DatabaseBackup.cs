using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models;
using Quartz;
using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace API.Core.Jobs
{
    /// <summary>
    /// 数据库备份
    /// </summary>
    public class DatabaseBackup : IJob
    {

        private readonly ManagerDbContext _dbContext;

        public DatabaseBackup(ManagerDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        public async Task Execute(IJobExecutionContext context)
        {
            var dbName = _dbContext.Database.GetDbConnection().Database;
            var path = Directory.GetCurrentDirectory();
            path = Path.Combine(path, "\\DataBackup");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            path = Path.Combine(path, $"\\{dbName}_{DateTime.Now.ToFileTime()}.bak");

            var command = $"backup database @DbName to disk='@path'";
            object[] paramters = {
                new SqlParameter("@DbName", SqlDbType.NVarChar) { Value = dbName },
                new SqlParameter("@path",SqlDbType.NVarChar){Value = path}
            };
            await _dbContext.Database.ExecuteSqlRawAsync(command, paramters);
        }
    }
}
