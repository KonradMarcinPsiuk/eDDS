using System.Diagnostics;
using DTOs;

using Microsoft.AspNetCore.Components;
using PlantService;

namespace PlantPicker
{
    public partial class PlantPickerComponent
    {
        //Services
        [Inject] public IPlantService PlantService { get; set; } = null!;

        [Parameter]
        public PlantDto? SelectedPlant { get; set; }

        private bool _disabled;

        public async Task SetPlant(ChangeEventArgs args)
        {
            var check = int.TryParse(args.Value.ToString(),out var id);
            SelectedPlant = check ? Plants.FirstOrDefault(x => x.Id == id) : null;
            Debug.WriteLine("Call OnPlantSelected event");
            await OnPlantSelected.InvokeAsync(SelectedPlant);
            StateHasChanged();
        }

        private int? SelectedPlantHandler => SelectedPlant?.Id;

        public async Task SetDefault()
        {
            if (Plants.Any())
            {
                await SetPlant(new ChangeEventArgs() {Value = Plants.First().Id});
            }
        }

        protected override async Task OnInitializedAsync()
        {
            Plants.AddRange(await PlantService.GetPlants());
            await SetDefault();
            await base.OnInitializedAsync();
        }

        [Parameter]
        public EventCallback<PlantDto?> OnPlantSelected { get; set; }

        [Parameter]
        public bool IsBusy { get; set; }

        public string Placeholder => IsBusy ? "placeholder" : "";


        public List<PlantDto> Plants { get; set; } = new();

    }
}
