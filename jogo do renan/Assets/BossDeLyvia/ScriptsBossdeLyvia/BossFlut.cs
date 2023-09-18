using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFlut : MonoBehaviour
{
    public float speed;

    public float Tflutution;

    public float Fflutuation;

    private Vector3 posicaoInicial;
   
    // Start is called before the first frame update
    void Start()
    {
        posicaoInicial = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        float offsetY = Mathf.Sin(Time.time * Fflutuation) * Tflutution;
        
        Vector3 novaPosicao = posicaoInicial + new Vector3(Mathf.PingPong(Time.time * speed, 2) - 1, offsetY, 0);

        transform.position = novaPosicao;
    }
}
