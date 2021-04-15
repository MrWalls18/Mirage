using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHeatDistortion : SingletonPattern<PlayerHeatDistortion>
{
    [SerializeField] private PlayerStats statsRef;
    private float distortionAmount;
    [SerializeField] private GameObject distortionField;
    private Material distortionShader;
    [SerializeField] private float scaleFactor;

    private void Start()
    {
        statsRef = gameObject.GetComponent<PlayerStats>();
        distortionShader = distortionField.GetComponent<MeshRenderer>().material;
        //InvokeRepeating("setDistortion", 2.0f, 1.0f);
    }

    private void Update()
    {
        setDistortion();
    }

    // Sets size and intensity of distortion
    private void setDistortion()
    {
        // if >= half sanity, hidden
        if (statsRef.sanity / statsRef.maxSanity >= 0.5f)
        {
            distortionShader.SetFloat("HeatDistortion", 0.0f);
            distortionAmount = (statsRef.sanity / statsRef.maxSanity) * 0f;
            distortionField.transform.localScale = new Vector3(distortionAmount, distortionAmount, distortionAmount);
        }
        // else if >= 0.1 sanity, scaling and appear
        else if (statsRef.sanity / statsRef.maxSanity >= 0.1f)
        {
            distortionAmount = (1f - (statsRef.sanity / statsRef.maxSanity)) / 2;
            distortionShader.SetFloat("HeatDistortion", distortionAmount);
            distortionAmount = (statsRef.sanity / statsRef.maxSanity) * scaleFactor;
            distortionField.transform.localScale = new Vector3(distortionAmount, distortionAmount, distortionAmount);
        }
        // else, fixed value close
        else
        {
            distortionAmount = 1f - (0.1f) * 0.15f;
            distortionShader.SetFloat("HeatDistortion", distortionAmount);
            distortionAmount = 3f;
            distortionField.transform.localScale = new Vector3(distortionAmount, distortionAmount, distortionAmount);
        }
    } 

}
