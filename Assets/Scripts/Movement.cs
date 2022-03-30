using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField] float thrustSpeed = 100f;
    [SerializeField] float rotateSpeed = 15f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        //audioSource.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        float thrustMultiplier = Time.deltaTime * thrustSpeed;
        if (Input.GetKey(KeyCode.Space))
        {
            //Debug.Log("Pressed SPACE - ENGAGE THRUSTERS");
            rb.AddRelativeForce(Vector3.up * thrustMultiplier);
            
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
        
    }

    void ProcessRotation()
    {
        float rotateMultiplier = Time.deltaTime * rotateSpeed;
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            //Debug.Log("Pressed A - ROTATE LEFT");
            RotateRocket(rotateMultiplier);
        }
        if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            //Debug.Log("Pressed D - ROTATE RIGHT");
            RotateRocket(-rotateMultiplier);
        }
    }

    private void RotateRocket(float rotateMultiplier)
    {
        rb.freezeRotation = true; // freezing rotation during object collision
        transform.Rotate(Vector3.forward * rotateMultiplier);
        rb.freezeRotation = false;
    }
}
