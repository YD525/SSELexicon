using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace SSELex.SkyrimManage
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
            // 获取当前用户
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);

            // 检查是否是管理员
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        public static void RestartAsAdmin()
        {
            // 获取当前程序的文件路径
            string exePath = DeFine.GetFullPath("SSELex.exe");
            string GetPath = exePath.Substring(0, exePath.LastIndexOf(@"\"));
            // 启动当前程序并请求管理员权限
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = exePath,
                WorkingDirectory = GetPath,
                UseShellExecute = true,
                Verb = "runas" // 使用管理员权限运行
            };

            try
            {
                Process.Start(startInfo); // 启动程序
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("无法以管理员身份启动程序: " + ex.Message);
            }
        }
    }
}
