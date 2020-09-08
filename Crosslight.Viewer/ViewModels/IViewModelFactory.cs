using Crosslight.Viewer.Models;

namespace Crosslight.Viewer.ViewModels
{
    public interface IViewModelFactory<TVM, UM> where TVM : ViewModelBase, IViewModelFor<UM> where UM : ModelBase
    {
        TVM Get(UM model);
    }
}
