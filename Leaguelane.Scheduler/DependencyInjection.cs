using Leaguelane.Scheduler.Scheduler;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Scheduler
{
    public static class DependencyInjection
    {
        public static void  AddScheduler(this IServiceCollection services)
        {
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();

                q.AddJob<SeasonsScheduler>(JobKey.Create(nameof(SeasonsScheduler)))
                    .AddTrigger(tgr => 
                            tgr
                                .ForJob(JobKey.Create(nameof(SeasonsScheduler)))
                                .WithSimpleSchedule(s => s
                                    .WithIntervalInMinutes(2)
                                    .RepeatForever()));
                
            });
            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        }
    }
}
