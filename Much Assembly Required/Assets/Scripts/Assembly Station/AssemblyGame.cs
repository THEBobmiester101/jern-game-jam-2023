using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyGame : MinigameManager
{
    [SerializeField] private GameObject partPrefab;
    private List<GameObject> buildingParts = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Let's play an assembly game");

        Transform partTargets = station.Spaceship.transform.GetChild(0);
        foreach(Transform part in partTargets.Find("PartTargets"))
        {
            Debug.Log("ship is missing part: " + part.gameObject);
            GameObject copy = Instantiate(part.gameObject, transform);
            buildingParts.Add(copy);
        }
    }

    public override void Exit() 
    { 
        base.Exit();

        buildingParts.Clear();
    }
}
