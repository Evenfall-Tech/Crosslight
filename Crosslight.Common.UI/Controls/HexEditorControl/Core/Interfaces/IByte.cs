//////////////////////////////////////////////
// Apache 2.0  - 2020
// Base author  : ehsan69h
//////////////////////////////////////////////

using Avalonia.Input;
using System.Collections.Generic;

namespace Crosslight.Common.UI.Controls.HexEditorControl.Core.Interfaces
{
    public delegate void D_ByteListProp(List<byte> newValue, int index);

    interface IByte
    {
        List<byte> Byte { get; set; }
        List<byte> OriginByte { get; set; }

        string GetText(DataVisualType type, DataVisualState state, ByteOrderType order);

        D_ByteListProp del_ByteOnChange { get; set; }

        bool IsEqual(byte[] bytes);

        (ByteAction, bool) Update(DataVisualType type, Key _key, ByteOrderType byteOrder, ref KeyDownLabel _keyDownLabel);

        void ChangeByteValue(byte newValue, long position);

    }
}
