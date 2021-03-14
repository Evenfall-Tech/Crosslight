using Crosslight.Language.Viewer.Models;

namespace Crosslight.Language.Viewer.ViewModels
{
    public interface IViewModelFactory<TVM, UM> where TVM : ViewModelBase, IViewModelFor<UM> where UM : ModelBase
    {
        TVM Get(UM model);
    }
}
