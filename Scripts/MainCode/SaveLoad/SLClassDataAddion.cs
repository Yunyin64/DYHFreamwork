using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ShuShan
{
	public abstract class SLClassDataAddion<T>
	{
		public abstract T GetSaveData();

		public abstract void LoadSaveData(T data);
	}
}