namespace Leaguelane.ApiService.Mappers
{
    public class SportMapper
    {
        public static Models.Dtos.SportDto MapToDto(Persistence.Entities.Sport sport)
        {
            return new Models.Dtos.SportDto
            {
                SportId = sport.SportId,
                Name = sport.Name,
                Description = sport.Description,
                ApiUrl = sport.ApiUrl,
                ApiHost = sport.ApiHost,
                ApiKey = sport.ApiKey,
                Active = sport.Active ?? false
            };
        }
        public static Persistence.Entities.Sport MapToEntity(Models.Dtos.SportDto sportDto)
        {
            return new Persistence.Entities.Sport
            {
                SportId = sportDto.SportId,
                Name = sportDto.Name,
                Description = sportDto.Description,
                ApiUrl = sportDto.ApiUrl,
                ApiHost = sportDto.ApiHost,
                ApiKey = sportDto.ApiKey,
                Active = true
            };
        }
    }
}
