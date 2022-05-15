using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using ShuShan;
// Token: 0x02000503 RID: 1283
namespace ShuShan
{
	public class ModsMgr:Mgr
	{
		public ModsMgr()
        {
			Name = "ModsMgr";
        }
		public static ModsMgr Instance { get; private set; }
		public override void Init()
		{
			ModsMgr.Instance = this;
			Dictionary<int, ModsMgr.ModData> dictionary = new Dictionary<int, ModsMgr.ModData>();
		}
        public override void End()
        {

        }
        public class PathData
		{
			// Token: 0x0600217F RID: 8575 RVA: 0x00124510 File Offset: 0x00122910
			public override string ToString()
			{
				return string.Format("{0}:{1}", this.mod, this.path);
			}

			// Token: 0x04001F44 RID: 8004
			public string path;

			// Token: 0x04001F45 RID: 8005
			public string mod;
		}
		public List<ModsMgr.PathData> GetPath(string localpath, bool check = true, bool checkv = true)
		{
			this.Init();
			List<ModsMgr.PathData> list = new List<ModsMgr.PathData>();
			string path = GFileUtil.LocateFile(localpath);
			if (!check || Directory.Exists(path))
			{
				list.Add(new ModsMgr.PathData
				{
					path = path,
					mod = null
				});
			}
			string path2 = "Settings/" + localpath;
			if (Directory.Exists(path2) || File.Exists(path2))
			{
				list.Add(new ModsMgr.PathData
				{
					path = path2,
					mod = null
				});
			}
			float num = (!checkv) ? 0f : float.Parse(Application.version);
			foreach (KeyValuePair<string, ModsMgr.ModData> keyValuePair in this.Mods)
			{
				if (keyValuePair.Value.IsActive)
				{
					string path3 = keyValuePair.Value.Path + "/" + localpath;
					if (!check || Directory.Exists(path3))
					{
						list.Add(new ModsMgr.PathData
						{
							path = path3,
							mod = keyValuePair.Key
						});
					}
				}
			}
			return list;
		}
		public bool LoadXmlDefs<T>(string localpath, Action<T, string> cb, string pattern = "*.xml", List<ModsMgr.PathData> paths = null) where T : class
		{

			bool result = false;
			string[] files = Directory.GetFiles(localpath, pattern, SearchOption.AllDirectories);
			int i = 0;
			while (i < files.Length)
			{
				T t = (T)((object)null);
				try
				{
					string text = GFileUtil.ReadTXT(files[i].Replace("\\", "/"), true);
					if (text.Length == 0)
					{
						goto IL_16B;
					}
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
					t = (T)((object)xmlSerializer.Deserialize(new StringReader(text)));
					
					//StringWriter xml = new StringWriter();
					//xmlSerializer.Serialize(xml, t);
					//Debug.Log(xml.ToString());
				}
				catch (Exception ex)
				{
					Debug.Log(ex);
				}
				goto IL_103;
			IL_16B:
				i++;
				continue;
			IL_103:
				if (t != null)
				{
					string arg = "s";
					if (!string.IsNullOrEmpty("mod"))
					{
						//arg = string.Format("{0}|{1}", this.Mods["mod"].Name, this.Mods["mod"].Version);
					}
					cb(t, arg);
					result = true;
					goto IL_16B;
				}
				goto IL_16B;

			}
			return result;
		}
		public void GetFiles(string localpath, Action<string, string> cb, string pattern = "*.xml")
		{
			List<ModsMgr.PathData> path = this.GetPath(localpath, true, true);
			foreach (ModsMgr.PathData pathData in path)
			{
				string[] files = Directory.GetFiles(pathData.path, pattern, SearchOption.AllDirectories);
				for (int i = 0; i < files.Length; i++)
				{
					cb(files[i], pathData.mod);
				}
			}
		}
		public Texture2D LoadTexture2D(string localpath)
		{
			List<ModsMgr.PathData> path = this.GetPath(localpath, false, true);
			for (int i = path.Count - 1; i >= 0; i--)
			{
				ModsMgr.PathData pathData = path[i];
				if (File.Exists(pathData.path))
				{
					return this.LoadTextureByIO(pathData.path);
				}
			}
			return null;
		}
		private Texture2D LoadTextureByIO(string path)
		{
			using (UnityWebRequest www = new(path))
			{
				while (!www.isDone)
				{
				}
				if (www.error == null)
				{
					return DownloadHandlerTexture.GetContent(www);
				}
				Debug.Log(www.error);
			}
			return null;
		}


		public Dictionary<string, ModsMgr.ModData> Mods = new Dictionary<string, ModsMgr.ModData>();
		public class ModData
		{

			[JsonIgnore]
			public float Score
			{
				get
				{
					if (this.Vote == 0UL && this.MyVote != 0)
					{
						return (this.MyVote != 1) ? 2f : 5f;
					}
					if (this.Vote == 0UL && this.MyVote == 0)
					{
						return 0f;
					}
					return this.Vote;
				}
			}

			[JsonIgnore]
			public ulong uID
			{
				get
				{
					ulong result;
					if (!ulong.TryParse(this.ID, out result))
					{
						result = 0UL;
					}
					return result;
				}
			}

			[JsonIgnore]
			public bool IsInstall
			{
				get
				{

					return this._IsInstall;
				}
				set
				{
					this._IsInstall = value;
				}
			}

			[JsonIgnore]
			public ulong SubNum
			{
				get
				{
					if (this._subNum == 0UL && this.IsSub > 0U)
					{
						return 1UL;
					}
					return this._subNum;
				}
				set
				{
					this._subNum = value;
				}
			}

			[JsonIgnore]
			public uint IsSub
			{
				get
				{

					return this._IsSub;
				}
				set
				{
					this._IsSub = value;
				}
			}

			[JsonIgnore]
			public bool IsActive
			{
				get
				{
					return true;
				}
			}

			[JsonIgnore]
			public bool IsValid
			{
				get
				{
					return this.GameVersion >= 0.95f && this.GameVersion <= float.Parse(Application.version);
				}
			}

			[JsonIgnore]
			public int Modhash
			{
				get
				{
					string text = this.Name + this.ID + ((!this.Local) ? 0 : 1);
					return text.GetHashCode();
				}
			}

			public static ModsMgr.ModData GetClone(ModsMgr.ModData oldModData)
			{
				return new ModsMgr.ModData
				{
					Name = oldModData.Name,
					ID = oldModData.ID,
					DisplayName = oldModData.DisplayName,
					Path = oldModData.Path,
					PreviewUrl = oldModData.PreviewUrl,
					Desc = oldModData.Desc,
					IsInstall = oldModData.IsInstall,
					Tags = ((oldModData.Tags != null) ? new List<string>(oldModData.Tags) : null),
					Enable = oldModData.Enable,
					Local = oldModData.Local,
					GameVersion = oldModData.GameVersion,
					Version = oldModData.Version,
					Author = oldModData.Author,
					UIPackage = oldModData.UIPackage,
					Flag = oldModData.Flag,
					IncreasedDesc = oldModData.IncreasedDesc,
					Url = oldModData.Url,
					PreviewLoacl = oldModData.PreviewLoacl,
					SubNum = oldModData.SubNum,
					IsSub = oldModData.IsSub,
					IsCreat = oldModData.IsCreat,
					MyVote = oldModData.MyVote,
					uAuthor = oldModData.uAuthor,
					Voteplay = oldModData.Voteplay,
					Vote = oldModData.Vote,
					UpdateTime = oldModData.UpdateTime,
					IsInsideCreat = oldModData.IsInsideCreat,
					ShareLevel = oldModData.ShareLevel,
					OnlineVersion = oldModData.OnlineVersion,
					LocalVersion = oldModData.LocalVersion,
					itempreviewlist = ((oldModData.itempreviewlist != null) ? new List<string>(oldModData.itempreviewlist) : null),
					Previews = ((oldModData.Previews != null) ? new List<string>(oldModData.Previews) : null),
					ParentModInfo = ((oldModData.ParentModInfo != null) ? new List<string>(oldModData.ParentModInfo) : null)
				};
			}

			public string Name;

			public string DisplayName;

			public float GameVersion;

			public float Version;

			public string Desc;

			public string Author;

			public string UIPackage;

			public string ID;

			public int Flag;

			public List<string> Tags;

			public string IncreasedDesc;

			public bool IsInsideCreat;

			public List<string> Previews;

			public List<string> ParentModInfo;

			[JsonIgnore]
			public List<string> itempreviewlist;

			[JsonIgnore]
			public bool _waittingdata;

			[JsonIgnore]
			public int Index;

			[JsonIgnore]
			public uint UpdateTime;

			[JsonIgnore]
			public ulong Vote;

			[JsonIgnore]
			public uint Voteplay;

			[JsonIgnore]
			public ulong uAuthor;

			[JsonIgnore]
			public bool Enable;

			// Token: 0x04001F36 RID: 7990
			[JsonIgnore]
			public int Sort;

			// Token: 0x04001F37 RID: 7991
			[JsonIgnore]
			public string Path;

			// Token: 0x04001F38 RID: 7992
			[JsonIgnore]
			public string PreviewUrl;

			// Token: 0x04001F39 RID: 7993
			[JsonIgnore]
			public string Url;

			// Token: 0x04001F3A RID: 7994
			[JsonIgnore]
			public string PreviewLoacl;

			// Token: 0x04001F3B RID: 7995
			[JsonIgnore]
			public bool _IsInstall;

			// Token: 0x04001F3C RID: 7996
			[JsonIgnore]
			public bool Local;

			// Token: 0x04001F3D RID: 7997
			[JsonIgnore]
			private ulong _subNum;

			// Token: 0x04001F3E RID: 7998
			[JsonIgnore]
			public uint _IsSub;

			// Token: 0x04001F3F RID: 7999
			[JsonIgnore]
			public bool IsCreat;

			// Token: 0x04001F40 RID: 8000
			[JsonIgnore]
			public int MyVote;

			// Token: 0x04001F41 RID: 8001
			[JsonIgnore]
			public int ShareLevel;

			// Token: 0x04001F42 RID: 8002
			[JsonIgnore]
			public string OnlineVersion;

			// Token: 0x04001F43 RID: 8003
			[JsonIgnore]
			public string LocalVersion;
		}
	}
}