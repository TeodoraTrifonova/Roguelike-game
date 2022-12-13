using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropBreak : MonoBehaviour
{
	private Vector3 originPosition;
	private Quaternion originRotation;
	private float shake_decay = 0.002f;
	private float shake_intensity = 0.1f;

	private Prop prop;
	private int currentHealth;

	[SerializeField]
	private GameObject destroyParticles;

	[SerializeField]
	private GameObject propHitParticles;

	private float temp_shake_intensity = 0;

    private void Start()
    {
		prop = GetComponent<Prop>();
		currentHealth = prop.Health;

	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.tag == "Bullet")
        {
			Shake();
			Instantiate(propHitParticles, transform.position, transform.rotation);
			currentHealth -= collision.gameObject.GetComponent<BulletScript>().Damage;
			if (currentHealth <= 0)
            {
				Instantiate(destroyParticles, transform.position, transform.rotation);
				Destroy(gameObject);
            }
		}
    }

    void Update()
	{
		if (temp_shake_intensity > 0)
		{
			transform.position = originPosition + Random.insideUnitSphere * temp_shake_intensity;
			transform.rotation = new Quaternion(
				originRotation.x + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .2f,
				originRotation.y + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .2f,
				originRotation.z + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .2f,
				originRotation.w + Random.Range(-temp_shake_intensity, temp_shake_intensity) * .2f);
			temp_shake_intensity -= shake_decay;
		}
	}

	void Shake()
	{
		originPosition = transform.position;
		originRotation = transform.rotation;
		temp_shake_intensity = shake_intensity;

	}
}
