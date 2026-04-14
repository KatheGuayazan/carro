using UnityEngine;

public class IACAR : MonoBehaviour
{
    public float distance = 11f;
    public float distance2 = 5f;

    public Transform sensor1;
    public Transform sensorIzq;
    public Transform sensorDere;

    public bool bool_S1 = true;
    public bool bool_SIzq = true;
    public bool bool_SDere = true;
    public bool isBrake = false;

    public PrometeoCarController CarController;
    void Update()
    {
        RaycastHit hit;
        RaycastHit hitIzq;
        RaycastHit hitDere;

        //----------------------------------------------------------------------------------------
        Vector3 origen1 = sensor1.position;
        Vector3 direccion1 = sensor1.forward;

        Vector3 origenIzq = sensorIzq.position;
        Vector3 direccionIzq = sensorIzq.forward;

        Vector3 origenDere = sensorDere.position;
        Vector3 direccionDere = sensorDere.forward;

        //----------------------------------------------------------------------------------------
        bool hit1 = Physics.Raycast(origen1, direccion1, out hit, distance);
        bool hit_Izq = Physics.Raycast(origenIzq, direccionIzq, out hitIzq, distance);
        bool hit_Dere = Physics.Raycast(origenDere, direccionDere, out hitDere, distance);

        //----------------------------------------------------------------------------------------
        if (hit1) { 
            Debug.DrawRay(origen1, direccion1 * hit.distance, Color.yellow); 
        }
        else { 
            Debug.DrawRay(origen1, direccion1 * distance, Color.green); 
        }

        if (hit_Izq)
        {
            Debug.DrawRay(origen1, direccion1 * hit.distance, Color.blueViolet);
        }
        else
        {
            Debug.DrawRay(origen1, direccion1 * distance, Color.green);
        }
        if (hit_Dere)
        {
            Debug.DrawRay(origen1, direccion1 * hit.distance, Color.pink);
        }
        else
        {
            Debug.DrawRay(origen1, direccion1 * distance, Color.green);
        }

        //----------------------------------------------------------------------------------------

        if (hit1)
        {
            isBrake = true;
            bool_S1 = false;

            if (!hit_Izq)
            {
                // Girar izquierda
                CarController.TurnLeft();
            }
            else if (!hit_Dere)
            {
                // Girar derecha
                CarController.TurnRight();
            }
            else
            {
                // Todo bloqueado → retroceder
                bool_S1 = false;
                isBrake = false;
                CarController.isDrive = false;
            }
        }
        else
        {
            // Camino libre
            bool_S1 = true;
            isBrake = false;
            CarController.isDrive = true;
            CarController.ResetSteeringAngle();
        }

        /*
        if (Physics.Raycast(origen1, direccion1, out hit, distance))
        {
            if (hit.distance < distance2){
                isBrake = true;
                bool_S1 = false;
            }
            else{
                isBrake = false;
                bool_S1 = true;
            }
        }
         else{
            Debug.DrawRay(origen1, direccion1 * distance, Color.green);
            bool_S1 = true;
            isBrake = false;
        }

        CarController.isDrive = bool_S1;
        CarController.isBrake = isBrake; */
    }
}
