using System;
using System.Runtime.InteropServices;

namespace ShortDev.Windows.DefaultApps.Internal
{
    [Guid("b00dc47b-bff4-4318-be5b-a5856f9169a2")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface ISetAppAsDefaultBrowser
    {
        [PreserveSig]
        int SetAppAsDefaultBrowser([MarshalAs(UnmanagedType.LPWStr)] string association, [MarshalAs(UnmanagedType.LPWStr)] string appId, AssociationType type);
    }
}
