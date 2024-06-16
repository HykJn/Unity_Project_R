using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mimic : Enemy
{
    private void Awake()
    {
        render = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        Chase();
    }
}
