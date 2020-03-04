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

    [Header("Parent")]
    public Transform levelParent;
    [Header("Prefabs")]
    public GameObject wallPrefab;

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
        var strategy = new RogueSharp.MapCreation.RandomRoomsMapCreationStrategy<Map>(27, 27, 50, 5, 3);
        Map = Map.Create(strategy);

        GenerateTerrain();
    }

    private void GenerateTerrain()
    {
        foreach (var cell in Map.GetAllCells())
        {
            if (!cell.IsWalkable)
            {
                var go = Instantiate(wallPrefab, new Vector3(cell.X, 0f, cell.Y), Quaternion.identity);
                go.transform.SetParent(levelParent);
                go.name = $"Wall ({cell.X}, {cell.Y})";
            }
        }
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
