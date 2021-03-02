using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisapperingTerrain : MonoBehaviour
{
    [SerializeField] private GameObject terrain, checkForVisibility;
    private MeshRenderer terrainRenderer, visibilityRenderer;

    private PlayerStats myStats;

    private void Awake()
    {
        myStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        terrainRenderer = terrain.GetComponent<MeshRenderer>();
        visibilityRenderer = checkForVisibility.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!terrainRenderer.isVisible
            && !visibilityRenderer.isVisible)
        {
            if (myStats.isHallucinating)
                terrain.SetActive(true);
            else
                terrain.SetActive(false);
        }
        
    }
}
