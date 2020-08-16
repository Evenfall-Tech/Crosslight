using Crosslight.Viewer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crosslight.Viewer.ViewModels
{
    public interface IViewModelFactory<TVM, UM> where TVM : ViewModelBase, IViewModelFor<UM> where UM : ModelBase
    {
        TVM Get(UM model);
    }
}
