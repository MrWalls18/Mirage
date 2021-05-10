using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class EndGameUI : SingletonPattern<EndGameUI>
{
    public GameObject EndPanel;
    public TMP_Text textBox1;
    public TMP_Text textBox2;
    public TMP_Text statusBox;
    public TMP_Text bonesText;

    public void WinGame()
    {
        EndPanel.SetActive(true);
        statusBox.text = "YOU WON!";
        textBox1.text = "Time Elapsed: " + Sun.Instance.timer.ToString("0.00");
        textBox2.text = "Distance Traveled: " + DistanceCheck.Instance.totalDistance.ToString("0.00") + " Meters";
        bonesText.text = PlayerStats.Instance.bonesFound.ToString() + "/8 Bones Found" ;
    }

    public void LoseGame()
    {
        EndPanel.SetActive(true);
        statusBox.text = "YOU DIED.";
        textBox1.text = "Time Elapsed: " + Sun.Instance.timer.ToString("0.00");
        textBox2.text = "Distance from Exit: " + DistanceCheck.Instance.DistanceToEnd().ToString("0.00") + " Meters";
    }


}
