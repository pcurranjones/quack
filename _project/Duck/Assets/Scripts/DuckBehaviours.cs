using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DuckBehaviours : MonoBehaviour
{

    // Set up our private variables

    // Duck body variables
   
    GameObject duckBody; // Our body that we're going to snap to the sphere
    GameObject duckHead;
    LineRenderer neckLine; // The neck's made from a line that goes from the head joint to the body joint
    Transform[] neckJoints;
    LineRenderer[] quackLines;
    
    // Duck function variables
    GameObject locoSphere; // Our sphere that we're using to move
    Rigidbody locoRb;

    public bool jumping = false; // Stops our duck from being able to fly

    AudioSource audioSource;

    //Camera variables
    public Transform camTransform;
    Camera camSettings;

    Transform cursorPosition; // Where our 3D cursor is placed

    // Set up our public variables, we wanna be able to tweak these for different feels
    [Header("Duck Movement")]
    public float acceleration;
    public float jumpStrength;

    [Header("Sounds")]
    public AudioClip quack;
    //public AudioClip footsteps;

    // Start is called before the first frame update
    void Start()
    {

        // Assign all those private variables
        locoSphere = transform.GetChild(0).gameObject;
        locoRb = locoSphere.GetComponent<Rigidbody>();

        duckBody = transform.GetChild(1).gameObject;
        duckHead = transform.GetChild(2).gameObject;

        neckLine = new LineRenderer();
     
        camTransform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        camSettings = camTransform.gameObject.GetComponent<Camera>();

        audioSource = duckHead.AddComponent<AudioSource>();
        
        /*foreach (LineRenderer t in FindObjectsOfType<LineRenderer>())
        {
            if (t.gameObject.name == "QuackLine")
            {
                quackLines[quackLines.Length] = t;
            }
        }*/

    }

    // Update is called once per frame
    void Update()
    {
        // Duck Movement
        if (Input.GetAxis("Vertical") != 0)
        {
            locoRb.AddForce(camTransform.forward * Input.GetAxis("Vertical") * acceleration);
        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            locoRb.AddForce(camTransform.right * Input.GetAxis("Horizontal") * acceleration);
        }
        if (Input.GetKeyDown(KeyCode.Space) && jumping == false)
        {
            locoRb.AddForce(transform.up * jumpStrength);
            jumping = true;
        }

        // Duck body behaviours
        duckBody.transform.position = locoSphere.transform.position;
        duckBody.transform.LookAt(locoRb.velocity * 1000);

        if(Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            if (cursorPosition != null)
            {
                duckHead.transform.LookAt(cursorPosition);
            }

            else { cursorPosition = GameObject.Find("Cursor").transform; }
        }

        // Quack!
        if(Input.GetMouseButtonDown(0)){
            
            // We want the quack to sound different each time
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(quack);

            // Show the lines
            foreach(LineRenderer line in quackLines)
            {
                line.enabled = true;
            }
            StartCoroutine("HideQuackLines");
        }

    }

    // We use FixedUpdate here to prevent any camera juttering
    private void FixedUpdate()
    {
        // Camera behaviours
        // Add a velocity qualifier to this and a camera offset with a float attached to mouse screen position
        if (camTransform != null)
        {
            camTransform.LookAt(duckBody.transform.position);
        }
        else { camTransform = GameObject.FindGameObjectWithTag("MainCamera").transform; }
    }

    IEnumerator HideQuackLines()
    {
        yield return new WaitForSeconds (quack.length);
        foreach (LineRenderer line in quackLines)
        {
            line.enabled = false;
        }
    }
}
