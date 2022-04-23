using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] float thrustSpeed = 100f;
    [SerializeField] float rotateSpeed = 15f;

    [SerializeField] ParticleSystem mainThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;

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
            StartThrusting(thrustMultiplier);
        }
        else
            StopThrusting();
    }

    void ProcessRotation()
    {
        float rotateMultiplier = Time.deltaTime * rotateSpeed;
        if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            StartRotatingLeft(rotateMultiplier);
        }
        else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            StartRotatingRight(rotateMultiplier);
        }
        else
            StopRotating();
    }

    void StartThrusting(float thrustMultiplier)
    {
        //Debug.Log("Pressed SPACE - ENGAGE THRUSTERS");
        rb.AddRelativeForce(Vector3.up * thrustMultiplier);


        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainThrusterParticles.isPlaying)
        {
            mainThrusterParticles.Play();
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        mainThrusterParticles.Stop();

    }

    
    void StartRotatingLeft(float rotateMultiplier)
    {
        //Debug.Log("Pressed A - ROTATE LEFT");
        if (!rightThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Play();
        }
        RotateRocket(rotateMultiplier);
    }

     void StartRotatingRight(float rotateMultiplier)
    {
        //Debug.Log("Pressed D - ROTATE RIGHT");
        if (!leftThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Play();
        }
        RotateRocket(-rotateMultiplier);
    }                                       

     void StopRotating()
    {
        leftThrusterParticles.Stop();
        rightThrusterParticles.Stop();
    }

      void RotateRocket(float rotateMultiplier)
    {
        rb.freezeRotation = true; // freezing rotation during object collision
        transform.Rotate(Vector3.forward * rotateMultiplier);
        rb.freezeRotation = false;
    }
}