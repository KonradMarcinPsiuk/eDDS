using Blazored.Modal;
using Blazored.Modal.Services;
using DailyTriggerService;
using DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DailyTriggerQuestionEditor
{
    public partial class DailyTriggerQuestionEditorComponent
    {
        [CascadingParameter] private BlazoredModalInstance ModalInstance { get; set; } = null!;
        [Inject] private IDailyTriggerService DailyTriggerService { get; set; } = null!;
        [Inject] private IJSRuntime JsRuntime { get; set; } = null!;

        [Parameter]
        public int? QuestionId { get; set; }

        private DailyTriggerQuestionDto? EditedQuestion { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (QuestionId != null)
            {
                EditedQuestion = await DailyTriggerService.GetQuestion((int)QuestionId);
            }
            else
            {
                EditedQuestion = new DailyTriggerQuestionDto();
                EditedQuestion.CreateNew();
            }
            await  base.OnInitializedAsync();
            StateHasChanged();
        }

        private async Task SaveEdit()
        {
            if (EditedQuestion != null)
            {
                var check = await DailyTriggerService.SaveQuestion(EditedQuestion);
                if(check)
                    await ModalInstance.CloseAsync(ModalResult.Ok(EditedQuestion));
            }
            else
            {
                await JsRuntime.InvokeVoidAsync("jsAlert", "Error while saving question");
            }
            
        }

        private async Task CancelEdit()
        {
            await ModalInstance.CloseAsync(ModalResult.Cancel());
        }
    }
}
