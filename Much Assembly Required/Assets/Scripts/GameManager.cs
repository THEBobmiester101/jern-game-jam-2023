using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> shipQueue;
    public List<Station> stations;
    public GameObject canvas;
    
    [SerializeField] GameObject shipPrefab;
    public List<GameObject> shipModels;


    [SerializeField] float camAnimationTime = 1.0f;
    [SerializeField] float camAnimationEccent = 16.0f;
    public GameObject cam;

    public GameObject shipHolder;
    public GameObject grid;

    int currentStation;

    // Start is called before the first frame update
    void Start()
    {
        currentStation = 0;

        cam.transform.position = stations[currentStation].transform.position * 0.7f;

        stations[currentStation].View();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            shipOrder();
        }
    }


    // switch given number of stations
    public async void switchStation(int places)
    {
        // deactivate buttons during animation
        canvas.transform.Find("LeftChange").gameObject.SetActive(false);
        canvas.transform.Find("RightChange").gameObject.SetActive(false);


        // unview last station
        stations[currentStation].Unview();


        // increase currentStation index,
        // modulo for whenever the index exceeds number of elements in the list,
        // +stations.Count for whenever decrementing the index makes it negative
        currentStation = (currentStation + places + stations.Count) % stations.Count;


        // realize camera motion across multiple frames via coroutine
        // use await to continue at this point in the function once SmoothMotion has finished
        
        // move away from current station
        StartCoroutine(MotionController.SmoothMotion_Absolute(cam.transform,
            new Vector3(.0f, .0f, .0f), null, null,
            camAnimationTime * .5f, camAnimationEccent * .5f));
        await MotionController.Wait(camAnimationTime * .5f);

        // rotate towards next station
        StartCoroutine(MotionController.SmoothMotion_Relative(cam.transform, 
            null, new Vector3(0.0f, 360.0f / stations.Count * places % 360.0f, 0.0f), null, 
            camAnimationTime, camAnimationEccent));
        await MotionController.Wait(camAnimationTime); 

        // zoom into new station
        StartCoroutine(MotionController.SmoothMotion_Absolute(cam.transform,
            stations[currentStation].transform.position * 0.7f, null, null,
            camAnimationTime * .5f, camAnimationEccent * .5f));
        await MotionController.Wait(camAnimationTime * .5f);


        // view the current station
        stations[currentStation].View();


        // reactivate buttons once animation completed
        canvas.transform.Find("LeftChange").gameObject.SetActive(true);
        canvas.transform.Find("RightChange").gameObject.SetActive(true);
    }


    // switch to station on the left
    public void leftStation()
    {
        switchStation(-1);
    }

    // switch to station on the right
    public void rightStation()
    {
        switchStation(1);
    }

    public void shipOrder()
    {
        GameObject ship = Instantiate(shipPrefab);
        shipQueue.Add(ship);
    }
}
