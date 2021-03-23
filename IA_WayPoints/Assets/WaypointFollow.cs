using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollow : MonoBehaviour
{
    public GameObject[] waypoints; 
    int currentWP= 0; //valor inicial
    public float speed= 2f;
    public float accuracy= 1.0f; //aproximação que o objeto terá
    public float rotSpeed= 0.4f; //rotação à nova rota
        
    void Start()
    {
        //localiza já de início a primeira rota a ser feita para o primeiro ponto
        waypoints = GameObject.FindGameObjectsWithTag("waypoint");
    }    
    private void LateUpdate()
    {
        if (waypoints.Length == 0) 
            return; //verifica o tamanho do array
        
        //define o local que o objeto precisa olhar 
        Vector3 lookAtGoal = new Vector3(waypoints[currentWP].transform.position.x, 
            this.transform.position.y, //não se modifica pois não se movimenta neste eixo
            waypoints[currentWP].transform.position.z);

        //define aonde o objeto precisa caminhar em relação ao local que está olhando
        Vector3 direction = lookAtGoal - this.transform.position; 
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, //rotação mais natural
            Quaternion.LookRotation(direction),
            Time.deltaTime * rotSpeed);

        //muda de rota em relação ao valor de acurácia e magnitude do vetor
        if (direction.magnitude < accuracy)
        {
            currentWP++; //próximo waypoint

            if (currentWP >= waypoints.Length) //retorna ao primeiro waypoint
            { currentWP = 0; }
        }
        this.transform.Translate(0, 0, speed * Time.deltaTime);
    }

}
