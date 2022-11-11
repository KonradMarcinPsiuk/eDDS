using Microsoft.AspNetCore.Components;

namespace eDDS.WebTool.Pages;

public partial class DailyPlanEditorPage
{
    private bool _loading;

    [Inject]
    public LevelNavigationObject
        LevelNavigationObject { get; set; } = null!;

    [Parameter] public int planId { get; set; }

    [Inject] public NavigationManager NavigationManager { get; set; }

    private void NavigateToPlansList()
    {
        NavigationManager.NavigateTo("/dailyplanner");
    }
}