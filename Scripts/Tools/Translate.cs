using System;
using System.Collections.Generic;
using ShuShan;
using UnityEngine;
public static class StringTranslate
{
    public static List<T> GetUniqueValue<T>(this List<T> list)
    {
        List<T> tmp = new List<T>();
        for (int i = 0; i < list.Count; i++)
        {
            if (!tmp.Contains(list[i]))
                tmp.Add(list[i]);
        }
        return tmp;
    }

    public static V GetRandomMapItem<T,V>(this Dictionary<T,V> map)
    {
        List<V> list = new List<V>();
        list.AddRange(map.Values);
        int index = UnityEngine.Random.Range(0, list.Count);
        return list[index];
    }
    public static List<T> Superpose<T>(this List<T> me, params List<T>[] lists)
    {
        foreach(var l in lists)
        {
            me.AddRange(l);
        }
        return me;
    }
    public static string Translate(this string str)
    {
        return str;
    }
    public static void Add<T>(this List<KeyValuePair<float, T>> List,float v1,T v2)
    {
        List.Add(new KeyValuePair<float, T>(v1, v2));
    }
    public static WeightResult<T> WeightCalculate<T>(this List<KeyValuePair<float, T>> List)
    {
        List<KeyValuePair<float, T>> temp = new List<KeyValuePair<float, T>>();

        WeightResult<T> WeightResult = new WeightResult<T>();
        float weight = 0;
        foreach (var item in List)
        {
            if (item.Key == 0) {
                WeightResult.Zeros.Add(item.Value);
                continue;
            }
            if (item.Key < 0)
            {
                WeightResult.Specials.Add(item.Value);
                continue;
            }

            weight += item.Key;
            temp.Add(new KeyValuePair<float, T>(weight, item.Value));
        }
        float index = UnityEngine.Random.Range(0, weight);
        WeightResult.index = index;
        for(int i = 0; i < temp.Count; i++)
        {
            if (index < temp[i].Key) {
                WeightResult.Result = temp[i].Value;
                break;
            } 
        }

        return WeightResult;
    }

    public class WeightResult<T> 
    {
        public T Result;

        public float index;

        public List<T> Zeros = new List<T>();

        public List<T> Specials = new List<T>();

    }

    public static List<int> RandomIndex(int num)
    {
        List<int> array = new List<int>();
        for (int i = 0; i < num; i++)
        {
            array.Add(i + 1);
        }
        System.Random rand = new System.Random();
        int iRand, iTmp;
        for (int i = array.Count - 1; i > 0; i--)
        {
            iRand = rand.Next(i);
            iTmp = array[i];
            array[i] = array[iRand];
            array[iRand] = iTmp;
        }
        return array;
    }

    public static float Limit(this float val,float up,float down)
    {
        return Math.Max(Math.Min(up, val), down);
    }
}