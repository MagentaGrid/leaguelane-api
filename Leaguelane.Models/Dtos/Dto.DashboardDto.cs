namespace Leaguelane.Models.Dtos
{
    public class DashboardDto
    {
        public int TotalFixtures { get; set; } = 0;
        public int MissingTips { get; set; } = 0;
        public int MissingPreview { get; set; } = 0;
        public int SyncData { get; set; } = 0;
    }
}
