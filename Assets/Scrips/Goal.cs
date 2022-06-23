using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class Goal : MonoBehaviour
{
    [SerializeField] private Agent Oponente;
    [SerializeField] private Agent Agente;
    [SerializeField] private Material winMaterialAg;
    [SerializeField] private Material winMaterialOp;
    [SerializeField] private MeshRenderer floorMeshRenderer;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Agente")
        {
            Agente.AddReward(1f);
            Oponente.AddReward(-1f);
            floorMeshRenderer.material = winMaterialAg;
            Agente.EndEpisode();
            Oponente.EndEpisode();
        }
        if (other.tag == "Oponente")
        {
            Oponente.AddReward(1f);
            Agente.AddReward(-1f);
            floorMeshRenderer.material = winMaterialOp;
            Agente.EndEpisode();
            Oponente.EndEpisode();
        }
    }


}
