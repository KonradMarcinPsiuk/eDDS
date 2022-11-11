using Blazored.Modal;
using Blazored.Modal.Services;
using DTOs;
using Microsoft.AspNetCore.Components;

namespace AllLevelPicker
{
    public partial class AllLevelPickerComponent
    {

        //Selected
        [Parameter]public PlantDto? SelectedPlant { get; set; }
        [Parameter]public ValueStreamDto? SelectedValueStream { get; set; }
        [Parameter] public DepartmentDto? SelectedDepartment { get; set; }
        [Parameter]public ProductionLineDto? SelectedProductionLine { get; set; }
        [Parameter] public LineAreaDto? SelectedLineArea { get; set; }


        //Events
        [Parameter] public EventCallback<PlantDto?> OnPlantSelected { get; set; }
        [Parameter] public EventCallback<ValueStreamDto?> OnValueStreamSelected { get; set; }
        [Parameter] public EventCallback<DepartmentDto?> OnDepartmentSelected { get; set; }
        [Parameter] public EventCallback<ProductionLineDto?> OnProductionLineSelected { get; set; }
        [Parameter] public EventCallback<LineAreaDto?> OnLineAreaSelected { get; set; }
 
        //Modal
        [CascadingParameter] BlazoredModalInstance ModalInstance { get; set; }


        private void OnPlantSelectedInternal(PlantDto? plant)
        {
            SelectedPlant = plant;
            OnPlantSelected.InvokeAsync(SelectedPlant);
            StateHasChanged();
        }

        private void OnValueStreamSelectedInternal(ValueStreamDto? vs)
        {
            SelectedValueStream = vs;
            OnValueStreamSelected.InvokeAsync(SelectedValueStream);
            StateHasChanged();
        }

        private void OnDepartmentSelectedInternal(DepartmentDto? dept)
        {
            SelectedDepartment = dept;
            OnDepartmentSelected.InvokeAsync(SelectedDepartment);
            StateHasChanged();
        }

        private void OnProductionLineSelectedInternal(ProductionLineDto? line)
        {
            SelectedProductionLine = line;
            OnProductionLineSelected.InvokeAsync(SelectedProductionLine);
            StateHasChanged();
        }

        public void OnLineAreaSelectedInternal(LineAreaDto? area)
        {
            SelectedLineArea = area;
            OnLineAreaSelected.InvokeAsync(SelectedLineArea);
            StateHasChanged();
        }

        private async Task CloseModal()
        {
            var result = new LevelNavigationObject();
            result.SelectedPlant = SelectedPlant;
            result.SelectedValueStream = SelectedValueStream;
            result.SelectedDepartment = SelectedDepartment;
            result.SelectedProductionLine = SelectedProductionLine;
            result.SelectedArea = SelectedLineArea;
            await ModalInstance.CloseAsync(ModalResult.Ok(result));
        }


    }
}

public class LevelNavigationObject
{
    public PlantDto? SelectedPlant { get; set; }
    public ValueStreamDto? SelectedValueStream { get; set; }
    public DepartmentDto? SelectedDepartment { get; set; }
    public ProductionLineDto? SelectedProductionLine { get; set; }
    public LineAreaDto? SelectedArea { get; set; }
    public event Action OnSelectionChanged = delegate { };

    public void InvokeEvent()
    {
        OnSelectionChanged?.Invoke();
    }

}
