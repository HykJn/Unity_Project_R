using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        DebugingName(collision.gameObject);
    }

    void DebugingName(GameObject obj)
    {
        Debug.Log(obj.name);
    }
}
