using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityObjectResolver;

namespace _Project
{
    public class GridMap : MonoBehaviour, IGridMap
    {
        [SerializeField] private TwoArray<Platform> _grid = new TwoArray<Platform>();

        private void Awake()
        {
            ObjectResolver.Scene.Register<IGridMap>(this);
        }

        public void SetupMap(ArrayLine<Platform>[] values) => _grid.Set(values);

        public ArrayLine<Platform>[] GetPlatforms() => _grid.GetAll();

        public Platform GetPlatform(Vector2Int index) => _grid.Get(index.x, index.y);

        public Platform FindPlatform(PlatformType platformType)
        {
            return _grid.GetAll()
                .SelectMany(line => line.Values)
                .Where(p => p != null && p.Type == platformType)
                .FirstOrDefault();
        }

        public Platform[] FindPlatforms<T>(PlatformType platformType)
        {
            return _grid.GetAll()
                .SelectMany(line => line.Values)
                .Where(platform => platform.Type == platformType)
                .ToArray();
        }

        public bool CheckPlatform(Vector2Int index)
        {
            return _grid.Check(index.x, index.y);
        }

        public bool TryGetPlatform(Vector2Int index, out Platform platform)
        {
            if (!CheckPlatform(index))
            {
                platform = null;
                return false;
            }

            platform = GetPlatform(index);
            return true;
        }
    }

}