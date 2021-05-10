using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    //private Animator animator;
    // Start is called before the first frame update
    public void EndGame()
    {
        //animator = GetComponent<Animator>();
        //animator.SetBool("isPlayerInMinAttackRange", true);
        //player dies, trigger death screen
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(0);
    }



}
