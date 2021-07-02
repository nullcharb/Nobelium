using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Win32;

namespace DecodeNobeliumPDF
{
    class Program
    {

		private static byte[] IV = Encoding.UTF8.GetBytes("1233t04p7jn3n4rg");
		private static byte[] Key = Encoding.UTF8.GetBytes("123do3y4r378o5t34onf7t3o573tfo73");
		static void Main(string[] args)
        {
			string filePath = @"C:\Users\Developer\Desktop\Malware\Nobelium\manual.pdf";
			string outputPath = @"C:\Users\Developer\Desktop\Malware\Nobelium\NativeCacheSvc.dll";
			decode_file(filePath, outputPath);

			filePath = @"C:\Users\Developer\Desktop\Malware\Nobelium\readme.pdf";
			outputPath = @"C:\Users\Developer\Desktop\Malware\Nobelium\CertPKIProvider.dll";
			decode_file(filePath, outputPath);
		}

		public static void decode_file(string filePath, string outputPath)
		{
			byte[] array = File.ReadAllBytes(filePath);
			if (array != null)
			{
				byte[] array2 = new byte[array.Length - 17];
				Array.Copy(array, 10, array2, 0, array2.Length);
				byte[] bytes = new crypt().aes_crypt_read(array2, Program.IV, Program.Key);
				File.WriteAllBytes(outputPath, bytes);
			}
			else
            {
				Console.WriteLine(filePath + "is Empty.");
            }
		}
	}
}
