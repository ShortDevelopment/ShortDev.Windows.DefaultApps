using ShortDev.Windows.DefaultApps.Internal;
using System;
using System.Runtime.InteropServices;

namespace ShortDev.Windows.DefaultApps.Browser
{
    public static class BrowserSettings
    {
        static IApplicationAssociationRegistrationInternal? registrationManagerInternal;
        static ISetAppAsDefaultBrowser? defaultBrowserManager;

        static BrowserSettings()
        {
            object appRegistration = Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid(AppAssociation.CLSID_Application_Registration)));
            registrationManagerInternal = appRegistration as IApplicationAssociationRegistrationInternal;
            defaultBrowserManager = appRegistration as ISetAppAsDefaultBrowser;
        }

        // MSEdgeHTM // MSEdgeHTML // MSEdgeBHTML //

        /// <summary>
        /// <c>HKEY_CURRENT_USER\SOFTWARE\RegisteredApplications</c> <br/>
        /// <c>HKEY_LOCAL_MACHINE\SOFTWARE\RegisteredApplications</c>
        /// </summary>
        internal static void SetDefault(string browserId, bool includePdf = false)
        {
            // Code has no effect
            throw new NotImplementedException();

            if (defaultBrowserManager == null)
                throw new PlatformNotSupportedException();

            Marshal.ThrowExceptionForHR(defaultBrowserManager.SetAppAsDefaultBrowser(browserId, ".htm", type: AssociationType.FileExtension));
            Marshal.ThrowExceptionForHR(defaultBrowserManager.SetAppAsDefaultBrowser(browserId, ".html", type: AssociationType.FileExtension));
            Marshal.ThrowExceptionForHR(defaultBrowserManager.SetAppAsDefaultBrowser(browserId, "http", type: AssociationType.UrlProtocol));
            Marshal.ThrowExceptionForHR(defaultBrowserManager.SetAppAsDefaultBrowser(browserId, "https", type: AssociationType.UrlProtocol));
            if (includePdf)
                Marshal.ThrowExceptionForHR(defaultBrowserManager.SetAppAsDefaultBrowser(browserId, ".pdf", type: AssociationType.FileExtension));
        }

        public static string DefaultBrowserAssociationId
            => AppAssociation.GetDefaultHandler(".html");

        public static string DefaultBrowserId
        {
            get
            {
                if (registrationManagerInternal == null)
                    throw new PlatformNotSupportedException();
                Marshal.ThrowExceptionForHR(registrationManagerInternal.GetDefaultBrowserInfo(BrowserInfoType.ProgId, out string info));
                return info;
            }
        }

        public static string DefaultBrowserCommand
        {
            get
            {
                if (registrationManagerInternal == null)
                    throw new PlatformNotSupportedException();
                Marshal.ThrowExceptionForHR(registrationManagerInternal.GetDefaultBrowserInfo(BrowserInfoType.BrowserCommand, out string info));
                return info;
            }
        }
    }
}
