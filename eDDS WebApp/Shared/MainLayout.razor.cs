using AllLevelPicker;
using Blazored.Modal;
using Blazored.Modal.Services;
using DatePicker;
using Microsoft.AspNetCore.Components;

namespace eDDS.WebTool.Shared;

public partial class MainLayout
{
    //Modal
    [CascadingParameter] public IModalService Modal { get; set; } = null!;

    //Singletons
    [Inject] public LevelNavigationObject LevelNavigation { get; set; } = null!;

    [Inject] public DateNavigationObject DateNavigation { get; set; } = null!;


    private string SelectedLevel
    {
        get
        {
            var selectedLevel = "Select plant model";
            if (LevelNavigation.SelectedPlant == null) return selectedLevel;

            selectedLevel = LevelNavigation.SelectedPlant.PlantName;

            if (LevelNavigation.SelectedValueStream != null)
                selectedLevel = $@"{selectedLevel} > {LevelNavigation.SelectedValueStream.ValueStreamName}";

            if (LevelNavigation.SelectedDepartment != null)
                selectedLevel = $@"{selectedLevel} > {LevelNavigation.SelectedDepartment.DepartmentName}";

            if (LevelNavigation.SelectedProductionLine != null)
                selectedLevel = $@"{selectedLevel} > {LevelNavigation.SelectedProductionLine.LineName}";

            if (LevelNavigation.SelectedArea != null)
                selectedLevel = $@"{selectedLevel} > {LevelNavigation.SelectedArea.AreaName}";
            return selectedLevel;
        }
    }

    private async Task OpenLevelSelectModal()
    {
        var parameters = new ModalParameters();
        parameters.Add("SelectedPlant", LevelNavigation.SelectedPlant);
        parameters.Add("SelectedValueStream", LevelNavigation.SelectedValueStream);
        parameters.Add("SelectedDepartment", LevelNavigation.SelectedDepartment);
        parameters.Add("SelectedProductionLine", LevelNavigation.SelectedProductionLine);
        parameters.Add("SelectedLineArea", LevelNavigation.SelectedArea);

        var options = new ModalOptions();
        options.DisableBackgroundCancel = true;
        options.HideCloseButton = true;
        options.HideHeader = true;
        options.Animation = ModalAnimation.FadeInOut(0.2);
        var modal = Modal
            .Show<AllLevelPickerComponent>("", parameters, options);
        var result = await modal.Result;
        var temp =
            (LevelNavigationObject) result
                .Data;
        LevelNavigation.SelectedPlant = temp.SelectedPlant;
        LevelNavigation.SelectedValueStream = temp.SelectedValueStream;
        LevelNavigation.SelectedDepartment = temp.SelectedDepartment;
        LevelNavigation.SelectedProductionLine = temp.SelectedProductionLine;
        LevelNavigation.SelectedArea = temp.SelectedArea;
        LevelNavigation.InvokeEvent();
    }

    private async Task OpenDateSelectModal()
    {
        var parameters = new ModalParameters();
        parameters.Add("SelectedDate", DateNavigation.SelectedDate);
        var options = new ModalOptions();
        options.DisableBackgroundCancel = true;
        options.HideCloseButton = true;
        options.HideHeader = true;
        options.Animation = ModalAnimation.FadeInOut(0.2);
        var modal = Modal
            .Show<DatePickerComponent>("", parameters, options);
        var result = await modal.Result;
        if (result.Data is DateOnly d)
        {
            DateNavigation.SelectedDate = d;
            DateNavigation.InvokeEvent();
        }
    }
}