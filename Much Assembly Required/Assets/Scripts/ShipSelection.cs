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
        selectedShip = ship;
    }

    private Task WaitForButton()
    {
        return Task.Run(() =>
            {
                while (selectedShip is null)
                {
                    Thread.Sleep(10);
                }
            }
        );
    }

    public async Task<Spaceship> SelectShip()
    {
        selectedShip = null;

        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(true);

        await WaitForButton();

        for (int i = 0; i < transform.childCount; i++)
            transform.GetChild(i).gameObject.SetActive(false);

        return selectedShip;
    }
}
