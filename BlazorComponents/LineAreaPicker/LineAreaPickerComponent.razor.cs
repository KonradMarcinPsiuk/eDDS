using DTOs;

using Microsoft.AspNetCore.Components;

namespace LineAreaPicker;

public partial class LineAreaPickerComponent
{
    //Services
    [Inject] public LineAreaService.Service LineAreaService { get; set; } = null!;

    [Parameter] public bool CanBeEmpty { get; set; } = false;
    [Parameter]
    public LineAreaDto? SelectedLineArea
    {
        get => selectedLineArea;
        set
        {
            selectedLineArea = value;
            SelectedLineAreaId = value?.Id;
        }
    }

    [Parameter]
    public int? SelectedLineAreaId { get; set; }

    private int? _oldLine;

    [Parameter]
    public EventCallback<LineAreaDto?> OnLineAreaSelected { get; set; }

    [Parameter]
    public int? SelectedLine { get; set; }

    public int? SelectedLineAreaHandler => SelectedLineArea?.Id;

    private string _placeholder = string.Empty;
    private LineAreaDto? selectedLineArea;

    private async Task SetLineArea(ChangeEventArgs args)
    {
        var check = int.TryParse(args.Value?.ToString(), out var id);
        await InternalSet(check ? LineAreas.FirstOrDefault(x => x.Id == id) : null);
    }

    private async Task Reset()
    {
        await InternalSet(null);
    }

    private async Task InternalSet(LineAreaDto? area)
    {
        SelectedLineArea = area;
        await OnLineAreaSelected.InvokeAsync(SelectedLineArea);
    }

    public async Task ResetSelected()
    {
        await InternalSet(null);
    }


    protected override async Task OnInitializedAsync()
    {
        if (_oldLine != SelectedLine)
        {
            Disabled = true;

            StateHasChanged();
            _oldLine = SelectedLine;
            LineAreas.Clear();
            if (SelectedLine != null)
                LineAreas.AddRange(await LineAreaService.GetLineAreasForProductionLine((int)SelectedLine));
            Disabled = false;
            if (LineAreas.All(x => x.Id != SelectedLineAreaHandler))
                await InternalSet(null);
            StateHasChanged();
        }
        await base.OnInitializedAsync();
    }

    public List<LineAreaDto> LineAreas { get; set; } = new();
    private bool Disabled { get; set; }
}