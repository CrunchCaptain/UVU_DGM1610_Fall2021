using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject objPrefab;
    public int createOnStart;

    private List<GameObject> pooledObjects = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < createOnStart; x++)
        {
            CreateNewObject();
        }
    }

    GameObject CreateNewObject()
    {
        //Creates objects for pool
        GameObject obj = Instantiate(objPrefab);
        obj.SetActive(false);
        pooledObjects.Add(obj);

        return obj;
    }

    public GameObject GetObject()
    {
        // Collect all of inactive pooled objects
        GameObject obj = pooledObjects.Find(x => x.activeInHierarchy == false);

        // If the scene has 0 active objects
        if (obj == null)
        {
            obj = CreateNewObject();
        }

        // Activate objects
        obj.SetActive(true);
        return obj;
    }
}
