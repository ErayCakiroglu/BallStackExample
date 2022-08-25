using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GatesController : MonoBehaviour
{
    [SerializeField] TMP_Text gateNumberText;
    [SerializeField] enum GateType
    {
        PositiveGate,
        NegativeGate
    }

    [SerializeField] GateType gateType;
    [SerializeField] int gateNumber;
    void Start()
    {
        RandomGateNumber();
    }
/*Oyun içi oyun objesine rastgele sayı atan ve sayıyı 'text' olarak ekrana yazdıran metot.*/
    void RandomGateNumber()
    {
        switch (gateType)
        {
            case GateType.PositiveGate:
                gateNumber = Random.Range(1, 10);
                gateNumberText.text = gateNumber.ToString();
                break;
            case GateType.NegativeGate:
                gateNumber = Random.Range(-10, -1);
                gateNumberText.text = gateNumber.ToString();
                break;
        }
    }
/*RandomGateNumber metotuyla gelen rastgele sayıları başka kod dosyalarında kullanabilmemiz için dışarıya kullanıma açan metot.*/
    public int GetGateNumber()
    {
        return gateNumber;
    }
}
