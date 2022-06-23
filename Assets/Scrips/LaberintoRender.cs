/*using System.Collections;
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
    [SerializeField] private Goal Goal;



    // Start is called before the first frame update

    //public override void OnEpisodeBegin()
    public void Start()
    {
        ResetLaberinto();
        //Spawn();
    }

    public void ResetLaberinto()
    {
        print("Start");
        wait(15f);
        print("Weno");
        foreach (Transform child in this.transform)
        {
            print(child.name);
            GameObject.Destroy(child.gameObject);
        }
        print("Salida");
        wait(15f);
        print("Exit");
        var laberinto = Generador.Generate(alto, ancho);
        Dibujar(laberinto);

    }
    private void Dibujar(Estados[,] laberinto)
    {
        var suelo = Instantiate(sueloPrefab, transform);
        suelo.position = new Vector3(alto / 2 + +17.5f, 0, ancho / 2 + 17.5f);
        suelo.localScale = new Vector3(alto/1.5f, 1, ancho/1.5f);

        for (int i = 0; i < alto; i++){
            for (int j = 0; j < ancho; j++){
                var celda = laberinto[i, j];
                var posicion = new Vector3(-alto/2  + i*size, 0, -ancho /2 + j*size);

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

IEnumerator wait(float waitTime)
    { //creating a function
        yield return new WaitForSeconds(waitTime); //tell unity to wait!!
    }
}
*/