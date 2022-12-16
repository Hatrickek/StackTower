using Sirenix.OdinInspector;
using UnityEngine;
using UnityEditor;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject PlatformPrefab;

    [SerializeField] private GameObject EndPlatform;

    [SerializeField] private float AngleStep;

    [SerializeField] private float PlatformHeight;

    [SerializeField] private float RotationSpeed = 70;

    [SerializeField] private Material GoodMaterial;

    [SerializeField] private Material BadMaterial;

    [SerializeField] private GameSO GameSO;

    private GameObject m_CurrentLevel;

    private void Awake()
    {
        GameSO.State = GameSO.GameState.Alive;
        LoadLevel();
    }

    private void Update()
    {
        transform.Rotate(Vector3.up * RotationSpeed * Time.deltaTime);
    }

    private void SaveLevel()
    {
        PrefabUtility.SaveAsPrefabAsset(m_CurrentLevel, "Assets/Resources/level.prefab", out var ok);
        Debug.Log(ok ? "Level saved successfully." : "Couldn't save the level.");
    }

    private void LoadLevel()
    {
        if (m_CurrentLevel != Instantiate(Resources.Load("level") as GameObject, transform)) return;
        Debug.Log("Succesfully loaded level.");
    }

    public void GenerateNewLevel()
    {
        BallController.ResetPosition();
        Camera.ResetPosition();
        Clear();
        GenerateLevel();
        GameSO.State = GameSO.GameState.Alive;
        GameSO.Score = 0;
    }

    [Button("Generate Level")]
    public void GenerateLevel()
    {
        Clear();
        m_CurrentLevel = new GameObject("Level");
        m_CurrentLevel.transform.parent = this.transform;
        for (var i = 0; i < GameSO.PlatformAmount; i++)
        {
            var newObj = Instantiate(PlatformPrefab, Vector3.up * -PlatformHeight * i, Quaternion.Euler(0, AngleStep * 1, 0), m_CurrentLevel.transform);

            var childCount = newObj.transform.childCount;
            for (var j = childCount - 1; j >= 0; j--)
            {
                var child = newObj.transform.GetChild(j).gameObject;
                child.tag = "Good";
                child.GetComponent<Renderer>().sharedMaterial = GoodMaterial;
            }

            if (Random.Range(0, 100) < 50)
            {
                var randChild = Random.Range(0, childCount);
                for (var j = childCount - 1; j >= 0; j--)
                {
                    if (j == randChild)
                        continue;
                    var child = newObj.transform.GetChild(j).gameObject;
                    child.tag = "Bad";
                    child.GetComponent<Renderer>().sharedMaterial = BadMaterial;
                }
            }
        }

        var endPlatform = Instantiate(EndPlatform, Vector3.up * -PlatformHeight * GameSO.PlatformAmount, Quaternion.identity, m_CurrentLevel.transform);
        endPlatform.tag = "End";

        SaveLevel();
    }

    [Button("Clear")]
    public void Clear()
    {
        var childCount = transform.childCount;
        for (var i = childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }
    }
}
