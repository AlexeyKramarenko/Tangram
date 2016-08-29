using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class CircleAdding : MonoBehaviour
{
    GameObject childCircle;
     
    public void EnableCircle()
    {
        if (childCircle)
            childCircle.GetComponent<Renderer>().enabled = true;
        else
        {
            childCircle = (GameObject)Instantiate(Resources.Load("Prefabs/Circle"));
            childCircle.transform.SetParent(this.transform);
            childCircle.transform.localPosition = Vector3.zero;
            childCircle.GetComponent<Renderer>().enabled = true;
        }
    }

    public void DisableCircle()
    {
        childCircle.GetComponent<Renderer>().enabled = false;
    }
 

}
