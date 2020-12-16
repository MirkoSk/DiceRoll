using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class DieSide
{
    public Transform transform;
    public int value;
}

[RequireComponent(typeof(Rigidbody))]
public class DieController : MonoBehaviour
{
    [Header("Standstill")]
    [SerializeField] float standstillAngularVelocity = 1f;
    [SerializeField] float standstillDuration = 0.3f;

    [Header("Sound")]
    [SerializeField] AudioSource audioSource = null;
    [SerializeField] AudioClip dieRollSound = null;

    [Header("Result Effects")]
    [SerializeField] Vector3 punchScale = Vector3.one;
    [SerializeField] float punchDuration = 0.5f;

    [Header("Die Sides")]
    [SerializeField] DieSide[] dieSides = new DieSide[6];

    new Rigidbody rigidbody = null;
    DieSide result;
    float durationStandingStill;



    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (result != null) return;

        if (rigidbody.angularVelocity.magnitude <= standstillAngularVelocity)
        {
            durationStandingStill += Time.deltaTime;
            if (durationStandingStill >= standstillDuration) ShowResult();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (audioSource && dieRollSound && collision.transform.tag == "RollBox") audioSource.PlayOneShot(dieRollSound);
    }



    void ShowResult()
    {
        result = null;
        float highest = 0f;
        foreach (DieSide dieSide in dieSides)
        {
            if (dieSide.transform.position.y > highest)
            {
                result = dieSide;
                highest = dieSide.transform.position.y;
            }
        }

        result.transform.DOPunchScale(punchScale, punchDuration, 1, 0);
    }
}
