using Blazored.Modal;
using Blazored.Modal.Services;
using DailyPlanService;
using DefectService;
using DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PlannedStopService;
using ProductionLineService;
using TinyHelpers.Extensions;

namespace DailyPlanEditor
{
    public partial class DailyPlanEditorComponent
    {
        public DailyPlanDto? EditedPlan { get; set; }

        [Parameter]
        public int? DailyPlanId { get; set; }
        [Parameter]
        public int? ProductionLineId { get; set; }
        [Parameter]
        public int? DepartmentId { get; set; }

        [Parameter]
        public EventCallback PlanSaved { get; set; }

        [CascadingParameter] public IModalService Modal { get; set; } = null!;

        [Inject] public IDailyPlanService DailyPlanService { get; set; } = null!;
        [Inject] public IDefectService DefectService { get; set; } = null!;
        [Inject] public IPlannedStopService PlannedStopService { get; set; } = null!;
        [Inject] IJSRuntime jSRuntime { get; set; } = null!;

        public List<ProductionLineDto> ProductionLines { get; set; } = new();
        [Inject] public IProductionLineService ProductionLineService { get; set; } = null!;

        [CascadingParameter] private BlazoredModalInstance ModalInstance { get; set; } = null!;

        [Parameter]
        public bool IsBusy { get; set; }





        public override async Task SetParametersAsync(ParameterView parameters)
        {
            parameters.SetParameterProperties(this);
            await base.SetParametersAsync(parameters);
            IsBusy = true;
            if (DepartmentId != null)
                ProductionLines.AddRange(await ProductionLineService.GetProductionLinesForDepartment((int)DepartmentId));
            if (DailyPlanId!=null && DailyPlanId != 0)
                EditedPlan = await DailyPlanService.GetDailyPlan((int)DailyPlanId);
            else
            {
                if (ProductionLineId != null)
                {
                    EditedPlan = new DailyPlanDto()
                    {
                        StartDateDt = DateTime.Now,
                        ProductionLineId = (int)ProductionLineId,
                    };
                    EditedPlan.Initialize();
                }
            }
            IsBusy = false;
            StateHasChanged();

        }

        public async Task SavePlan()
        {
            if (EditedPlan != null)
            {
                IsBusy = true;
                await DailyPlanService.SavePlan(EditedPlan);
                await PlanSaved.InvokeAsync();
                IsBusy = false;
            }

        }


        public async Task AddDefect()
        {
            var modal =
                Modal.Show<DefectPicker.DefectPickerComponent>("Plan editor",
                   modalParam(), modalOpt());

            var result = await modal.Result;
            if (!result.Cancelled)
            {
                var defect = await DefectService.GetDefect((Guid)result.Data);
                var task = new DailyPlanDefectTaskDto();

                task.SetLinkedDefect(defect);

                if (EditedPlan != null)
                {
                    EditedPlan.DailyPlanDefectTasks ??= new List<DailyPlanDefectTaskDto>();
                    EditedPlan.DailyPlanDefectTasks.Add(task);
                }

            }
            StateHasChanged();

        }

        private ModalParameters modalParam()
        {
            var parameters = new ModalParameters();
            parameters.Add("SelectedLine", ProductionLineId);
            return parameters;
        }

        private ModalOptions modalOpt()
        {
            return new ModalOptions
            {
                DisableBackgroundCancel = true,
                HideCloseButton = true,
                HideHeader = true,
                Animation = ModalAnimation.FadeInOut(0.2),


            };
        }

        private async Task AddPm(PmTaskDto t)
        {
            var pmTask = await PlannedStopService.GetPmTask(t.Id);
            var task = new DailyPlanPmTaskDto();

            task.SetLinkedTask(pmTask);

            if (EditedPlan != null)
            {
                EditedPlan.DailyPlanPmTasks ??= new List<DailyPlanPmTaskDto>();
                EditedPlan.DailyPlanPmTasks.Add(task);
            }
        }

        private async Task AddCil(CilTaskDto t)
        {
            var cilTask = await PlannedStopService.GetCilTask(t.Id);
            var task = new DailyPlanCilTaskDto();

            task.SetLinkedTask(cilTask);

            if (EditedPlan != null)
            {
                EditedPlan.DailyPlanCilTasks ??= new List<DailyPlanCilTaskDto>();
                EditedPlan.DailyPlanCilTasks.Add(task);
            }
        }

        private async Task AddCl(ClTaskDto t)
        {
            var clTask = await PlannedStopService.GetClTask(t.Id);
            var task = new DailyPlanClTaskDto();

            task.SetLinkedTask(clTask);

            if (EditedPlan != null)
            {
                EditedPlan.DailyPlanClTasks ??= new List<DailyPlanClTaskDto>();
                EditedPlan.DailyPlanClTasks.Add(task);
            }
        }

        private async Task AddOther(OtherTaskDto t)
        {
            var otherTask = await PlannedStopService.GetOtherTask(t.Id);
            var task = new DailyPlanOtherTaskDto();

            task.SetLinkedTask(otherTask);

            if (EditedPlan != null)
            {
                EditedPlan.DailyPlanOtherTasks ??= new List<DailyPlanOtherTaskDto>();
                EditedPlan.DailyPlanOtherTasks.Add(task);
            }
        }


        public async Task AddPmTask()
        {
            var modal =
              Modal.Show<PmTaskEditor.PmTaskEditorComponent>("Plan editor",
                  modalParam(), modalOpt());

            var result = await modal.Result;
            if (!result.Cancelled && result.Data is PmTaskDto) await AddPm((PmTaskDto)result.Data);

            StateHasChanged();
        }

        public async Task AddCilTask()
        {
            var modal =
              Modal.Show<CilTaskEditor.CilTaskEditorComponent>("Plan editor",
                  modalParam(), modalOpt());

            var result = await modal.Result;
            if (!result.Cancelled && result.Data is CilTaskDto) await AddCil((CilTaskDto)result.Data);

            StateHasChanged();
        }

        public async Task AddClTask()
        {
            var modal =
              Modal.Show<ClTaskEditor.ClTaskEditorComponent>("Plan editor",
                  modalParam(), modalOpt());

            var result = await modal.Result;
            if (!result.Cancelled && result.Data is ClTaskDto) await AddCl((ClTaskDto)result.Data);

            StateHasChanged();
        }

        public async Task AddOtherTask()
        {
            var modal =
              Modal.Show<OtherTaskEditor.OtherTaskEditorComponent>("Plan editor",
                  modalParam(), modalOpt());

            var result = await modal.Result;
            if (!result.Cancelled && result.Data is OtherTaskDto) await AddOther((OtherTaskDto)result.Data);

            StateHasChanged();
        }

        public async Task AddExistingTask()
        {


            var modal =
              Modal.Show<PlannedStopPicker.PlannedStopPickerComponent>("Plan editor",
                   modalParam(), modalOpt());

            var result = await modal.Result;
            if (!result.Cancelled)
            {
                if (result.Data is PmTaskDto) await AddPm((PmTaskDto)result.Data);
                if (result.Data is CilTaskDto) await AddCil((CilTaskDto)result.Data);
                if (result.Data is ClTaskDto) await AddCl((ClTaskDto)result.Data);
                if (result.Data is OtherTaskDto) await AddOther((OtherTaskDto)result.Data);
                StateHasChanged();
            }
  
        }

        string _menuCss = "collapse";
        private void ToggleMenu()
        {

            if (string.IsNullOrEmpty(_menuCss))
                _menuCss = "collapse";
            else
                _menuCss = "";
        }

        string rowBackgroudClass(bool status) => status ? "table-success" : "";
        string cellBackgroudClass(bool status) => status ? "table-success" : "table-warning";

        private async Task<bool> Prompt(string message)
        {
            return await jSRuntime.InvokeAsync<bool>("jsPro", message);
        }

        private async Task ChangeUnderlyingTask(object args, ChangeEventArgs x)
        {
            if (args is IPlannedTask)
            {
                var t = args as IPlannedTask;
                if (x.Value is true && !t.IsClosed)
                {
                    var r = await Prompt("Work done! Nice! This part of the plan is done but the underlying task is still open. Do You want to close it now?");
                    if (r)
                    {
                        t.CloseTask();
                        await SaveTask(t);
                    }
                }

                if (x.Value is false && t.IsClosed)
                {
                    var r = await Prompt("Looks like this part of the plan is not quiet there yet but the underlying task is closed. Do You want to re-open underlying task also?");
                    if (r)
                    {
                        t.OpenTask();
                        await SaveTask(t);
                    }
                }
            }
        }

        private async Task SaveTask(IPlannedTask task)
        {
            if (task is DefectDto) await DefectService.SaveDefect((DefectDto)task);
            if (task is PmTaskDto) await    PlannedStopService.SavePmTask((PmTaskDto)task);
            if (task is CilTaskDto) await   PlannedStopService.SaveCilTask((CilTaskDto)task);
            if (task is ClTaskDto) await    PlannedStopService.SaveClTask((ClTaskDto)task);
            if (task is OtherTaskDto) await PlannedStopService.SaveOtherTask((OtherTaskDto)task);

        }

        private async Task<bool> DeleteTaskCheck()
        {
            return await jSRuntime.InvokeAsync<bool>("jsPro", "Do You want to delete this task?");
        }

    }
}
