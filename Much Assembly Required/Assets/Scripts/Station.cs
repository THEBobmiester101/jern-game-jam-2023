using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Station : MonoBehaviour
{
    protected Spaceship? _spaceship = null;
    virtual public Spaceship Spaceship 
    { 
        get { return _spaceship; }
        set 
        { 
            if(_spaceship is null)
            {
                _spaceship = value;
            }
            else
            {
                Debug.LogWarning("Station occupied");
            }
        }
    }

    protected MinigameManager minigame;



    void Awake()
    {
        this.minigame = this.gameObject.GetComponent<MinigameManager>();
        this.minigame.enabled = false;
    }


    public async virtual void View()
    {
        // if station currently doesn't have a spaceship, pull up the ship selection menus
        if(Spaceship is null && FindObjectOfType<GameManager>().shipQueue.Count > 0)
        {
            Spaceship ship = await FindObjectOfType<ShipSelection>().SelectShip();
            await ship.MoveStation(this);
        }

        // activate stations UI when switching view to this station
        this.gameObject.GetComponentInChildren<Canvas>(true).gameObject.SetActive(true);
        // pass true to also search inactive objects  --^--

        // activate stations minigame
        minigame.enabled = true;
        minigame.Enter();
    }


    public virtual void Unview()
    {
        // deactivate stations UI when leaving this station
        this.gameObject.GetComponentInChildren<Canvas>(true).gameObject.SetActive(false);

        // deactivate stations minigame
        minigame.Exit();
        minigame.enabled = false;
    }
}
