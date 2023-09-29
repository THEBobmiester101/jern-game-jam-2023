using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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

    public void ButtonCallback(GameObject panel)
    {
        GameObject uiPart = panel.GetComponentInChildren<PartAnimation>().gameObject;
        GameObject dragablePart = Instantiate(uiPart, FindObjectOfType<GameManager>().cam.transform);
        uiPart.SetActive(false);
        dragablePart.transform.localScale = Vector3.one * 5;
        dragablePart.layer = LayerMask.NameToLayer("Default");
        StartCoroutine(DragPart(uiPart, dragablePart));
    }

    public IEnumerator DragPart(GameObject uiPart, GameObject dragablePart)
    {
        Debug.Log("drag part coroutine");
        while(!Input.GetMouseButtonDown(1))
        {
            Vector2 mousePos = Input.mousePosition;
            Vector2 screenDim = new Vector2(Screen.currentResolution.width, Screen.currentResolution.height);
            mousePos = 2.0f * mousePos - screenDim;
            mousePos /= screenDim.x;

            Debug.Log("mousePos: " + mousePos);

            dragablePart.transform.localPosition = new Vector3(mousePos.x, mousePos.y, 1.0f);
            yield return 0;
        }
        Destroy(dragablePart);
        uiPart.SetActive(true);
    }

    public static float MaxNorm(Vector3 vec)
    {
        float max1 = vec.x > vec.y ? vec.x : vec.y;
        return max1 > vec.z ? max1 : vec.z;
    }
    public override void Enter()
    {
        base.Enter();

        Transform shipPartTargets = station.Spaceship.transform.GetChild(0).Find("PartTargets");
        Transform uiPartHolder = station.GetComponentInChildren<Canvas>().transform.Find("PartHolder");
        for (int i = 0; i < shipPartTargets.childCount; i++)
        {
            GameObject target = shipPartTargets.GetChild(i).gameObject;
            GameObject panel = uiPartHolder.GetChild(i).gameObject;

            GameObject copy = Instantiate(target, panel.transform);
            buildingParts.Add(copy);

            // arrange copy on ui panel
            {
                float currentScale = MaxNorm(copy.transform.localScale);
                copy.transform.localScale *= uiPartScale / currentScale;
                currentScale = MaxNorm(copy.transform.localScale);
                copy.transform.localEulerAngles = Vector3.zero;
                copy.transform.localPosition = Vector3.zero - copy.GetComponent<BoxCollider>().center * currentScale;
                copy.transform.localPosition += new Vector3(0, 0, -100);
            
                copy.layer = LayerMask.NameToLayer("UI");
                copy.GetComponent<MeshRenderer>().material = rawMaterial;
            }

            // connect button
            {
                Button button = panel.GetComponentInChildren<Button>();
                UnityAction<GameObject> action = new UnityAction<GameObject>(this.ButtonCallback);
                UnityEventTools.AddObjectPersistentListener<GameObject>(button.onClick, action, panel);
            }
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
