using DTOs;
using Microsoft.AspNetCore.Components;
using ValueStreamService;

namespace ValueStreamPicker;

public partial class ValueStreamPickerComponent
    {
    //Services
    [Inject] public IValueStreamService ValueStreamService { get; set; } = null!;
        [Parameter] public bool CanBeEmpty { get; set; }

        [Parameter]
        public ValueStreamDto? SelectedValueStream { get; set; }

        [Parameter]
        public EventCallback<ValueStreamDto?> OnValueStreamSelected { get; set; }

        private async Task SetValueStream(ChangeEventArgs args)
        {
            var check = int.TryParse(args.Value?.ToString(), out var id);
            await InternalSet(check? ValueStreams.FirstOrDefault(x => x.Id == id) : null);
         }

        private async Task InternalSet(ValueStreamDto? vs)
        {
            SelectedValueStream = vs;
            await OnValueStreamSelected.InvokeAsync(SelectedValueStream);
    }

        public async Task ResetSelected()
        {
            await InternalSet(null);
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            parameters.SetParameterProperties(this);
            await base.SetParametersAsync(parameters);
             if (_oldPlantId != PlantId)
             {
                _oldPlantId = PlantId;
                Disabled = true;
                StateHasChanged();
                ValueStreams.Clear();
                if(PlantId != null)
                    ValueStreams.AddRange(await ValueStreamService.GetValueStreams((int)PlantId));
                Disabled = false;
                if(ValueStreams.All(x => x.Id != SelectedValueStreamHandler))
                    await InternalSet(null);
                StateHasChanged();
             }
        }



        private int? SelectedValueStreamHandler => SelectedValueStream?.Id;

        public bool IsBusy { get; set; }
        public string Placeholder => IsBusy ? "placeholder" : "";

        private int? _oldPlantId;

        [Parameter]
        public int? PlantId { get; set; }

       public List<ValueStreamDto> ValueStreams { get; set; } = new();

        private bool Disabled { get; set; }

    }

