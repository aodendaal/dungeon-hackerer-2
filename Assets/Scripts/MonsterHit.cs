using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHit : MonoBehaviour
{
    [SerializeField]
    private GameObject meshObject;

    [SerializeField]
    public Material[] hitMaterials;
    private Material[] defaultMaterials;

    private bool isHit = false;
    private float hitTime;
    private float hitDuration = 0.1f;

    private MeshRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = meshObject.GetComponent<MeshRenderer>();
        defaultMaterials = renderer.materials;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHit)
        {
            if (Time.time > hitTime)
            {
                renderer.materials = defaultMaterials;
                isHit = false;

                MonsterController.instance.Monsters.Remove(gameObject);

                Destroy(gameObject);
            }
        }
    }

    public void GetHit()
    {
        isHit = true;
        renderer.materials = hitMaterials;
        hitTime = Time.time + hitDuration;
    }
}
