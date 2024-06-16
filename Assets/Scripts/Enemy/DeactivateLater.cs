using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateLater : MonoBehaviour
{
    public float time;
    float tick;

    private void Update()
    {
        tick += Time.deltaTime;
        if(tick >= time)
        {
            tick = 0;
            this.gameObject.SetActive(false);
        }
    }
}
