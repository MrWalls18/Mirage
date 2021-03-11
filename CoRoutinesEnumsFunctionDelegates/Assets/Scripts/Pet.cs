using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enums are declared outside of a class definition
/// <summary>
/// Enums are actually ints masquerading as other values
/// which means they can be cast to ints
/// </summary>

public enum PetType
{
    none,
    dog,
    cat,
    bird,
    fish,
    other
}

public enum Gender
{
    unspecified,
    female,
    male
}

public class Pet : MonoBehaviour
{
    public string name = "Flash";
    public PetType pType = PetType.dog;
    public Gender gender = Gender.male;

    private void Awake()
    {
        int i = (int)PetType.cat; //i is = 2
        PetType pt = (PetType)4; //pt equals PetType.fish

        switch (pType)
        {
            case PetType.none:
                break;
            case PetType.dog:
                break;
            case PetType.cat:
                break;
            case PetType.bird:
                break;
            case PetType.fish:
                break;
            case PetType.other:
                break;
            default:
                break;
        }

        switch (gender)
        {
            case Gender.unspecified:
                break;
            case Gender.female:
                break;
            case Gender.male:
                break;
            default:
                break;
        }
    }




}
