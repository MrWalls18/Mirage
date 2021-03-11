using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Tick());
        StartCoroutine(DoCheck());
    }

    // Update is called once per frame
    void Update()
    {
        //bool warnflag = ProximityCheck();
    }

    /*bool ProximityCheck()
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if(Vector3.Distance(transform.position, enemies[i].transform.position) < dangerDist)
            {
                return true;
            }
        }
        return false;
    }
   */     



    IEnumerator DoCheck()
    {
        for (; ; )
        {
            //ProximityCheck();
            yield return new WaitForSeconds(.1f);
        }
    }

    //IEnumerator is a return type for all coroutine classes
    //stands for InterfaceEnumerator
    IEnumerator Tick()
    {
        //need an infinite while loop to keep the print happening
        //until corouting is halted or program is stopped
        while (true)
        {
            print(System.DateTime.Now.ToString());

            //this yield statement tell the corouting to wait 1 second
            //before continuing
            //Note - coroutine time isn't exact
            yield return new WaitForSeconds(1f);
        }


    }

}
