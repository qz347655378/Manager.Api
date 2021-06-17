using API.Core.Filters;
using API.Core.Jobs;
using API.ViewModel;
using Common.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Quartz;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/sys/[controller]")]
    [ApiController, Authorize]
    public class DataBaseController : BaseController
    {
        private readonly ISchedulerFactory _schedulerFactory;

        public DataBaseController(ISchedulerFactory schedulerFactory)
        {
            _schedulerFactory = schedulerFactory;
        }

        /// <summary>
        /// 备份数据库
        /// </summary>
        /// <param name="cron">cron数据</param>
        /// <returns></returns>
        [HttpGet, Action("BackupData")]
        public async Task<ResponseResult<string>> BackupData(string cron)
        {
            var result = new ResponseResult<string>
            {
                Code = ResponseStatusEnum.Ok,
                Msg = "启动任务成功"
            };

            var scheduler = await _schedulerFactory.GetScheduler();
            await scheduler.Start();
            var trigger = TriggerBuilder.Create().WithCronSchedule(cron).Build();
            var job = JobBuilder.Create<DatabaseBackup>().WithIdentity("backData", "group").Build();
            await scheduler.ScheduleJob(job, trigger);
            return result;


        }
    }
}
