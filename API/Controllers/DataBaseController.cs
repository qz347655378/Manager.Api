using System;
using API.Core.Jobs;
using API.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Quartz;
using System.Threading.Tasks;
using API.Core.Filters;
using Common.Enum;
using Microsoft.AspNetCore.Authorization;

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
