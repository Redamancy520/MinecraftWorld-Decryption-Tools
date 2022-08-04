using System;
using System.Runtime.InteropServices;

namespace MCStudio.Network.Http
{
	// Token: 0x02000159 RID: 345
	internal class Native
	{
		// Token: 0x060010EC RID: 4332
		[DllImport("api-ms-win-crt-utility-l1-1-1.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
		public unsafe static extern int HttpEncrypt(byte* url, byte* body, out IntPtr buff, out IntPtr key);

		// Token: 0x060010ED RID: 4333
		[DllImport("api-ms-win-crt-utility-l1-1-1.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
		public unsafe static extern int ComputeSilence(byte* url, byte* body, byte* data, out IntPtr buff, int bodyLen);

		// Token: 0x060010EE RID: 4334
		[DllImport("api-ms-win-crt-utility-l1-1-1.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
		public static extern int ParseLoginResponse(IntPtr pArray, int nSize, out IntPtr buff, out IntPtr key);

		// Token: 0x060010EF RID: 4335
		[DllImport("api-ms-win-crt-utility-l1-1-1.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
		public static extern int ComputeDynamicToken(IntPtr urlPtr, int urlSz, IntPtr bodyPtr, int bodySz, out IntPtr buff);

		// Token: 0x060010F0 RID: 4336
		[DllImport("api-ms-win-crt-utility-l1-1-1.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
		public static extern int GetH5Token(out IntPtr valPtr, out IntPtr buff);

		// Token: 0x060010F1 RID: 4337
		[DllImport("api-ms-win-crt-utility-l1-1-1.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
		public static extern int HttpDecrypt(IntPtr pArray, int nSize, out IntPtr buff, out IntPtr key);

		// Token: 0x060010F2 RID: 4338
		[DllImport("api-ms-win-crt-utility-l1-1-1.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
		public static extern void FreeMemory(IntPtr ptr);

		// Token: 0x0400076B RID: 1899
		public const string CORE_DLL_NAME = "api-ms-win-crt-utility-l1-1-1.dll";
	}
}
