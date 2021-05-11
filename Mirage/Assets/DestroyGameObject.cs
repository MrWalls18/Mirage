using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
    IEnumerator TimeToDestroy()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }


    private void Start()
    {
        TimeToDestroy();
    }
}
