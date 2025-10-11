using System.Collections.Generic;
using UnityEditor;

namespace _Project.GridWindow
{
    [System.Serializable]
    public class PlatformArray : MoodArray<Platform>
    {
        public void Enable()
        {
            List<Platform> list = new List<Platform>();
            string[] guids = AssetDatabase.FindAssets("Platform t:prefab", new[] { "Assets/Content/Prefabs/Platforms" });
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                Platform prefab = AssetDatabase.LoadAssetAtPath<Platform>(path);

                if (prefab != null)
                    list.Add(prefab);
            }

            _array = list.ToArray();
        }
    }
}