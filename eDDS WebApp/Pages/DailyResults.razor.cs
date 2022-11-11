using DailyResultsService;
using DatePicker;
using DTOs;
using eDDS.WebTool.Scoped;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace eDDS.WebTool.Pages;

public partial class DailyResults : IDisposable
{
    private DailyResultsDto? _dailyResults;

    private readonly CancellationTokenSource _generalTokenSource = new();

    private bool _isLoading = true;
    private CancellationTokenSource src = new();
    [Inject] private IDailyResultsService DailyResultsService { get; set; } = null!;
    [Inject] private LevelNavigationObject LevelNavigationObject { get; set; } = null!;
    [Inject] private DateNavigationObject DateNavigationObject { get; set; } = null!;
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;
    [Inject] private IsBusyScoped IsBusyScoped { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;


    public void Dispose()
    {
        _generalTokenSource.Cancel();
        _generalTokenSource.Dispose();
        LevelNavigationObject.OnSelectionChanged -= SelectionChanged;
        DateNavigationObject.OnSelectionChanged -= SelectionChanged;
        src.Dispose();
    }

    protected override async Task OnInitializedAsync()
    {
        if (_isLoading)
        {
            LevelNavigationObject.OnSelectionChanged += SelectionChanged;
            DateNavigationObject.OnSelectionChanged += SelectionChanged;

            IsBusyScoped.IsBusy = true;
            StateHasChanged();
            _isLoading = false;

            await LoadData();
        }

        await base.OnInitializedAsync();
    }


    private async void SelectionChanged()
    {
        await LoadData();
    }

    private async Task LoadData()
    {
        _dailyResults = null;
        IsBusyScoped.IsBusy = true;
        StateHasChanged();
        if (LevelNavigationObject.SelectedProductionLine != null)
        {
            var check = await DailyResultsService.CheckIfTheResultsForDateAndLine(DateNavigationObject.SelectedDate,
                LevelNavigationObject.SelectedProductionLine.Id);
            if (check)
            {
                _dailyResults = await DailyResultsService.GetResultsForDateAndLine(
                    DateNavigationObject.SelectedDate,
                    LevelNavigationObject.SelectedProductionLine.Id);
            }
            else
            {
                if (!_generalTokenSource.IsCancellationRequested)
                {
                    var ask = await JsRuntime.InvokeAsync<bool>("jsPro",
                        "There is no daily results for selected date and line. Do You want to create a new entry?");

                    if (ask)
                        _dailyResults = await DailyResultsService.GetResultsForDateAndLine(
                            DateNavigationObject.SelectedDate,
                            LevelNavigationObject.SelectedProductionLine.Id);
                }
            }
        }

        IsBusyScoped.IsBusy = false;
        StateHasChanged();
    }

    private async Task SaveData()
    {
        if (_dailyResults != null)
        {
            src.Cancel();
            src.Dispose();
            src = new CancellationTokenSource();
            await DailyResultsService.SaveDailyResult(_dailyResults, src.Token);
        }
    }
}