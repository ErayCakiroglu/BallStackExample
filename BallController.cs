using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallController : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab, gameOver;
    [SerializeField] List<GameObject> balls = new List<GameObject>();
    [SerializeField] float horizontalSpeed, horizontalLimit, moveSpeed;
    [SerializeField] TMP_Text ballStackText, gameOverText;
    float horizontal;
    int gateNumber, targetCount, ballCount;
    
    void Update()
    {
        ballCount = balls.Count;
        HorizontalBallMove();
        ForwardBallMove();
        UpdateBallStackText();
    }
/* Oyun objesini Mouse'un sol tuşundan gelen veriye göre hareket ettirmeye yarayan metot.*/
    void HorizontalBallMove()
    {
        float xPositionNew;

        if(Input.GetMouseButton(0))
        {
            horizontal = Input.GetAxisRaw("Mouse X");
        }
        else
        {
            horizontal = 0;
        }

        xPositionNew = transform.position.x + horizontal * horizontalSpeed * Time.deltaTime;
        xPositionNew = Mathf.Clamp(xPositionNew, -horizontalLimit, horizontalLimit);

        transform.position = new Vector3(xPositionNew, transform.position.y, transform.position.z);
    }
/*Oyun objesine z ekseni doğrultusunda kuvvet uygulayan ve hareket ettiren metot.*/
    void ForwardBallMove()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
/*Toplamış olduğumuz oyun objelerini ekrana yazdıran metot.*/
    void UpdateBallStackText()
    {
        ballStackText.text = balls.Count.ToString();
    }
/*Oyun objesinin başka oyun objelerine temas ettiği zamanlarda gerçekleşecek olan 'yığınlama' işleminin gerçekleştiği metot.*/
    void OnTriggerEnter(Collider temas)
    {
        if(temas.gameObject.CompareTag("BallStack"))
        {
            temas.gameObject.transform.SetParent(transform);
            temas.gameObject.GetComponent<SphereCollider>().enabled = false;
            temas.gameObject.transform.localPosition = new Vector3(0f, 0f, balls[balls.Count - 1].transform.localPosition.z - 1f);
            balls.Add(temas.gameObject);
        }

        if(temas.gameObject.CompareTag("Gate"))
        {
            gateNumber = temas.gameObject.GetComponent<GatesController>().GetGateNumber();

            targetCount = ballCount + gateNumber;

            if(gateNumber > 0)
            {
                IncreaseBallCount();
            }
            
            if(gateNumber < 0)
            {
                if(Mathf.Abs(gateNumber) > balls.Count)
                {
                    Time.timeScale = 0;
                    gameOverText.text = "Oyun Bitti Kaybettiniz";
                    gameOver.SetActive(true);
                }
                DecreaseBallCount();
            }
        }
    }
/*Oyun objesinin oyun içindeki diğer oyun objeleriyle gerçekleştirdiği temas sonucu oluşacak olan arttırım işleminin gerçekleşeceği metot.*/
    void IncreaseBallCount()
    {
        for (int i = 0; i < gateNumber; i++)
        {
            GameObject ballNew = Instantiate(ballPrefab);
            ballNew.transform.SetParent(transform);
            ballNew.GetComponent<SphereCollider>().enabled = false;
            ballNew.transform.localPosition = new Vector3(0f, 0f, balls[balls.Count - 1].transform.localPosition.z - 1);
            balls.Add(ballNew);
        }
    }
/*Oyun objesinin oyun içindeki diğer oyun objeleriyle gerçekleştirdiği temas sonucu oluşacak olan azaltım işleminin gerçekleşeceği metot.*/
    void DecreaseBallCount()
    {
        for (int i = ballCount - 1; i >= targetCount; i--)
        {
            balls[i].SetActive(false);
            balls.RemoveAt(i);
        }
    }
}
