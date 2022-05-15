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
    [LuaCallCSharp(GenFlag.No)]
    public class EventMgr:Mgr
    {
        public static EventMgr Instance { get; private set; }

        public override void Init()
        {
            EventMgr.Instance = this;
        }
        

        public override void Step(float dt)
        {

        }
        public override void Update()
        {
            
        }
        

		public override void End()
		{
			this.hashLuaEvent.Clear();
			EventMgr.Instance = null;
			if (this.EventLua != null)
			{
				this.EventLua.Destory();
				this.EventLua = null;
			}
		}

		public void EventTrigger(Em_Event id, object t, params object[] objs)
		{
			
			List<EventMgr.EventHandler> list = null;
			if (this.m_mapEvents.TryGetValue(id, out list) && list != null)
			{
				this.triggering++;

				try
				{
					foreach (EventMgr.EventHandler eventHandler in list)
					{
						if (eventHandler != null)
						{
							eventHandler(t, objs);
						}
					}
				}
				catch (Exception ex)
				{
					LogMgr.Dbg(ex.ToString(), new object[0]);
				}
				this.triggering--;
				if (this.triggering <= 0)
				{
					foreach (KeyValuePair<Em_Event, Dictionary<EventMgr.EventHandler, int>> keyValuePair in this.m_TempEvent)
					{
						foreach (KeyValuePair<EventMgr.EventHandler, int> keyValuePair2 in keyValuePair.Value)
						{
							if (keyValuePair2.Value > 0)
							{
								this.RegisterEvent(keyValuePair.Key, keyValuePair2.Key);
							}
							if (keyValuePair2.Value < 0)
							{
								this.RemoveEvent(keyValuePair.Key, keyValuePair2.Key);
							}
						}
					}
					this.m_TempEvent.Clear();
				}
			}
			if (this.hashLuaEvent.Contains((int)id))
			{
				if (this.EventLua == null)
				{
					this.EventLua = new GLuaTable(LuaMgr.Instance.Env.DoString("return GameMain:GetMod('_Event')", "chunk", null)[0] as LuaTable);
				}
				this.EventLua.CallFunctionNoRs<Em_Event, object, object[]>("OnEvent", id, t, objs);
			}
		}

		public void RegisterEvent(Em_Event id, EventMgr.EventHandler act)
		{


			if (this.triggering > 0)
			{
				Dictionary<EventMgr.EventHandler, int> dictionary = null;
				if (!this.m_TempEvent.TryGetValue(id, out dictionary))
				{
					this.m_TempEvent[id] = new Dictionary<EventMgr.EventHandler, int>();
				}
				if (!this.m_TempEvent[id].ContainsKey(act))

				{
					this.m_TempEvent[id][act] = 1;
				}
				else
				{
					Dictionary<EventMgr.EventHandler, int> dictionary2;
					(dictionary2 = this.m_TempEvent[id])[act] = dictionary2[act] + 1;
				}
				return;
			}
			List<EventMgr.EventHandler> list = null;
			if (!this.m_mapEvents.TryGetValue(id, out list))
			{
				this.m_mapEvents[id] = new List<EventMgr.EventHandler>();
			}
			if (!this.m_mapEvents[id].Contains(act))
			{
				this.m_mapEvents[id].Add(act);
			}
		}

		public void RemoveEvent(Em_Event id, EventMgr.EventHandler act)
		{
			if (this.triggering > 0)
			{
				Dictionary<EventMgr.EventHandler, int> dictionary = null;
				if (!this.m_TempEvent.TryGetValue(id, out dictionary))
				{
					this.m_TempEvent[id] = new Dictionary<EventMgr.EventHandler, int>();
				}
				if (!this.m_TempEvent[id].ContainsKey(act))
				{
					this.m_TempEvent[id][act] = -1;
				}
				else
				{
					Dictionary<EventMgr.EventHandler, int> dictionary2;
					(dictionary2 = this.m_TempEvent[id])[act] = dictionary2[act] - 1;
				}
				return;
			}
			List<EventMgr.EventHandler> list = null;
			if (!this.m_mapEvents.TryGetValue(id, out list))
			{
				return;
			}
			list.Remove(act);
		}

		
		public void LuaRegisterEvent(Em_Event e)
		{
			this.hashLuaEvent.Add((int)e);
		}

		public void LuaUnRegisterEvent(Em_Event e)
		{
			this.hashLuaEvent.Remove((int)e);
		}

		private Dictionary<Em_Event, List<EventMgr.EventHandler>> m_mapEvents = new Dictionary<Em_Event, List<EventMgr.EventHandler>>();

		private Dictionary<Em_Event, Dictionary<EventMgr.EventHandler, int>> m_TempEvent = new Dictionary<Em_Event, Dictionary<EventMgr.EventHandler, int>>();

		private HashSet<int> hashLuaEvent = new HashSet<int>();

		private GLuaTable EventLua;

		private int triggering = 0;

		public delegate void EventHandler(object sender, object[] obs);

	}
    
}
