using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project
{
    [System.Serializable]
    public struct ArrayLine<T> 
    {
        public T[] Values;
    }

    [System.Serializable]
    public class TwoArray<T> where T : Object
    {
        [SerializeField] private ArrayLine<T>[] _values;

        public int LenghtX => _values.Length;
        public int LenghtY => _values[0].Values.Length;

        public Vector2Int Size => new Vector2Int(LenghtX, LenghtY);

        public void Set(ArrayLine<T>[] values) => _values = values;

        public ArrayLine<T>[] GetAll() => _values;

        public T Get(int x, int y)
        {
            if (!Check(x, y))
                throw new ArgumentException($"Index error: X-{x}, Y-{y}");

            return _values[x].Values[y];
        }

        public bool Check(int x, int y)
        {
            if (x < 0 || y < 0 || x >= LenghtX || y >= LenghtY)
                return false;

            return true;
        }

        public T[,] Convert()
        {
            T[,] newArray = new T[LenghtX, LenghtY];
            for (int x = 0; x < LenghtX; x++)
            {
                for (int y = 0; y < LenghtY; y++)
                {
                    newArray[x, y] = _values[x].Values[y];
                }
            }

            return newArray;
        }
    }
}