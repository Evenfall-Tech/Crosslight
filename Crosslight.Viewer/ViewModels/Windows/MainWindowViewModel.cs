﻿using Crosslight.Viewer.ViewModels.Viewports;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace Crosslight.Viewer.ViewModels.Windows
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ViewerViewportViewModel viewportViewModel;
        public ViewerViewportViewModel ViewportViewModel
        {
            get => viewportViewModel;
            set => this.RaiseAndSetIfChanged(ref viewportViewModel, value);
        }
    }
}
