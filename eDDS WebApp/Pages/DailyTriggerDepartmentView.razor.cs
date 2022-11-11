using DailyTriggerService;
using DatePicker;
using DTOs;
using Microsoft.AspNetCore.Components;
using ProductionLineService;

namespace eDDS.WebTool.Pages;

public partial class DailyTriggerDepartmentView : IDisposable
{
    private readonly List<DailyTriggerAnswerDto> _answers = new();

    private readonly List<ProductionLineDto> _lines = new();

    private bool _isLoaded;
    [Inject] private LevelNavigationObject LevelNavigationObject { get; set; } = null!;
    [Inject] private DateNavigationObject DateNavigationObject { get; set; } = null!;
    [Inject] private IDailyTriggerService DailyTriggerService { get; set; } = null!;
    [Inject] private IProductionLineService ProductionLineService { get; set; } = null!;

    public void Dispose()
    {
        LevelNavigationObject.OnSelectionChanged -= LoadDataMethod;
        DateNavigationObject.OnSelectionChanged -= LoadDataMethod;
    }

    private async void LoadDataMethod()
    {
        await LoadData();
    }

    protected override async Task OnInitializedAsync()
    {
        if (!_isLoaded)
        {
            _isLoaded = true;
            LevelNavigationObject.OnSelectionChanged += LoadDataMethod;
            DateNavigationObject.OnSelectionChanged += LoadDataMethod;
            await LoadData();
        }

        await base.OnInitializedAsync();
    }

    private async Task LoadData()
    {
        _lines.Clear();
        _answers.Clear();

        if (LevelNavigationObject.SelectedDepartment != null)
        {
            _lines.AddRange(
                await ProductionLineService.GetProductionLinesForDepartment(LevelNavigationObject.SelectedDepartment
                    .Id));
            _answers.AddRange(await DailyTriggerService.GetAnswersForDateAndDepartment(
                DateNavigationObject.SelectedDate, LevelNavigationObject.SelectedDepartment.Id));
            StateHasChanged();
        }
    }

    private string GetAnswer(int? a, Dictionary<int, string> dict)
    {
        if (a is { } i) return dict[i];
        return "Not selected";
    }
}