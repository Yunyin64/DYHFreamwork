using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using ShuShan;
using XLua;

namespace ShuShan
{
	[CSharpCallLua]
    public class LuaMgr:Mgr
    {
        public static LuaMgr Instance { get; private set; }

        public override void Init()
		{
			LuaMgr.Instance = this;
			this.luaEnv.AddLoader(delegate (ref string path)
			{
				return GFileUtil.ReadBytes(path, false);
			});
			this.LoadAll();
			luaStart.Invoke();

		}

		public LuaEnv Env
		{
			get
			{
				return this.luaEnv;
			}
		}
		public void SetGlobal(string name, object obj)
		{
			this.luaEnv.Global.Set<string, object>(name, obj);
		}
		public void LoadAll()
		{
			this.luaStart = null;
			this.luaBeforeStart = null;
			this.luaOnDestroy = null;
			this.luaUpdate = null;
			this.luaRender = null;
			this.luaSave = null;
			this.luaLoad = null;
			this.luaAfterload = null;
			this.luaHotKey = null;

			this.luaEnv.DoString("require('Xlua/main')", "chunk", null);
			LuaTable luaTable = this.luaEnv.Global.Get<LuaTable>("GameMain");
			luaTable.Get<string, Action<double>>("Render", out this.luaRender);
			luaTable.Get<string, Action<double>>("Step", out this.luaUpdate);
			luaTable.Get<string, Action>("Init", out this.luaStart);
			luaTable.Get<string, Action>("BeforeInit", out this.luaBeforeStart);
			luaTable.Get<string, Action>("Destroy", out this.luaOnDestroy);
			luaTable.Get<string, Func<string>>("Save", out this.luaSave);
			luaTable.Get<string, Action<string>>("Load", out this.luaLoad);
			luaTable.Get<string, Func<string>>("SyncSave", out this.luaSyncSave);
			luaTable.Get<string, Action<string>>("SyncLoad", out this.luaSyncLoad);
			luaTable.Get<string, Action>("AfterLoad", out this.luaAfterload);
			luaTable.Get<string, Action<string, string>>("HotKey", out this.luaHotKey);

			luaTable.Dispose();


			ModsMgr.Instance.GetFiles("Xlua", delegate (string path, string mod)
			{
				try
				{
					this.SetGlobal("LOAD_FILE_MOD", mod);
					if (path.EndsWith(".luac"))
					{
						this.luaEnv.DoString(GFileUtil.ReadBytes(path, true), mod, null);
					}
					else if (path.EndsWith(".lua"))
					{
						this.luaEnv.DoString(GFileUtil.ReadTXT(path, true), mod, null);
					}
				}
				catch (Exception ex)
				{
					Debug.LogError(ex.ToString() + "\n" + path);
				}
			}, "*.*");
			this.SetGlobal("LOAD_FILE_MOD", null);
		}

		public override void Step(float dt)
		{
			this.luaUpdate(dt);
			if (Time.time - lastGCTime > 1f)
			{
				this.luaEnv.Tick();
				lastGCTime = Time.time;
			}
		}
        public override void Update()
        {

        }
        public override void End()
        {

        }


		private LuaEnv luaEnv = new LuaEnv();

		public float lastGCTime = 0f;

		private const float GCInterval = 1f;

		private Action luaStart;

		private Action luaBeforeStart;

		private Action<double> luaUpdate;

		private Action<double> luaRender;

		private Action luaOnDestroy;

		private Func<string> luaSave;

		private Action<string> luaLoad;

		private Func<string> luaSyncSave;

		private Action<string> luaSyncLoad;

		private Action luaAfterload;

		private Action<string, string> luaHotKey;


	}
}
