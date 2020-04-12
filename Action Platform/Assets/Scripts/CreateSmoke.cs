using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSmoke : MonoBehaviour
{
    public GameObject smoke;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(smoke, transform.position, transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
