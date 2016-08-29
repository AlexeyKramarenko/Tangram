using UnityEngine;
using System.Collections;

public class Mouse : MonoBehaviour
{
    #region Variables

    private RaycastHit result;
    private float distance;
    private Vector3 posObj;
    private float angleFrom = 0;
    private float angleTo = 0;
    private float speed = 3f;

    private GameObject collideObj = null;
    public static bool bingo = false;

    #endregion Variables

    void Update()
    {
        if (Input.GetMouseButton(0) && !bingo)
        {
            SelectFigure();
        }
        else if (Input.GetKey(KeyCode.RightArrow) && collideObj)
        {
            RotateRight();
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && collideObj)
        {
            RotateLeft();
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetMouseButtonUp(0))
        {
            AngleFixing();
        }
        else if (collideObj && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow) && !Input.GetMouseButton(0))
        {
            SmoothMoveEnding();
        }
    }


    #region Methods
    void SelectFigure()
    {
        if (collideObj)
        {
            collideObj.GetComponent<CircleAdding>().DisableCircle();
            collideObj = null;
            angleTo = 0;
        }

        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hit = Physics2D.Raycast(ray.origin, ray.direction);

        if (hit && hit.collider.gameObject.layer == 9 && hit.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic == false)//если мы попали в объект и он разблокирован
        {
            collideObj = hit.collider.gameObject;
            distance = hit.distance;
        }

        else if (hit && hit.collider.gameObject.tag == "Board")
        {
            return;
        }
        else
        {
            return;
        }

        posObj = ray.origin + distance * ray.direction;

        angleFrom = collideObj.transform.eulerAngles.z;

        //ДОБАВЛЯЕМ КОЛЕСО ПРОКРУТКИ
        collideObj.GetComponent<CircleAdding>().EnableCircle();

        //меняем положение объекта, в который попали лучом по X и Y
        collideObj.transform.position = new Vector3(posObj.x, posObj.y, collideObj.transform.position.z);

        //для возможности при нажатии кнопки "Повернуть" поворота последней выбранной фигуры по оси Y
        ButtonScript.LastSelectedFigure = collideObj;

    }

    private void RotateLeft()
    {
        collideObj.transform.Rotate(new Vector3(0, 0, 90) * Time.smoothDeltaTime * speed);
        angleFrom = collideObj.transform.eulerAngles.z;
    }

    private void RotateRight()
    {
        collideObj.transform.Rotate(new Vector3(0, 0, -90) * Time.smoothDeltaTime * speed);
        angleFrom = collideObj.transform.eulerAngles.z;
    }

    private void AngleFixing()
    {
        if (angleFrom < -45 && angleFrom >= -135)
        {
            angleTo = -90;
        }
        else if (angleFrom < -135 && angleFrom >= -225)
        {
            angleTo = -180;
        }
        else if (angleFrom < -225 && angleFrom >= -315)
        {
            angleTo = -270;
        }
        else if ((angleFrom < -315 && angleFrom >= -360) || (angleFrom < 0 && angleFrom >= -45))
        {
            angleTo = 0;
        }
        else if (angleFrom > 45 && angleFrom <= 135)
        {
            angleTo = 90;
        }
        else if (angleFrom > 135 && angleFrom <= 225)
        {
            angleTo = 180;
        }
        else if (angleFrom > 225 && angleFrom <= 315)
        {
            angleTo = 270;
        }
        else if ((angleFrom > 315 && angleFrom <= 360) || (angleFrom > 0 && angleFrom <= 45))
        {
            angleTo = 0;
        }
    }

    void SmoothMoveEnding()
    {
        //ПЛАВНЫЙ ПЕРЕХОД В СТОРОНУ ВРАЩЕНИЯ
        Quaternion current = Quaternion.Euler(collideObj.transform.eulerAngles.x, collideObj.transform.eulerAngles.y, collideObj.transform.eulerAngles.z);
        Quaternion rotation = Quaternion.Euler(collideObj.transform.eulerAngles.x, collideObj.transform.eulerAngles.y, angleTo);
        collideObj.transform.rotation = Quaternion.Lerp(current, rotation, 15 * Time.deltaTime);

    }

    #endregion Methods
}
