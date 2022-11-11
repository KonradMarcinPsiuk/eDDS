using DTOs;
using Microsoft.AspNetCore.Components;
using PeopleService;

namespace PersonPicker;

public partial class PersonPickerComponent
{
    private bool _alreadyLoaded = false;
    private string _placeholder = string.Empty;

    public List<PersonDto> People = new();

    [Parameter] public int? DepartmentId { get; set; }

    [Parameter] public bool CanBeEmpty { get; set; } = false;

    public int? SelectedPersonHandler => SelectedPerson?.Id;

    private bool Disabled { get; set; }

    [Parameter] public EventCallback<PersonDto?> OnPersonSelected { get; set; }

    [Parameter] public bool IsBusy { get; set; }


    [Inject] public IPeopleService PeopleService { get; set; } = null!;

    [Parameter] public PersonDto? SelectedPerson { get; set; }

    [Parameter] public bool LabelVisible { get; set; } = true;

    private async Task SetPerson(ChangeEventArgs args)
    {
        var check = int.TryParse(args.Value?.ToString(), out var id);
        await InternalSet(check ? People.FirstOrDefault(x => x.Id == id) : null);
    }

    private async Task InternalSet(PersonDto? person)
    {
        SelectedPerson = person;
        await OnPersonSelected.InvokeAsync(SelectedPerson);
    }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        if (!_alreadyLoaded && DepartmentId != null)
        {
            IsBusy = true;
            StateHasChanged();
            _alreadyLoaded = true;
            People.AddRange(await PeopleService.GetPeopleForDepartment((int) DepartmentId));
            IsBusy = false;
        }

        StateHasChanged();
    }
}