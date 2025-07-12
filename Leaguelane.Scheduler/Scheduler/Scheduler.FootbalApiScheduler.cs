using Leaguelane.Service.Services;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leaguelane.Scheduler.Scheduler
{
    public class FootbalApiScheduler : IJob
    {
        private readonly ISeasonService _seasonService;
        private readonly ICountryService _countryService;
        private readonly ILeagueService _leagueService;
        public FootbalApiScheduler(ISeasonService seasonService, ICountryService countryService, ILeagueService leagueService)
        {
            _seasonService = seasonService;
            _countryService = countryService;
            _leagueService = leagueService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            await _seasonService.GetAllSeasons(CancellationToken.None);
            await _countryService.GetAllCountriesAsync(CancellationToken.None);
            await _leagueService.GetAllLeaguesAsync(CancellationToken.None);
            //Console.WriteLine("Seasons Job");
        }
    }
}
