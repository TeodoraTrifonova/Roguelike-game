using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStates : MonoBehaviour
{
    public static GameStates Instance { get; private set; }
    public State CurrentState { get => currentState; set => currentState = value; }

    private void Awake()
    {
        Instance = this;
    }

    private State currentState;

    public enum State
    {
        beginning,
        someIngredientsFound,
        bossFound,
        allIngredientsFound
    };
}
