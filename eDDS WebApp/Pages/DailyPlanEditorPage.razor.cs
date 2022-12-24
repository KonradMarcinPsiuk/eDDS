using Microsoft.AspNetCore.Components;

namespace eDDS.WebTool.Pages;

public partial class DailyPlanEditorPage
{
    private bool _loading;

    [Inject]
    public LevelNavigationObject
        LevelNavigationObject { get; set; } = null!;

    [Parameter] public string planId { get; set; }

    private int? _planId
    {
        get
        {
            if (int.TryParse(planId, out var id))
                return id;
            return null;
        }
    }

    [Inject] public NavigationManager NavigationManager { get; set; }

    private void NavigateToPlansList()
    {
        NavigationManager.NavigateTo("/dailyplanner");
    }
}