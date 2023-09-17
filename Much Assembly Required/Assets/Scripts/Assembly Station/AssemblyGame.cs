using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyGame : MinigameManager
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Let's play an assembly game");
    }

    public override void Exit() 
    { 
        base.Exit();
    }
}
