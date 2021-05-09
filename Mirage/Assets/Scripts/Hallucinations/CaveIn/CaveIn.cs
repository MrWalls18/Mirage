using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveIn : MonoBehaviour
{
    [SerializeField] private GameObject caveInFake, caveInAnim;

    private List<Transform> caveInLayers = new List<Transform>();

    private int index = 0;

    private void Awake()
    {
        for (int i = 0; i < caveInFake.transform.childCount; i++)
        {
            caveInLayers.Add(caveInFake.transform.GetChild(i));

            //Debug.Log(caveInLayers[i].gameObject.name);
        }
               
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            caveInAnim.SetActive(true);
        }

        InvokeRepeating("StackRockLayer", 1.5f, 1f);
                
    }


    void StackRockLayer()
    {
        caveInLayers[index].gameObject.SetActive(true);

        index++;

        if (index >= caveInLayers.Count)
        {
            caveInAnim.SetActive(false);
            
            this.GetComponent<BoxCollider>().enabled = false;
            caveInFake.GetComponent<BoxCollider>().enabled = true;
            CancelInvoke();
        }
    }
}
