using Leaguelane.Constants.Enums;
using System;

namespace Leaguelane.Persistence.Entities
{
    public class Settings : Entity
    {
        public int SettingsId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
