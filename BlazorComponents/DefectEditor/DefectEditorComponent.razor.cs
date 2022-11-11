using Blazored.Modal;
using Blazored.Modal.Services;
using DefectService;
using DTOs;
using Microsoft.AspNetCore.Components;


namespace DefectEditor;

public partial class DefectEditorComponent
{
   
    [Parameter]
    public Guid? DefectId { get; set; }
    [Parameter]
    public int? DepartmentId { get; set; }
    [Parameter]
    public int? SelectedLine { get; set; }
   
    
    public DefectDto? EditedDefect { get; set; }

    [CascadingParameter] private BlazoredModalInstance ModalInstance { get; set; } = null!;

    [Parameter]
    public bool IsBusy { get; set; }

    private bool PeoplePickerBusy
    {
        get; 
        set;
    }

    public PersonDto? Owner
    {
        get => EditedDefect?.Owner;
        set
        {
            if (EditedDefect is null) return;
            EditedDefect.Owner = value;
            EditedDefect.OwnerId = value?.Id;
        }
    }

    [Inject] public IDefectService DefectService { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await  base.OnInitializedAsync();
        if (!_alreadyLoaded)
        {
            _alreadyLoaded = true;
            if (DefectId != null)
                EditedDefect = await DefectService.GetDefect((Guid)DefectId);
            else
            {
                EditedDefect = new DefectDto();
                EditedDefect.SetAsNew();
            }
        }
        StateHasChanged();
    }

    private bool _alreadyLoaded = false;
    private bool _isBusy;


    private async Task SaveEdit()
    {
        if (EditedDefect != null)
            await DefectService.SaveDefect(EditedDefect);
        await ModalInstance.CloseAsync(ModalResult.Ok(EditedDefect));
    }

    private async Task CancelEdit()
    {
        await ModalInstance.CloseAsync(ModalResult.Cancel());
    }

    private void SelectLineArea(LineAreaDto? area)
    {
        if (EditedDefect != null && area!=null)
        {
            EditedDefect.LineArea = area;
            EditedDefect.LineAreaId = area.Id;
        }
    }

    private async Task StatusChanged(int value, DefectDto defect)
    {
      
           if (value == 1 || value == 3)
               EditedDefect?.OpenTask();
           if (value == 2)
               EditedDefect?.CloseTask();
       
    }
}