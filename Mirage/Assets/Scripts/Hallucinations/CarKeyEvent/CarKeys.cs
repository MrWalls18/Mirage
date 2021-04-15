using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarKeys : MonoBehaviour
{
    [HideInInspector]public List<GameObject> carKeySpawnLocations;
    private List<GameObject> destroyKeys;

    [SerializeField] private GameObject carKeyPrefab, fakeCarKeyPrefab;

    private GameObject carKeyClone, fakeCarKeyClone;


    void Awake()
    {
        //Finds all key spawn locations in the scene and adds them to a list
        carKeySpawnLocations = new List<GameObject>();
        carKeySpawnLocations.AddRange(GameObject.FindGameObjectsWithTag("CarKeySpawn"));
    }


    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject carKeySpawn in carKeySpawnLocations)
        {
            //Spawns all fake keys in the beginning of the game
            fakeCarKeyClone = Instantiate(fakeCarKeyPrefab, carKeySpawn.transform.position, carKeySpawn.transform.rotation);
            fakeCarKeyClone.transform.parent = carKeySpawn.transform;
        }
    }

    public void RandomizeRealKeyLocation()
    {
        //Finds all keys in the scene and adds them to a list
        destroyKeys = new List<GameObject>();
        destroyKeys.AddRange(GameObject.FindGameObjectsWithTag("FakeKey"));

        //Destorys all fake keys found in the scene
        foreach (GameObject keys in destroyKeys)
        {
            Destroy(keys);
        }

        //Finds and destroys the real key
        GameObject realKey = GameObject.Find("CarKey");
        if (realKey != null)
        {
            Destroy(realKey);
        }

        //Randomizes where key will be found
        int realKeyIndex = Random.Range(0, carKeySpawnLocations.Count);


        //Cycles through the list and spawns 1 real key at the random location
        //The rest of the spawn points will spawn a fake key
        for (int i = 0; i < carKeySpawnLocations.Count; i++)
        {
            if (i == realKeyIndex)
            {
                carKeyClone = Instantiate(carKeyPrefab, carKeySpawnLocations[realKeyIndex].transform.position, carKeySpawnLocations[realKeyIndex].transform.rotation);

                carKeyClone.transform.parent = carKeySpawnLocations[realKeyIndex].transform;

                carKeyClone.name = "CarKey";
            }

            else
            {
                fakeCarKeyClone = Instantiate(fakeCarKeyPrefab, carKeySpawnLocations[i].transform.position, carKeySpawnLocations[i].transform.rotation);

                fakeCarKeyClone.transform.parent = carKeySpawnLocations[i].transform;
            }
        }
    }
}
