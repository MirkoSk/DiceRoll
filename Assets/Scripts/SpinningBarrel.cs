using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SpinningBarrel : MonoBehaviour
{
    [SerializeField] float torque = 1000f;

    new Rigidbody rigidbody;



    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Start()
    {
        SpinTheWheel();
    }



    public void SpinTheWheel()
    {
        rigidbody.AddRelativeTorque(new Vector3(0f, torque, 0f), ForceMode.VelocityChange);
    }
}
