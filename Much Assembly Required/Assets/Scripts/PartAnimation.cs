using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartAnimation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.layer == LayerMask.NameToLayer("UI"))
        {
            // TODO: make rotation work...

            // Vector3 tempPosition = transform.position;
            // transform.position = Vector3.zero;
            // transform.Rotate(rotationSpeed * Time.deltaTime, 0, 0);
            // transform.position = tempPosition;

            // transform.RotateAround(transform.position
            //     + gameObject.GetComponent<BoxCollider>().center * AssemblyGame.MaxNorm(transform.localScale),
            //     Vector3.one,
            //     rotationSpeed * Time.deltaTime);
        }
    }
}
