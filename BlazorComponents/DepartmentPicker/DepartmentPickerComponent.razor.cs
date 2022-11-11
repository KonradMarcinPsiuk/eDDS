using DepartmentService;
using DTOs;

using Microsoft.AspNetCore.Components;

namespace DepartmentPicker;

public partial class DepartmentPickerComponent
{
    [Inject] public IDepartmentService DepartmentService { get; set; } = null!;
    [Parameter] public bool CanBeEmpty { get; set; } = false;

    [Parameter]
    public DepartmentDto? SelectedDepartment { get; set; }

    [Parameter]
    public EventCallback<DepartmentDto?> OnDepartmentSelected { get; set; }

    public int? SelectedDepartmentHandler => SelectedDepartment?.Id;

    private string _placeholder = string.Empty;

    private async Task SetDepartment(ChangeEventArgs args)
    {
        var check = int.TryParse(args.Value?.ToString(), out var id);
        await InternalSet( check ? Departments.FirstOrDefault(x => x.Id == id) : null);
    }

    private async Task InternalSet(DepartmentDto? dept)
    {
        SelectedDepartment = dept;
        await OnDepartmentSelected.InvokeAsync(SelectedDepartment);
    }

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        parameters.SetParameterProperties(this);
        await base.SetParametersAsync(parameters);
        if (_oldValueStream != ValueStreamId)
        {
           
            _oldValueStream = ValueStreamId;
            Disabled = true;
            StateHasChanged();
            Departments.Clear();
            if (ValueStreamId != null)
                Departments.AddRange(await DepartmentService.GetDepartmentsForValueStream((int)ValueStreamId));
            Disabled = false;
            if(Departments.All(x=>x.Id!=SelectedDepartmentHandler))
                 await InternalSet(null);
            StateHasChanged();
        }
       
    }

    private bool Disabled { get; set; }

    public async Task ResetSelected()
    {
        await InternalSet(null);
    }

    [Parameter]
    public int? ValueStreamId { get; set; }

    private int? _oldValueStream;

    [Parameter]
    public List<DepartmentDto> Departments { get; set; } = new();

    
}