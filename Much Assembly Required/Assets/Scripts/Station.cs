using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Station : MonoBehaviour
{
    private Spaceship? _spaceship = null;
    public Spaceship Spaceship 
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

    private MinigameManager minigame;



    void Awake()
    {
        this.minigame = this.gameObject.GetComponent<MinigameManager>();
        this.minigame.enabled = false;
    }



    public virtual void View()
    {
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
