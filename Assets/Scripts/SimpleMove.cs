using System.Linq;
using TMPro;
using UnityEngine;

public class SimpleMove : MonoBehaviour
{
    public TMP_Text positionText;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Keypad8))
        {
            var newPos = transform.position + transform.forward;
            newPos = new Vector3(Mathf.RoundToInt(newPos.x), 0f, Mathf.RoundToInt(newPos.z));

            if (LevelController.instance.IsWalkable(newPos))
            {
                transform.position = newPos;
                ShowLocation();
            }
            else
            {
                GameInfo.instance.Log($"Bump");
            }
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            var newPos = transform.position - transform.forward;
            newPos = new Vector3(Mathf.RoundToInt(newPos.x), 0f, Mathf.RoundToInt(newPos.z));

            if (LevelController.instance.IsWalkable(newPos))
            {
                transform.position = newPos;
                ShowLocation();
            }
            else
            {
                GameInfo.instance.Log("Bump");
            }
        }

        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            var newPos = transform.position + transform.right;
            newPos = new Vector3(Mathf.RoundToInt(newPos.x), 0f, Mathf.RoundToInt(newPos.z));

            if (LevelController.instance.IsWalkable(newPos))
            {
                transform.position = newPos;
                ShowLocation();
            }
            else
            {
                GameInfo.instance.Log("Bump");
            }
        }

        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            var newPos = transform.position - transform.right;
            newPos = new Vector3(Mathf.RoundToInt(newPos.x), 0f, Mathf.RoundToInt(newPos.z));

            if (LevelController.instance.IsWalkable(newPos))
            {
                transform.position = newPos;
                ShowLocation();
            }
            else
            {
                GameInfo.instance.Log("Bump");
            }
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Keypad7))
        {
            transform.Rotate(new Vector3(0, -90f, 0));
            ShowLocation();
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Keypad9))
        {
            transform.Rotate(new Vector3(0, 90f, 0));
            ShowLocation();
        }
    }

    private void ShowLocation()
    {
        var direction = "Unknown";
        if (transform.eulerAngles.y == 0) direction = "North";
        if (transform.eulerAngles.y == 90) direction = "East";
        if (transform.eulerAngles.y == 180) direction = "South";
        if (transform.eulerAngles.y == 270) direction = "West";
        positionText.text = $"Position: ({transform.position.x}, {transform.position.z}) Facing: {direction}";
    }

    public void PlacePlayer()
    {
        var walkable = LevelController.instance.Map.GetAllCells().Where(c => c.IsWalkable).ToList();
        var monsterPositions = MonsterController.instance.Monsters.Select(m => m.transform.position);
        walkable.RemoveAll(c => monsterPositions.Any(m => m.x == c.X && m.z == c.Y));

        var first = walkable.First();
        transform.position = new Vector3(first.X, 0f, first.Y);

        ShowLocation();
    }
}