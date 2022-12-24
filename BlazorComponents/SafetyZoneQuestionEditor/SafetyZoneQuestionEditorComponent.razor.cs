using Blazored.Modal;
using Blazored.Modal.Services;
using DTOs;
using Microsoft.AspNetCore.Components;
using ZoneTriggerService;

namespace SafetyZoneQuestionEditor
{
    public partial class SafetyZoneQuestionEditorComponent
    {
        private SafetyZoneTriggerQuestionDto? _question;
        [Inject] private ISafetyService _safetyService { get; set; }
        [Parameter] public int? QuestionId { get; set; }
        [CascadingParameter] public BlazoredModalInstance ModalInstance { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            if (QuestionId != null)
            {
                _question = await _safetyService.GetSafetyQuestion((int)QuestionId);
            }
            else
            {
                _question = new();
            }
            await base.OnInitializedAsync();
        }

        public void CancelMethod()
        {
            ModalInstance.CloseAsync(ModalResult.Cancel());
        }

        private async Task SaveQuestion()
        {
            await _safetyService.SaveSafetyZoneTriggerQuestion(_question);
            ModalInstance.CloseAsync(ModalResult.Ok(_question));
        }
    }
}
