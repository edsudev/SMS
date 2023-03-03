using static EDSU_SYSTEM.Models.Enum;

namespace EDSU_SYSTEM.Models
{
    public class Position
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public VacancyType? Type { get; set; }
    }
}
