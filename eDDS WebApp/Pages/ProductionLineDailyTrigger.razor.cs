using Blazored.Modal;
using Blazored.Modal.Services;
using DailyTriggerQuestionEditor;
using DailyTriggerService;
using DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace eDDS.WebTool.Pages;

public partial class ProductionLineDailyTrigger
{
    private readonly List<DailyTriggerQuestionDto> _questions = new();
    [Inject] private IDailyTriggerService DailyTriggerService { get; set; } = null!;
    [Inject] private IJSRuntime _jsRuntime { get; set; } = null!;
    [Inject] private LevelNavigationObject LevelNavigationObject { get; set; } = null!;
    [CascadingParameter] public IModalService Modal { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await LoadQuestions();
    }

    private async Task LoadQuestions()
    {
        _questions.Clear();
        _questions.AddRange(await DailyTriggerService.GetAllQuestions());
        StateHasChanged();
    }

    private async Task DeleteLineLink(ProductionLineDailyTriggerQuestionDto link, int questionId)
    {
        if (await _jsRuntime.InvokeAsync<bool>("jsPro",
                "Are You sure You want to permanently delete this 'Production line - Question' link?"))
        {
            var check = await DailyTriggerService.DeleteProductionLineDailyTriggerLine(link);
            if (check)
            {
                await _jsRuntime.InvokeVoidAsync("jsAlert",
                    "'Production Line - Question' link removed successfully!");
                var dailyTriggerQuestionDto = _questions.First(x => x.Id == questionId);
                dailyTriggerQuestionDto.ProductionLineDailyTriggerQuestions.RemoveAll(x => x.Id == link.Id);
                StateHasChanged();
            }
            else
            {
                await _jsRuntime.InvokeVoidAsync("jsAlert", "Error while removing link");
            }
        }
    }

    private async Task AddNewLine(DailyTriggerQuestionDto question)
    {
        if (LevelNavigationObject.SelectedProductionLine == null)
        {
            await _jsRuntime.InvokeVoidAsync("jsAlert", "Select production line and try again");
            return;
        }

        if (question.ProductionLineDailyTriggerQuestions.Any(x =>
                x.ProductionLineId == LevelNavigationObject.SelectedProductionLine.Id))
        {
            await _jsRuntime.InvokeVoidAsync("jsAlert", "This line is already added");
            return;
        }

        var link = new ProductionLineDailyTriggerQuestionDto();
        link.AddNewLink(question, LevelNavigationObject.SelectedProductionLine);
        var check = await DailyTriggerService.SaveProductionLineDailyTriggerLine(link);
        if (check)
        {
            question.AddNewLine(link);
            StateHasChanged();
        }
        else
        {
            await _jsRuntime.InvokeVoidAsync("jsAlert", "Error while saving the link");
        }
    }

    private async Task LogNewQuestion()
    {
        await EditQuestion(null);
    }

    private async Task EditQuestion(int? id)
    {
        var parameters = new ModalParameters();
        parameters.Add("QuestionId", id);
        var options = new ModalOptions
        {
            DisableBackgroundCancel = true,
            HideCloseButton = true,
            HideHeader = true
        };

        var modal = Modal
            .Show<DailyTriggerQuestionEditorComponent>("Question editor", parameters, options);

        var r = await modal.Result;
        if (!r.Cancelled)
            await LoadQuestions();
    }
}