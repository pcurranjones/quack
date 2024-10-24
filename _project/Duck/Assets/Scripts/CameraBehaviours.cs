using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviours : MonoBehaviour
{

    Transform target; // Where our cursor's at in 3D space
    Transform camRig; // Our parent
    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        target = new GameObject("Cursor").transform;
        camRig = transform.parent;
        cam = GetComponent<Camera>();
    }



    void Update()
    {
        // We're setting a 3D cursor for our duck's head to look at
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            RaycastHit hitPos;
            var ray = cam.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hitPos))
            {
                target.position = hitPos.point;
            }
        }
    }
}
