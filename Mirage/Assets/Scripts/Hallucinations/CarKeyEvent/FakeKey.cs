using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeKey : MonoBehaviour
{
    private GameObject keySpawn;

    private CarKeys carKeyScriptRef;
    void Start()
    {
        //Gets reference to the CarKeys.cs and the spawn GameObject
        carKeyScriptRef = GameObject.Find("CarKeySpawnManager").GetComponent<CarKeys>();
        keySpawn = this.transform.parent.gameObject;



        //Debug.Log("The parent of " + this.gameObject.name + " is " + this.gameObject.transform.parent.name);
    }

    public void ResetKeyLocations()
    {
        //Removes the fake key found spawn from list and destroys the spawn
        carKeyScriptRef.carKeySpawnLocations.Remove(keySpawn);
        Destroy(keySpawn);

        //Activates Random Key function
        carKeyScriptRef.RandomizeRealKeyLocation();
    }
}
