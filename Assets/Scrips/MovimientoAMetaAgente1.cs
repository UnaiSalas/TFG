using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class MovimientoAMetaAgente1 : Agent
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Material winMaterial;
    [SerializeField] private Material loseMaterial;
    [SerializeField] private MeshRenderer floorMeshRenderer;

    [SerializeField] private Agent Oponente;

    


    public override void OnEpisodeBegin()
    {
        //x:-21 16 z:-25 10
        //transform.localPosition = new Vector3(Random.Range(-21f, 16f), -5.2f, Random.Range(-25f, 10f));
        //targetTransform.localPosition = new Vector3(Random.Range(-21f, 16f), -5.2f, Random.Range(-25f, 10f));
        //Oponente.transform.localPosition = new Vector3(Random.Range(-21f, 16f), -5.2f, Random.Range(-25f, 10f));
        //this.SetReward(0f);
        //Oponente.SetReward(0f);
        Spawn();
    }
    /*public override void CollectObservations()
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(targetTransform.localPosition);
        
    }*/
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        var discreteActionsOut = actionsOut.DiscreteActions;
        var discreteActionsOut2 = actionsOut.DiscreteActions;
        //forward
        if (Input.GetKey(KeyCode.W))
        {
            discreteActionsOut[0] = 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            discreteActionsOut[0] = 2;
        }
        //rotate
        if (Input.GetKey(KeyCode.A))
        {
            discreteActionsOut[0] = 3;
        }
        if (Input.GetKey(KeyCode.D))
        {
            discreteActionsOut[0] = 4;
        }
        //right
        if (Input.GetKey(KeyCode.E))
        {
            discreteActionsOut[0] = 5;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            discreteActionsOut[0] = 6;
        }
    }
    public override void OnActionReceived(ActionBuffers actions) 
    { 
    
        float moveAgente = actions.DiscreteActions[0];
        float moveOponente = actions.DiscreteActions[0];

        float moveSpeed = 2f;

        switch (moveAgente)
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


        this.AddReward(-0.00001f);
    }
    private void OnTriggerEnter(Collider other)
    {
        /*if(other.TryGetComponent<Goal>(out Goal goal)){
            this.AddReward(1f);
            Oponente.AddReward(-1f);
            floorMeshRenderer.material = winMaterial;
            EndEpisode();
            Oponente.EndEpisode();
        }*/
        if (other.TryGetComponent<Muro>(out Muro muro)){
            this.AddReward(-1f);
            floorMeshRenderer.material = loseMaterial;
            EndEpisode();
            Oponente.EndEpisode();
        }
    }
    private void Spawn()
    {
        targetTransform.localPosition = new Vector3(Random.Range(-21f, 16f), -5.2f, Random.Range(-25f, 10f));
       
        float xAgente = -22f;
        float zAgente = -26f;
        float xOponente = -22f;
        float zOponente = -26f;
        float RadioSpawn = Random.Range(5f, 30f);
        // 8 -13 || -20 4
        while (xAgente < -21f || xAgente > 16f || zAgente < -25f || zAgente > 10f)
        {

            float angle = Random.Range(0, 360);
            xAgente = Mathf.Cos(angle * Mathf.Deg2Rad);
            zAgente = Mathf.Sin(angle * Mathf.Deg2Rad);
            xAgente = targetTransform.localPosition.x + xAgente * RadioSpawn;
            zAgente = targetTransform.localPosition.z + zAgente * RadioSpawn;
        }
        transform.localPosition = new Vector3(xAgente, -5.2f, zAgente);
        while (xOponente < -21f || xOponente > 16f || zOponente < -25f || zOponente > 10f)
        {
            float angle = Random.Range(0, 360);
            xOponente = Mathf.Cos(angle * Mathf.Deg2Rad);
            zOponente = Mathf.Sin(angle * Mathf.Deg2Rad);
            xOponente = targetTransform.localPosition.x + xOponente * RadioSpawn;
            zOponente = targetTransform.localPosition.z + zOponente * RadioSpawn;
        }
        Oponente.transform.localPosition = new Vector3(xOponente, -5.2f, zOponente);
        
        this.SetReward(0f);
        Oponente.SetReward(0f);

        /*Vector3 puntoMeta = new Vector3(Random.Range(4.5f, -8f), -5.2f, Random.Range(0f, -15f));
        Vector3 comprobacion = puntoMeta;
        comprobacion = puntoMeta + Random.insideUnitSphere.normalized * RadioSpawn;
        while((comprobacion.x > 5f || comprobacion.x < -8f) && comprobacion.y!=-5.2f && (comprobacion.z > 0f || comprobacion.z < -15f))
        {
            comprobacion = puntoMeta + Random.insideUnitSphere.normalized * RadioSpawn;
        }
        Vector3 puntoAgente = comprobacion;
        //public GameObject Agente2;

        // Start is called before the first frame 

        //targetTransform.localPosition = new Vector3(Random.Range(4.5f, -8f), -5.2f, Random.Range(0f, -15f));
        transform.localPosition = puntoAgente;
        targetTransform.localPosition = puntoMeta;
        */
    }

}
