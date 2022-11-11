using Microsoft.AspNetCore.Components;

namespace ModalBaseLibrary;

public class ModalBaseClass:ComponentBase 
{
    private bool _show;

    [Parameter]
    public bool Show
    {
        get => _show;
        set
        {
            _show = value;
            HandleModal();
        }
    }

    [Parameter]
    public EventCallback ModalClosed { get; set; }

    public  bool Fade { get; set; }

    public bool ModalRender
    {
        get;
        set;
    }
    
    public async Task HandleModal()
    {
        if (Show)
        {
            ModalRender = true;
            await Task.Delay(10);
            Fade = true;
            StateHasChanged();
        }

        if (!Show)
        {
            Fade = false;
            await Task.Delay(10);
            ModalRender = false;
            await ModalClosed.InvokeAsync();
            StateHasChanged();
        }

        
    }

}