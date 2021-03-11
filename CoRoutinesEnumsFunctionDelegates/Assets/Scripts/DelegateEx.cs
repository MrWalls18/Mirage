using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateEx : MonoBehaviour
{
    //define the type of delegate
    public delegate float FloatOpDelegate(float f0, float f1);

    //now we just instantiate a member variable of the FloatOpDelegate
    public FloatOpDelegate fod;

    //create instances of the delegates
    public float FloatAdd(float f0, float f1)
    {
        float result = f0 + f1;
        Debug.Log("FloatAdd.Result : " + result);
        return result;
    }

    public float FloatMultiply(float f0, float f1)
    {
        float result = f0 * f1;
        Debug.Log("FloatMultiply.result : " + result);
        return result;
    }

    void Awake()
    {
        //assign FloatAdd to the delegate
        fod = FloatAdd;

        //call fod as if it were a function
        float value = fod(2, 3); //returns S

        //reassign fod
        fod = FloatMultiply;

        //call fod as if it were a function
        value = fod(2, 3);

        //delegates can also be multicast
        fod = FloatAdd;
        fod += FloatMultiply;

        //Note - need to always check that the delegate is not null for you to call it
        if(fod != null)
        {
            float result = fod(3, 4); //calls FloatAdd then FloatMulitply
            print(result);//prints 12
        }


    }



}
