using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;

    [SerializeField]
    private Player player;

    [Header("Weapons // Throwables")]
    [SerializeField]
    private List<GameObject> weapons;

    public int selectedWeapon;

    [SerializeField]
    private Transform shootingPoint;

    [SerializeField]
    private bool canFire;

    private float timer;

    [SerializeField]
    private float timeBetweenFiring;

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].GetComponentInChildren<BoxCollider2D>().enabled = false;
        }
        selectedWeapon = -1;
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring)
            {
                canFire = true;
                timer = 0;
            }
        }

        if (Input.GetMouseButtonDown(0) && canFire && (selectedWeapon >= 0 && selectedWeapon < weapons.Count))
        {
            
            canFire = false;
            Instantiate(weapons[selectedWeapon], new Vector2(transform.position.x, transform.position.y - 0.33f), Quaternion.identity);
            AudioManager.instance.Play("shoot");
            if (selectedWeapon == 0)
            {
                player.RemoveItem(new Item { itemType = Item.ItemType.rollingPin, amount = 1 });
            }
            else if(selectedWeapon == 1)
            {
                player.RemoveItem(new Item { itemType = Item.ItemType.cookingPot, amount = 1 });
            }
            else if(selectedWeapon == 2)
            {
                player.RemoveItem(new Item { itemType = Item.ItemType.ladle, amount = 1});
            }
            else if(selectedWeapon == 3)
            {
                player.RemoveItem(new Item { itemType = Item.ItemType.cookingKnife, amount = 1});
            }
            selectedWeapon = -1;
        }
    }
}
