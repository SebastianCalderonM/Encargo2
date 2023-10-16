using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cubo : MonoBehaviour
{
    private float rangoVision = 5;
    public LayerMask capaDelJugador;

    public Transform jugador;
    public float velocidad;

    public bool estarALerta = false; //cuando el jugador entra es verdadero

    // Start is called before the first frame update
    void Start()
    {
        jugador = GameObject.Find("Character").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.MoveTowards(this.transform.position, targetObj.position, 10 * Time.deltaTime);
        estarALerta = Physics.CheckSphere(transform.position, rangoVision, capaDelJugador);

        if (estarALerta)
        {
            transform.LookAt(new Vector3(jugador.position.x,transform.position.y ,jugador.position.z));
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(jugador.position.x,transform.position.y ,jugador.position.z), velocidad * Time.deltaTime);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangoVision);
    }


}
