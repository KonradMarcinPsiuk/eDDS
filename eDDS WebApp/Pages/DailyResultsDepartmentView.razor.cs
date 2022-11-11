using DailyResultsService;
using DatePicker;
using DTOs;
using Microsoft.AspNetCore.Components;
using ProductionLineService;

namespace eDDS.WebTool.Pages;

public partial class DailyResultsDepartmentView : IDisposable
{
    private readonly List<ProductionLineDto> _lines = new();
    private readonly List<DailyResultsDto> _results = new();

    private bool _isLoaded;
    [Inject] private LevelNavigationObject LevelNavigationObject { get; set; } = null!;
    [Inject] private DateNavigationObject DateNavigationObject { get; set; } = null!;
    [Inject] private IDailyResultsService DailyResultsService { get; set; } = null!;
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
        _results.Clear();
        StateHasChanged();

        if (LevelNavigationObject.SelectedDepartment != null)
        {
            _lines.AddRange(
                await ProductionLineService.GetProductionLinesForDepartment(LevelNavigationObject.SelectedDepartment
                    .Id));
            _results.AddRange(await DailyResultsService.GetResultsForDateAndDepartment(
                DateNavigationObject.SelectedDate, LevelNavigationObject.SelectedDepartment.Id));
            StateHasChanged();
        }
    }
}