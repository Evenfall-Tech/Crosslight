using Crosslight.Viewer.Models;

namespace Crosslight.Viewer.ViewModels
{
    public interface IViewModelFor<UM> where UM : ModelBase
    {
        bool IsViewModelOf(UM model);
        UM Model { get; }
    }
}
