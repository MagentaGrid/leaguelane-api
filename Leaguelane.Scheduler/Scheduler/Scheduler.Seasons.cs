using Leaguelane.Service.Services;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Scheduler.Scheduler
{
    public class SeasonsScheduler: IJob
    {
        private readonly ISeasonService _seasonService;
        public SeasonsScheduler(ISeasonService seasonService)
        {
            _seasonService = seasonService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await _seasonService.GetAllSeasons(CancellationToken.None);
            Console.WriteLine("Seasons Job");
        }
    }
}
