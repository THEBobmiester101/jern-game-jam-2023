using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AssemblyGame : MinigameManager
{
    [SerializeField] private GameObject partPrefab;
    private List<GameObject> buildingParts = new List<GameObject>();

    [SerializeField] private Material targetMaterial;
    [SerializeField] private Material rawMaterial;
    [SerializeField][Range(1000.0f, 5000.0f)] private float uiPartScale;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static float MaxNorm(Vector3 vec)
    {
        float max1 = vec.x > vec.y ? vec.x : vec.y;
        return max1 > vec.z ? max1 : vec.z;
    }
    public override void Enter()
    {
        base.Enter();

        Debug.Log("Let's play an assembly game");

        // Transform partTargets = station.Spaceship.transform.GetChild(0);
        // foreach(Transform part in partTargets.Find("PartTargets"))
        // {
        //     Debug.Log("ship is missing part: " + part.gameObject);
        //     GameObject copy = Instantiate(part.gameObject, transform);
        //     buildingParts.Add(copy);
        // }
        // 
        // Transform uiPartHolder = station.transform
        //     .GetComponentInChildren<Canvas>().transform.Find("PartHolder");
        // for(int i = 0; i < buildingParts.Count; i++)
        // { }

        Transform shipPartTargets = station.Spaceship.transform.GetChild(0).Find("PartTargets");
        Transform uiPartHolder = station.GetComponentInChildren<Canvas>().transform.Find("PartHolder");
        for (int i = 0; i < shipPartTargets.childCount; i++)
        {
            GameObject target = shipPartTargets.GetChild(i).gameObject;
            GameObject panel = uiPartHolder.GetChild(i).gameObject;

            Debug.Log("ship is missing part: " + target.name);
            GameObject copy = Instantiate(target, panel.transform);
            buildingParts.Add(copy);

            float currentScale = MaxNorm(copy.transform.localScale);
            copy.transform.localScale *= uiPartScale / currentScale;
            currentScale = MaxNorm(copy.transform.localScale);
            copy.transform.localEulerAngles = Vector3.zero;
            copy.transform.localPosition = Vector3.zero - 
                copy.GetComponent<BoxCollider>().center * currentScale;
            copy.transform.localPosition += new Vector3(0, 0, -100);

            copy.layer = LayerMask.NameToLayer("UI");
            copy.GetComponent<MeshRenderer>().material = rawMaterial;
        }
    }

    public override void Exit() 
    { 
        base.Exit();

        foreach(var part in buildingParts)
        {
            Destroy(part);
        }
        buildingParts.Clear();
    }
}
