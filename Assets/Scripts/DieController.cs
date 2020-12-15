using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieController : MonoBehaviour
{
    [SerializeField] AudioSource audioSource = null;
    [SerializeField] AudioClip dieRollSound = null;


    private void OnCollisionEnter(Collision collision)
    {
        if (audioSource && dieRollSound && collision.transform.tag == "RollBox") audioSource.PlayOneShot(dieRollSound);
    }
}
