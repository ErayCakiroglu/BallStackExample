using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform ballTransform;
    [SerializeField] float lerpTime;
    Vector3 offset;
    void Start()
    {
        offset = transform.position - ballTransform.position;
    }
    /*Parametre olarak gönderilen Transform türünden olan ballTransform değişkenini takip etmemizi sağlayan kamera takip metodu.*/
    void LateUpdate()
    {
        Vector3 newPosition = Vector3.Lerp(transform.position, ballTransform.position + offset, lerpTime * Time.deltaTime);
        transform.position = newPosition;
    }
}
