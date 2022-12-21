using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackpackController : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        GetComponent<Button_UI>().MouseRightClickFunc = () =>
        {
            if(Backpack.ItemsCount >= 5)
            {
                Debug.Log("-5 items");
                Backpack.RemoveItems(5);
                StartCoroutine(GetTemporaryBoost());
            }
        };
    }

    IEnumerator GetTemporaryBoost()
    {
        player.GetComponent<PlayerMovement>().BoostSpeed += 1;
        player.GetComponent<PlayerHealth>().UpdateHealth(20);
        yield return new WaitForSeconds(20f);
        player.GetComponent<PlayerMovement>().BoostSpeed -= 1;
    }
}
