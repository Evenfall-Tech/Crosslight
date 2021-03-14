using Crosslight.Language.Viewer.Models;

namespace Crosslight.Language.Viewer.ViewModels
{
    public interface IViewModelFor<UM> where UM : ModelBase
    {
        bool IsViewModelOf(UM model);
        UM Model { get; }
    }
}
