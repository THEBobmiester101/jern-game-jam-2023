using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MinigameManager : MonoBehaviour
{
    public Station station;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Enter is called whenever you switch to this station
    public virtual void Enter()
    {

    }

    // Exit is called whenever you leave this station
    public virtual void Exit()
    {

    }
}
