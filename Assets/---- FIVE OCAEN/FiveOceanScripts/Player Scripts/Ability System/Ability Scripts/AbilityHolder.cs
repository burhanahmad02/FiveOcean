using RavingBots.Water2D;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    Water2DEffects waterPhysics;
    public static AbilityHolder Instance;
    public Ability ability;
    float cooldownTime;
    float activeTime;
    public GameObject playerController;
    enum AbilityState
    {
        ready,
        active,
        cooldown
    }
    AbilityState state = AbilityState.ready;

    public KeyCode key;
    void Awake()
    {

        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }
    // Update is called once per frame
    void Update()
    {
        switch (state)
        {

            case AbilityState.ready:
                if (Input.GetKey(key))
                {
                    ability.Activate(gameObject);
                    state = AbilityState.active;
                    activeTime = ability.activeTime;
                }
                break;
            case AbilityState.active:
                if (activeTime > 0)
                {
                    activeTime -= Time.deltaTime;
                }
                else
                {
                    state = AbilityState.cooldown;
                    cooldownTime = ability.cooldownTime;
                }
                break;
            case AbilityState.cooldown:
                if (cooldownTime > 0)
                {
                    cooldownTime -= Time.deltaTime;
                    Debug.Log("Cooldown time = " + cooldownTime);
                }
                else
                {

                    state = AbilityState.ready;
                    playerController.GetComponent<PlayerMobileInput>().enabled = true;
                    playerController.GetComponent<SwimController>().enabled = false;
                    waterPhysics.GetComponent<BuoyancyEffector2D>().enabled = true;
                }
                break;
        }
    }
}
