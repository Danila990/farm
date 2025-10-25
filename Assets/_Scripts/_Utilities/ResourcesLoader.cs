using System;
using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project
{
    public static class ResourcesLoader
    {
        public static Task<T> LoadAsync<T>(string path) where T : Object
        {
            var tcs = new TaskCompletionSource<T>();
            ResourceRequest request = Resources.LoadAsync<T>(path);

            request.completed += _ =>
            {
                if (request.asset is T asset)
                    tcs.SetResult(asset);
                else
                    tcs.SetException(new Exception($"Failed to load resource at path: {path}"));
            };

            return tcs.Task;
        }

        public static T Load<T>(string path) where T : Object
        {
            T loadObejct = Resources.Load<T>(path);
            if (loadObejct == null)
                throw new ArgumentNullException($"Failed to load resource at path: {path}");

            return loadObejct;
        }

        public static T LoadInstantiate<T>(string path) where T : Object
        {
            T loadObejct = Load<T>(path);
            return Object.Instantiate(loadObejct); ;
        }

        public static async Task<T> LoadInstantiateAsync<T>(string path) where T : Object
        {
            T data = await LoadAsync<T>(path);
            return Object.Instantiate(data);
        }
    }
}