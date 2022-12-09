using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    private float speed;


    private const float MAX_ROTATION = 14f;
    private const float MIN_ROTATION = 2f;


    [SerializeField]
    public int damage;

    private Vector3 mousePos;
    private Camera mainCam;

    [SerializeField]
    private GameObject particles;

    private GameObject rotationPoint;

    private Rigidbody2D rb;


    void Start()
    {

        var shootingPoint = GameObject.Find("ShootingPoint");
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rotationPoint = GameObject.Find("RotationPoint");

        rb = GetComponent<Rigidbody2D>();

        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 direction = shootingPoint.transform.position - rotationPoint.transform.position;
        Vector3 rotation = transform.position - mousePos;

        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
        rb.AddTorque(Random.Range(MIN_ROTATION, MAX_ROTATION));

        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    private void Update()
    {

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(particles, transform.position, transform.rotation);
        Destroy(gameObject);

    }

}
