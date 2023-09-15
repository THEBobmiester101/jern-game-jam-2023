using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public GameObject[] stationPanels;
    public GameObject cam;
    int curStation;
    // Start is called before the first frame update
    void Start()
    {
        curStation = 0;
        stationPanels[curStation].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void leftStation()
    {
        //Rotate Camera -90 degrees
        Vector3 newRotation = new Vector3(0, -72, 0);
        cam.transform.Rotate(newRotation);

        //By pressing the left button, the station tracker moves backwards in the array by one element

        curStation = curStation - 1;

        //If the station count drops below 0, assign it to the last element in the array

        if(curStation < 0)
        {
            curStation = 4;
        }

        //Set all panels to false active in order to ensure theres not 2 panels open (I'd like to make this more efficient later)
        foreach(GameObject i in stationPanels)
        {
            i.SetActive(false);
        }


        //Set the current station panel to active as to be visible
        stationPanels[curStation].SetActive(true);

        //Thats it!
    }

    public void rightStation()
    {
        //Rotate Camera 90 degrees
        Vector3 newRotation = new Vector3(0, 72, 0);
        cam.transform.Rotate(newRotation);

        //By pressing the right button, the station tracker moves forwards in the array by one element

        curStation = curStation + 1;

        //If the station count goes above 3, assign it to the first element in the array

        if (curStation > 4)
        {
            curStation = 0;
        }

        //Set all panels to false active in order to ensure theres not 2 panels open (I'd like to make this more efficient later)
        foreach (GameObject i in stationPanels)
        {
            i.SetActive(false);
        }


        //Set the current station panel to active as to be visible
        stationPanels[curStation].SetActive(true);

        //Thats it!
    }
}
