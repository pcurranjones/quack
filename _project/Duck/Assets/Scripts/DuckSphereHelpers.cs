using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckSphereHelpers : MonoBehaviour
{
    DuckBehaviours duckBehaviours;
    // Start is called before the first frame update
    void Start()
    {
        duckBehaviours = transform.parent.GetComponent<DuckBehaviours>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        duckBehaviours.jumping = false;
    }
}
