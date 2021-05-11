using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
    IEnumerator TimeToDestroy()
    {
        AudioManager.Instance.Play("Jumpscare");
        yield return new WaitForSeconds(0.7f);
        Destroy(this.gameObject);
    }


    private void Start()
    {
        StartCoroutine(TimeToDestroy());
    }
}
