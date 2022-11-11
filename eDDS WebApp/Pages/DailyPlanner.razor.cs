using Blazored.Modal.Services;
using DailyPlanService;
using DTOs;
using GenericHelpersLibrary;
using Microsoft.AspNetCore.Components;

namespace eDDS.WebTool.Pages;

public partial class DailyPlanner : IDisposable
{
    [Inject] public IDailyPlanService DailyPlanService { get; set; } = null!;

    [Inject] public LevelNavigationObject LevelNavigationObject { get; set; } = null!;

    [Inject] public NavigationManager NavigationManager { get; set; }

    //Modal
    [CascadingParameter] public IModalService Modal { get; set; } = null!;

    public List<DailyPlanDto> Plans { get;  } = new();

    public List<int> Years { get; } = new(Enumerable.Range(2020, 2027));

    public int? SelectedYear { get; set; }
    public int? SelectedWeek { get; set; }

    public List<int> Weeks { get;  } = new();

    public void Dispose()
    {
        LevelNavigationObject.OnSelectionChanged -= LevelNavigationObject_OnSelectionChanged;
    }

    protected override async Task OnInitializedAsync()
    {
        LevelNavigationObject.OnSelectionChanged += LevelNavigationObject_OnSelectionChanged;
        SelectedYear = DateTime.Now.Year;
        UpdateWeeks();
        SelectedWeek = GetWeekNumberFromDateClass.GetWeekNumber(DateTime.Now);
        await OnProductionLineSelected();
        await base.OnInitializedAsync();
    }

    private async void LevelNavigationObject_OnSelectionChanged()
    {
        await InvokeAsync(async () => { await OnProductionLineSelected(); });
    }


    private async Task OnProductionLineSelected()
    {
        Plans.Clear();
        if (LevelNavigationObject.SelectedProductionLine != null) await LoadPlans();
        StateHasChanged();
    }

    private async Task SelectedYearChanged(ChangeEventArgs args)
    {
        if (args.Value != null)
        {
            SelectedYear = int.Parse(args.Value.ToString()!);
            UpdateWeeks();
            await LoadPlans();
        }
    }

    private void UpdateWeeks()
    {
        if (SelectedYear == null) return;
        
        Weeks.Clear();
        Weeks.AddRange(GetWeeksInYearClass.GetWeeksInYear((int) SelectedYear).GetWeeksInYearToArray());
        
        if (SelectedWeek is not > 1) return;
        
        var i = (int) SelectedWeek;
        while (!Weeks.Contains(i)) i--;

        SelectedWeek = i;
    }

    private async Task SelectedWeekChanged(ChangeEventArgs args)
    {
        if (args.Value != null)
        {
            SelectedWeek = int.Parse(args.Value.ToString()!);
            await LoadPlans();
        }
    }

    private async Task LoadPlans()
    {
        Plans.Clear();
        if (SelectedWeek != null && SelectedYear != null && LevelNavigationObject.SelectedProductionLine != null)
        {
            var list = await DailyPlanService.GetDailyPlans(LevelNavigationObject.SelectedProductionLine.Id,
                (int) SelectedWeek, (int) SelectedYear);
            Plans.AddRange(list);
        }

        StateHasChanged();
    }

    private async Task EditPlan(int planId)
    {
        if (LevelNavigationObject.SelectedProductionLine != null && LevelNavigationObject.SelectedDepartment != null)
            NavigationManager.NavigateTo("/dailyplaneditor/" + planId);
    }

    private async Task CreateNewPlan()
    {
        if (LevelNavigationObject.SelectedProductionLine != null && LevelNavigationObject.SelectedDepartment != null)
            NavigationManager.NavigateTo("/dailyplaneditor");
    }
}