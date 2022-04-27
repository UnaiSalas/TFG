using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class MovimientoAMetaOponente : Agent
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Material winMaterial;
    [SerializeField] private Material loseMaterial;
    [SerializeField] private MeshRenderer floorMeshRenderer;

    [SerializeField] private Agent Agente;



    public override void OnEpisodeBegin()
    {
        transform.localPosition = new Vector3(Random.Range(-21f, 16f), -5.2f, Random.Range(-25f, 10f));
        targetTransform.localPosition = new Vector3(Random.Range(-21f, 16f), -5.2f, Random.Range(-25f, 10f));
        Agente.transform.localPosition = new Vector3(Random.Range(-21f, 16f), -5.2f, Random.Range(-25f, 10f));
        this.SetReward(0f);
        Agente.SetReward(0f);
        
    }
    /*public override void CollectObservations()
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.localPosition);
        
    }*/
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        //forward
        if (Input.GetKey(KeyCode.U))
        {
            discreteActionsOut[0] = 1;
        }
        if (Input.GetKey(KeyCode.J))
        {
            discreteActionsOut[0] = 2;
        }
        //rotate
        if (Input.GetKey(KeyCode.H))
        {
            discreteActionsOut[0] = 3;
        }
        if (Input.GetKey(KeyCode.K))
        {
            discreteActionsOut[0] = 4;
        }
        //right
        if (Input.GetKey(KeyCode.I))
        {
            discreteActionsOut[0] = 5;
        }
        if (Input.GetKey(KeyCode.Y))
        {
            discreteActionsOut[0] = 6;
        }
    }
    public override void OnActionReceived(ActionBuffers actions) 
    { 
    
        float moveX = actions.DiscreteActions[0];

        float moveSpeed = 1f;

        switch (moveX)
        {
            case 1:
                transform.localPosition += transform.forward * Time.deltaTime * moveSpeed;
                break;
            case 2:
                transform.localPosition += -transform.forward * Time.deltaTime * moveSpeed;
                break;
            case 3:
                transform.localPosition += -transform.right * Time.deltaTime * moveSpeed;
                break;
            case 4:
                transform.localPosition += transform.right * Time.deltaTime * moveSpeed;
                break;
            case 5:
                transform.Rotate(transform.up, 1f);
                break;
            case 6:
                transform.Rotate(transform.up, -1f);
                break;
        }


        //transform.localPosition += new Vector3(moveX, 0, moveZ) * Time.deltaTime * moveSpeed;
        //transform.Rotate(rotacion, Time.deltaTime * 50f);
        AddReward(-0.00001f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Goal>(out Goal goal)){
            this.AddReward(1f);
            Agente.AddReward(-1f);
            floorMeshRenderer.material = winMaterial;
            EndEpisode();
        }
        if (other.TryGetComponent<Muro>(out Muro muro)){
            this.AddReward(-1f);
            floorMeshRenderer.material = loseMaterial;
            EndEpisode();
        }
    }
}
