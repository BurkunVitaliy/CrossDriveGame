
using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{

    public AudioClip crash;
    public AudioClip[] accelerates;
    public GameObject turnLeftSignal, turnRightLeft, explosion, exhaust ;
    public bool rightTurn, leftTurn, moveFromUp;
    public float speed = 15f, force = 50f;
    private Rigidbody carRb;
    private float originRotationY,rotateMultRiht = 6f, rotateMultLeft = 4.5f;
    private Camera mainCamera;
    public LayerMask carsLayer;
    private bool isMovingFast, carCrashed;
    [NonSerialized] public bool carPassed;
   
    [NonSerialized] public static bool isLose;

    [NonSerialized] public static int countCars;
    

    private void Start()
    {
        mainCamera = Camera.main;
        originRotationY = transform.eulerAngles.y;
        carRb = GetComponent<Rigidbody>();

        if (rightTurn)
        {
            StartCoroutine(TurnSignals(turnRightLeft));
        }
        else if (leftTurn)
        {
            StartCoroutine(TurnSignals(turnLeftSignal));
        }
    }

    IEnumerator TurnSignals(GameObject turnSignal)
    {
        while (!carPassed)
        {
            turnSignal.SetActive(!turnSignal.activeSelf);
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void FixedUpdate()
    {
        carRb.MovePosition(transform.position - transform.forward * speed * Time.fixedDeltaTime);
    }

    private void Update()
    {
#if UNITY_EDITOR
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
#else
        if (Input.touchCount == 0)
            return;
        
        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
#endif
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f,carsLayer ))
        {
            string carName = hit.transform.gameObject.name;
            
#if UNITY_EDITOR
            if (Input.GetMouseButtonDown(0) && !isMovingFast && gameObject.name == carName)
            {
#else
            if (Input.GetTouch(0).phase == TouchPhase.Began && !isMovingFast && gameObject.name == carName){
#endif

                GameObject vfx =  Instantiate(exhaust, new Vector3(transform.position.x,transform.position.y + 1.5f,
                    transform.position.z), Quaternion.Euler(90,0,0));
                Destroy(vfx,2f);
                speed *= 2;
                isMovingFast = true;
                if (PlayerPrefs.GetString("Music") != "No")
                {
                    GetComponent<AudioSource>().clip = accelerates[Random.Range(0, accelerates.Length)];
                    GetComponent<AudioSource>().Play();
                }
                    
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Car") && !carCrashed)
        {
            carCrashed = true;
            isLose = true;
            speed = 0f;
            other.gameObject.GetComponent<CarController>().speed = 0f;

            GameObject vfx = Instantiate(explosion, transform.position, Quaternion.identity);
           Destroy(vfx, 5f);
            
            if (isMovingFast)
            {
                force *= 1.2f;
            }
            carRb.AddRelativeForce(Vector3.back * force);
            
            if (PlayerPrefs.GetString("Music") != "No")
            {
                GetComponent<AudioSource>().clip = crash;
                GetComponent<AudioSource>().Play();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (carCrashed)
            return;
        if (other.transform.CompareTag("TurnBlock Right") && rightTurn)
        {
            RotateCar(rotateMultRiht);
        }
        else if (other.transform.CompareTag("TurnBlock Left") && leftTurn)
        {
            RotateCar(rotateMultLeft, -1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Car") && other.GetComponent<CarController>().carPassed)
            other.GetComponent<CarController>().speed = speed +5f;
    }

    private void OnTriggerExit(Collider other)
    {
        if (isLose)
            return;
        if (other.transform.CompareTag("TriggerPass"))
        {
            if (carPassed)
            {
                return;
            }
            carPassed = true;
            Collider[] coliders = GetComponents<BoxCollider>();
            foreach (Collider col in coliders)
                col.enabled = true;
            countCars++;
        }
        if (other.transform.CompareTag("TurnBlock Right") && rightTurn)
        {
            carRb.rotation = Quaternion.Euler(0,originRotationY + 90,0);
        }
        else if (other.transform.CompareTag("TurnBlock Left") && leftTurn)
        {
            carRb.rotation = Quaternion.Euler(0,originRotationY - 90,0);
        }
        else if (other.transform.CompareTag("Delete Trigger"))
        {
            
            Destroy(gameObject);
        }
    }

    private void RotateCar(float speedRotate, int dir = 1)
    {
        if (isLose)
            return;
        
        if (dir == -1 && transform.localRotation.eulerAngles.y < originRotationY -90f)
        {
            return;
        }

        if (dir == -1 && moveFromUp && transform.localRotation.eulerAngles.y >250f && transform.localRotation.eulerAngles.y < 270f)
        {
            return;
        }
        
        float rotateSpeed = speed * rotateMultRiht * dir;
        Quaternion deltaRotation = Quaternion.Euler(new Vector3(0,rotateSpeed,0)* Time.fixedDeltaTime);
        carRb.MoveRotation(carRb.rotation * deltaRotation);
    }
}
