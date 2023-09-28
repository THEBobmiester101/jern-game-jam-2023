using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public abstract class Station : MonoBehaviour
{
    public Spaceship? _spaceship { get; protected set; } = null;

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
        this.minigame.station = this;
        this.minigame.enabled = false;
    }


    public virtual async Task View()
    {
        // if station currently doesn't have a spaceship, pull up the ship selection menu
        if(Spaceship is null && FindObjectOfType<GameManager>().shipQueue.Count > 0)
        {
            Spaceship ship = await FindObjectOfType<ShipSelection>().SelectShip();
            await ship.MoveStation(this);
        }

        if(Spaceship is not null)
        {
            // activate stations UI when switching view to this station
            this.gameObject.GetComponentInChildren<Canvas>(true).gameObject.SetActive(true);
            // pass true to also search inactive objects  --^--

            // activate stations minigame
            minigame.enabled = true;
            minigame.Enter();
        }
    }


    public virtual void Unview()
    {
        // deactivate stations UI when leaving this station
        this.gameObject.GetComponentInChildren<Canvas>(true).gameObject.SetActive(false);

        if(minigame.enabled)
        {
            // deactivate stations minigame
            minigame.Exit();
            minigame.enabled = false;
        }
    }
}
