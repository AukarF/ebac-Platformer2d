using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TranslateGameObject : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }
}
