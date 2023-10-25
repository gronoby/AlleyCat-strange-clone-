using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rats_basics : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Attack", 0, 2);
        InvokeRepeating("Back", 1, 2);

    }

    void Attack()
    {
        var pos = this.transform.position;
        pos.y += 1;
        transform.position = pos;
    }
    void Back()
    {
        var pos = this.transform.position;
        pos.y -= 1;
        transform.position = pos;
    }
}
