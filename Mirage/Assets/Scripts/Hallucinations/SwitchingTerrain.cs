using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchingTerrain : MonoBehaviour
{
    [SerializeField] private GameObject realTerrain;
    [SerializeField] private GameObject fakeTerrain;
    private MeshRenderer realRenderer, fakeRenderer;

    private PlayerStats myStats;


    private void Awake()
    {
        myStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        realRenderer = realTerrain.GetComponent<MeshRenderer>();
        fakeRenderer = fakeTerrain.GetComponent<MeshRenderer>();
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!realRenderer.isVisible
            && !fakeRenderer.isVisible)
        {
            if (myStats.isHallucinating)
            {
                realTerrain.SetActive(false);
                fakeTerrain.SetActive(true);
            }
            else
            {
                fakeTerrain.SetActive(false);
                realTerrain.SetActive(true);
            }
        }
    }
}
