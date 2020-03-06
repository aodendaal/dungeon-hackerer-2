using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RogueSharp;
using System.Linq;
using Random = UnityEngine.Random;

public class LevelController : MonoBehaviour
{
    [HideInInspector]
    public static LevelController instance;

    public Transform levelParent;
    [Header("Prefabs")]
    public GameObject[] wallPrefabs;
    public GameObject[] floorPropPrefabs;
    public GameObject[] wallPropPrefabs;
    public GameObject exitPrefab;

    public Map Map;
    private GameObject player;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Start is called before the first frame update
    public void Initialize()
    {
        ClearOldTerrain();
        var strategy = new RogueSharp.MapCreation.RandomRoomsMapCreationStrategy<Map>(27, 27, 50, 5, 3);
        Map = Map.Create(strategy);

        GenerateTerrain();
    }

    private void ClearOldTerrain()
    {
        foreach(Transform childTransform in levelParent)
        {
            Destroy(childTransform.gameObject);
        }
    }

    private void GenerateTerrain()
    {
        // Walls
        foreach (var cell in Map.GetAllCells())
        {
            if (!cell.IsWalkable)
            {
                var go = Instantiate(wallPrefabs[Random.Range(0, wallPrefabs.Length)], new Vector3(cell.X, 0f, cell.Y), Quaternion.identity);
                go.transform.SetParent(levelParent);
                go.name = $"Wall - Level {GameController.level} - ({cell.X}, {cell.Y})";
            }
        }

        // Floor Props
        var openCells = Map.GetAllCells().Where(c => c.IsWalkable).ToList();
        for (var i = 0; i < 5; i++)
        {
            var pos = openCells.ToArray()[Random.Range(0, openCells.Count)];

            var prefab = floorPropPrefabs[Random.Range(0, floorPropPrefabs.Length)];

            var go = Instantiate(prefab, new Vector3(pos.X, 0, pos.Y), Quaternion.identity);
            go.name = $"{go.name} - Level {GameController.level}";
            go.transform.SetParent(levelParent);

            openCells.Remove(pos);
        }

        // Wall Props
        for (var i = 0; i < 5; i++)
        {
            var pos = openCells.ToArray()[Random.Range(0, openCells.Count)];

            var roatations = new List<float>();
            if (!Map.GetCell(pos.X, pos.Y + 1).IsWalkable) roatations.Add(0f);
            if (!Map.GetCell(pos.X + 1, pos.Y).IsWalkable) roatations.Add(90f);
            if (!Map.GetCell(pos.X, pos.Y - 1).IsWalkable) roatations.Add(180f);
            if (!Map.GetCell(pos.X - 1, pos.Y).IsWalkable) roatations.Add(270f);

            if (roatations.Count == 0)
            {
                continue;
            }

            var rotation = roatations.ToArray()[Random.Range(0, roatations.Count)];

            var prefab = wallPropPrefabs[Random.Range(0, wallPropPrefabs.Length)];

            var go = Instantiate(prefab, new Vector3(pos.X, 0, pos.Y), Quaternion.identity);
            go.transform.SetParent(levelParent);
            if (rotation == 0f) go.name = $"{prefab.name} - Level {GameController.level} -  (North)";
            if (rotation == 90f) go.name = $"{prefab.name} - Level {GameController.level} -  (East)";
            if (rotation == 180f) go.name = $"{prefab.name} - Level {GameController.level} -  (South)";
            if (rotation == 270f) go.name = $"{prefab.name} - Level {GameController.level} -  (West)";

            go.transform.Rotate(new Vector3(0, rotation, 0));

            openCells.Remove(pos);
        }

        // Exit
        var exitPos = openCells.Last();

        var exit = Instantiate(exitPrefab, new Vector3(exitPos.X, 0, exitPos.Y), Quaternion.identity);
        exit.transform.SetParent(levelParent);
    }

    public bool IsWalkable(Vector3 worldPos)
    {
        if (!Map.GetCell((int)worldPos.x, (int)worldPos.z).IsWalkable)
            return false;

        var positions = MonsterController.instance.Monsters.Select(m => m.transform.position);
        if (positions.Any(p => p.x == worldPos.x && p.z == worldPos.z))
            return false;

        if (player.transform.position.x == worldPos.x && player.transform.position.z == worldPos.z)
            return false;

        return true;
    }
}
