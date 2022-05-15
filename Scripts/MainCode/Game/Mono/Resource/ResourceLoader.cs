using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using System.Threading;
using Tool;

namespace ShuShan
{
    public class ResourceLoader:BaseMono
    {
		public static ResourceLoader Instance { get; private set; }
		private void Start()
		{
			this.cacheroot = new GameObject("_ObjCache");
			Transform transform = this.cacheroot.transform;
			transform.position = Vector3.zero;
			transform.localScale = Vector3.one;
			transform.rotation = Quaternion.identity;
			this.cacheroottransform = transform;
		}
		private void OnDestroy()
		{
			foreach (Thread thread in this.loadsyncs)
			{
				if (thread.IsAlive)
				{
					thread.Interrupt();
				}
			}
			this.loadsyncs.Clear();
			this.loadCache.Clear();
			this.cacheMap.Clear();
			ResourceLoader.Instance = null;
		}
		public void Preloading(List<string> paths)
		{
			base.StartCoroutine(this._Preloading(paths));
		}

		private IEnumerator _Preloading(List<string> paths)
		{
			foreach (string path in paths)
			{
				yield return Resources.LoadAsync(path);
			}
			yield break;
		}
		/*
		public UnityEngine.Object Load(string path)
		{
			UnityEngine.Object @object = null;
			if (!this.loadCache.TryGetValue(path, out @object))
			{
				@object = Resources.Load(path);
				if (@object == null)
				{
					string fileExist = ModsMgr.Instance.GetFileExist("Resources/" + path);
					if (!string.IsNullOrEmpty(fileExist))
					{
						if (this.mapOutsideObj.TryGetValue(path, out @object))
						{
							return @object;
						}
						using (AssetLoader assetLoader = new AssetLoader())
						{
							try
							{
								GameObject gameObject = assetLoader.LoadFromFile(fileExist.Replace("/", "\\"), this.GetAssetLoaderOptions(), null, null, null);
								if (gameObject != null)
								{
									gameObject.transform.GetChild(0).localEulerAngles = new Vector3(-90f, 180f, 0f);
									@object = gameObject;
									this.mapOutsideObj.Add(path, @object);
									MeshRenderer[] componentsInChildren = gameObject.GetComponentsInChildren<MeshRenderer>();
									for (int i = componentsInChildren.SafeLength<MeshRenderer>() - 1; i >= 0; i--)
									{
										MeshRenderer meshRenderer = componentsInChildren[i];
										Material[] sharedMaterials = meshRenderer.sharedMaterials;
										if (sharedMaterials != null)
										{
											foreach (Material material in sharedMaterials)
											{
												if (!(material == null))
												{
													if (material.HasProperty("_Color"))
													{
														material.SetColor("_Color", Color.white);
													}
												}
											}
										}
									}
								}
							}
							catch (Exception ex)
							{
								Debug.LogError(ex.ToString());
							}
						}
					}
				}
				this.loadCache[path] = @object;
			}
			return @object;
		}
		*/
		private static float KILLTIME = 120f;

		private GameObject cacheroot;

		private Transform cacheroottransform;

		private Dictionary<string, Queue<GameObject>> cacheMap = new Dictionary<string, Queue<GameObject>>();

		private Dictionary<string, float> cacheTime = new Dictionary<string, float>();

		private List<Thread> loadsyncs = new List<Thread>();

		private Dictionary<string, UnityEngine.Object> mapOutsideObj = new Dictionary<string, UnityEngine.Object>();

		private Dictionary<string, UnityEngine.Object> loadCache = new Dictionary<string, UnityEngine.Object>();

		private List<string> removelist = new List<string>();

		private List<string> relist = new List<string>();

		private float removet;

		private int BuildingCount;

		private List<string> AsyncLoading = new List<string>();
	}
}
