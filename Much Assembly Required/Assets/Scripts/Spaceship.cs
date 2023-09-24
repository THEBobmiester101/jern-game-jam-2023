using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Spaceship : MonoBehaviour
{
    private GameManager gameManager;

    public  GameObject ShipHolder { get; private set; }
    private GameObject model;
    public string Name { get; private set; }

    private Station currentStation;

    [SerializeField] private float moveAnimationTime = 1.0f;
    [SerializeField] private float moveAnimationEccen = 32.0f;

    

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

        // set currentStation to first station
        currentStation = gameManager.stations[0];
        transform.position = currentStation.transform.position;

        // create shipholder for ship selection
        CreateSelectionShipHolder();
    }


    public void SetPartTargetsActive(bool active)
    {
        Transform partTargets = model.transform.Find("PartTargets");
        partTargets.gameObject.SetActive(active);
    }


    void CreateSelectionShipHolder()
    {
        ShipSelection shipSelection = FindObjectOfType<ShipSelection>(true);

        // Create the shipholder object
        this.ShipHolder = Instantiate(
            gameManager.shipHolder,
            shipSelection.GetComponentInChildren<GridLayoutGroup>(true).transform);

        // Set the name displayed on the shipholder
        TMP_Text shipName = ShipHolder.transform.Find("ShipPanel/ShipName").GetComponent<TMP_Text>();
        shipName.text = Name;

        // Set up the shipHolder button
        Button button = ShipHolder.GetComponentInChildren<Button>();
        UnityAction<Spaceship> action = new UnityAction<Spaceship>(shipSelection.ButtonCallback);
        UnityEventTools.AddObjectPersistentListener(button.onClick, action, this);
    }


    public async Task MoveStation(Station targetStation)
    {
        // log out of previous station
        currentStation.Spaceship = null;

        // move to target station
        StartCoroutine(MotionController.SmoothMotion_Absolute(transform,
            targetStation.transform.position,
            targetStation.transform.eulerAngles,
            null, moveAnimationTime, moveAnimationEccen));
        await MotionController.Wait(moveAnimationTime);

        // log into target station
        currentStation = targetStation;
        targetStation.Spaceship = this;


    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
