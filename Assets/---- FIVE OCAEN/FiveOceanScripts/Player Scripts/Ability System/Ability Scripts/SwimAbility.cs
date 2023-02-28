using RavingBots.Water2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SwimAbility : Ability 
{

    Water2DEffects waterPhysics;
    public override void Activate(GameObject parent)
    {
        Rigidbody2D rb = parent.GetComponent<Rigidbody2D>();
        rb.velocity *= 0.9f;
        rb.gravityScale = 0;
        parent.GetComponent<PlayerMobileInput>().enabled = false;
        parent.GetComponent<SwimController>().enabled = true;
        waterPhysics.GetComponent<BuoyancyEffector2D>().enabled = false;
        rb.freezeRotation = false;


    }

}
