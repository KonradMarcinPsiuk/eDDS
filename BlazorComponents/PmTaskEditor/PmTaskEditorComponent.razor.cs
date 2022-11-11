using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazored.Modal.Services;
using DTOs;
using PlannedTaskEditorBase;

namespace PmTaskEditor
{
    public partial class PmTaskEditorComponent:PlannedTaskEditorBase.EditorBase
    {
        public PmTaskEditorComponent():base(TaskType.PmTask)
        {
            
        }
        public override async Task SaveTask()
        {
            await PlannedStopService.SavePmTask(EditedTask as PmTaskDto);
            await ModalInstance.CloseAsync(ModalResult.Ok(EditedTask));
        }
    }
}
