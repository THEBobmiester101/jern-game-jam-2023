using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    GameObject model;

    // Start is called before the first frame update
    void Start()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();

        // assign random ship model
        int modelIndex = UnityEngine.Random.Range(0, gameManager.shipModels.Count - 1);
        model = Instantiate(gameManager.shipModels[modelIndex], transform);

        // make sure to deactivate part targets on model
        SetPartTargetsActive(false);
    }

    public void SetPartTargetsActive(bool active)
    {
        Transform partTargets = model.transform.Find("PartTargets");
        partTargets.gameObject.SetActive(active);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
