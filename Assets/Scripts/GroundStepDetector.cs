using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundStepDetector : MonoBehaviour
{
    private AddFootsteps footstepScript;
    private void Start()
    {
        footstepScript = GetComponentInParent<AddFootsteps>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (!footstepScript)
        {
            footstepScript = GetComponentInParent<AddFootsteps>();
        }
        if (!other.gameObject.GetComponent<GroundStepDetector>())
        {
            footstepScript.PlayFootStepAudio();
        }
    }
}
