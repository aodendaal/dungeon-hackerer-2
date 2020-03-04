using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class SimpleSmack : MonoBehaviour
{
    public TMP_Text experienceText;

    private int experience;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AttackButton_Click();
        }
    }

    public void AttackButton_Click()
    {
        var pos = transform.position + transform.forward;

        var monster = MonsterController.instance.Monsters.FirstOrDefault(m => m.transform.position == pos);

        if (monster != null)
        {
            var component = monster.GetComponent<MonsterHit>();
            component.GetHit();
        }
    }
}
