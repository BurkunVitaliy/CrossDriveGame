using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class DeleteHorn : MonoBehaviour
{

    public float timeDelete = 2f;
    private void Start()
    {
        Destroy(gameObject,timeDelete);
    }
}
