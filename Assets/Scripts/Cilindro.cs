using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cilindro : MonoBehaviour
{

    private float rangoVision = 5;
    public LayerMask capaDelJugador;

    public Transform jugador;
    public bool estarALerta; //cuando el jugador entra es verdadero

    // Start is called before the first frame update
    void Start()
    {
        jugador = GameObject.Find("Character").transform;
    }

    // Update is called once per frame
    void Update()
    {
        estarALerta = Physics.CheckSphere(transform.position, rangoVision, capaDelJugador);

        if (estarALerta)
        {
            transform.LookAt(new Vector3(jugador.position.x,transform.position.y ,jugador.position.z));
        }
    }
}
