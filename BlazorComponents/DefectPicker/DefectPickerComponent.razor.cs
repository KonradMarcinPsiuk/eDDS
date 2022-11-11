using Blazored.Modal;
using Blazored.Modal.Services;
using DefectService;
using Microsoft.AspNetCore.Components;

using DTOs;

namespace DefectPicker
{
    public partial class DefectPickerComponent
    {
        public List<DefectDto> Defects { get; set; } = new();

       [Parameter]
        public int? SelectedLine { get; set; }
        [CascadingParameter] public BlazoredModalInstance ModalInstance { get; set; } = null!;

        [Inject] public IDefectService DefectService { get; set; } = null!;


        protected override async Task OnInitializedAsync()
        {
            await LoadDefects();
            await base.OnInitializedAsync();
        }

        private async Task LoadDefects()
        {
            Defects.Clear();
            if (SelectedLine != null)
            {
                var defects = await DefectService.GetOpenDefectsForProductionLine((int)SelectedLine);
                Defects.AddRange(defects);
                StateHasChanged();
            }
        }

        private void CancelMethod()
        {
            ModalInstance.CloseAsync(ModalResult.Cancel());
        }

        private void SelectDefect(Guid id)
        {
            ModalInstance.CloseAsync(ModalResult.Ok(id));
        }
    }
}
