using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XLua;

namespace ShuShan
{
	public class GLuaTable
	{
		// Token: 0x06000D49 RID: 3401 RVA: 0x00052127 File Offset: 0x00050527
		public GLuaTable()
		{
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x0005213A File Offset: 0x0005053A
		public GLuaTable(LuaTable table)
		{
			this.Init(table);
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x00052154 File Offset: 0x00050554
		public LuaTable GetTable()
		{
			return this.tbMain;
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x0005215C File Offset: 0x0005055C
		protected void Init(LuaTable table)
		{
			this.tbMain = table;
			//GException.G_Assert(this.tbMain != null, null);
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x00052177 File Offset: 0x00050577
		public void Set(string name, object obj)
		{
			this.tbMain.Set<string, object>(name, obj);
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x00052188 File Offset: 0x00050588
		public TResult CallFunction<TResult>(string name)
		{
			LuaFunction function = this.GetFunction(name);
			if (function != null)
			{
				return function.Func<LuaTable, TResult>(this.tbMain);
			}
			return default(TResult);
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x000521BC File Offset: 0x000505BC
		public TResult CallFunction<T1, T2, TResult>(string name, T1 a1, T2 a2)
		{
			LuaFunction function = this.GetFunction(name);
			if (function != null)
			{
				return function.Func<LuaTable, T1, T2, TResult>(this.tbMain, a1, a2);
			}
			return default(TResult);
		}

		// Token: 0x06000D50 RID: 3408 RVA: 0x000521F0 File Offset: 0x000505F0
		public TResult CallFunction<T1, T2, T3, TResult>(string name, T1 a1, T2 a2, T3 a3)
		{
			LuaFunction function = this.GetFunction(name);
			if (function != null)
			{
				return function.Func<LuaTable, T1, T2, T3, TResult>(this.tbMain, a1, a2, a3);
			}
			return default(TResult);
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x00052228 File Offset: 0x00050628
		public TResult CallFunction<T1, T2, T3, T4, TResult>(string name, T1 a1, T2 a2, T3 a3, T4 a4)
		{
			LuaFunction function = this.GetFunction(name);
			if (function != null)
			{
				return function.Func<LuaTable, T1, T2, T3, T4, TResult>(this.tbMain, a1, a2, a3, a4);
			}
			return default(TResult);
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x00052260 File Offset: 0x00050660
		public TResult CallFunction<T1, TResult>(string name, T1 a1)
		{
			LuaFunction function = this.GetFunction(name);
			if (function != null)
			{
				return function.Func<LuaTable, T1, TResult>(this.tbMain, a1);
			}
			return default(TResult);
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x00052294 File Offset: 0x00050694
		public void CallFunctionNoRs(string name)
		{
			LuaFunction function = this.GetFunction(name);
			if (function != null)
			{
				function.Action<LuaTable>(this.tbMain);
			}
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x000522BC File Offset: 0x000506BC
		public void CallFunctionNoRs<T1>(string name, T1 a1)
		{
			LuaFunction function = this.GetFunction(name);
			if (function != null)
			{
				function.Action<LuaTable, T1>(this.tbMain, a1);
			}
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x000522E4 File Offset: 0x000506E4
		public void CallFunctionNoRs<T1, T2>(string name, T1 a1, T2 a2)
		{
			LuaFunction function = this.GetFunction(name);
			if (function != null)
			{
				function.Action<LuaTable, T1, T2>(this.tbMain, a1, a2);
			}
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x00052310 File Offset: 0x00050710
		public void CallFunctionNoRs<T1, T2, T3>(string name, T1 a1, T2 a2, T3 a3)
		{
			LuaFunction function = this.GetFunction(name);
			if (function != null)
			{
				function.Action<LuaTable, T1, T2, T3>(this.tbMain, a1, a2, a3);
			}
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x0005233C File Offset: 0x0005073C
		public LuaFunction GetFunction(string name)
		{
			LuaFunction luaFunction = null;
			if (this.Func.TryGetValue(name, out luaFunction))
			{
				return luaFunction;
			}
			this.tbMain.Get<string, LuaFunction>(name, out luaFunction);
			if (luaFunction == null)
			{
				LogMgr.Dbg("Cant Find Func:" + name, new object[0]);
			}
			return luaFunction;
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x0005238C File Offset: 0x0005078C
		public void Destory()
		{
			this.OnDestory();
			foreach (KeyValuePair<string, LuaFunction> keyValuePair in this.Func)
			{
				if (keyValuePair.Value != null)
				{
					keyValuePair.Value.Dispose();
				}
			}
			this.Func.Clear();
			if (this.tbMain != null)
			{
				this.tbMain.Dispose();
				this.tbMain = null;
			}
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x00052428 File Offset: 0x00050828
		protected virtual void OnDestory()
		{
		}

		// Token: 0x04001357 RID: 4951
		protected LuaTable tbMain;

		// Token: 0x04001358 RID: 4952
		private Dictionary<string, LuaFunction> Func = new Dictionary<string, LuaFunction>();
	}
}
