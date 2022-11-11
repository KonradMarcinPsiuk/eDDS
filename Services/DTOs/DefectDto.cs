using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


namespace DTOs
{
    public class DefectDto:IPlannedTask
    {
        public string SubTypeDescription => SubTypes[SubTypeId];

        public Dictionary<int, string> SubTypes { get; } = new()
    {
        {0,""},
        {1, "Minor Defect"},
        {2, "Lack Of Base Condition"},
        {3, "Hard To Reach Area"},
        {4, "Source Of Contamination"},
        {5, "Quality Defect"},
        {6, "Unnecessary Item"},
        {7, "Safety"},
        {8, "Temporary Centerline"}
    };

        public Dictionary<int, string> Priorities { get; } = new()
    {
        {0,""},
        {1,"Low"},
        {2,"Medium"},
        {3,"High"}
    };

        public Dictionary<int, string> Statuses { get; } = new()
    {
        {0,""},
        {1,"Open"},
        {2,"Closed"},
        {3,"Work in progress"}
    };

        public bool IsClosed
        {
            get => Status == 2;
            set => Status = value ? 2 : 1;
        }

        public string PriorityDescription => Priorities[Priority];
        public string StatusDescription => Statuses[Status];

        public Guid Id { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Select field")]
        public int LineAreaId { get; set; }
        [Required]
        public string? Description { get; set; }
        public string? Action { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Select field")]
        public int Status { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Select field")]
        public int SubTypeId { get; set; }
        public int? OwnerId { get; set; }
        public PersonDto? Owner { get; set; }
        [Required]
        [JsonInclude]
        public DateTime OpenDate {  get; private set; }
        [JsonInclude]
        public DateTime? CloseDate { get; private set; }
        public LineAreaDto LineArea { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Select field")]
        public int Priority { get; set; }
        public bool FoundDuringCil { get; set; }
        public bool PmHelpNeeded { get; set; }
        public string? PmHelpText { get; set; }

        public void AssignOwner(PersonDto? owner)
        {
            if (owner != null)
            {
                Owner = owner;
                OwnerId = owner.Id;
            }
            else
            {
                Owner = null;
                OwnerId = null;
            }
        }
        public void CloseTask()
        {
            Status = 2;
            CloseDate = DateTime.Now;

        }

        public void SetAsNew()
        {
            Id = Guid.NewGuid();
            OpenDate = DateTime.Now;
        }
        public void OpenTask()
        {
            Status = 1;
            CloseDate = null;
        }
    }
}
