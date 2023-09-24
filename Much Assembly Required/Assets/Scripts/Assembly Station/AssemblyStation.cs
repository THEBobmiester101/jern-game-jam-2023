using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyStation : Station
{
    override public Spaceship Spaceship 
    { 
        get { return _spaceship; } 
        set 
        {
            Debug.Log("assembly spaceship setter\n" +
                "value: " + value + "\n" +
                "ship:  " + _spaceship);

            if (_spaceship is null)
            {
                _spaceship = value;
                _spaceship.SetPartTargetsActive(true);
            }
            else if (value is null)
            {
                _spaceship.SetPartTargetsActive(false);
                _spaceship = value;
            }
            else
            {
                Debug.LogWarning("Station occupied");
            }
        } 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
