using Blazored.Modal;
using Blazored.Modal.Services;
using CilTaskEditor;
using ClTaskEditor;
using DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using OtherTaskEditor;
using PlannedStopService;
using PmTaskEditor;

namespace eDDS.WebTool.Pages;

public partial class PlannedTasksList : IDisposable
{
    private readonly List<CilTaskDto> CilTasks = new();
    private readonly List<ClTaskDto> ClTasks = new();

    private readonly bool openOnly = true;
    private readonly List<OtherTaskDto> OtherTasks = new();


    private readonly List<PmTaskDto> PmTasks = new();
    [Inject] private IPlannedStopService PlannedStopService { get; set; } = null!;

    [Inject] private LevelNavigationObject LevelNavigationObject { get; set; } = null!;
    [Inject] private IJSRuntime JSRuntime { get; set; }
    [CascadingParameter] public IModalService Modal { get; set; } = null!;

    public void Dispose()
    {
        LevelNavigationObject.OnSelectionChanged -= LevelNavigationObject_OnSelectionChanged;
    }

    protected override async Task OnInitializedAsync()
    {
        LevelNavigationObject.OnSelectionChanged += LevelNavigationObject_OnSelectionChanged;
        await OnProductionLineSelected();
        await base.OnInitializedAsync();
    }

    private async void LevelNavigationObject_OnSelectionChanged()
    {
        await InvokeAsync(async () => { await OnProductionLineSelected(); });
    }

    private async Task OnProductionLineSelected()
    {
        PmTasks.Clear();
        CilTasks.Clear();
        ClTasks.Clear();
        OtherTasks.Clear();

        if (LevelNavigationObject.SelectedProductionLine != null)
        {
            PmTasks.AddRange(await PlannedStopService.GetPmTasksForLine(LevelNavigationObject.SelectedProductionLine.Id,
                openOnly));
            CilTasks.AddRange(
                await PlannedStopService.GetCilTasksForLine(LevelNavigationObject.SelectedProductionLine.Id, openOnly));
            ClTasks.AddRange(await PlannedStopService.GetClTasksForLine(LevelNavigationObject.SelectedProductionLine.Id,
                openOnly));
            OtherTasks.AddRange(
                await PlannedStopService.GetOtherTasksForLine(LevelNavigationObject.SelectedProductionLine.Id,
                    openOnly));
        }

        StateHasChanged();
    }

    private async Task<bool> DeleteCheck()
    {
        return await JSRuntime.InvokeAsync<bool>("jsPro", "Are You sure You want to permanently delete this task?");
    }

    private async Task LogNewPmTask()
    {
        if (LevelNavigationObject.SelectedProductionLine != null) await EditTask(null, TaskType.PmTask);
    }

    private async Task LogNewCilTask()
    {
        if (LevelNavigationObject.SelectedProductionLine != null) await EditTask(null, TaskType.CilTask);
    }

    private async Task LogNewClTask()
    {
        if (LevelNavigationObject.SelectedProductionLine != null) await EditTask(null, TaskType.ClTask);
    }

    private async Task LogNewOtherTask()
    {
        if (LevelNavigationObject.SelectedProductionLine != null) await EditTask(null, TaskType.OtherTask);
    }


    private async Task EditTask(Guid? id, TaskType type)
    {
        if (LevelNavigationObject.SelectedDepartment != null && LevelNavigationObject.SelectedProductionLine != null)
        {
            var parameters = new ModalParameters();
            parameters.Add("TaskId", id);
            parameters.Add("SelectedLine", LevelNavigationObject.SelectedProductionLine.Id);

            var options = new ModalOptions
            {
                DisableBackgroundCancel = true,
                HideCloseButton = true,
                HideHeader = true
            };

            IModalReference? modal = null;

            switch (type)
            {
                case TaskType.PmTask:
                    modal = Modal
                        .Show<PmTaskEditorComponent>("Pm Editor", parameters, options);
                    break;

                case TaskType.CilTask:
                    modal = Modal
                        .Show<CilTaskEditorComponent>("Cil editor", parameters, options);
                    break;

                case TaskType.ClTask:
                    modal = Modal
                        .Show<ClTaskEditorComponent>("Cl editor", parameters, options);
                    break;

                case TaskType.OtherTask:
                    modal = Modal
                        .Show<OtherTaskEditorComponent>("Other editor", parameters, options);
                    break;
            }

            if (modal != null)
            {
                var r = await modal.Result;
                if (!r.Cancelled)
                    await OnProductionLineSelected();
            }
        }
    }
}

internal enum TaskType
{
    PmTask,
    CilTask,
    ClTask,
    OtherTask
}