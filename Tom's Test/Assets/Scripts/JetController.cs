using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class JetController : MonoBehaviour
{
    float speed = 250f;
    private float turnSpeed = 70;
    private float horizontalInput;
    private float verticalInput;

    Rigidbody rb;
    public Slider healthSlider;    

    public float hp = 70;
    private float gravity = 40;
    int damage = 10;
    bool damaged;

    public bool boosting = false;
    float boostingTime = 3;
    bool grounded = true;

    float damageTime = 5;
    float lightshotTime = 5;
    float countdownTime = 3;

    public Rigidbody projectile;
    float bulletSpeed = 300;
    public float bulletsLeft;
    public Text bulletText;

    public Rigidbody lightPro;
    bool lightShot;

    bool shieldActive;

    public GameObject lightMode;
    public GameObject shield;
    public GameObject spawnPoint;
    public GameObject jetPrefab;
    public GameObject finishScreen;
    public GameObject isRace;
    public GameObject door1;
    public GameObject door2;


    public GlobalTimer globalTimer;

    void Start()
    {

        Time.timeScale = 1f;
        rb = GetComponent<Rigidbody>();
        healthSlider.value = hp;
    }

    // Update is called once per frame
    void Update()
    {
        countdownTime -= Time.deltaTime;

        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        if (countdownTime < 0)
        {
            if (Input.GetMouseButton(0))
            {
                rb.isKinematic = true;
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }

            else
            {
                rb.isKinematic = false;
                rb.AddForce(1, -gravity, gravity);
            }
        }

        if(transform.position.y >= 360)
        {
            rb.isKinematic = false;            
        }
        if (transform.position.y <= 0)
        {
            rb.isKinematic = false;
        }

        if (grounded == true)
        {
            transform.Rotate(Vector3.right * Time.deltaTime * verticalInput * turnSpeed);
            transform.Rotate(-Vector3.up * turnSpeed * horizontalInput * Time.deltaTime);
        }
        else
        {
            transform.Rotate(Vector3.right * Time.deltaTime * verticalInput * turnSpeed);
            transform.Rotate(-Vector3.forward * turnSpeed * horizontalInput * Time.deltaTime);
        }
        
        if (Input.GetKeyDown(KeyCode.Space) & bulletsLeft > 0 & lightMode.activeSelf)
        {
            Shoot();
            bulletsLeft -= 1;
        }
        if (Input.GetKeyDown(KeyCode.Space) & !lightMode.activeSelf & lightshotTime == 5)
        {
            ShootLight();
        }

        if(lightShot)
        {
            lightshotTime -= Time.deltaTime;
        }
        if(lightshotTime <= 0 )
        {
            lightShot = false;
            lightshotTime = 5;
        }

        string bulletstring = string.Format("{0}", bulletsLeft);
        bulletText.text = bulletstring;

        if (boosting == true)
        {
            healthSlider.value = hp;
            boostingTime -= Time.deltaTime;
            speed = 350f;
            if(boostingTime < 0)
            {
                boosting = false;
                boostingTime = 3;
                speed = 250f;
            }
        }
        else
        {
            hp -= Time.deltaTime * 2;
            healthSlider.value = hp;
        }
        if (hp <= 0)
        {
            Death();
        }

        if(damaged)
        {
            damageTime -= Time.deltaTime;
        }

        if (damageTime <= 0)
        {
            damaged = false;
            damageTime = 5;
        }
    }

    void OnCollisionEnter(Collision col)
    {
        
        if(col.gameObject.layer == 9)
        {
            if (damageTime == 5)
            {
                Damage(damage);
            }
            Destroy(col.gameObject);            
        }
        if(col.gameObject.layer == 9 & boosting == true)
        {
            Damage(damage*10);
        }
        if(col.gameObject.layer == 12 & grounded == false)
        {
            rb.isKinematic = false;
            if (damageTime == 5)
            {
                Damage(damage);
            }
        }        
        
        
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.layer == 10)
        {
            Destroy(col.gameObject);
            if (hp < 90)
            {
                hp += 10;
            }
            
        }

        if (col.gameObject.tag == "BulletBoost")
        {
            Destroy(col.gameObject);

            if (bulletsLeft == 0)
            {
                bulletsLeft += 3;
            }
            if (bulletsLeft == 1)
            {
                bulletsLeft += 2;
            }
            if (bulletsLeft == 2)
            {
                bulletsLeft += 1;
            }
            if (bulletsLeft == 3)
            {
                bulletsLeft += 0;
            }
        }

        if (col.gameObject.tag == "SpeedBoost")
        {
            boosting = true;
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == "ShieldBoost")
        {
            shieldActive = true;
            Destroy(col.gameObject);
            shield.SetActive(true);
        }

        if (col.gameObject.layer == 13)
        {
            grounded = false;
        }

        if (col.gameObject.tag == "Respawn")
        {
            spawnPoint = col.gameObject;
        }
        if (col.gameObject.tag == "Finish")
        {
            globalTimer.SetRecord();
            finishScreen.SetActive(true);
            globalTimer.SetMedal();
            Time.timeScale = 0f;
        }
        if (col.gameObject.tag == "Key 1")
        {
            door1.SetActive(false);
        }
        if (col.gameObject.tag == "Key 2")
        {
            door2.SetActive(false);
        }
    }

    public void Damage(int damageCount)
    {
        damaged = true;
        if (shieldActive == false)
        {
            hp -= damageCount;

            healthSlider.value = hp;

            if (hp <= 0)
            {
                Death();
            }            
        }
        else
        {
            shieldActive = false;
            shield.SetActive(false);
        }
        
    }

    void Death()
    {
        if (isRace.activeSelf)
        {
            jetPrefab.transform.position = spawnPoint.transform.position;
            transform.rotation = spawnPoint.transform.rotation;
            rb.isKinematic = true;
            hp += 70;
            healthSlider.value = 70;
            return;
        }
        if (!isRace.activeSelf)
        {
            finishScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    //Shooting method
    void Shoot()
    {
        {            
            Rigidbody instantiatedProjectile = Instantiate(projectile, transform.position, Quaternion.identity) as Rigidbody;
            instantiatedProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, bulletSpeed));
        }

    }
    void ShootLight()
    {
        {
            lightShot = true;
            Rigidbody instantiatedLightProjectile = Instantiate(lightPro, transform.position, Quaternion.identity) as Rigidbody;
            instantiatedLightProjectile.velocity = transform.TransformDirection(new Vector3(0, 0, bulletSpeed));
        }

    }

}
