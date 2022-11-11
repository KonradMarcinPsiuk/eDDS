using Blazored.Modal;
using Blazored.Modal.Services;
using DTOs;
using Microsoft.AspNetCore.Components;
using PlannedStopService;

namespace PlannedStopPicker
{
    public partial class PlannedStopPickerComponent
    {
        [Inject] IPlannedStopService PlannedStopService { get; set; } = null!;
        [CascadingParameter] private BlazoredModalInstance ModalInstance { get; set; } = null!;


        List<PmTaskDto> PmTasks { get; set; } = new List<PmTaskDto>();
        List<CilTaskDto> CilTasks { get; set; } = new List<CilTaskDto>();
        List<ClTaskDto> ClTasks { get; set; } = new List<ClTaskDto>();
        List<OtherTaskDto> OtherTasks { get; set; } = new List<OtherTaskDto>();

        //1 is for PM
        //2 is for Cil
        //3 is for Cl
        //4 is for Other
        int selectedOption = 1;

        [Parameter]
        public int? SelectedLine { get; set; }


        private async Task LoadTasks()
        {
            if (SelectedLine != null)
            {
                switch (selectedOption)
                {
                    case 1:
                        {
                            PmTasks.Clear();
                            PmTasks.AddRange(await PlannedStopService.GetPmTasksForLine((int)SelectedLine, true));
                            break;
                        }
                    case 2:
                        {
                            CilTasks.Clear();
                            CilTasks.AddRange(await PlannedStopService.GetCilTasksForLine((int)SelectedLine, true));
                            break;
                        }
                    case 3:
                        {
                            ClTasks.Clear();
                            ClTasks.AddRange(await PlannedStopService.GetClTasksForLine((int)SelectedLine, true));
                            break;
                        }
                    case 4:
                        {
                            OtherTasks.Clear();
                            OtherTasks.AddRange(await PlannedStopService.GetOtherTasksForLine((int)SelectedLine, true));
                            break;
                        }
                }
            }
        }

        private async Task SelectionChanged( int i)
        {
            selectedOption = i;
            await LoadTasks();
        }

        protected override async Task OnInitializedAsync()
        {
            await  base.OnInitializedAsync();
            await LoadTasks();
        }

        private async Task CancelMethod()
        {
            await ModalInstance.CloseAsync(ModalResult.Cancel());
        }

        private async Task SelectTask(object task)
        {
            await ModalInstance.CloseAsync(ModalResult.Ok(task));
        }
    }
}
