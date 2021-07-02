using System;
using System.IO;
using System.Security.Cryptography;

namespace DecodeNobeliumPDF
{
	// Token: 0x02000003 RID: 3
	internal class crypt
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000021D8 File Offset: 0x000003D8
		public byte[] aes_crypt_read(byte[] SourceData, byte[] IV, byte[] Key)
		{
			Aes aes = Aes.Create();
			aes.Key = Key;
			aes.IV = IV;
			aes.Mode = CipherMode.CBC;
			ICryptoTransform transform = aes.CreateDecryptor(aes.Key, aes.IV);
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream(SourceData))
			{
				using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Read))
				{
					byte[] array = new byte[SourceData.Length];
					cryptoStream.Read(array, 0, SourceData.Length);
					result = array;
				}
			}
			return result;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002274 File Offset: 0x00000474
		public byte[] aes_crypt_write(byte[] SourceData, byte[] IV, byte[] Key)
		{
			Aes aes = Aes.Create();
			aes.Key = Key;
			aes.IV = IV;
			aes.Mode = CipherMode.CBC;
			ICryptoTransform transform = aes.CreateEncryptor(aes.Key, aes.IV);
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write))
				{
					cryptoStream.Write(SourceData, 0, SourceData.Length);
				}
				result = memoryStream.ToArray();
			}
			return result;
		}
	}
}
