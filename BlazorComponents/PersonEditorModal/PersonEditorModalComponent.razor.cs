using Interfaces;
using Microsoft.AspNetCore.Components;
using ModalBaseLibrary;

namespace PersonEditorModal
{
    public partial class PersonEditorModalComponent<T,TDept> : ModalBaseClass  where T:class, IPerson<TDept>, IPersonEditor<TDept>, new() where TDept : class, IDepartment
    {
        [Parameter]
        public T? EditedPerson
        {
            get => _editedPerson;
            set
            {
                _editedPerson = new T();
                if(value!=null)
                    _editedPerson.EditPerson(value);
            }
        }

        private T? _editedPerson;

        [Parameter] public List<TDept> Departments { get; set; } = new();

        private List<TDept> FilteredDepartments =>
            _editedPerson!=null ? Departments.Where(x => _editedPerson.Departments.All(z => x.Id != z.Id)).ToList() : new();

        private TDept? SelectedDepartment { get; set; }

        private int? SelectedDepartmentHandler => SelectedDepartment?.Id;

        private void SetDepartment(ChangeEventArgs args)
        {
            var check = int.TryParse(args.Value?.ToString(), out var id);
            if(check)
                SelectedDepartment = Departments.FirstOrDefault(x => x.Id == id);
        }

        private async Task CloseModal()
        {
            Show = false;
            await OnSubmit.InvokeAsync(_editedPerson);
        }

        private void AddDept()
        {
            if (SelectedDepartment != null && _editedPerson != null)
            {
                _editedPerson.Departments.Add(SelectedDepartment);
                StateHasChanged();
            }

        }

        private async Task CancelMethod()
        {
            Show = false;
            await OnCancel.InvokeAsync();
        }

        private void DeleteDept(int deptId)
        {
            _editedPerson?.Departments.RemoveAll(x => x.Id == deptId);
            StateHasChanged();
        }
        
        [Parameter]
        public EventCallback<T> OnSubmit { get; set; }
        [Parameter]
        public EventCallback OnCancel { get; set; }
    }
}
