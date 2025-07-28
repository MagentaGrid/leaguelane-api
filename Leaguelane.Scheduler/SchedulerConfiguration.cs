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

                q.AddJob<SeasonsScheduler>(JobKey.Create(nameof(SeasonsScheduler)))
                    .AddTrigger(tgr => 
                            tgr
                                .ForJob(JobKey.Create(nameof(SeasonsScheduler)))
                                .WithSimpleSchedule(s => s
                                    //.WithIntervalInMinutes(2)
                                    .WithIntervalInHours(24)
                                    .RepeatForever()));

                q.AddJob<LeagueScheduler>(JobKey.Create(nameof(LeagueScheduler)))
                    .AddTrigger(tgr =>
                            tgr
                                .ForJob(JobKey.Create(nameof(LeagueScheduler)))
                                .WithSimpleSchedule(s => s
                                    .WithIntervalInMinutes(2)
                                    //.WithIntervalInHours(24)
                                    .RepeatForever()));

                q.AddJob<CountryScheduler>(JobKey.Create(nameof(CountryScheduler)))
                    .AddTrigger(tgr =>
                            tgr
                                .ForJob(JobKey.Create(nameof(CountryScheduler)))
                                .WithSimpleSchedule(s => s
                                    .WithIntervalInMinutes(2)
                                    //.WithIntervalInHours(24)
                                    .RepeatForever()));

                q.AddJob<FixtureScheduler>(JobKey.Create(nameof(FixtureScheduler)))
                    .AddTrigger(tgr =>
                            tgr
                                .ForJob(JobKey.Create(nameof(FixtureScheduler)))
                                .WithSimpleSchedule(s => s
                                    .WithIntervalInMinutes(2)
                                    //.WithIntervalInHours(24)
                                    .RepeatForever()));

                q.AddJob<BookmakerScheduler>(JobKey.Create(nameof(BookmakerScheduler)))
                    .AddTrigger(tgr =>
                            tgr
                                .ForJob(JobKey.Create(nameof(BookmakerScheduler)))
                                .WithSimpleSchedule(s => s
                                    //.WithIntervalInMinutes(2)
                                    .WithIntervalInHours(24)
                                    .RepeatForever()));

                q.AddJob<BetScheduler>(JobKey.Create(nameof(BetScheduler)))
                    .AddTrigger(tgr =>
                            tgr
                                .ForJob(JobKey.Create(nameof(BetScheduler)))
                                .WithSimpleSchedule(s => s
                                    //.WithIntervalInMinutes(2)
                                    .WithIntervalInHours(24)
                                    .RepeatForever()));

            });
            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        }
    }
}
