using System.Linq;
using TMPro;
using UnityEngine;

public class SimpleMove : MonoBehaviour
{
    public TMP_Text positionText;

    private bool isMoving = false;

    // Update is called once per frame
    private void Update()
    {
        if (GameController.IsPaused)
        {
            return;
        }

        if (isMoving)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Keypad8))
        {
            MoveForward();
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            MoveBackward();
        }

        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            MoveRight();
        }

        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            MoveLeft();
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.Keypad7))
        {
            TurnLeft();
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.Keypad9))
        {
            TurnRight();
        }
    }

    private void TurnRight()
    {
        isMoving = true;
        LeanTween.rotateAround(gameObject, Vector3.up, 90f, 0.2f).setOnComplete(() =>
        {
            ShowLocation();
            isMoving = false;
        });
    }

    private void TurnLeft()
    {
        isMoving = true;
        LeanTween.rotateAround(gameObject, Vector3.up, -90f, 0.2f).setOnComplete(() =>
        {
            ShowLocation();
            isMoving = false;
        });
    }

    private void MoveLeft()
    {
        var newPos = transform.position - transform.right;
        newPos = new Vector3(Mathf.RoundToInt(newPos.x), 0f, Mathf.RoundToInt(newPos.z));

        if (LevelController.instance.IsWalkable(newPos))
        {
            isMoving = true;
            LeanTween.move(gameObject, newPos, 0.1f).setOnComplete(() =>
            {
                ShowLocation();
                isMoving = false;
            });
        }
        else
        {
            GameInfo.instance.Log("Bump");
        }
    }

    private void MoveRight()
    {
        var newPos = transform.position + transform.right;
        newPos = new Vector3(Mathf.RoundToInt(newPos.x), 0f, Mathf.RoundToInt(newPos.z));

        if (LevelController.instance.IsWalkable(newPos))
        {
            isMoving = true;
            LeanTween.move(gameObject, newPos, 0.1f).setOnComplete(() =>
            {
                ShowLocation();
                isMoving = false;
            });
        }
        else
        {
            GameInfo.instance.Log("Bump");
        }
    }

    private void MoveBackward()
    {
        var newPos = transform.position - transform.forward;
        newPos = new Vector3(Mathf.RoundToInt(newPos.x), 0f, Mathf.RoundToInt(newPos.z));

        if (LevelController.instance.IsWalkable(newPos))
        {
            isMoving = true;
            LeanTween.move(gameObject, newPos, 0.1f).setOnComplete(() =>
            {
                ShowLocation();
                isMoving = false;
            });
        }
        else
        {
            GameInfo.instance.Log("Bump");
        }
    }

    private void MoveForward()
    {
        var newPos = transform.position + transform.forward;
        newPos = new Vector3(Mathf.RoundToInt(newPos.x), 0f, Mathf.RoundToInt(newPos.z));

        if (LevelController.instance.IsWalkable(newPos))
        {
            isMoving = true;

            LeanTween.move(gameObject, newPos, 0.1f).setOnComplete(() =>
            {
                ShowLocation();
                isMoving = false;
            });
        }
        else
        {
            GameInfo.instance.Log($"Bump");
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
        LeanTween.cancel(gameObject);
        isMoving = false;

        var walkable = LevelController.instance.Map.GetAllCells().Where(c => c.IsWalkable).ToList();
        var monsterPositions = MonsterController.instance.Monsters.Select(m => m.transform.position);
        walkable.RemoveAll(c => monsterPositions.Any(m => m.x == c.X && m.z == c.Y));

        var first = walkable.First();
        transform.position = new Vector3(first.X, 0f, first.Y);

        ShowLocation();
    }

    #region Button Clicks

    public void MoveLeftButton_Click()
    {
        MoveLeft();
    }

    public void MoveRightButton_Click()
    {
        MoveRight();
    }

    public void MoveForwardButton_Click()
    {
        MoveForward();
    }

    public void MoveBackardButton_Click()
    {
        MoveBackward();
    }

    public void TurnLeftButton_Click()
    {
        TurnLeft();
    }

    public void TurnRightButton_Click()
    {
        TurnRight();
    }

    #endregion
}