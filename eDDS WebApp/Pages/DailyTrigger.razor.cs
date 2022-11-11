using DailyTriggerService;
using DatePicker;
using DTOs;
using eDDS.WebTool.Scoped;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace eDDS.WebTool.Pages;

public partial class DailyTrigger : IDisposable
{
    private readonly List<DailyTriggerAnswerDto> _answers = new();
    private readonly CancellationTokenSource _generalTokenSource = new();

    private bool _isInitialized;

    private CancellationTokenSource src = new();
    [Inject] private DateNavigationObject DateNavigation { get; set; } = null!;
    [Inject] private LevelNavigationObject LevelNavigation { get; set; } = null!;
    [Inject] private IDailyTriggerService DailyTriggerService { get; set; } = null!;
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;
    [Inject] private IsBusyScoped IsBusyScoped { get; set; } = null!;

    public void Dispose()
    {
        _generalTokenSource.Cancel();
        _generalTokenSource.Dispose();
        DateNavigation.OnSelectionChanged -= DateOrLineNavigation_OnSelectionChanged;
        LevelNavigation.OnSelectionChanged -= DateOrLineNavigation_OnSelectionChanged;
        src.Dispose();
    }

    private async Task SaveAnswers()
    {
        src.Cancel();
        src.Dispose();
        src = new CancellationTokenSource();
        await DailyTriggerService.SaveAnswers(_answers, src.Token);
    }

    protected override async Task OnInitializedAsync()
    {
        if (!_isInitialized)
        {
            _isInitialized = true;
            DateNavigation.OnSelectionChanged += DateOrLineNavigation_OnSelectionChanged;
            LevelNavigation.OnSelectionChanged += DateOrLineNavigation_OnSelectionChanged;
            await LoadData();
        }

        await base.OnInitializedAsync();
    }

    private async void DateOrLineNavigation_OnSelectionChanged()
    {
        await LoadData();
        StateHasChanged();
    }


    private async Task LoadData()
    {
        _answers.Clear();
        IsBusyScoped.IsBusy = true;
        StateHasChanged();
        if (LevelNavigation.SelectedProductionLine != null)
        {
            var check = await DailyTriggerService.CheckIfAnswersForLineAndDateExists(
                DateNavigation.SelectedDate, LevelNavigation.SelectedProductionLine.Id);
            if (!check && !_generalTokenSource.IsCancellationRequested)
            {
                var newList = await JsRuntime.InvokeAsync<bool>("jsPro",
                    "There is no triggers for selected date and line. Do You want to create new trigger list?");

                if (newList)
                {
                    var checkQuest =
                        await DailyTriggerService.GetNumberOfQuestionsAssignedToTheLine(LevelNavigation
                            .SelectedProductionLine.Id);

                    if (checkQuest > 0)
                    {
                        var list = await DailyTriggerService.GetTriggersForLineAndDate(
                            DateNavigation.SelectedDate, LevelNavigation.SelectedProductionLine.Id);
                        if (list != null)
                            _answers.AddRange(list);
                        IsBusyScoped.IsBusy = false;
                        StateHasChanged();
                        return;
                    }

                    await JsRuntime.InvokeVoidAsync("jsAlert",
                        "There is no trigger questions assigned to this line");
                }
            }
            else
            {
                var list = await DailyTriggerService.GetTriggersForLineAndDate(DateNavigation.SelectedDate,
                    LevelNavigation.SelectedProductionLine.Id);
                if (list != null)
                    _answers.AddRange(list);
            }
        }

        IsBusyScoped.IsBusy = false;
        StateHasChanged();
    }
}