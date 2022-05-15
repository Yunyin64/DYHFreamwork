using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

// Token: 0x02000488 RID: 1160
public static class FileTools
{
	// Token: 0x06001C1C RID: 7196 RVA: 0x000E11E8 File Offset: 0x000DF5E8
	public static string Write(string txt, string path, string filename)
	{
		string result = string.Empty;
		try
		{
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
			StreamWriter streamWriter = new StreamWriter(path + "/" + filename, false, Encoding.UTF8);
			streamWriter.Write(txt);
			streamWriter.Flush();
			streamWriter.Dispose();
		}
		catch (Exception ex)
		{
			result = "Fail:" + ex.Message;
		}
		return result;
	}

	// Token: 0x06001C1D RID: 7197 RVA: 0x000E1268 File Offset: 0x000DF668
	public static string DeleDirectory(string path)
	{
		string result = string.Empty;
		try
		{
			if (Directory.Exists(path))
			{
				foreach (string text in Directory.GetFileSystemEntries(path))
				{
					if (File.Exists(text))
					{
						File.Delete(text);
						Console.WriteLine(text);
					}
					else
					{
						FileTools.DeleDirectory(text);
					}
				}
				Directory.Delete(path);
			}
			else
			{
				result = "No exists " + path;
			}
		}
		catch (Exception ex)
		{
			result = "Fail:" + ex.Message;
		}
		return result;
	}

	// Token: 0x06001C1E RID: 7198 RVA: 0x000E1310 File Offset: 0x000DF710
	public static string DeleFile(string path)
	{
		string result = string.Empty;
		try
		{
			if (File.Exists(path))
			{
				File.Delete(path);
			}
			else
			{
				result = "No exists :" + path;
			}
		}
		catch (Exception ex)
		{
			result = "Fail:" + ex.Message;
		}
		return result;
	}

	// Token: 0x06001C1F RID: 7199 RVA: 0x000E1374 File Offset: 0x000DF774
	public static string CopyFile(string path, string cpath)
	{
		string result = string.Empty;
		try
		{
			if (File.Exists(path))
			{
				File.Copy(path, cpath);
			}
			else
			{
				result = "No exists :" + path;
			}
		}
		catch (Exception ex)
		{
			result = "Fail:" + ex.Message;
		}
		return result;
	}

	public static void SerializeObjectTo(object obj,string path,string file)
	{
		JsonSerializerSettings settings = new JsonSerializerSettings();
		settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
		string json = JsonConvert.SerializeObject(obj, settings);
		Write(json, path, file);
	}
	public static void unitySerializeObjectTo(object obj, string path, string file)
	{
		JsonSerializerSettings settings = new JsonSerializerSettings();
		settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
		string json = JsonConvert.SerializeObject(obj, settings);
		string js = JsonUtility.ToJson(obj);
		Write(json, path, file);
	}
	public static T SerializeObjectFrom<T>(string path, string file)
	{
		StreamReader streamReader = new StreamReader(path +"/" + file);
		T a = JsonConvert.DeserializeObject<T>(streamReader.ReadToEnd());
		return a;
	}
}
