using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject regularBulletPrefab;
    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip tripleShotSound;
    [SerializeField] private AudioClip missileSound;

    const int NUMBER_OF_BULLETS = 100;
    const int NUMBER_OF_MISSILES = 100;
    const float TIME_BETWEEN_BULLETS = 0.15f;
    const float TIME_BETWEEN_MISSILES = 1.0f;
    const int BULLET_SPEED = 100;

    private List<GameObject> bullets;
    private List<GameObject> missiles;

    private bool bulletMode;
    private bool missileMode;
    private bool tripleShotMode;
    private int nbOfMissiles;

    private float timeLeftBeforeCanShoot;
    private float timeLeftInTripleShotMode;

    private InputAction shootAction;
    private InputAction switchAmmoModeAction;
    private AudioSource audioSource;

    private void Awake()
    {
        bulletMode = true;
        missileMode = false;
        tripleShotMode = false;
        nbOfMissiles = 0;
        timeLeftBeforeCanShoot = 0;
        timeLeftInTripleShotMode = 0;
        bullets = new List<GameObject>();
        missiles = new List<GameObject>();
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        for (int i = 0; i < NUMBER_OF_BULLETS; i++)
        {
            GameObject newBullet = Instantiate(regularBulletPrefab, Vector3.zero, Quaternion.identity);
            newBullet.SetActive(false);
            bullets.Add(newBullet);
        }
        for (int i = 0; i < NUMBER_OF_MISSILES; i++)
        {
            GameObject newMissile = Instantiate(missilePrefab, Vector3.zero, Quaternion.identity);
            newMissile.SetActive(false);
            missiles.Add(newMissile);
        }
        shootAction = InputSystem.actions.FindAction("Shoot");
        switchAmmoModeAction = InputSystem.actions.FindAction("SwitchAmmo");
        gameManager.UpdateNbOfMissiles(nbOfMissiles);
        gameManager.UpdateTripleShotTimeRemaining(timeLeftInTripleShotMode);
    }

    void Update()
    {
        if (switchAmmoModeAction.WasPerformedThisFrame())
        {
            this.bulletMode = !this.bulletMode;
            this.missileMode = !this.missileMode;
            if (this.bulletMode)
            {
                timeLeftBeforeCanShoot = TIME_BETWEEN_BULLETS;
            }
            if (this.missileMode)
            {
                timeLeftBeforeCanShoot = TIME_BETWEEN_MISSILES;
            }
        }
        if (timeLeftInTripleShotMode > 0)
        {
            timeLeftInTripleShotMode -= Time.deltaTime;
            gameManager.UpdateTripleShotTimeRemaining(timeLeftInTripleShotMode);
        }
        else
        {
            tripleShotMode = false;
        }

        if (timeLeftBeforeCanShoot > 0)
        {
            timeLeftBeforeCanShoot -= Time.deltaTime;
        }

        if (shootAction.IsPressed() && timeLeftBeforeCanShoot <= 0)
        {
            if (bulletMode)
            {
                if (tripleShotMode)
                {
                    if (audioSource != null && tripleShotSound != null)
                    {
                        audioSource.PlayOneShot(tripleShotSound, 1.0f);
                    }

                    ShootProjectile(ProjectileType.BULLET, transform.forward);
                    ShootProjectile(ProjectileType.BULLET, transform.forward + transform.right * 0.5f);
                    ShootProjectile(ProjectileType.BULLET, transform.forward - transform.right * 0.5f);
                ShootProjectile(ProjectileType.BULLET, transform.root.forward);
                if (tripleShotMode)
                {
                    ShootProjectile(ProjectileType.BULLET, transform.root.forward * 0.5f + transform.root.right * 0.5f);
                    ShootProjectile(ProjectileType.BULLET, transform.root.forward * 0.5f - transform.root.right * 0.5f);
                }
                else
                {
                    if (audioSource != null && shootSound != null)
                    {
                        audioSource.PlayOneShot(shootSound, 1.0f);
                    }

                    ShootProjectile(ProjectileType.BULLET, transform.forward);
                }
                timeLeftBeforeCanShoot = TIME_BETWEEN_BULLETS;
            }
            else if (missileMode && nbOfMissiles > 0)
            {
                if (audioSource != null && missileSound != null)
                {
                    audioSource.PlayOneShot(missileSound, 1.0f);
                }

                ShootProjectile(ProjectileType.MISSILE, transform.forward);
                timeLeftBeforeCanShoot = TIME_BETWEEN_MISSILES;
                gameManager.UpdateNbOfMissiles(nbOfMissiles);
            }
        }
    }

    private GameObject GetAvailableBullets()
    {
        foreach (GameObject bullet in bullets)
        {
            if (!bullet.activeSelf)
            {
                return bullet;
            }
        }
        GameObject newBullet = Instantiate(regularBulletPrefab, Vector3.zero, Quaternion.identity);
        newBullet.SetActive(true);
        bullets.Add(newBullet);
        return newBullet;
    }

    private GameObject GetAvailableMissile()
    {
        foreach (GameObject missile in missiles)
        {
            if (!missile.activeSelf)
            {
                return missile;
            }
        }
        GameObject newMissile = Instantiate(missilePrefab, Vector3.zero, Quaternion.identity);
        newMissile.SetActive(true);
        missiles.Add(newMissile);
        return newMissile;
    }

    public void GainMissiles(int missilesGained)
    {
        nbOfMissiles += missilesGained;
        gameManager.UpdateNbOfMissiles(nbOfMissiles);
    }

    public void ActivateTripleShotMode(float amountOfTime)
    {
        tripleShotMode = true;
        timeLeftInTripleShotMode = amountOfTime;
    }

    public void ShootProjectile(ProjectileType type, Vector3 direction)
    {
        if(type == ProjectileType.BULLET)
        {
            GameObject bullet = GetAvailableBullets();
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);
            bullet.GetComponent<Rigidbody>().AddForce(direction * BULLET_SPEED, ForceMode.Impulse);
        }
        else if(type == ProjectileType.MISSILE)
        {
            GameObject missile = GetAvailableMissile();
            missile.transform.position = transform.position;
            missile.transform.rotation = transform.rotation;
            missile.SetActive(true);
            missile.GetComponent<Rigidbody>().AddForce(direction * BULLET_SPEED, ForceMode.Impulse);
            nbOfMissiles--;
        }
    }
}