using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFootsteps : MonoBehaviour
{
    public AudioClip[] footstepSounds;
    public float colliderRadius = 10f;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();


        GameObject leftFoot = GetComponent<Animator>().GetBoneTransform(HumanBodyBones.LeftFoot).gameObject;
        GameObject rightFoot = GetComponent<Animator>().GetBoneTransform(HumanBodyBones.RightFoot).gameObject;

        leftFoot.AddComponent<SphereCollider>();
        rightFoot.AddComponent<SphereCollider>();

        leftFoot.AddComponent<Rigidbody>();
        rightFoot.AddComponent<Rigidbody>();

        leftFoot.AddComponent<GroundStepDetector>();
        rightFoot.AddComponent<GroundStepDetector>();

        leftFoot.GetComponent<SphereCollider>().radius = colliderRadius;
        rightFoot.GetComponent<SphereCollider>().radius = colliderRadius;

        leftFoot.GetComponent<Rigidbody>().isKinematic = true;
        rightFoot.GetComponent<Rigidbody>().isKinematic = true;

    }
    public void PlayFootStepAudio()
    {
        // pick & play a random footstep sound from the array,
        // excluding sound at index 0
        int n = Random.Range(1, footstepSounds.Length);
        audioSource.clip = footstepSounds[n];
        audioSource.PlayOneShot(audioSource.clip);
        // move picked sound to index 0 so it's not picked next time
        footstepSounds[n] = footstepSounds[0];
        footstepSounds[0] = audioSource.clip;
    }
}
