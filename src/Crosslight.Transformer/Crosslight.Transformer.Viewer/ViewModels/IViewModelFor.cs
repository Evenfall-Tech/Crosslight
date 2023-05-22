using Crosslight.Transformer.Viewer.Models;

namespace Crosslight.Transformer.Viewer.ViewModels
{
    public interface IViewModelFor<UM> where UM : ModelBase
    {
        bool IsViewModelOf(UM model);
        UM Model { get; }
    }
}
