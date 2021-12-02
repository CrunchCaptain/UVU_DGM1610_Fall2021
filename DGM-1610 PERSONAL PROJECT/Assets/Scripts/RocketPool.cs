using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPool : MonoBehaviour
{
    public GameObject rocketPref;
    public int createOnStart;

    private readonly List<GameObject> rocketPool = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < createOnStart; x++)
            createRockets();
    }

    GameObject createRockets()
    {
        GameObject rocket = Instantiate(rocketPref);
        rocket.SetActive(false);
        rocketPool.Add(rocket);

        return rocket;
    }

    public GameObject GetRocket()
    {
        GameObject rocket = rocketPool.Find(x => x.activeInHierarchy == false);

        if (rocket == null)
            rocket = createRockets();

        rocket.SetActive(true);
        return rocket;
    }
}
