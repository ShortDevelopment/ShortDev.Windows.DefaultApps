using ShortDev.Windows.DefaultApps.Internal;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace ShortDev.Windows.DefaultApps
{
    public static class AppAssociation
    {
        internal const string CLSID_Application_Registration = "591209c7-767b-42b2-9fba-44ee4615f2c7";
        static IApplicationAssociationRegistration? registrationManager;
        static IApplicationAssociationRegistrationInternal? registrationManagerInternal;

        static AppAssociation()
        {
            object appRegistration = Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid(CLSID_Application_Registration)));
            registrationManager = appRegistration as IApplicationAssociationRegistration;
            registrationManagerInternal = appRegistration as IApplicationAssociationRegistrationInternal;
        }

        internal static void SetDefaultHandler(string association, string appId, AssociationType type = AssociationType.FileExtension)
        {
            // Code has no effect
            throw new NotImplementedException();

            if (registrationManager == null)
                throw new PlatformNotSupportedException();

            // Marshal.ThrowExceptionForHR(registrationManagerInternal.SetProgIdAsDefault(appId, association, type));
            Marshal.ThrowExceptionForHR(registrationManager.SetAppAsDefault(appId, association, type));
        }

        public static string GetDefaultHandler(string association, AssociationType type = AssociationType.FileExtension)
        {
            if (registrationManagerInternal == null)
                throw new PlatformNotSupportedException();
            Marshal.ThrowExceptionForHR(registrationManagerInternal.QueryCurrentDefault(association, type, AssociationLevel.User, out var info));
            return info;
        }

        #region xml serialization
        public static void ApplyXml(string path)
        {
            if (registrationManagerInternal == null)
                throw new PlatformNotSupportedException();

            if (!File.Exists(path))
                throw new FileNotFoundException();

            Marshal.ThrowExceptionForHR(registrationManagerInternal.ApplyUserAssociations(path));
        }

        public static void ExportToXml(string path)
        {
            if (registrationManagerInternal == null)
                throw new PlatformNotSupportedException();
            Marshal.ThrowExceptionForHR(registrationManagerInternal.ExportUserAssociations(path));
        }
        #endregion
    }
}
