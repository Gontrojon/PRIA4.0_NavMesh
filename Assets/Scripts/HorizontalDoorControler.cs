using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalDoorControler : MonoBehaviour
{
    public float amplitud = 2f;
    public float periodo = 2.5f;
    private float inicial;
    // Start is called before the first frame update
    void Start()
    {
        inicial = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
            Vector3 position = transform.position;
            // time ofset sirve para poder hacer que la puerta siempre inicie en 0
            position.z = DoorPosition(Time.time + periodo / 4);
            transform.position = position; 
    }

    private float DoorPosition(float tiempo){
        // devolvemos el movimiento que tiene que hacer la puerta con el metodo pingpong
        // multiplicamos por 2 el tiempo antes de dividirlo para que sean de base 1 y no 0,5
        // y multiplicamos al final por la amplitud del movimiento menos la mitad del mismo
        return Mathf.SmoothStep( inicial, amplitud+inicial, Mathf.PingPong(tiempo * 2 / periodo, 1));
        
    }

}
