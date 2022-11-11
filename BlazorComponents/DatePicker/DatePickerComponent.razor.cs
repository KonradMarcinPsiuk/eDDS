using System.ComponentModel.Design;
using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;

namespace DatePicker
{
    public partial class DatePickerComponent
    {
        [Parameter]
        public DateOnly SelectedDate { get; set; }
        
        [CascadingParameter] private BlazoredModalInstance ModalInstance { get; set; } = null!;

        [Parameter] public EventCallback<DateOnly?> OnDateChanged { get; set; }

        private async Task SelectDate(ChangeEventArgs e)
        {
            var check = DateTime.TryParse(e.Value.ToString(), out var d);
            if (!check) return;
            SelectedDateAsDt = d;
           
            
        }

        private DateTime SelectedDateAsDt
        {
            get => new (SelectedDate.Year, SelectedDate.Month, SelectedDate.Day);
            set => SelectedDate = DateOnly.FromDateTime(value);
        }

        private async Task CloseModal()
        {
            await OnDateChanged.InvokeAsync(SelectedDate);
            await ModalInstance.CloseAsync(ModalResult.Ok(SelectedDate));
        }

    }

    public class DateNavigationObject
    {
        public DateOnly SelectedDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public event Action OnSelectionChanged = delegate { };

        public void InvokeEvent()
        {
            OnSelectionChanged?.Invoke();
        }
    }
}
