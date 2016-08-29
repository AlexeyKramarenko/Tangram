using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ButtonScript : MonoBehaviour, IPointerClickHandler
{
    public static GameObject LastSelectedFigure = null;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (LastSelectedFigure && !Mouse.bingo)
        {
            LastSelectedFigure.transform.eulerAngles = new Vector3(LastSelectedFigure.transform.eulerAngles.x, LastSelectedFigure.transform.eulerAngles.y + 180, LastSelectedFigure.transform.eulerAngles.z);
            LastSelectedFigure = null;
        }
    }
}
