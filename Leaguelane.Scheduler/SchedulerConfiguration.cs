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

                // Register jobs, marking those without triggers as durable
                q.AddJob<CountryScheduler>(j => j.WithIdentity(nameof(CountryScheduler)));
                q.AddJob<LeagueScheduler>(j => j.WithIdentity(nameof(LeagueScheduler)).StoreDurably());
                q.AddJob<SeasonsScheduler>(j => j.WithIdentity(nameof(SeasonsScheduler)).StoreDurably());
                q.AddJob<FixtureScheduler>(j => j.WithIdentity(nameof(FixtureScheduler)).StoreDurably());
                q.AddJob<RoundsScheduler>(j => j.WithIdentity(nameof(RoundsScheduler)).StoreDurably());
                q.AddJob<TeamsScheduler>(j => j.WithIdentity(nameof(TeamsScheduler)).StoreDurably());
                q.AddJob<TeamStatsScheduler>(j => j.WithIdentity(nameof(TeamStatsScheduler)).StoreDurably());
                q.AddJob<BetScheduler>(j => j.WithIdentity(nameof(BetScheduler)).StoreDurably());
                q.AddJob<BookmakerScheduler>(j => j.WithIdentity(nameof(BookmakerScheduler)).StoreDurably());
                q.AddJob<OddsScheduler>(j => j.WithIdentity(nameof(OddsScheduler)).StoreDurably());

                // Only the FIRST job gets a trigger
                q.AddTrigger(t => t
                    .ForJob(nameof(CountryScheduler))
                    .WithIdentity("CountryTrigger")
                    .WithSimpleSchedule(s => s.WithIntervalInHours(24).RepeatForever())
                );

                // Define chaining order
                var chain = new JobChainingJobListener("JobChain");
                chain.AddJobChainLink(new JobKey(nameof(CountryScheduler)), new JobKey(nameof(LeagueScheduler)));
                chain.AddJobChainLink(new JobKey(nameof(LeagueScheduler)), new JobKey(nameof(SeasonsScheduler)));
                chain.AddJobChainLink(new JobKey(nameof(SeasonsScheduler)), new JobKey(nameof(FixtureScheduler)));
                chain.AddJobChainLink(new JobKey(nameof(FixtureScheduler)), new JobKey(nameof(RoundsScheduler)));
                chain.AddJobChainLink(new JobKey(nameof(RoundsScheduler)), new JobKey(nameof(TeamsScheduler)));
                chain.AddJobChainLink(new JobKey(nameof(TeamsScheduler)), new JobKey(nameof(TeamStatsScheduler)));
                chain.AddJobChainLink(new JobKey(nameof(TeamStatsScheduler)), new JobKey(nameof(BetScheduler)));
                chain.AddJobChainLink(new JobKey(nameof(BetScheduler)), new JobKey(nameof(BookmakerScheduler)));
                chain.AddJobChainLink(new JobKey(nameof(BookmakerScheduler)), new JobKey(nameof(OddsScheduler)));

                q.AddJobListener(chain);
            });

            services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        }
    }
}
