using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat_basics : MonoBehaviour
{
    [SerializeField] float walkspeed = 0.1f;
    [SerializeField] float jumpheight = 200f;

    int result = 0;
    public Canvas vict_canv;
    public Canvas lose_canv;
    private Animator anim;
    private Rigidbody rigitbody;
    private static readonly int walking = Animator.StringToHash("walking");
    Quaternion base_look;
    Quaternion opposite_look;

    private bool on_ground = true;
    void Start()
    {
        anim = GetComponent<Animator>();
        rigitbody = GetComponent<Rigidbody>();
        base_look = this.transform.rotation;
        opposite_look = base_look;
        opposite_look.SetEulerAngles(base_look.eulerAngles.x, base_look.eulerAngles.y + 2.4f, base_look.eulerAngles.z);
    }

    // Update is called once per frame
    void Update()
    {
        var delta = new Vector3(x: Input.GetAxis("Horizontal"), y: Input.GetAxis("Vertical"), z: 0);
        rigitbody.MovePosition(rigitbody.position + new Vector3(delta.x,0,0) * walkspeed * Time.deltaTime);
        
        if (delta.x > 0)
        {
            var look = this.transform.rotation;
            if (look != base_look)
            {
                look = base_look;
            }
            transform.rotation = look;
        }
        else if (delta.x < 0)
        {
            var look = this.transform.rotation;
            if (look != opposite_look)
            {
                look = opposite_look;
            }
            transform.rotation = look;
        }

        if ((delta.y > 0) && (on_ground))
        {
            rigitbody.AddForce(Vector3.up * jumpheight, ForceMode.VelocityChange);
        }

        if (anim && delta.sqrMagnitude > 0.01f)
        {
            anim.SetBool(walking, true);
        }
        else
        {
            anim.SetBool(walking, false);
        }

        if (transform.position.y > 4.3)
        {
            var pos = rigitbody.position;
            pos.z = -4;
            rigitbody.MovePosition(pos);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            on_ground = true;
        }
        else if (collision.collider.CompareTag("Window") && (result == 0)) {
            vict_canv.gameObject.SetActive(true);
            result = 1;
        }
        else if (collision.collider.CompareTag("Enemy") && (result == 0))
        {
            lose_canv.gameObject.SetActive(true);
            result = -1;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            on_ground = false;
        }
    }

}
