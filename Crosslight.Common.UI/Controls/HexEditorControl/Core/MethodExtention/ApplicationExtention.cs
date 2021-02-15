﻿//////////////////////////////////////////////
// Apache 2.0  - 2016-2020
// Author : Derek Tremblay (derektremblay666@gmail.com)
//////////////////////////////////////////////

using Avalonia;
using Avalonia.Threading;

namespace Crosslight.Common.UI.Controls.HexEditorControl.Core.MethodExtention
{
    /// <summary>
    /// DoEvents when control is in long task. Control do not freeze the dispatcher.
    /// </summary>
    public static class ApplicationExtention
    {
        //private static readonly DispatcherOperationCallback ExitFrameCallback = ExitFrame;

        public static void DoEvents(this Application _, DispatcherPriority priority = DispatcherPriority.Background)
        {
            Dispatcher.UIThread.RunJobs(priority);
            //var nestedFrame = new DispatcherFrame();
            //var exitOperation = Dispatcher.UIThread.BeginInvoke(priority, ExitFrameCallback, nestedFrame);

            //try
            //{
            //    //execute all next message
            //    Dispatcher.PushFrame(nestedFrame);

            //    //If not completed, will stop it
            //    if (exitOperation.Status != DispatcherOperationStatus.Completed)
            //        exitOperation.Abort();
            //}
            //catch
            //{
            //    exitOperation.Abort();
            //}
        }

        //private static object ExitFrame(object f)
        //{
        //    (f as DispatcherFrame).Continue = false;
        //    return null;
        //}
    }
}
