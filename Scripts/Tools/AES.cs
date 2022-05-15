using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Assets.USecurity
{
	// Token: 0x0200060A RID: 1546
	public static class AES
	{
		// Token: 0x060039A6 RID: 14758 RVA: 0x000A2967 File Offset: 0x000A0D67
		public static string Encrypt(string value, string password)
		{
			return AES.Encrypt(Encoding.UTF8.GetBytes(value), password);
		}

		// Token: 0x060039A7 RID: 14759 RVA: 0x000A297C File Offset: 0x000A0D7C
		public static string Encrypt(byte[] value, string password)
		{
			byte[] bytes = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes("XhSG9hLyZ7T~Ge3@")).GetBytes(AES.KeyLength / 8);
			RijndaelManaged rijndaelManaged = new RijndaelManaged
			{
				Mode = CipherMode.CBC,
				Padding = PaddingMode.Zeros
			};
			ICryptoTransform transform = rijndaelManaged.CreateEncryptor(bytes, Encoding.UTF8.GetBytes("~8YSi0Xv2@|{aDfb"));
			string result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write))
				{
					cryptoStream.Write(value, 0, value.Length);
					cryptoStream.FlushFinalBlock();
					cryptoStream.Close();
					memoryStream.Close();
					result = Convert.ToBase64String(memoryStream.ToArray());
				}
			}
			return result;
		}

		// Token: 0x060039A8 RID: 14760 RVA: 0x000A2A5C File Offset: 0x000A0E5C
		public static string Decrypt2Str(string value, string password)
		{
			byte[] cipherTextBytes = Convert.FromBase64String(value);
			return AES.Decrypt2Str(cipherTextBytes, password);
		}

		// Token: 0x060039A9 RID: 14761 RVA: 0x000A2A78 File Offset: 0x000A0E78
		public static string Decrypt2Str(byte[] cipherTextBytes, string password)
		{
			byte[] bytes = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes("XhSG9hLyZ7T~Ge3@")).GetBytes(AES.KeyLength / 8);
			RijndaelManaged rijndaelManaged = new RijndaelManaged
			{
				Mode = CipherMode.CBC,
				Padding = PaddingMode.None
			};
			ICryptoTransform transform = rijndaelManaged.CreateDecryptor(bytes, Encoding.UTF8.GetBytes("~8YSi0Xv2@|{aDfb"));
			string result;
			using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
			{
				using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Read))
				{
					byte[] array = new byte[cipherTextBytes.Length];
					int count = cryptoStream.Read(array, 0, array.Length);
					memoryStream.Close();
					cryptoStream.Close();
					result = Encoding.UTF8.GetString(array, 0, count).TrimEnd("\0".ToCharArray());
				}
			}
			return result;
		}

		// Token: 0x060039AA RID: 14762 RVA: 0x000A2B70 File Offset: 0x000A0F70
		public static byte[] Decrypt(byte[] cipherTextBytes, string password)
		{
			byte[] bytes = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes("XhSG9hLyZ7T~Ge3@")).GetBytes(AES.KeyLength / 8);
			RijndaelManaged rijndaelManaged = new RijndaelManaged
			{
				Mode = CipherMode.CBC,
				Padding = PaddingMode.None
			};
			ICryptoTransform transform = rijndaelManaged.CreateDecryptor(bytes, Encoding.UTF8.GetBytes("~8YSi0Xv2@|{aDfb"));
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
			{
				using (CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Read))
				{
					byte[] array = new byte[cipherTextBytes.Length];
					int num = cryptoStream.Read(array, 0, array.Length);
					memoryStream.Close();
					cryptoStream.Close();
					result = array;
				}
			}
			return result;
		}

		// Token: 0x04001DC5 RID: 7621
		public static int KeyLength = 128;

		// Token: 0x04001DC6 RID: 7622
		private const string SaltKey = "XhSG9hLyZ7T~Ge3@";

		// Token: 0x04001DC7 RID: 7623
		private const string VIKey = "~8YSi0Xv2@|{aDfb";
	}
}
