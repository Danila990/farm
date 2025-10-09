using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project
{
    [System.Serializable]
    public class MoodArray<TObject> where TObject : Object
    {
        [SerializeField] protected TObject[] _array;

        public TReturn Get<TReturn>(string key) where TReturn : TObject
        {
            return (TReturn)Get(key);
        }

        public TObject Get(string key)
        {
            foreach (var info in _array)
            {
                if (info.name.Equals(key))
                    return info;
            }

            throw new NullReferenceException($"The desired object is missing: Key - {key}");
        }
    }
}