using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Linq;

public class CanvasButttonScript : MonoBehaviour
{
    private float timer = 0;

    void Update()
    {
        if (TriggerScript.countFigsInPlace < 3)
        {
            timer += Time.deltaTime;
        }
        else
            GameObject.Find("imgCongratulations").GetComponentInChildren<Text>().text = string.Format("Пройдено за {0} сек", Mathf.Floor(timer));
    }

}
