using DTOs;

using Microsoft.AspNetCore.Components;
using ProductionLineService;

namespace ProductionLinePicker;

public partial class ProductionLinePickerComponent

{
    [Parameter] public bool CanBeEmpty { get; set; } = false;
    [Parameter]
    public ProductionLineDto? SelectedProductionLine { get; set; }

    [Parameter]
    public EventCallback<ProductionLineDto?> OnProductionLineSelected { get; set; }

    //Services
    [Inject] public IProductionLineService ProductionLineService { get; set; } = null!;


    [Parameter]
    public int? DepartmentId { get; set; }

    public int? SelectedProductionLineHandler => SelectedProductionLine?.Id;

    private string _placeholder = string.Empty;

    private async Task SetProductionLine(ChangeEventArgs args)
    {
        var check = int.TryParse(args.Value?.ToString(), out var id);
        await InternalSet( check ? ProductionLines.FirstOrDefault(x => x.Id == id) : null);
    }

    private async Task InternalSet(ProductionLineDto? line)
    {
        SelectedProductionLine = line;
        await OnProductionLineSelected.InvokeAsync(SelectedProductionLine);
    }

    public async Task ResetSelected()
    {
        await InternalSet(null);
    }

    public List<ProductionLineDto> ProductionLines { get; set; } = new();

    private int? _oldDept;

    public override async Task SetParametersAsync(ParameterView parameters)
    {
        parameters.SetParameterProperties(this);
        await base.SetParametersAsync(parameters);
       
        if (DepartmentId != _oldDept)
        {
            _oldDept = DepartmentId;
            _disabled = true;
           
            StateHasChanged();
            ProductionLines.Clear();
            if (DepartmentId != null)
                ProductionLines.AddRange(await ProductionLineService.GetProductionLinesForDepartment((int)DepartmentId));
            
            _disabled = false;
            if(ProductionLines.All(x=>x.Id!=SelectedProductionLineHandler))
                await InternalSet(null);
            StateHasChanged();
        }
        
    }

    private bool _disabled { get; set; }
}