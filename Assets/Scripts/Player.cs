using DigitalRuby.PyroParticles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed;
    public static float noiseLevel;
    public GameObject[] Prefabs;
    public static bool MouseLookToggle;
    public Image power;
    public Sprite[] powers;
    public int maxHealth=100;
    public int currentHealth;
    public HealthBar healthBar;
    public float maxEnergy = 50;
    public float currentEnergy;
    public EnergyBar energyBar;

    private GameObject currentPrefabObject;
    private FireBaseScript currentPrefabScript;
    private int currentPrefabIndex;
    

    private enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    private RotationAxes axes = RotationAxes.MouseXAndY;
    private float sensitivityX = 15F;
    private float sensitivityY = 15F;
    private float minimumX = -360F;
    private float maximumX = 360F;
    private float minimumY = -60F;
    private float maximumY = 60F;
    private float rotationX = 0F;
    private float rotationY = 0F;
    private Quaternion originalRotation;

    // Start is called before the first frame update
    void Start()
    {
        currentEnergy = maxEnergy;
        energyBar.SetMaxEnergy(maxEnergy);
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        originalRotation = transform.localRotation;
        MouseLookToggle = true;
        noiseLevel = 0;
        StartCoroutine("noiseControl");
    }

    private void UpdateEffect()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            int energy = 0;
            if (currentPrefabIndex == 0)
            {
                energy = 15;
            }else if (currentPrefabIndex == 1)
            {
                energy = 25;
            }
            else
            {
                energy = 45;
            }
            if (energyBar.GetEnergy() > energy) 
            {
                StartCurrent();
                ConsumeEnergy(energy); 
            }
            
        }
    }

    private void BeginEffect()
    {
        Vector3 pos;
        float yRot = transform.rotation.eulerAngles.y;
        Vector3 forwardY = Quaternion.Euler(0.0f, yRot, 0.0f) * Vector3.forward;
        Vector3 forward = transform.forward;
        Vector3 right = transform.right;
        Vector3 up = transform.up;
        Quaternion rotation = Quaternion.identity;
        currentPrefabObject = GameObject.Instantiate(Prefabs[currentPrefabIndex]);
        currentPrefabScript = currentPrefabObject.GetComponent<FireConstantBaseScript>();

        if (currentPrefabScript == null)
        {
            // temporary effect, like a fireball
            currentPrefabScript = currentPrefabObject.GetComponent<FireBaseScript>();
            if (currentPrefabScript.IsProjectile)
            {
                // set the start point near the player
                rotation = transform.rotation;
                pos = transform.position + forward + right + up;
            }
            else
            {
                // set the start point in front of the player a ways
                pos = transform.position + (forwardY * 10.0f);
            }
        }
        else
        {
            // set the start point in front of the player a ways, rotated the same way as the player
            pos = transform.position + (forwardY * 5.0f);
            rotation = transform.rotation;
            pos.y = 0.0f;
        }

        FireProjectileScript projectileScript = currentPrefabObject.GetComponentInChildren<FireProjectileScript>();
        if (projectileScript != null)
        {
            // make sure we don't collide with other fire layers
            projectileScript.ProjectileCollisionLayers &= (~UnityEngine.LayerMask.NameToLayer("FireLayer"));
        }

        currentPrefabObject.transform.position = pos;
        currentPrefabObject.transform.rotation = rotation;
    }

    public void StartCurrent()
    {
        StopCurrent();
        BeginEffect();
    }

    private void StopCurrent()
    {
        // if we are running a constant effect like wall of fire, stop it now
        if (currentPrefabScript != null && currentPrefabScript.Duration > 10000)
        {
            currentPrefabScript.Stop();
        }
        currentPrefabObject = null;
        currentPrefabScript = null;
    }

    private void UpdateMouseLook()
    {
        if (!MouseLookToggle)
        {
            return;
        }
        else if (axes == RotationAxes.MouseXAndY)
        {
            // Read the mouse input axis
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;

            rotationX = ClampAngle(rotationX, minimumX, maximumX);
            rotationY = ClampAngle(rotationY, minimumY, maximumY);

            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);

            transform.localRotation = originalRotation * xQuaternion * yQuaternion;
        }
        else if (axes == RotationAxes.MouseX)
        {
            rotationX += Input.GetAxis("Mouse X") * sensitivityX;
            rotationX = ClampAngle(rotationX, minimumX, maximumX);

            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
            transform.localRotation = originalRotation * xQuaternion;
        }
        else
        {
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
            rotationY = ClampAngle(rotationY, minimumY, maximumY);

            Quaternion yQuaternion = Quaternion.AngleAxis(-rotationY, Vector3.right);
            transform.localRotation = originalRotation * yQuaternion;
        }
    }

    private static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
        {
            angle += 360F;
        }
        if (angle > 360F)
        {
            angle -= 360F;
        }

        return Mathf.Clamp(angle, min, max);
    }

    public void NextPrefab()
    {
        currentPrefabIndex++;
        if (currentPrefabIndex == Prefabs.Length)
        {
            currentPrefabIndex = 0;
        }
        power.sprite = powers[currentPrefabIndex];
    }

    public void PreviousPrefab()
    {
        currentPrefabIndex--;
        if (currentPrefabIndex == -1)
        {
            currentPrefabIndex = Prefabs.Length - 1;
        }
        power.sprite = powers[currentPrefabIndex];
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManagerLogic.state != GameManagerLogic.State.pause)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                NextPrefab();
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                PreviousPrefab();
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                MouseLookToggle = !MouseLookToggle;
            }

            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                if (Input.GetKey(KeyCode.LeftShift) && energyBar.GetEnergy() > 0)
                {
                    transform.position = Vector3.Lerp(transform.position, transform.position + transform.forward, speed * 2 * Time.deltaTime);
                    ConsumeEnergy(.2f);
                    noiseLevel = 2;
                }
                else
                {
                    transform.position = Vector3.Lerp(transform.position, transform.position + transform.forward, speed * Time.deltaTime);
                    noiseLevel = 1;
                }
            }
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                if (Input.GetKey(KeyCode.LeftShift) && energyBar.GetEnergy() > 0)
                {
                    transform.position = Vector3.Lerp(transform.position, transform.position - transform.forward, speed * 2 * Time.deltaTime);
                    ConsumeEnergy(.2f);
                    noiseLevel = 2;
                }
                else
                {
                    transform.position = Vector3.Lerp(transform.position, transform.position - transform.forward, speed * Time.deltaTime);
                    noiseLevel = 1;
                }
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                if (Input.GetKey(KeyCode.LeftShift) && energyBar.GetEnergy() > 0)
                {
                    transform.position = Vector3.Lerp(transform.position, transform.position + transform.right, speed * 2 * Time.deltaTime);
                    ConsumeEnergy(.2f);
                    noiseLevel = 2;
                }
                else
                {
                    transform.position = Vector3.Lerp(transform.position, transform.position + transform.right, speed * Time.deltaTime);
                    noiseLevel = 1;
                }
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                if (Input.GetKey(KeyCode.LeftShift) && energyBar.GetEnergy() > 0)
                {
                    transform.position = Vector3.Lerp(transform.position, transform.position - transform.right, speed * 2 * Time.deltaTime);
                    ConsumeEnergy(.2f);
                    noiseLevel = 2;
                }
                else
                {
                    transform.position = Vector3.Lerp(transform.position, transform.position - transform.right, speed * Time.deltaTime);
                    noiseLevel = 1;
                }
            }
            if (!Input.GetKey(KeyCode.LeftShift))
            {
                if (currentEnergy < 50)
                    ReloadEnergy();
            }
            UpdateMouseLook();
            UpdateEffect();
        }
    }

    void ReloadEnergy()
    {
        currentEnergy += 0.2f;
        energyBar.SetEnergy(currentEnergy);
    }

    void ConsumeEnergy(float energy)
    {
        currentEnergy -= energy;
        energyBar.SetEnergy(currentEnergy);    
    }

    IEnumerator noiseControl()
    {
        while(true){
            if (!Input.anyKey)
                noiseLevel=0;
            yield return new WaitForSeconds(1);
        }
    }
}
