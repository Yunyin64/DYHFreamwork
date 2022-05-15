using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Assets.USecurity;
// Token: 0x02000443 RID: 1091
public class GFileUtil
{
	// Token: 0x06002A8F RID: 10895 RVA: 0x00048ECC File Offset: 0x000472CC
	public static string ReadTXT(string fileName, bool fullpath = false)
	{
		if (!fullpath)
		{
			fileName = GFileUtil.LocateFile(fileName);
		}
		if (!File.Exists(fileName))
		{
			return null;
		}
		string result;
		using (FileStream fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
		{
			using (StreamReader streamReader = new StreamReader(fileStream))
			{
				string text = streamReader.ReadToEnd();
				if (text.StartsWith("|E|"))
				{
					text = text.Substring(3);
					if (GFileUtil.DecryptStrFun != null)
					{
						text = GFileUtil.DecryptStrFun(text);
					}
					else
					{
						text = AES.Decrypt2Str(text, "no spoilers please");
					}
					if (text[0] == '﻿')
					{
						text = text.Substring(1);
					}
				}
				result = text;
			}
		}
		return result;
	}

	// Token: 0x06002A90 RID: 10896 RVA: 0x00048FA4 File Offset: 0x000473A4
	public static byte[] ReadBytes(string fileName, bool fullpath = false)
	{
		if (!fullpath)
		{
			fileName = GFileUtil.LocateFile(fileName);
		}
		if (!File.Exists(fileName))
		{
			return null;
		}
		byte[] result;
		using (FileStream fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
		{
			using (BinaryReader binaryReader = new BinaryReader(fileStream, Encoding.UTF8))
			{
				byte[] array = binaryReader.ReadBytes((int)binaryReader.BaseStream.Length);
				result = array;
			}
		}
		return result;
	}

	// Token: 0x06002A91 RID: 10897 RVA: 0x00049034 File Offset: 0x00047434
	public static void ForeachFiles(DirectoryInfo cDirInfo, Predicate<DirectoryInfo> dePredicate_Dir, Action<FileInfo> deAction_File)
	{
		if (!dePredicate_Dir(cDirInfo))
		{
			return;
		}
		foreach (FileInfo obj in cDirInfo.GetFiles())
		{
			deAction_File(obj);
		}
		foreach (DirectoryInfo cDirInfo2 in cDirInfo.GetDirectories())
		{
			GFileUtil.ForeachFiles(cDirInfo2, dePredicate_Dir, deAction_File);
		}
	}

	// Token: 0x06002A92 RID: 10898 RVA: 0x000490A4 File Offset: 0x000474A4
	public static Encoding GetFileEncoding(string fileName)
	{
		Encoding encoding = Encoding.UTF8;
		Encoding result;
		using (FileStream fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
		{
			using (BinaryReader binaryReader = new BinaryReader(fileStream, Encoding.UTF8))
			{
				if (fileStream.Length >= 3L)
				{
					byte[] array = binaryReader.ReadBytes(3);
					if (array[0] >= 239)
					{
						if (array[0] == 239 && array[1] == 187 && array[2] == 191)
						{
							encoding = Encoding.UTF8;
						}
						else if (array[0] == 254 && array[1] == 255)
						{
							encoding = Encoding.BigEndianUnicode;
						}
						else if (array[0] == 255 && array[1] == 254)
						{
							encoding = Encoding.Unicode;
						}
					}
				}
				result = encoding;
			}
		}
		return result;
	}

	// Token: 0x06002A93 RID: 10899 RVA: 0x000491A8 File Offset: 0x000475A8
	public static Encoding GetFileEncoding(byte[] aryBytes_File)
	{
		Encoding result = Encoding.UTF8;
		if (aryBytes_File.Length >= 3 && aryBytes_File[0] >= 239)
		{
			if (aryBytes_File[0] == 239 && aryBytes_File[1] == 187 && aryBytes_File[2] == 191)
			{
				result = Encoding.UTF8;
			}
			else if (aryBytes_File[0] == 254 && aryBytes_File[1] == 255)
			{
				result = Encoding.BigEndianUnicode;
			}
			else if (aryBytes_File[0] == 255 && aryBytes_File[1] == 254)
			{
				result = Encoding.Unicode;
			}
		}
		return result;
	}

	// Token: 0x06002A94 RID: 10900 RVA: 0x0004924C File Offset: 0x0004764C
	public static string Join(string path0, params string[] otherPaths)
	{
		path0 = path0.Replace('\\', '/');
		string text = path0;
		for (int i = 0; i < otherPaths.Length; i++)
		{
			string text2 = otherPaths[i].Replace('\\', '/');
			bool flag = text2.Length > 0 && text2[0] == '/';
			bool flag2 = text.Length > 0 && text[text.Length - 1] == '/';
			if (flag2)
			{
				text += ((!flag) ? text2 : text2.Substring(1));
			}
			else
			{
				text += ((!flag) ? ('/' + text2) : text2);
			}
		}
		return text;
	}

	// Token: 0x06002A95 RID: 10901 RVA: 0x00049320 File Offset: 0x00047720
	public static string LocateFile(string szFilePath)
	{
		return GFileUtil.Join(Environment.CurrentDirectory, new string[]
		{
			szFilePath
		});
	}

	// Token: 0x06002A96 RID: 10902 RVA: 0x00049344 File Offset: 0x00047744
	public static string GetFileHash(string szFilePath)
	{
		string result;
		using (FileStream fileStream = new FileStream(szFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
		{
			MD5 md = new MD5CryptoServiceProvider();
			byte[] array = md.ComputeHash(fileStream);
			fileStream.Close();
			string text = string.Empty;
			foreach (byte value in array)
			{
				text += Convert.ToString(value, 16);
			}
			result = text;
		}
		return result;
	}

	// Token: 0x0400135C RID: 4956
	public static bool USE_D = true;

	// Token: 0x0400135D RID: 4957
	public static Func<string, string> DecryptStrFun;
}
