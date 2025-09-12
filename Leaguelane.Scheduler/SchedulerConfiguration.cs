using Leaguelane.Scheduler.Scheduler;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Listener;

namespace Leaguelane.Scheduler
{
    public static class SchedulerConfiguration
    {
        public static void AddScheduler(this IServiceCollection services)
        {
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();

                // Register all jobs
                q.AddJob<CountryScheduler>(JobKey.Create(nameof(CountryScheduler)));
                q.AddJob<LeagueScheduler>(JobKey.Create(nameof(LeagueScheduler)));
                q.AddJob<SeasonsScheduler>(JobKey.Create(nameof(SeasonsScheduler)));
                q.AddJob<FixtureScheduler>(JobKey.Create(nameof(FixtureScheduler)));
                q.AddJob<RoundsScheduler>(JobKey.Create(nameof(RoundsScheduler)));
                q.AddJob<TeamsScheduler>(JobKey.Create(nameof(TeamsScheduler)));
                q.AddJob<TeamStatsScheduler>(JobKey.Create(nameof(TeamStatsScheduler)));
                q.AddJob<BetScheduler>(JobKey.Create(nameof(BetScheduler)));
                q.AddJob<BookmakerScheduler>(JobKey.Create(nameof(BookmakerScheduler)));
                q.AddJob<OddsScheduler>(JobKey.Create(nameof(OddsScheduler)));

                // Only trigger the FIRST job (Country) daily
                q.AddTrigger(t => t
                    .ForJob(JobKey.Create(nameof(CountryScheduler)))
                    .WithIdentity("CountryTrigger")
                    .WithSimpleSchedule(s => s.WithIntervalInHours(24).RepeatForever())
                );

                // Create and configure job chain
                var chain = new JobChainingJobListener("JobChain");
                chain.AddJobChainLink(JobKey.Create(nameof(CountryScheduler)), JobKey.Create(nameof(LeagueScheduler)));
                chain.AddJobChainLink(JobKey.Create(nameof(LeagueScheduler)), JobKey.Create(nameof(SeasonsScheduler)));
                chain.AddJobChainLink(JobKey.Create(nameof(SeasonsScheduler)), JobKey.Create(nameof(FixtureScheduler)));
                chain.AddJobChainLink(JobKey.Create(nameof(FixtureScheduler)), JobKey.Create(nameof(RoundsScheduler)));
                chain.AddJobChainLink(JobKey.Create(nameof(RoundsScheduler)), JobKey.Create(nameof(TeamsScheduler)));
                chain.AddJobChainLink(JobKey.Create(nameof(TeamsScheduler)), JobKey.Create(nameof(TeamStatsScheduler)));
                chain.AddJobChainLink(JobKey.Create(nameof(TeamStatsScheduler)), JobKey.Create(nameof(BetScheduler)));
                chain.AddJobChainLink(JobKey.Create(nameof(BetScheduler)), JobKey.Create(nameof(BookmakerScheduler)));
                chain.AddJobChainLink(JobKey.Create(nameof(BookmakerScheduler)), JobKey.Create(nameof(OddsScheduler)));

                // Attach job listener
                q.AddJobListener(chain);
            });

            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        }
    }
}
