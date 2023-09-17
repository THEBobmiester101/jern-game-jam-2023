using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public static class MotionController
{
    private static float SmoothStep(float x, float eccentricity)
    {
        float a = eccentricity;
        float b = -2.5f * a;
        float c = 2.0f * a - 2.0f;
        float d = 3.0f - 0.5f * a;

        // not using Mathf.Pow for small exponents
        return a * x*x*x*x*x + b * x*x*x*x + c * x*x*x + d * x*x;
    }

    /// <summary>
    /// Performs a smooth motion on the given transform, according to relative parameters
    /// </summary>
    /// <param name="transform"></param> transform to perform the motion on
    /// <param name="deltaPos"></param> change in position relative to starting position, pass null for no change
    /// <param name="deltaRot"></param> change in rotation relative to starting rotation, pass null for no change
    /// <param name="deltaScale"></param> change in scale relative to starting scale, pass null for no change
    /// <param name="animationTime"></param> time of the animation in seconds
    /// <param name="eccentricity"></param> eccentricity of the motion, range (0; inf) (near linear to very aggressive)
    public static IEnumerator SmoothMotion_Relative(Transform transform, 
        Vector3? deltaPos, Vector3? deltaRot, Vector3? deltaScale, 
        float animationTime, float eccentricity = 2.0f)
    {
        // save starting values
        Vector3 startPos = transform.position;
        Vector3 startRot = transform.eulerAngles;
        Vector3 startScale = transform.localScale;

        // default values for empty parameters
        if (deltaPos is null) deltaPos = new Vector3(.0f, .0f, .0f);
        if (deltaRot is null) deltaRot = new Vector3(.0f, .0f, .0f);
        if (deltaScale is null) deltaScale = new Vector3(0f, 0f, 0f);

        // loop through animation
        float startTime = Time.time;
        while (Time.time < startTime + animationTime)
        {
            float split = (Time.time - startTime) / animationTime;
            split = SmoothStep(split, eccentricity);

            transform.position = startPos + split * (Vector3)deltaPos;
            transform.eulerAngles = startRot + split * (Vector3)deltaRot;
            transform.localScale = startScale + split * (Vector3)deltaScale;

            yield return new WaitForSeconds(0.001f);
        }

        // final step with "split = 1.0" for full completion of motion
        transform.position = startPos + (Vector3)deltaPos;
        transform.eulerAngles = startRot + (Vector3)deltaRot;
        transform.localScale = startScale + (Vector3)deltaScale;
    }

    /// <summary>
    /// Performs a smooth motion of the transform to the absolute destination parameters
    /// </summary>
    /// <param name="transform"></param> transform to perform the motion on
    /// <param name="dstPos"></param> absolute destination position, pass null for no change
    /// <param name="dstRot"></param> absolute destination rotation, pass null for no change
    /// <param name="dstScale"></param> absolute destination scale, pass null for no change
    /// <param name="animationTime"></param> time of the animation in seconds
    /// <param name="eccentricity"></param> eccentricity of the motion, range (0; inf) (near linear to very aggressive)
    public static IEnumerator SmoothMotion_Absolute(Transform transform,
        Vector3? dstPos, Vector3? dstRot, Vector3? dstScale,
        float animationTime, float eccentricity = 2.0f)
    {
        Vector3? deltaPos, deltaRot, deltaScale;

        if (dstPos is null) deltaPos = null;
        else deltaPos = (Vector3)dstPos - transform.position;

        if (dstRot is null) deltaRot = null;
        else deltaRot = (Vector3)dstRot - transform.eulerAngles;

        if (dstScale is null) deltaScale = null;
        else deltaScale = (Vector3)dstScale - transform.localScale;

        return SmoothMotion_Relative(transform, deltaPos, deltaRot, deltaScale, animationTime, eccentricity);
    }

    /// <summary>
    /// Returns a Task to wait on using an 'await'-statement
    /// </summary>
    /// <param name="seconds"></param> seconds to wait for
    public static Task Wait(float seconds)
    {
        return Task.Run(() => { Thread.Sleep((int)(seconds * 1000)); });
    }
}
