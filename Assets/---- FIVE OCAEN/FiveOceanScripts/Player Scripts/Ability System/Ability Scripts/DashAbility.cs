using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class DashAbility : Ability
{
    public float dashVelocity;
    public override void Activate(GameObject parent)
    {
        PlayerMobileInput movement = parent.GetComponent<PlayerMobileInput>();
        Rigidbody2D rb = parent.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(movement.input.x * dashVelocity, 0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
