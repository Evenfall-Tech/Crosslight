//////////////////////////////////////////////
// Apache 2.0  - 2017-2019
// Author : Derek Tremblay (derektremblay666@gmail.com)
//////////////////////////////////////////////

using Crosslight.Common.UI.Controls.HexEditorControl.Core.Bytes;

namespace Crosslight.Common.UI.Controls.HexEditorControl.Core.Interfaces
{
    public interface IByteModified
    {
        //Properties
        ByteAction Action { get; set; }

        byte? Byte { get; set; }
        long BytePositionInStream { get; set; }
        bool IsValid { get; }
        long Length { get; set; }

        //Methods
        void Clear();

        ByteModified GetCopy();
        string ToString();
    }
}
