using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShipSelection : MonoBehaviour
{
    public Spaceship selectedShip { get; private set; } = null;



    void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(false);
    }



    public void ButtonCallback(Spaceship ship)
    {
        Debug.LogError("button called back");
        selectedShip = ship;
    }

    private Task WaitForButton()
    {
        return Task.Run(() =>
            {
                while (selectedShip is null)
                {
                    Thread.Sleep(10);
                    // Debug.Log("selected ship: " + selectedShip);
                }
            }
        );
    }

    public async Task<Spaceship> SelectShip()
    {
        this.selectedShip = null;

        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(true);

        await WaitForButton();

        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(false);

        return selectedShip;
    }
}
