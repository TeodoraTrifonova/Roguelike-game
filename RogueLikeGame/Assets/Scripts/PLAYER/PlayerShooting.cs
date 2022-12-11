using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;

    [SerializeField]
    private GameObject bullet;

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

        if (Input.GetMouseButton(0) && canFire)
        {
            canFire = false;
            Instantiate(bullet, new Vector2(transform.position.x, transform.position.y - 0.33f), Quaternion.identity);

        }
    }
}
