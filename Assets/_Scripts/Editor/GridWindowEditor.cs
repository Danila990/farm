using System;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.GridWindow
{
    [Serializable]
    public class ConstructorLine
    {
        public PlatformType[] lineY;
    }

    public class GridWindowEditor : EditorWindow
    {
        [SerializeField] private GridMap _gridMap;
        [SerializeField] private PlatformArray _platforms;
        [SerializeField] private Player _player;
        [SerializeField] private PlayerArrow _arrow;

        private float _offsetPlatform = 2f;
        private Vector2Int _sizeGrid = new Vector2Int(1, 1);
        private ConstructorLine[] _linesX;

        [MenuItem("Tools/GridWindow")]
        public static void ShowWindow() => GetWindow<GridWindowEditor>("Grid Window");

        private void OnEnable()
        {
            ResetGrid();
            StartFind();
        }

        private void OnGUI()
        {
            LevelEditorExtension.BaseMidlleText("Settings", 15);
            DrawFields();
            DrawGridSizeControls();
            DrawActionButtons();
            LevelEditorExtension.BaseMidlleText("GridMap", 15, 5);
            DrawPreviewGrid();
        }

        private void StartFind()
        {
            _player = FindFirstObjectByType<Player>();
            _gridMap = FindFirstObjectByType<GridMap>();

            ConverToPreviewGrid();
        }

        private void DrawActionButtons()
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Create Grid Map"))
            {
                CreateGrid();
                CreatePlayer();
                CreateArrow();
            }

            if (GUILayout.Button("Destroy Grid Map"))
            {
                DestroyGrid();
                DestroyPlayer();
                DestroyArrow();
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Reset Preview Grid"))
                ResetGrid();

            if (GUILayout.Button("Reset Preview Types"))
                ResetPlatformsTypes();
            EditorGUILayout.EndHorizontal();
        }

        private void DrawGridSizeControls()
        {
            EditorGUILayout.BeginHorizontal();
            _sizeGrid = EditorGUILayout.Vector2IntField("Grid map Size", _sizeGrid);
            _sizeGrid = Vector2Int.Max(Vector2Int.Min(_sizeGrid, new Vector2Int(10, 10)), new Vector2Int(0, 0));
            if (GUILayout.Button("Create Prew map", GUILayout.Width(100)))
            {
                if (_sizeGrid.x > 0 && _sizeGrid.y > 0)
                    CreatePreviewGrid(_sizeGrid);
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawFields()
        {
            EditorGUILayout.Space(5);
            LevelEditorExtension.SerializedCustomPropetry(this, "_platforms");
            LevelEditorExtension.SerializedCustomPropetry(this, "_gridMap");
            LevelEditorExtension.SerializedCustomPropetry(this, "_player");
            LevelEditorExtension.SerializedCustomPropetry(this, "_arrow");
            _platforms.Enable();
            _offsetPlatform = EditorGUILayout.FloatField(new GUIContent("Offset Platform", "Distance between platforms"), _offsetPlatform);
        }

        #region GridLogic
        /*        private void CreatePrefab()
                {
                    if (_gridMap != null)
                    {
                        string localPath = $"Assets/{_savePathGrid}/{_gridMap.gameObject.name}.prefab";
                        localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
                        GameObject prefab = PrefabUtility.SaveAsPrefabAsset(_gridMap.gameObject, localPath);
                        Debug.Log($"Prefab created at: {localPath}", prefab);
                        DestroyGrid();
                    }
                }
        */

        private void CreatePlayer()
        {
            if (_gridMap == null) return;

            if (_player != null)
                DestroyPlayer();

            string[] guids = AssetDatabase.FindAssets("Player t:prefab", new[] { "Assets/Content/Prefabs" });
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                Player prefab = AssetDatabase.LoadAssetAtPath<Player>(path);

                if(prefab != null)
                {
                    _player = Instantiate(prefab);
                    _player.transform.position = _gridMap.FindPlatform(PlatformType.StartPlayer).transform.position;
                    Undo.RegisterCreatedObjectUndo(_player.gameObject, "Create Player");
                    EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                    break;
                }
            }

            if (_player == null)
                throw new NullReferenceException("Player Create Error");
        }

        private void DestroyPlayer()
        {
            if (_player == null) return;

            DestroyImmediate(_player.gameObject);
        }

        private void CreateArrow()
        {
            if (_gridMap == null) return;

            if (_arrow != null)
                DestroyArrow();

            string[] guids = AssetDatabase.FindAssets("Arrow t:prefab", new[] { "Assets/Content/Prefabs" });
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                PlayerArrow prefab = AssetDatabase.LoadAssetAtPath<PlayerArrow>(path);

                if (prefab != null)
                {
                    _arrow = Instantiate(prefab);
                    _arrow.transform.position = _gridMap.FindPlatform(PlatformType.StartPlayer).transform.position;
                    Undo.RegisterCreatedObjectUndo(_arrow.gameObject, "Create Arrow");
                    EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
                    break;
                }
            }

            if (_arrow == null)
                throw new NullReferenceException("Arrow Create Error");
        }

        private void DestroyArrow()
        {
            if (_arrow == null) return;

            DestroyImmediate(_arrow.gameObject);
        }

        private PlatformType[,] ConvertGrid()
        {
            if (_linesX == null || _linesX.Length == 0) return new PlatformType[0, 0];

            int width = _linesX.Length;
            int height = _linesX[0].lineY.Length;
            PlatformType[,] platforms = new PlatformType[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    platforms[x, y] = _linesX[x].lineY[y];
                }
            }
            return platforms;
        }

        private void CreateGrid()
        {
            if (_gridMap != null)
                DestroyGrid();

            PlatformType[,] platfromTypes = ConvertGrid();
            Vector2Int gridSize = new Vector2Int(platfromTypes.GetLength(0), platfromTypes.GetLength(1));
            Vector3 spawnOffset = MiddleOffest(_offsetPlatform, gridSize);

            _gridMap = new GameObject($"GridMap: X-{platfromTypes.GetLength(0)},Y-{platfromTypes.GetLength(1)}").AddComponent<GridMap>();

            ArrayLine<Platform>[] grid = new ArrayLine<Platform>[gridSize.x];
            for (int x = 0; x < gridSize.x; x++)
            {
                grid[x].Values = new Platform[gridSize.y];
                for (int y = 0; y < gridSize.y; y++)
                {
                    Platform platform = CreatePlatform(platfromTypes[x, y], x, y, spawnOffset);
                    grid[x].Values[y] = platform;
                    platform.SetIndex(new Vector2Int(x, y));
                    platform.gameObject.name = $"{x}, {y}";
                }
            }

            _gridMap.SetupMap(grid);
            Undo.RegisterCreatedObjectUndo(_gridMap.gameObject, "Create Grid map");
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }

        private void DestroyGrid()
        {
            if (_gridMap == null) return;

            DestroyImmediate(_gridMap.gameObject);
            _gridMap = null;
        }

        private Platform CreatePlatform(PlatformType platformType, int gridHeightX, int gridWidthY, Vector3 spawnOffset)
        {
            string namePlatform = $"Platform_{platformType}";
            Platform platform = Instantiate(_platforms.Get<Platform>(namePlatform));
            platform.transform.position = new Vector3(gridHeightX * _offsetPlatform, 0, gridWidthY * _offsetPlatform) - spawnOffset;
            platform.transform.parent = _gridMap.transform;
            return platform;
        }

        private Vector3 MiddleOffest(float platformOffset, Vector2Int gridSize)
        {
            float gridWidth = gridSize.y * platformOffset - platformOffset;
            float gridHeight = gridSize.x * platformOffset - platformOffset;
            return new Vector3(gridWidth, 0, gridHeight) / 2;
        }
        #endregion

        #region Preview Grid Drawing

        private void ConverToPreviewGrid()
        {
            if(_gridMap == null) return;

            var platforms = _gridMap.GetPlatforms();

            _linesX = new ConstructorLine[platforms.Length];
            for (int x = 0; x < _linesX.Length; x++)
            {
                _linesX[x] = new ConstructorLine();
                _linesX[x].lineY = new PlatformType[platforms[x].Values.Length];
                for (int y = 0; y < _linesX[x].lineY.Length; y++)
                {
                    _linesX[x].lineY[y] = platforms[x].Values[y].Type;
                }
            }
        }

        private void CreatePreviewGrid(Vector2Int size)
        {
            _linesX = new ConstructorLine[size.x];
            for (int x = 0; x < size.x; x++)
            {
                _linesX[x] = new ConstructorLine();
                _linesX[x].lineY = new PlatformType[size.y];
                for (int y = 0; y < size.y; y++)
                {
                    _linesX[x].lineY[y] = PlatformType.Default;
                }
            }
        }

        private void ResetGrid()
        {
            _linesX = new ConstructorLine[3]
            {
                    new ConstructorLine()
                    {
                        lineY = new PlatformType[3]{ PlatformType.StartPlayer, PlatformType.Default, PlatformType.Fruit }
                    },
                    new ConstructorLine()
                    {
                        lineY = new PlatformType[3]{ PlatformType.Empty, PlatformType.Fruit, PlatformType.Rock }
                    },
                    new ConstructorLine()
                    {
                        lineY = new PlatformType[3]{ PlatformType.Fruit, PlatformType.Default, PlatformType.Finish }
                    }
            };
        }

        private void ResetPlatformsTypes()
        {
            if (_linesX == null) return;

            for (int x = 0; x < _linesX.Length; x++)
            {
                for (int y = 0; y < _linesX[x].lineY.Length; y++)
                {
                    _linesX[x].lineY[y] = PlatformType.Default;
                }
            }
        }

        private void DrawPreviewGrid()
        {
            if (_linesX == null || _linesX.Length == 0) return;

            for (int i = 0; i < _linesX.Length; i++)
            {
                EditorGUILayout.Space(5);
                EditorGUILayout.BeginHorizontal(GUILayout.Width(50));

                for (int j = 0; j < _linesX[i].lineY.Length; j++)
                {
                    EditorGUILayout.BeginVertical(GUILayout.Width(50));

                    Rect rect = GUILayoutUtility.GetRect(20, 20);
                    EditorGUI.DrawRect(rect, GetPlatformColor(_linesX[i].lineY[j]));

                    _linesX[i].lineY[j] = (PlatformType)EditorGUILayout.EnumPopup(_linesX[i].lineY[j]);

                    EditorGUILayout.EndVertical();
                    EditorGUILayout.Space(5);
                }

                EditorGUILayout.EndHorizontal();
            }
        }

        private Color GetPlatformColor(PlatformType platformType)
        {
            return platformType switch
            {
                PlatformType.Default => Color.white,
                PlatformType.StartPlayer => Color.green,
                PlatformType.Empty => Color.black,
                PlatformType.Finish => Color.blue,
                PlatformType.Fruit => Color.yellow,
                PlatformType.Rock => Color.red,
                _ => Color.gray,
            };
        }
        #endregion
    }
}