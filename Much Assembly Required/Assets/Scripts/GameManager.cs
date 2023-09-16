using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public List<GameObject> shipQueue;
    public List<Station> stations;
    public GameObject canvas;
    public GameObject cam;
    public GameObject loadShip;
    int currentStation;

    // Start is called before the first frame update
    void Start()
    {
        currentStation = 0;
        stations[currentStation].View();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            shipOrder();
        }
    }


    // switch given number of stations
    public async void switchStation(int places)               // TODO: smooth camera movement
    {
        // modulo to keep the number of places to switch within the number of available stations
        places %= stations.Count;

        // rotate Camera
        // Vector3 newRotation = new Vector3(0, 72 * places, 0);
        // cam.transform.Rotate(newRotation);


        // unview last station
        stations[currentStation].Unview();


        // deactivate buttons during animation
        canvas.transform.Find("LeftChange").gameObject.SetActive(false);
        canvas.transform.Find("RightChange").gameObject.SetActive(false);


        // camera rotation across multiple frames via coroutine
        CameraController camController = cam.GetComponent<CameraController>();
        StartCoroutine(camController.SmoothRotate(360.0f / stations.Count * places));
        await Task.Run(() => { 
            Thread.Sleep((int)(camController.animationTime * 1000)); 
        });


        // reactivate buttons once animation completed
        canvas.transform.Find("LeftChange").gameObject.SetActive(true);
        canvas.transform.Find("RightChange").gameObject.SetActive(true);


        // increase currentStation index,
        // modulo for whenever the index exceeds number of elements in the list,
        // +stations.Count for whenever decrementing the index makes it negative
        currentStation = (currentStation + places + stations.Count) % stations.Count;

        // view the current station
        stations[currentStation].View();
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
        GameObject temp = Instantiate(loadShip);
        shipQueue.Add(temp);
    }
}
