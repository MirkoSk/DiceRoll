using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceRoller : MonoBehaviour
{
    [SerializeField] GameObject dicePrefab = null;
    [SerializeField] Transform spawnLocation = null;
    [SerializeField] Transform throwTarget = null;

    [Header("Throw Dynamics")]
    [SerializeField] float throwSpeed = 10f;
    [SerializeField] float throwSpeedVariance = 2f;
    [SerializeField] float angularSpeedInThrowDirection = 10f;
    [Tooltip("Angular speed perpendicular to the throw direction.")]
    [SerializeField] float angularSpeedVariance = 7f;
    [SerializeField] float spaceBetweenDice = 0.7f;

    [Header("Sound")]
    [SerializeField] AudioSource windUpAudioSource = null;

    int numberOfDiceToRoll = 1;
    List<GameObject> currentDice = new List<GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            WindUpRoll();
        }
        else if (Input.GetButtonUp("Jump"))
        {
            RollDice();
        }
    }



    public void WindUpRoll()
    {
        windUpAudioSource.Play();
    }

    public void RollDice()
    {
        windUpAudioSource.Stop();

        foreach (GameObject die in currentDice)
        {
            Destroy(die);
        }
        currentDice.Clear();

        for (int i = 0; i < numberOfDiceToRoll; i++)
        {
            Vector3 spawnPosition = spawnLocation.position 
                + Vector3.forward * (i / 2) * spaceBetweenDice
                + Vector3.right * (i % 2) * spaceBetweenDice;

            GameObject die = Instantiate(dicePrefab, spawnPosition, Random.rotation);
            Rigidbody dieRB = die.GetComponent<Rigidbody>();

            Vector3 throwDirection = (throwTarget.position - spawnPosition).normalized;
            dieRB.velocity = throwDirection * (throwSpeed + Random.Range(-throwSpeedVariance, throwSpeedVariance));

            Vector3 angularVelocity = Vector3.Cross(throwDirection, Vector3.down) * angularSpeedInThrowDirection;
            angularVelocity += throwDirection * Random.Range(-angularSpeedVariance, angularSpeedVariance);
            if (dieRB.maxAngularVelocity < angularSpeedInThrowDirection) dieRB.maxAngularVelocity = angularSpeedInThrowDirection;
            dieRB.angularVelocity = angularVelocity;

            currentDice.Add(die);
        }
    }

    public void UpdateNumberOfDiceToRoll(string number)
    {
        numberOfDiceToRoll = int.Parse(number);
    }
}
