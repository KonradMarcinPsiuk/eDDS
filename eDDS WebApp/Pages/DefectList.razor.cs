using Blazored.Modal;
using Blazored.Modal.Services;
using DefectEditor;
using DefectService;
using DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace eDDS.WebTool.Pages;

public partial class DefectList : IDisposable
{
    private string status = "open";

    //Lists
    public List<DefectDto> Defects { get; set; } = new();

    public List<DefectDto> FilteredDefects
    {
        get
        {
            return LevelNavigationObject.SelectedArea != null
                ? Defects.Where(x => x.LineAreaId == LevelNavigationObject.SelectedArea.Id).ToList()
                : Defects;
        }
    }


    //Services
    [Inject] public IDefectService DefectService { get; set; } = null!;

    //Modal
    [CascadingParameter] public IModalService Modal { get; set; } = null!;

    //Singletons
    [Inject] public LevelNavigationObject LevelNavigationObject { get; set; } = null!;

    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;

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

    private async Task OpenSelectionChanged(ChangeEventArgs args)
    {
        if (args.Value != null)
        {
            status = args.Value.ToString();
            await OnProductionLineSelected();
        }
    }

    private async Task OnProductionLineSelected()
    {
        Defects.Clear();
        if (LevelNavigationObject.SelectedProductionLine != null)
            Defects.AddRange(
                await DefectService.GetDefectsForProductionLine(LevelNavigationObject.SelectedProductionLine.Id,
                    status));
        StateHasChanged();
    }

    private async Task LogNewDefect()
    {
        if (LevelNavigationObject.SelectedProductionLine != null) await EditDefect(null);
    }

    private async Task EditDefect(Guid? id)
    {
        if (LevelNavigationObject.SelectedDepartment != null && LevelNavigationObject.SelectedProductionLine != null)
        {
            var parameters = new ModalParameters();
            parameters.Add("DefectId", id);
            parameters.Add("DepartmentId", LevelNavigationObject.SelectedDepartment.Id);
            parameters.Add("SelectedLine", LevelNavigationObject.SelectedProductionLine.Id);

            var options = new ModalOptions
            {
                DisableBackgroundCancel = true,
                HideCloseButton = true,
                HideHeader = true
            };

            var modal = Modal
                .Show<DefectEditorComponent>("Defect editor", parameters, options);

            var r = await modal.Result;
            if (!r.Cancelled)
                await OnProductionLineSelected();
        }
    }

    private async Task<bool> DeleteCheck()
    {
        return await JsRuntime.InvokeAsync<bool>("jsPro", "Are You sure You want to permanently delete this defect?");
    }
}