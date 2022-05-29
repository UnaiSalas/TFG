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
        //transform.localPosition = new Vector3(Random.Range(-21f, 16f), -5.2f, Random.Range(-25f, 10f));
        //targetTransform.localPosition = new Vector3(Random.Range(-21f, 16f), -5.2f, Random.Range(-25f, 10f));
        //Agente.transform.localPosition = new Vector3(Random.Range(-21f, 16f), -5.2f, Random.Range(-25f, 10f));
        //this.SetReward(0f);
        //Agente.SetReward(0f);
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
            Agente.EndEpisode();
        }
        if (other.TryGetComponent<Muro>(out Muro muro)){
            this.AddReward(-1f);
            floorMeshRenderer.material = loseMaterial;
            EndEpisode();
            Agente.EndEpisode();
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
        Agente.transform.localPosition = new Vector3(xOponente, -5.2f, zOponente);

        this.SetReward(0f);
        Agente.SetReward(0f);

        
        /*while (xOponente < -21f || xOponente > 16f || zOponente < -25f || zOponente > 10f)
        {
            float anguloOponente = Random.Range(0, 360) * Mathf.PI * 2;
            xOponente = targetTransform.localPosition.x + Mathf.Cos(anguloOponente) * RadioSpawn;
            zOponente = targetTransform.localPosition.z + Mathf.Sin(anguloOponente) * RadioSpawn;
        }
        Agente.transform.localPosition = new Vector3(xOponente, -5.2f, zOponente);*/

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
