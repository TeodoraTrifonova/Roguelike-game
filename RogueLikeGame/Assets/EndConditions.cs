using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EndConditions
{
    private static bool completeEndConditions = false;
    private static bool failEndConditions = false;

    public static bool CompleteEndConditions { get => completeEndConditions; set => completeEndConditions = value; }
    public static bool FailEndConditions { get => failEndConditions; set => failEndConditions = value; }
}
