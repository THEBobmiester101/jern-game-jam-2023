using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public GameObject[] stationPanels;
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
        //By pressing the left button, the station tracker moves backwards in the array by one element

        curStation = curStation - 1;

        //If the station count drops below 0, assign it to the last element in the array

        if(curStation < 0)
        {
            curStation = 3;
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
        //By pressing the right button, the station tracker moves forwards in the array by one element

        curStation = curStation + 1;

        //If the station count goes above 3, assign it to the first element in the array

        if (curStation > 3)
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
