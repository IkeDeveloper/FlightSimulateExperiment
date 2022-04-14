using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCraft : MonoBehaviour
{
   
    public float x, y, w;
    Rigidbody m_Rigidbody;
    public float m_Thrust;
    public float movementSpeed;
    public float pitchspeed, yawspeed;
    public float rotationz;
    public float rollspeed;
    public float minroll;
    public float maxroll;
    public float timeElapsed;
    public float newtons;
    public float thrustincrement;
    public float lerpduration;


    // Start is called before the first frame update

    //Disable mouse cursor and assigns variables
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Thrust = 0.0f;
        movementSpeed = 1.0f;
        pitchspeed = 20.0f;
        yawspeed = 0.1f;
        rotationz = 0.0f;
        minroll = -90.0f;
        maxroll = 90.0f;
        lerpduration = 5.0f;
        newtons = 9.81f;
        thrustincrement = 100.0f;
        rollspeed = 20.0f;

    }

    void Update()
    {
        x -= (Input.GetAxis("Mouse X"));

        if (Input.GetKey(KeyCode.A))
        {
            rotationz += rollspeed * Time.deltaTime;

            checkzangle();
        }

        else

        if (Input.GetKey(KeyCode.D))
        {
            rotationz -= rollspeed * Time.deltaTime;

            checkzangle();
        }

        else

        if (timeElapsed < lerpduration)
        {
            rotationz = Mathf.Lerp(rotationz, 0.0f, timeElapsed / lerpduration);
            timeElapsed += 0.001f * Time.deltaTime;
        }
        else timeElapsed = 0.0f;

        //Controls pitch 
        if (Input.GetKey(KeyCode.W))
            if (w > -4.6f)
                w -= movementSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.S))
            if (w < 4.6f)
                w += movementSpeed * Time.deltaTime;

        //Accelerate/decelerate
        if (Input.GetKey(KeyCode.Space))
        {
            m_Thrust = Mathf.Clamp(m_Thrust, 0, 500);
            m_Thrust += thrustincrement * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.B)) //apply air brakes
            m_Thrust = 0;

        transform.rotation = Quaternion.Euler(-w * pitchspeed, -x * yawspeed, rotationz);
    }

    void FixedUpdate()
    {
        m_Rigidbody.AddForce(transform.forward * m_Thrust); //Apply forward thrust
        m_Rigidbody.AddForce(transform.up * newtons); // Apply up thrust
    }

    void checkzangle()
    {
        rotationz = Mathf.Clamp(rotationz, minroll, maxroll);
    }

}
