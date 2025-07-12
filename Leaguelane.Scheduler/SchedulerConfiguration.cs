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
    public static class SchedulerConfiguration
    {
        public static void  AddScheduler(this IServiceCollection services)
        {
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();

                q.AddJob<FootbalApiScheduler>(JobKey.Create(nameof(FootbalApiScheduler)))
                    .AddTrigger(tgr => 
                            tgr
                                .ForJob(JobKey.Create(nameof(FootbalApiScheduler)))
                                .WithSimpleSchedule(s => s
                                    .WithIntervalInMinutes(2)
                                    //.WithIntervalInHours(24)
                                    .RepeatForever()));
                
            });
            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        }
    }
}
