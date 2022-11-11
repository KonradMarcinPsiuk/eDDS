using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazored.Modal.Services;
using DTOs;
using PlannedTaskEditorBase;

namespace ClTaskEditor
{
    public partial class ClTaskEditorComponent:PlannedTaskEditorBase.EditorBase
    {

        public ClTaskEditorComponent():base(TaskType.ClTask)
        {
            
        }
        
     
        public override async Task SaveTask()
        {
            await PlannedStopService.SaveClTask(EditedTask as ClTaskDto);
            await ModalInstance.CloseAsync(ModalResult.Ok(EditedTask));
        }
    
    }
}
