using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [HideInInspector]
    public static MonsterController instance;

    public List<GameObject> Monsters;

    public Transform parentTransform;

    public GameObject monsterPrefab;

    void Awake()
    {
        instance = this;
    }

    public void Initialize()
    {
        DestroyOldMonsters();

        GenerateMonsters();
    }

    private void DestroyOldMonsters()
    {
        foreach (var monster in Monsters)
        {
            Destroy(monster);
        }
    }

    private void GenerateMonsters()
    {
        Monsters = new List<GameObject>();

        var openCells = LevelController.instance.Map.GetAllCells().Where(c => c.IsWalkable).ToArray();
        for (var i = 0; i < 10; i++)
        {
            var walkable = openCells[Random.Range(0, openCells.Length)];

            var monster = Instantiate(monsterPrefab, new Vector3(walkable.X, 0, walkable.Y), Quaternion.identity);
            monster.name = $"{monster.name} - Level {GameController.level}";
            monster.transform.SetParent(parentTransform);

            Monsters.Add(monster);
        }
    }
}
