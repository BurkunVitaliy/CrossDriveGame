using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{

    public GameObject[] maps;
    public bool isMainScene;
    public GameObject[] cars;
    public GameObject canvasLosePanel;
    public float timeToSpawnFrom = 2f, timeToSpawnTo = 4.5f;
    private int countCars;
    private Coroutine bottomCars, leftCars, rightCars, upCars;
    private bool isLoseOnce;
    public TMP_Text nowScore, topScore, coinsCount;
    public GameObject horn ;
    public AudioSource turnSignal;
    private void Start()
    {

        if (PlayerPrefs.GetInt("NowMap") == 2)
        {
            Destroy(maps[0]);
            maps[1].SetActive(true);
            Destroy(maps[2]);
        }
        else if (PlayerPrefs.GetInt("NowMap") == 3)
        {
            Destroy(maps[0]);
            Destroy(maps[1]);
            maps[2].SetActive(true);
        }
        else
        {
            maps[0].SetActive(true);
            Destroy(maps[1]);
            Destroy(maps[2]);
        }
        CarController.isLose = false;
        CarController.countCars = 0;

        if (isMainScene)
        {
            timeToSpawnFrom = 4f;
            timeToSpawnTo = 6f;
        }
        bottomCars = StartCoroutine(BottomCars());
        leftCars = StartCoroutine(LeftCars());
        rightCars = StartCoroutine(RightCars());
        upCars = StartCoroutine(UpCars());

        StartCoroutine(CreateHorn());

    }

    private void Update()
    {
        if (CarController.isLose && !isLoseOnce)
        {
           StopCoroutine(bottomCars);
           StopCoroutine(leftCars);
           StopCoroutine(rightCars);
           StopCoroutine(upCars);
           nowScore.text = "<color=#FDFF0E>SCORE:</color> " + CarController.countCars;
           if (PlayerPrefs.GetInt("Score") < CarController.countCars )
           {
               PlayerPrefs.SetInt("Score", CarController.countCars);
           }

           topScore.text = "<color=#FDFF0E>TOP:</color> " + PlayerPrefs.GetInt("Score");
           PlayerPrefs.SetInt("Coins",PlayerPrefs.GetInt("Coins") +  CarController.countCars);
           coinsCount.text = PlayerPrefs.GetInt("Coins").ToString();
           
           canvasLosePanel.SetActive(true);
           isLoseOnce = true;
        }
    }

    IEnumerator BottomCars()
    {
        while (true)
        {
            SpawnCar(new Vector3(-1.79f,-0.1f,-27.43f), 180f);
            float timeToSpawn = Random.Range(timeToSpawnFrom,timeToSpawnTo);
            yield return new WaitForSeconds(timeToSpawn);
        }
    }
    IEnumerator LeftCars()
    {
        while (true)
        {
            SpawnCar(new Vector3(-61.9f,-0.1f,2.6f), 270f);
            float timeToSpawn = Random.Range(timeToSpawnFrom,timeToSpawnTo);
            yield return new WaitForSeconds(timeToSpawn);
        }
    }IEnumerator RightCars()
    {
        while (true)
        {
            SpawnCar(new Vector3(23f,-0.1f,10.67f), 90f);
            float timeToSpawn = Random.Range(timeToSpawnFrom,timeToSpawnTo);
            yield return new WaitForSeconds(timeToSpawn);
        }
    }IEnumerator UpCars()
    {
        while (true)
        {
            SpawnCar(new Vector3(-8.08f,-0.1f,62.31f), 0f,true);
            float timeToSpawn = Random.Range(timeToSpawnFrom,timeToSpawnTo);
            yield return new WaitForSeconds(timeToSpawn);
        }
    }

    void SpawnCar(Vector3 pos, float rotationY, bool isMoveFromUp = false)
    {
        GameObject newObj = Instantiate(cars[Random.Range(0,cars.Length)],pos, Quaternion.Euler(0,rotationY,0));
        countCars++;
        newObj.name = "Car - " + countCars ;   
        
            int random = isMainScene ? 1 : Random.Range(1,6);
            if (isMainScene)
            {
                newObj.GetComponent<CarController>().speed = 10f;
            }
            switch (random)
            {
                case 1 :
                case 2 :
                    newObj.GetComponent<CarController>().rightTurn = true;
                    if (PlayerPrefs.GetString("Music") !=  "No" &&  !turnSignal.isPlaying)
                    {
                        turnSignal.Play();
                    }
                    Invoke("StopSound", 2f);
                    break;
                case 3 :
                case 4 :
                    newObj.GetComponent<CarController>().leftTurn = true;
                    if (PlayerPrefs.GetString("Music") !=  "No" &&  !turnSignal.isPlaying)
                    {
                        turnSignal.Play();
                    }
                    Invoke("StopSound", 2f);
                    if (isMoveFromUp)
                    {
                        newObj.GetComponent<CarController>().moveFromUp = true;
                    }
                    break;
                case 5 :
                    
                    break;
            }
    }

    void StopSound()
    {
        turnSignal.Stop();
    }
    IEnumerator CreateHorn()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(5, 9));
            if (PlayerPrefs.GetString("Music") !=  "No")
            {
                Instantiate(horn, Vector3.zero, Quaternion.identity);
            }
        }
    }
}
