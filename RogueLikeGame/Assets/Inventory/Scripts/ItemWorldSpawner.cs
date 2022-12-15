using System.Collections.Generic;
using UnityEngine;

public class ItemWorldSpawner : MonoBehaviour {

    [SerializeField]
    private List<GameObject> prefab;

    public void Drop() {
        Instantiate(prefab[Random.Range(0,prefab.Count)], transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
