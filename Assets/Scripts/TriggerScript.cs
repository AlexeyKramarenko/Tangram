using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TriggerScript : MonoBehaviour
{
    #region Variables
    private Animator finalSpriteAnim = null;
    private Animator congratulationAnim = null;

    public static int countFigsInPlace = 0;

    //список маленьких фигур занесенных к большой целевой фигуре
    static private List<GameObject> parts = new List<GameObject>();

    //конечные целевые координаты трех фигур
    readonly Vector3 Fig1_COORDS = new Vector3(0.63f, 2.85f, 0.5f);
    readonly Vector3 Fig2_COORDS = new Vector3(0.19f, 1.36f, 0.5f);
    readonly Vector3 Fig3_COORDS = new Vector3(0, -0.05f, 0.5f);

    #endregion Variables

    void Start()
    {
        finalSpriteAnim = GameObject.FindGameObjectWithTag("FinalSprite").GetComponent<Animator>();
        congratulationAnim = GameObject.FindGameObjectWithTag("Congratulations").GetComponent<Animator>();
    }

    void Update()
    {
        if (countFigsInPlace == 3)
        {
            FinishLevel();
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (Input.GetMouseButton(0) && !Mouse.bingo)
        {
            MoveFigureToPlace(col);
        }
    }


    #region Methods
    private void MoveFigureToPlace(Collider2D col)
    {
        float Z = col.gameObject.GetComponent<Transform>().eulerAngles.z;
        float Y = col.gameObject.GetComponent<Transform>().eulerAngles.y;

        if (this.tag == "Trigger1" && col.tag == "Fig1" && ((Y > -355 && Y < 5) || (Y > 175 && Y < 185))
            && Z <= 5f)
        {
            MoveFigureToTargetCoords(col, Fig1_COORDS);
        }
        else if (this.tag == "Trigger2" && col.tag == "Fig2" && (Y > -355 && Y < 5)
            && ((Z >= -5f && Z <= 5f) || (Z >= 175f && Z <= 185f)))
        {
            MoveFigureToTargetCoords(col, Fig2_COORDS);
        }
        else if (this.tag == "Trigger3" && col.tag == "Fig3" && (Y > -355 && Y < 5)
            && Z <= 5f)
        {
            MoveFigureToTargetCoords(col, Fig3_COORDS);
        }
    }

    private void MoveFigureToTargetCoords(Collider2D col, Vector3 constCoords)
    {
        col.gameObject.GetComponent<Transform>().eulerAngles = new Vector3(0, 0, 0);
        col.gameObject.GetComponent<Transform>().position = constCoords;
        AddingFigToPlace(col);
    }

    private void AddingFigToPlace(Collider2D col)
    {
        if (!parts.Contains(col.gameObject))
        {
            parts.Add(col.gameObject);
            countFigsInPlace++;
            col.gameObject.GetComponent<CircleAdding>().DisableCircle();
            col.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            col.gameObject.GetComponent<BoxCollider2D>().enabled = false;

            ButtonScript.LastSelectedFigure = null;
        }
    }

    private void FinishLevel()
    {
        Mouse.bingo = true;

        foreach (var obj in parts)
        {
            obj.GetComponent<SpriteRenderer>().color = new Color32(143, 143, 143, 255);
            obj.gameObject.GetComponent<CircleAdding>().DisableCircle();
        }

        //АНИМАЦИЯ
        StartCoroutine(AnimationsPlay());
    }

    //coroutine
    private IEnumerator AnimationsPlay()
    {
        finalSpriteAnim.SetTrigger("play");
        yield return new WaitForSeconds(1.5f);
        congratulationAnim.SetBool("playCanvasAnim", true);
    }
    
    #endregion Methods

}
