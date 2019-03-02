using System;

namespace PhySim2D.UI.Views
{
    [Flags]
    internal enum DebugViewFlags
    {
    	AABB = 1 << 0, // 00001 == 1
        SHAPE = 1 << 1, // 00010 == 2
        POSITION = 1 << 2, // 00100 == 4
        CENTER_OF_MASS = 1 << 3, // 01000 == 8
        FORCES = 1 << 4, // 10000 == 16
        CONTACTS_POINT = 1 << 5,
        CONTACTS_NORMAL = 1 << 6,
        CONTACTS_PAIR = 1 << 7,
        AXIS = 1 << 8
    }
}
