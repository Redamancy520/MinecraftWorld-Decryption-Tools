using MCStudio.Network.Http;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using static System.Net.Mime.MediaTypeNames;


namespace MCStudio.Utils
{
	
	public class LevelDbEncryptHelper
	{
        // Token: 0x06001136 RID: 4406
        [DllImport("XOREncryptDLL.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
        public unsafe static extern int decrypt_file(byte* filePath, int filePathLen, out IntPtr buff, out int len);

        // Token: 0x06001137 RID: 4407 RVA: 0x0003B958 File Offset: 0x0003B958
        public static string DecryptRecord(string dbDir)
        {
            if (!Directory.Exists(dbDir))
            {
                return "";
            }
            return LevelDbEncryptHelper._decryptRecord(new DirectoryInfo(dbDir));
        }

        // Token: 0x06001138 RID: 4408 RVA: 0x0003B974 File Offset: 0x0003B974
        private static string _decryptRecord(DirectoryInfo dbDirInfo)
        {
            DirectoryInfo[] directories = dbDirInfo.GetDirectories();
            for (int i = 0; i < directories.Length; i++)
            {
                string text = LevelDbEncryptHelper._decryptRecord(directories[i]);
                if (!string.IsNullOrEmpty(text))
                {
                    return text;
                }
            }
            FileInfo[] files = dbDirInfo.GetFiles();
            for (int i = 0; i < files.Length; i++)
            {
                string text2 = LevelDbEncryptHelper._decryptFile(files[i].FullName);
                if (!string.IsNullOrEmpty(text2))
                {
                    return text2;
                }
            }
            return "";
        }

        // Token: 0x06001139 RID: 4409 RVA: 0x0003B9E0 File Offset: 0x0003B9E0
        private unsafe static string _decryptFile(string filePath)
        {
            string text;
            byte[] bytes = Encoding.UTF8.GetBytes(filePath);
            int length = filePath.Length;
            IntPtr zero = IntPtr.Zero;
            int num = 0;
            fixed (byte* ptr = &bytes[0])
            {
                int num2 = LevelDbEncryptHelper.decrypt_file(ptr, length, out zero, out num);
                if (num2 != 0)
                {
                    return string.Format("存档解密失败,错误码：{0}", num2);
                }
                if (num != 0 && zero != IntPtr.Zero)
                {
                    byte[] array = new byte[num];
                    Marshal.Copy(zero, array, 0, num);
                    CoreNative.FreeMemory(zero);
                    string @string = Encoding.UTF8.GetString(array);
                    if (filePath == @string)
                    {
                        return "";
                    }
                    try
                    {
                        File.Copy(@string, filePath, true);
                        Delete(@string);
                        return "";
                    }

                    catch (Exception ex)
                    {
                        text = string.Format("转换失败！文件复制出错，错误: " + ex.Message);
                        Delete(@string);
                        return text;
                    }


                 
                }
            }
            return "";
        }

        // Token: 0x0400074E RID: 1870
        public const string CORE_DLL_NAME = "XOREncryptDLL.dll";
        public static void Delete(string file)
        {
            if (File.Exists(file))
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }
            if (Directory.Exists(file))
            {
                Directory.Delete(file);
            }

        }

    }
}
