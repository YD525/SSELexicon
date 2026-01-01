using System;
using System.Diagnostics;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Windows;


namespace LexTranslator.SkyrimManage
{
    public class FileAttributesHelper
    {
        public static void RemoveZoneIdentifier(string filePath)
        {
            string zoneIdentifierPath = filePath + ":Zone.Identifier";
            if (File.Exists(zoneIdentifierPath))
            {
                try
                {
                    File.Delete(zoneIdentifierPath);
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"{filePath}\r\n: {ex.Message}");
                }
            }
        }

        public static void TakeOwnership(string filePath)
        {
            var currentUser = WindowsIdentity.GetCurrent();
            var userSid = currentUser.User;

            var fileInfo = new FileInfo(filePath);
            var fileSecurity = fileInfo.GetAccessControl();

            var currentOwner = fileSecurity.GetOwner(typeof(SecurityIdentifier));

            if (!currentOwner.Equals(userSid))
            {
                fileSecurity.SetOwner(userSid);

                var accessRule = new FileSystemAccessRule(
                    userSid,
                    FileSystemRights.FullControl,
                    InheritanceFlags.None,
                    PropagationFlags.NoPropagateInherit,
                    AccessControlType.Allow
                );

                fileSecurity.AddAccessRule(accessRule);

                fileInfo.SetAccessControl(fileSecurity);
            }
            else
            {

            }
        }
        public static void UnlockAllFiles(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
            {
                return;
            }

            string[] files = Directory.GetFiles(directoryPath, "*", SearchOption.AllDirectories);

            foreach (var file in files)
            {
                try
                {
                    File.SetAttributes(file, FileAttributes.Normal);

                    FileInfo fileInfo = new FileInfo(file);
                    if (fileInfo.IsReadOnly)
                    {
                        fileInfo.IsReadOnly = false;
                    }

                    RemoveZoneIdentifier(file);

                    TakeOwnership(file);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(file + "\r\n" + ex.Message);
                }
            }
        }

        public static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);

           
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public static void RestartAsAdmin()
        {
            
            string exePath = DeFine.GetFullPath("LexTranslator.exe");
            string GetPath = exePath.Substring(0, exePath.LastIndexOf(@"\"));
            
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = exePath,
                WorkingDirectory = GetPath,
                UseShellExecute = true,
                Verb = "runas" 
            };

            try
            {
                Process.Start(startInfo); 
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
