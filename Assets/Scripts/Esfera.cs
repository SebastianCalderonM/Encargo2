using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esfera : MonoBehaviour
{
    private float rangoVision = 4;
    public LayerMask capaDelJugador;

    private Transform jugador;
    private float velocidad;

    //private Cell celdaContenedora;

    public bool estarALerta = false; //cuando el jugador entra es verdadero
    

    // Start is called before the first frame update
    void Start()
    {
        jugador = GameObject.Find("Character").transform;
        velocidad= Random.Range (6,9);
    }

    
    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector3.MoveTowards(this.transform.position, targetObj.position, 10 * Time.deltaTime);
        estarALerta = Physics.CheckSphere(transform.position, rangoVision, capaDelJugador);

        if (estarALerta   ) //InRoom(jugador.position, )
        {
            transform.LookAt(new Vector3(jugador.position.x,transform.position.y ,jugador.position.z));
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(jugador.position.x,transform.position.y ,jugador.position.z), velocidad * Time.deltaTime);
        }
    }

    private bool InRoom(Vector3 pos, Vector3 centro)
    {
        if (pos.x < centro.x - 5.5f || pos.x > centro.x + 5.5f || pos.z < centro.z - 5.5f || pos.z > centro.z + 5.5f)
        {
            return false;
        }
        return true;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, rangoVision);
    }
    
}
