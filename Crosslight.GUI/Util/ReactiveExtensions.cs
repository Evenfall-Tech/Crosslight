using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;

namespace Crosslight.GUI.Util
{
    public static class ReactiveUIExtensions
    {
        public static IObservable<bool> ExecuteIfPossible<TParam, TResult>(
            this ReactiveCommand<TParam, TResult> cmd, 
            TParam param = default
        ) =>
            cmd.CanExecute.FirstAsync().Where(can => can).Do(async _ => await cmd.Execute(param));
    }
}
