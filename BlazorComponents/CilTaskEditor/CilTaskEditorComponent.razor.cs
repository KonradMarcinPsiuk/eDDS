using Blazored.Modal.Services;
using DTOs;
using PlannedTaskEditorBase;

namespace CilTaskEditor
{
    public partial class CilTaskEditorComponent : PlannedTaskEditorBase.EditorBase
    {
        public CilTaskEditorComponent() : base(TaskType.CilTask)
        {
        }
        
        public override async Task SaveTask()
        {
            await PlannedStopService.SaveCilTask((CilTaskDto)EditedTask);
            await ModalInstance.CloseAsync(ModalResult.Ok(EditedTask));
        }
        
    }
}
