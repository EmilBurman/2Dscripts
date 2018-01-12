using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnChildEntity : MonoBehaviour
{

    public GameObject prefab;
    GameObject obj;

    public void SpawnObject(bool asChild)
    {
        obj = Instantiate(prefab);
        if (asChild)
            obj.transform.parent = gameObject.transform;
    }

    public void DestroyGameObject()
    {
        Destroy(obj);
    }
}
