using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float animationTime = 1.0f;        // time of animations in seconds
    [SerializeField] float moveEccent = 2.0f;     // eccentricity of SmoothMove motion
    [SerializeField] float rotEccent = 2.0f;      // eccentricity of SmoothRotate motion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // smooth transition from [0, 0] to [1, 1]
    private static float SmoothStep(float x, float eccentricity = 2.0f)
    {
        float a = eccentricity;
        float b = -2.5f * a;
        float c = 2.0f * a - 2.0f;
        float d = 3.0f - 0.5f * a;

        // not using Mathf.Pow for small exponents
        return a * x*x*x*x*x + b * x*x*x*x + c * x*x*x + d * x*x;
    }

    public IEnumerator SmoothRotate(float targetAngle)
    {
        float currentAngle = 0.0f;
        float startTime = Time.time;
        while(Time.time < startTime + animationTime)
        {
            float completed = (Time.time - startTime) / animationTime;

            float newAngle = SmoothStep(completed, rotEccent) * targetAngle;
            float diffAngle = newAngle - currentAngle;
            currentAngle = newAngle;

            this.transform.Rotate(new Vector3(0, diffAngle, 0));

            yield return new WaitForSeconds(0.001f);
        }

        this.transform.Rotate(new Vector3(0, targetAngle - currentAngle, 0));
    }

}
