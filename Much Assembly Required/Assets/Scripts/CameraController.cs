using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // time of animations in seconds
    public float animationTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // smooth transition from [0, 0] to [1, 1]
    private static float SmoothStep(float x)
    {
        return -2*x*x*x + 3*x*x;
    }

    public IEnumerator SmoothRotate(float targetAngle)
    {
        float currentAngle = 0.0f;
        float startTime = Time.time;
        while(Time.time < startTime + animationTime)
        {
            float completed = (Time.time - startTime) / animationTime;

            float newAngle = SmoothStep(completed) * targetAngle;
            float diffAngle = newAngle - currentAngle;
            currentAngle = newAngle;

            this.transform.Rotate(new Vector3(0, diffAngle, 0));

            yield return new WaitForSeconds(0.001f);
        }

        this.transform.Rotate(new Vector3(0, targetAngle - currentAngle, 0));
    }

}
