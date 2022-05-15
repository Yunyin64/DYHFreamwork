using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

// Token: 0x0200042C RID: 1068
public class Loom : MonoBehaviour
{
	// Token: 0x1700027C RID: 636
	// (get) Token: 0x060021BB RID: 8635 RVA: 0x000E46DE File Offset: 0x000E28DE
	public static Loom Current
	{
		get
		{
			Loom.Initialize();
			return Loom._current;
		}
	}

	// Token: 0x060021BC RID: 8636 RVA: 0x000E46EA File Offset: 0x000E28EA
	private void Awake()
	{
		Loom._current = this;
		Loom.initialized = true;
	}

	// Token: 0x060021BD RID: 8637 RVA: 0x000E46F8 File Offset: 0x000E28F8
	public static void Initialize()
	{
		if (!Loom.initialized)
		{
			if (!Application.isPlaying)
			{
				return;
			}
			Loom.initialized = true;
			GameObject gameObject = new GameObject("Loom");
			Loom._current = gameObject.AddComponent<Loom>();
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
		}
	}

	// Token: 0x060021BE RID: 8638 RVA: 0x000E4729 File Offset: 0x000E2929
	public static void QueueOnMainThread(Action<object> taction, object tparam)
	{
		Loom.QueueOnMainThread(taction, tparam, 0f);
	}

	// Token: 0x060021BF RID: 8639 RVA: 0x000E4738 File Offset: 0x000E2938
	public static void QueueOnMainThread(Action<object> taction, object tparam, float time)
	{
		if (time != 0f)
		{
			List<Loom.DelayedQueueItem> delayed = Loom.Current._delayed;
			lock (delayed)
			{
				Loom.Current._delayed.Add(new Loom.DelayedQueueItem
				{
					time = Time.time + time,
					action = taction,
					param = tparam
				});
				return;
			}
		}
		List<Loom.NoDelayedQueueItem> actions = Loom.Current._actions;
		lock (actions)
		{
			Loom.Current._actions.Add(new Loom.NoDelayedQueueItem
			{
				action = taction,
				param = tparam
			});
		}
	}

	// Token: 0x060021C0 RID: 8640 RVA: 0x000E480C File Offset: 0x000E2A0C
	public static Thread RunAsync(Action a)
	{
		Loom.Initialize();
		while (Loom.numThreads >= Loom.maxThreads)
		{
			Thread.Sleep(100);
		}
		Interlocked.Increment(ref Loom.numThreads);
		ThreadPool.QueueUserWorkItem(new WaitCallback(Loom.RunAction), a);
		return null;
	}
	public static Thread RunAsync(List<Action> a)
	{
		Loom.Initialize();
		while (Loom.numThreads >= Loom.maxThreads)
		{
			Thread.Sleep(100);
		}
		Interlocked.Increment(ref Loom.numThreads);
		for(int i = 0; i < a.Count; i++)
        {
			ThreadPool.QueueUserWorkItem(new WaitCallback(Loom.RunAction), a[i]);
		}
		return null;
	}

	// Token: 0x060021C1 RID: 8641 RVA: 0x000E4848 File Offset: 0x000E2A48
	private static void RunAction(object action)
	{
		try
		{
			((Action)action)();
		}
		catch (Exception message)
		{
			Debug.LogError(message);
		}
		finally
		{
			Interlocked.Decrement(ref Loom.numThreads);
		}
	}

	// Token: 0x060021C2 RID: 8642 RVA: 0x000E4894 File Offset: 0x000E2A94
	private void OnDisable()
	{
		if (Loom._current == this)
		{
			Loom._current = null;
		}
	}

	// Token: 0x060021C3 RID: 8643 RVA: 0x00004095 File Offset: 0x00002295
	private void Start()
	{
	}

	// Token: 0x060021C4 RID: 8644 RVA: 0x000E48AC File Offset: 0x000E2AAC
	private void Update()
	{
		if (this._actions.Count > 0)
		{
			List<Loom.NoDelayedQueueItem> actions = this._actions;
			lock (actions)
			{
				this._currentActions.Clear();
				this._currentActions.AddRange(this._actions);
				this._actions.Clear();
			}
			for (int i = 0; i < this._currentActions.Count; i++)
			{
				this._currentActions[i].action(this._currentActions[i].param);
			}
		}
		if (this._delayed.Count > 0)
		{
			List<Loom.DelayedQueueItem> delayed = this._delayed;
			lock (delayed)
			{
				this._currentDelayed.Clear();
				this._currentDelayed.AddRange(from d in this._delayed
											  where d.time <= Time.time
											  select d);
				for (int j = 0; j < this._currentDelayed.Count; j++)
				{
					this._delayed.Remove(this._currentDelayed[j]);
				}
			}
			for (int k = 0; k < this._currentDelayed.Count; k++)
			{
				this._currentDelayed[k].action(this._currentDelayed[k].param);
			}
		}
	}

	// Token: 0x04001B59 RID: 7001
	public static int maxThreads = 8;

	// Token: 0x04001B5A RID: 7002
	public static int numThreads;

	// Token: 0x04001B5B RID: 7003
	public static Loom _current;

	// Token: 0x04001B5C RID: 7004
	public static bool initialized;

	// Token: 0x04001B5D RID: 7005
	private List<Loom.NoDelayedQueueItem> _actions = new List<Loom.NoDelayedQueueItem>();

	// Token: 0x04001B5E RID: 7006
	private List<Loom.DelayedQueueItem> _delayed = new List<Loom.DelayedQueueItem>();

	// Token: 0x04001B5F RID: 7007
	private List<Loom.DelayedQueueItem> _currentDelayed = new List<Loom.DelayedQueueItem>();

	// Token: 0x04001B60 RID: 7008
	private List<Loom.NoDelayedQueueItem> _currentActions = new List<Loom.NoDelayedQueueItem>();

	// Token: 0x020012D6 RID: 4822
	public struct NoDelayedQueueItem
	{
		// Token: 0x0400657C RID: 25980
		public Action<object> action;

		// Token: 0x0400657D RID: 25981
		public object param;
	}

	// Token: 0x020012D7 RID: 4823
	public struct DelayedQueueItem
	{
		// Token: 0x0400657E RID: 25982
		public float time;

		// Token: 0x0400657F RID: 25983
		public Action<object> action;

		// Token: 0x04006580 RID: 25984
		public object param;
	}
}
