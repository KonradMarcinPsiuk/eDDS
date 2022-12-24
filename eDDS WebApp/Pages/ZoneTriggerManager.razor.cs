using Blazored.Modal;
using Blazored.Modal.Services;
using DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using SafetyZoneQuestionEditor;
using ZoneTriggerService;

namespace eDDS.WebTool.Pages;

public partial class ZoneTriggerManager
{
    private bool _isLoading = true;


    private readonly List<SafetyZoneTriggerQuestionDto> _safetyQuestions = new();

    [Inject] private ISafetyService _zoneSafetyTriggerService { get; set; }

    // [Inject] private ZoneTriggerService.QualityService _zoneQualityTriggerService { get; set; }
    [Inject] private LevelNavigationObject LevelNavigationObject { get; set; } = null!;
    [Inject] private IJSRuntime _jsRuntime { get; set; } = null!;
    [CascadingParameter] public IModalService Modal { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        if (_isLoading)
        {
            _isLoading = false;
            await LoadQuestions();
        }

        await base.OnInitializedAsync();
    }

    private async Task LoadQuestions()
    {
        _safetyQuestions.AddRange(await _zoneSafetyTriggerService.GetSafetyZoneTriggerQuestions());
    }

    private async Task AddNewDepartment(SafetyZoneTriggerQuestionDto question)
    {
        if (LevelNavigationObject.SelectedDepartment == null)
        {
            await _jsRuntime.InvokeVoidAsync("jsAlert", "Select department and try again");
            return;
        }

        if (question.SafetyZoneTriggerQuestionDepartments.Any(x =>
                x.DepartmentId == LevelNavigationObject.SelectedDepartment.Id))
        {
            await _jsRuntime.InvokeVoidAsync("jsAlert", "This department is already added");
            return;
        }

        var link = new SafetyZoneTriggerQuestionDepartmentDto();
        link.AddNewLink(question, LevelNavigationObject.SelectedDepartment);
        var check = await _zoneSafetyTriggerService.AddDepartmentToQuestion(link);
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

    private async Task LogNewSafetyQuestion()
    {
        await EditSafetyQuestion(null);
    }

    private ModalParameters ModalParam(int? id)
    {
        var parameters = new ModalParameters();
        parameters.Add("QuestionId", id);
        return parameters;
    }

    private ModalOptions ModalOpt()
    {
        var options = new ModalOptions
        {
            DisableBackgroundCancel = true,
            HideCloseButton = true,
            HideHeader = true
        };
        return options;
    }

    private async Task EditSafetyQuestion(int? id)
    {
        var modal = Modal
            .Show<SafetyZoneQuestionEditorComponent>("Question editor", ModalParam(id), ModalOpt());
        var r = await modal.Result;
        if (!r.Cancelled)
            await LoadQuestions();
    }

    private async Task DeleteDepartmentLink(SafetyZoneTriggerQuestionDepartmentDto link, int id)
    {
        if (await _jsRuntime.InvokeAsync<bool>("jsPro",
                "Are You sure You want to permanently delete this 'Department - Question' link?"))
        {
            var check = await _zoneSafetyTriggerService.DeleteDepartmentToQuestion(link);
            if (check)
            {
                await _jsRuntime.InvokeVoidAsync("jsAlert",
                    "'Department - Question' link removed successfully!");
                var safetyQuestion = _safetyQuestions.First(x => x.Id == id);
                safetyQuestion.SafetyZoneTriggerQuestionDepartments.RemoveAll(x => x.Id == link.Id);
                StateHasChanged();
            }
            else
            {
                await _jsRuntime.InvokeVoidAsync("jsAlert", "Error while removing link");
            }
        }
    }
}