using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class LaberintoRender : MonoBehaviour
{

    [SerializeField]
    private int alto;

    [SerializeField]
    private int ancho;

    [SerializeField]
    private Transform muroPrefab;

    [SerializeField]
    private float size = 1f;

    [SerializeField]
    private Transform sueloPrefab;


    [SerializeField] private Agent Agente;
    [SerializeField] private Agent Oponente;


    // Start is called before the first frame update
    
    //public override void OnEpisodeBegin()
    public void Start()
    {
        var laberinto = Generador.Generate(alto, ancho);
        Dibujar(laberinto);
        //Spawn();
    }
    private void Dibujar(Estados[,] laberinto)
    {
        var suelo = Instantiate(sueloPrefab, transform);
        suelo.position = new Vector3(alto / 2 + +17.5f, 0, ancho / 2 + 17.5f);
        suelo.localScale = new Vector3(alto/1.5f, 1, ancho/1.5f);

        for (int i = 0; i < alto; i++){
            for (int j = 0; j < ancho; j++){
                var celda = laberinto[i, j];
                var posicion = new Vector3(-alto/2  + i*6, 0, -ancho /2 + j*6);

                if (celda.HasFlag(Estados.ARRIBA))
                {
                    var topMuro = Instantiate(muroPrefab, transform) as Transform;
                    topMuro.position = posicion + new Vector3(0, 0, size/2);
                    topMuro.localScale = new Vector3(size, topMuro.localScale.y, topMuro.localScale.z);
                }
                if (celda.HasFlag(Estados.IZQUIERDA))
                {
                    var izqMuro = Instantiate(muroPrefab, transform) as Transform;
                    izqMuro.position = posicion + new Vector3(-size/2, 0, 0);
                    izqMuro.eulerAngles = new Vector3(0, 90, 0);
                    izqMuro.localScale = new Vector3(size, izqMuro.localScale.y, izqMuro.localScale.z);
                }
                if (i == alto - 1)
                {
                    if (celda.HasFlag(Estados.DERECHA))
                    {
                        var derMuro = Instantiate(muroPrefab, transform) as Transform;
                        derMuro.position = posicion + new Vector3(size / 2, 0, 0);
                        derMuro.eulerAngles = new Vector3(0, 90, 0);
                        derMuro.localScale = new Vector3(size, derMuro.localScale.y, derMuro.localScale.z);
                    }
                }
                if (j == 0)
                {
                    var downMuro = Instantiate(muroPrefab, transform) as Transform;
                    downMuro.position = posicion + new Vector3(0, 0, -size / 2);
                    downMuro.localScale = new Vector3(size, downMuro.localScale.y, downMuro.localScale.z);
                }
            }
        }
    }

    // Update is called once per frame
    /*private void Spawn()
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
    }*/
}
