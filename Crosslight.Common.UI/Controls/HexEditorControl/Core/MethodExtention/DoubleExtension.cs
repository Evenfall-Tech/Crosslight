﻿//////////////////////////////////////////////
// Apache 2.0  - 2016-2020
// Author : Derek Tremblay (derektremblay666@gmail.com)
//////////////////////////////////////////////

using System;

namespace Crosslight.Common.UI.Controls.HexEditorControl.Core.MethodExtention
{
    public static class DoubleExtension
    {
        public static double Round(this double s, int digit = 2) => Math.Round(s, digit);
    }
}
