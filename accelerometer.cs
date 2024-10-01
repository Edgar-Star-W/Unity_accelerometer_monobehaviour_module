using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//This function calculates the acceleration vector in meter/second^2.
    //Input: position. If the output is used for motion simulation, the input transform
    //has to be located at the seat base, not at the vehicle CG. Attach an empty GameObject
    //at the correct location and use that as the input for this function.
    //Gravity is not taken into account but this can be added to the output if needed.
    //A low number of samples can give a jittery result due to rounding errors.
    //If more samples are used, the output is more smooth but has a higher latency.

public class accelerometer : MonoBehaviour
{
    public Vector3 averageSpeedChange = Vector3.zero;
    public Vector3 vector_acceleration = Vector3.zero; 
    public Vector3 deltaDistance; 
    public float deltaTime; 
    public Vector3 speedA; 
    public Vector3 speedB; 
    public static int samples = 3 ;
    // Start is called before the first frame update
    public Vector3[] positionRegister = new Vector3[samples];
    public float[] posTimeRegister = new float[samples];

    public int positionSamplesTaken = 0 ;

    Vector3 acceleration(Vector3 position, out bool inputIsValid){
        inputIsValid = true;

        for(int i = 0; i < positionRegister.Length - 1; i++)
        {
            positionRegister[i] = positionRegister[i+1];
            posTimeRegister[i] = posTimeRegister[i+1];
        }
        positionRegister[positionRegister.Length - 1] = position;
        posTimeRegister[posTimeRegister.Length - 1] = Time.time;
        positionSamplesTaken++;
        //Debug.Log(positionSamplesTaken);
        if(positionSamplesTaken >= samples)
        {
            for(int i = 0; i < positionRegister.Length - 2; i++)
            {
                deltaDistance = positionRegister[i+1] - positionRegister[i];
                deltaTime = posTimeRegister[i+1] - posTimeRegister[i];
                //If deltaTime is 0, the output is invalid.
                if(deltaTime == 0)
                {
                    inputIsValid = false;
                    return Vector3.zero;
                }
                speedA = deltaDistance / deltaTime;
                deltaDistance = positionRegister[i+2] - positionRegister[i+1];
                deltaTime = posTimeRegister[i+2] - posTimeRegister[i+1];
                if(deltaTime == 0)
                {
                    inputIsValid = false;
                    return Vector3.zero;
                }
                speedB = deltaDistance / deltaTime;
                //This is the accumulated speed change at this stage, not the average yet.
                averageSpeedChange += speedB - speedA; 
            }
            //Now this is the average speed change.
            averageSpeedChange /= positionRegister.Length - 2;
            //Get the total time difference.
            float deltaTimeTotal = posTimeRegister[posTimeRegister.Length - 1] - posTimeRegister[0]; 
            //Now calculate the acceleration, which is an average over the amount of samples taken.
            Vector3  acceleration  = averageSpeedChange / deltaTimeTotal;
            return acceleration;
        }
        else{
            inputIsValid = false;
            return Vector3.zero;

        }    
    }

    void Start()
    {

    }
       
    // Update is called once per frame
    void Update()
    {
        bool check;
        Vector3 acc = acceleration(transform.position, out check);

        Debug.Log(acc);
        //Debug.Log(check);
    }
}
