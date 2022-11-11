using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazored.Modal.Services;
using DTOs;
using PlannedTaskEditorBase;

namespace OtherTaskEditor
{
    public partial class OtherTaskEditorComponent:PlannedTaskEditorBase.EditorBase
    {
        public OtherTaskEditorComponent():base(TaskType.OtherTask)
        {
            
        }
        
     
        public override async Task SaveTask()
        {
            await PlannedStopService.SaveOtherTask(EditedTask as OtherTaskDto);
            await ModalInstance.CloseAsync(ModalResult.Ok(EditedTask));
        }
    
    }
}
