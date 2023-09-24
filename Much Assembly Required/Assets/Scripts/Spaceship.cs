using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Spaceship : MonoBehaviour
{
    GameManager gameManager;

    [SerializeField] GameObject shipHolder;
    GameObject model;
    public string Name { get; private set; }

    

    // Start is called before the first frame update
    void Start()
    {
        this.gameManager = FindObjectOfType<GameManager>();

        // assign random ship model
        int modelIndex = UnityEngine.Random.Range(0, gameManager.shipModels.Count - 1);
        this.model = Instantiate(gameManager.shipModels[modelIndex], transform);

        // make sure to deactivate part targets on model
        SetPartTargetsActive(false);

        // set random codename
        this.Name = '#' + UnityEngine.Random.Range(1000f, 2000f).ToString();

        // create shipholder for ship selection
        CreateSelectionShipHolder();
    }


    public void SetPartTargetsActive(bool active)
    {
        Transform partTargets = model.transform.Find("PartTargets");
        partTargets.gameObject.SetActive(active);
    }


    void func()
    {
        Debug.LogError("listener?");
    }

    void CreateSelectionShipHolder()
    {
        ShipSelection shipSelection = FindObjectOfType<ShipSelection>(true);

        // Create the shipholder object
        GameObject shipHolder = Instantiate(
            gameManager.shipHolder,
            shipSelection.GetComponentInChildren<GridLayoutGroup>(true).transform);

        // Set the name displayed on the shipholder
        TMP_Text shipName = shipHolder.transform.Find("ShipPanel/ShipName").GetComponent<TMP_Text>();
        shipName.text = Name;

        // Set up the button on the shipHolder
        Button button = shipHolder.GetComponentInChildren<Button>();
        button.onClick.AddListener(() => { shipSelection.ButtonCallback(this); });
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
