using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DTOs;

public class PlannedTask : IPlannedTask
{
    public bool IsClosed
    {
        get => Status == 2;
        set => Status = value ? 2 : 1;
    }

    public string PriorityDescription => Priorities[Priority];
    public string StatusDescription => Statuses[Status];
    public Guid Id { get; set; }
    public string? Description { get; set; }
    public string? Action { get; set; }
    public int Status { get; set; }
    [JsonInclude] public DateTime OpenDate { get; private set; }
    [JsonInclude] public DateTime? CloseDate { get; private set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Select line area")]
    public int LineAreaId { get; set; }

    public LineAreaDto LineArea { get; set; }
    public bool IsDeleted { get; set; }
    public int Priority { get; set; }


    [JsonIgnore]
    public Dictionary<int, string> Statuses { get; } = new Dictionary<int, string>
    {
        {1, "Open"},
        {2, "Closed"},
        {3, "Work in progress"}
    };

    [JsonIgnore]
    public Dictionary<int, string> Priorities { get; } = new()
    {
        {0, ""},
        {1, "Low"},
        {2, "Medium"},
        {3, "High"}
    };

    public void CloseTask()
    {
        Status = 2;
        CloseDate = DateTime.Now;
    }

    public void OpenTask()
    {
        Status = 1;
        CloseDate = null;
    }

    public void SetAsNew()
    {
        Id = Guid.NewGuid();
        Status = 1;
        Priority = 1;
        OpenDate = DateTime.Now;
    }
}