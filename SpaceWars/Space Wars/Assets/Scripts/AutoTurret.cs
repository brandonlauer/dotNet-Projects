using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AutoTurret : NetworkBehaviour {

    public float FireRate = 10;
    public float ProjectileSpeed = 10;
    public int LaserDamage;
    public GameObject TurretLaser;
    public float LifeTime = 30;
    public Transform HealthBar;

    [SyncVar(hook ="SetTarget")]private Transform _target;
    private Transform firePoint;
    private float timeToFire = 0;
    private float currentLife;

    private void Start()
    {
        currentLife = LifeTime;
        firePoint = transform.Find("FirePoint");
        if(firePoint == null)
        {
            Debug.LogError("No firepoint assigned to turret!!");
        }

        if(TurretLaser == null)
        {
            Debug.LogError("No laser assigned to the turret!!");
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void DamageTurret(float damage)
    {
        currentLife -= damage;
    }
	
	// Update is called once per frame
	void Update () {
        currentLife -= Time.deltaTime;
        if(currentLife <= 0)
        {
            Destroy(transform.parent.gameObject);
        }

        HealthBar.transform.localScale = new Vector3(currentLife / LifeTime, transform.localScale.y);
        
        if (_target == null) { return; }

        Vector3 difference = _target.position - transform.position;
        difference.Normalize();

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        if(transform.parent.localScale.x == -1)
        {
            transform.rotation = Quaternion.Euler (0f, 0f, rotZ);
        }
        else
        {
            transform.rotation = Quaternion.Euler (0f, 0f, rotZ + 180);
        }
        
        if(Time.time > timeToFire)
        {
            timeToFire = Time.time + 1 / FireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        Vector2 direction = _target.position - firePoint.position;
        direction.Normalize();
        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

        GameObject laser = (GameObject)Instantiate(TurretLaser, firePoint.position, rotation);
        laser.GetComponent<Projectile>().parent = transform.parent;
        laser.GetComponent<Projectile>().Damage = LaserDamage;
        laser.GetComponent<Rigidbody2D>().velocity = direction * ProjectileSpeed;
    }
}
