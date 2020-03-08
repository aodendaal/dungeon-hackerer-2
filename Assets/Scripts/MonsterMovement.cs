using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterMovement : MonoBehaviour
{
    private float moveTime;
    private float moveRate = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        moveTime = Time.time + moveRate;    
    }

    // Update is called once per frame
    void Update()
    {
        if (GameController.IsPaused)
        {
            return;
        }

        if (Time.time > moveTime)
        {
            var options = new List<Vector3>();

            if (LevelController.instance.IsWalkable(transform.position + new Vector3(0, 0, 1f))) options.Add(transform.position + new Vector3(0, 0, 1f));
            if (LevelController.instance.IsWalkable(transform.position + new Vector3(0, 0, -1f))) options.Add(transform.position + new Vector3(0, 0, -1f));
            if (LevelController.instance.IsWalkable(transform.position + new Vector3(1f, 0, 0))) options.Add(transform.position + new Vector3(1f, 0, 0));
            if (LevelController.instance.IsWalkable(transform.position + new Vector3(-1f, 0, 0))) options.Add(transform.position + new Vector3(-1f, 0, 0));

            if (options.Count > 0)
            {
                var newPos = options.ToArray()[Random.Range(0, options.Count)];

                transform.position = newPos;
            }

            moveTime = Time.time + moveRate;
        }
    }
}
