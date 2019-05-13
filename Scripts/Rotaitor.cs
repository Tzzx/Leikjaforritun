using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotaitor : MonoBehaviour
{
  
    // Update is called once per frame
    void Update()
    {
        // læt penningana snúast
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);

    }
}
