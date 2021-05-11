using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
    IEnumerator TimeToDestroy()
    {
        AudioManager.Instance.Play("Jumpscare");
        yield return new WaitForSeconds(0.8f);
        Destroy(this.gameObject);
    }


    private void Start()
    {
        StartCoroutine(TimeToDestroy());
    }
}
