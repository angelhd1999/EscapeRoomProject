using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// This script weights in a realistic way the different objects that collide with the weight scales.
/// </summary>
public class WeightScale : MonoBehaviour
{
    
    public float combinedForce;
    public float calculatedMass;
    public int registeredRigidbodies;

    [SerializeField] private GameObject PhysicsManager = null;
    [SerializeField] private GameObject Safe = null;
    private float forceToMass;
    private float currentDeltaTime;
    private float lastDeltaTime;

    Dictionary<Rigidbody, float> impulsePerRigidBody = new Dictionary<Rigidbody, float>(); 

    private void Awake()
    {
        forceToMass = 1f / PhysicsManager.GetComponent<PhysicsManager>().gravity; //With our personalized gravity.
    }

    void UpdateWeight()
    {
        registeredRigidbodies = impulsePerRigidBody.Count;
        combinedForce = 0;

        foreach (var force in impulsePerRigidBody.Values)
        {
            combinedForce += Math.Abs(force);
        }

        calculatedMass = (float)(combinedForce * forceToMass);
        Safe.GetComponent<MesureMovement>().UpdateMesures();
    }

    private void FixedUpdate()
    {
        lastDeltaTime = currentDeltaTime;
        currentDeltaTime = Time.deltaTime;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.rigidbody != null)
        {
            if (impulsePerRigidBody.ContainsKey(collision.rigidbody))
                impulsePerRigidBody[collision.rigidbody] = collision.impulse.y / lastDeltaTime;
            else
                impulsePerRigidBody.Add(collision.rigidbody, collision.impulse.y / lastDeltaTime);

            UpdateWeight();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody != null)
        {
            if (impulsePerRigidBody.ContainsKey(collision.rigidbody))
                impulsePerRigidBody[collision.rigidbody] = collision.impulse.y / lastDeltaTime;
            else
                impulsePerRigidBody.Add(collision.rigidbody, collision.impulse.y / lastDeltaTime);

            UpdateWeight();
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.rigidbody != null)
        {
            impulsePerRigidBody.Remove(collision.rigidbody);
            UpdateWeight();
        }
    }
}
