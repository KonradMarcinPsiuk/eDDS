using Blazored.Modal;
using Blazored.Modal.Services;
using DTOs;
using LineAreaPicker;
using Microsoft.AspNetCore.Components;
using PlannedStopService;

namespace PlannedTaskEditorBase
{
    public class EditorBase : ComponentBase 
    {
        public EditorBase(TaskType type)
        {
            _type = type;
        }
        
        [Inject] public IPlannedStopService PlannedStopService { get; set; } = null!;
        [CascadingParameter] public BlazoredModalInstance ModalInstance { get; set; } = null!;

        [Parameter]
        public Guid? TaskId { get; set; }

        [Parameter]
        public int? SelectedLine { get; set; }

        public IPlannedTask? EditedTask { get; set; }

        private TaskType _type;

        public LineAreaPickerComponent? LinePicker { get; set; }

        public LineAreaDto? SelectedLineArea
        {
            get => EditedTask?.LineArea;
            set
            {
                if (EditedTask == null || value == null) return;
                EditedTask.LineArea = value;
                EditedTask.LineAreaId = value.Id;
            }
        }

        protected override async Task OnInitializedAsync()
        {

            if (TaskId != null)
            {
                if (_type==TaskType.CilTask) EditedTask = await PlannedStopService.GetCilTask((Guid) TaskId);
                if (_type==TaskType.ClTask) EditedTask = await PlannedStopService.GetClTask((Guid)TaskId);
                if (_type==TaskType.PmTask) EditedTask = await PlannedStopService.GetPmTask((Guid)TaskId);
                if (_type==TaskType.OtherTask) EditedTask = await PlannedStopService.GetOtherTask((Guid)TaskId);
            }
            else
            {
                EditedTask = _type switch
                {
                    TaskType.PmTask => new PmTaskDto(),
                    TaskType.OtherTask => new OtherTaskDto(),
                    TaskType.CilTask => new CilTaskDto(),
                    TaskType.ClTask => new ClTaskDto(),
                        _ => EditedTask
                    
                };

                EditedTask?.SetAsNew();

                if (LinePicker != null)
                    await LinePicker.ResetSelected();
            }

            await base.OnInitializedAsync();
            StateHasChanged();
        }

        public void CancelMethod()
        {
            ModalInstance.CloseAsync(ModalResult.Cancel());
        }

        public virtual Task SaveTask()
        {
            return Task.CompletedTask;
        }

        public void SetLineArea(LineAreaDto? args)
        {
            if (args != null)
                SelectedLineArea = args;
        }
    }

    public enum TaskType
    {
        PmTask,CilTask,ClTask,OtherTask
    }
}
