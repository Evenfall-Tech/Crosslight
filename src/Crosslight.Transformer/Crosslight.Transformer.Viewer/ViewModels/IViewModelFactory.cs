using Crosslight.Transformer.Viewer.Models;

namespace Crosslight.Transformer.Viewer.ViewModels
{
    public interface IViewModelFactory<TVM, UM> where TVM : ViewModelBase, IViewModelFor<UM> where UM : ModelBase
    {
        TVM Get(UM model);
    }
}
