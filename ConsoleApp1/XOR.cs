using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;


namespace MCStudio.Utils
{
	
	public class LevelDbEncryptHelper
	{
		
		[DllImport("XOREncryptDLL.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
		public unsafe static extern int decrypt_file(byte* filePath, int filePathLen, out IntPtr buff, out int len);

	
		public static string DecryptRecord(string dbDir)
		{
			if (!Directory.Exists(dbDir))
			{
				return "";
			}
			return LevelDbEncryptHelper._decryptRecord(new DirectoryInfo(dbDir));
		}

		
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

	
		private unsafe static string _decryptFile(string filePath)
		{
		
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
					 MCStudio.Network.Http.CoreNative.FreeMemory(zero);
					string @string = Encoding.UTF8.GetString(array);
					string text = "Success成功";

					if (filePath == @string)
					{
						return "";
					}
					try
					{
						File.Copy(@string, filePath, true);
						return text;
					}
					catch (Exception ex)
                    {
						text = string.Format("转换失败！文件复制出错，错误: " + ex.Message);
						return text;
					}
					
					Delete(@string);
				}
			}
			return "Success成功";
		}

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


		// Token: 0x04000698 RID: 1688
		public const string CORE_DLL_NAME = "XOREncryptDLL.dll";
	}
}
