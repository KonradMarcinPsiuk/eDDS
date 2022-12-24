using Blazored.Modal;
using Blazored.Modal.Services;
using DefectEditor;
using DTOs;
using Microsoft.AspNetCore.Components;
using PeopleService;
using PersonEditorModal;

namespace eDDS.WebTool.Pages;

public  partial class PeoplesManager : IDisposable
{
    [Inject] private IPeopleService _peopleService { get; set; }
    
    [Inject] public LevelNavigationObject LevelNavigation { get; set; } = null!;
    [CascadingParameter] public IModalService Modal { get; set; } = null!;


    private List<PersonDto> people = new();
    private bool _isLoading = true;

    protected override async Task OnInitializedAsync()
    {
        LevelNavigation.OnSelectionChanged += LevelNavigationOnOnSelectionChanged;
        if (_isLoading && LevelNavigation.SelectedPlant!=null)
        {
            _isLoading = false;
            people.AddRange(await _peopleService.GetPeopleForPlant(LevelNavigation.SelectedPlant.Id));
        }
        await base.OnInitializedAsync();
    }

    private async void LevelNavigationOnOnSelectionChanged()
    {
        if (LevelNavigation.SelectedPlant!=null)
        {
            people.Clear();
            people.AddRange(await _peopleService.GetPeopleForPlant(LevelNavigation.SelectedPlant.Id));
            StateHasChanged();
        }
    }

    public void Dispose()
    {
        LevelNavigation.OnSelectionChanged -= LevelNavigationOnOnSelectionChanged;
    }

    private void DeleteDepartment(int deptId, int personId)
    {
        
    }

    private async Task NewPerson()
    {
       
            var parameters = new ModalParameters();
           

            var options = new ModalOptions
            {
                DisableBackgroundCancel = true,
                HideCloseButton = true,
                HideHeader = true
            };

            var modal = Modal
                .Show<PersonEditorModalComponent>("Person account editor", parameters, options);

            var r = await modal.Result;
        
        
    }
}