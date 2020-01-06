using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction
{
    // Constantes

    // Définition des valeurs possibles
    public enum DirectionValue : short
    {
        LEFT = -1,
        NO = 0,
        RIGHT = 1
    }


    // Méthodes

    public static short GetValue(DirectionValue v)
    {
        return (short) v;
    }
}
