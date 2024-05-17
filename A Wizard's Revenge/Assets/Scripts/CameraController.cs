using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    
    public GameObject target;

  
    public float aheadDistance;

    
    public float slideInTime;

    public bool following;

    private Vector3 targetPos;

    //want to know our y starting location
    //want acces to the player controller
    private float yStart;
    private CharacterController player;

    //threshold will detect area of vert movement
    public float yThreshold;

    public float orthographicSize;


    public float yOffset;

    // Start is called before the first frame update
    void Start()
    {
        yStart = transform.position.y;
        player = FindObjectOfType<CharacterController>();
        following = true;

        Camera.main.orthographicSize = orthographicSize;

    }

    // Update is called once per frame
    void Update()
    {
        if (following)
        {
            //use a different pos if we happen
            //to jump off the vertical platform early
            //instead of riding it all the way down
            if (transform.position.y > yStart) //&& !player.onVert)
            {
                targetPos = new Vector3(target.transform.position.x, yStart, transform.position.z);
            }
            else
            {
                targetPos = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
            }

            if (target.transform.position.y > yThreshold || target.transform.position.y < (yThreshold * -1))
            {
                targetPos = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
            }


            if (target.transform.localScale.x > 0f)
            {
                targetPos = new Vector3(targetPos.x + aheadDistance, targetPos.y, targetPos.z);
            }
            else
            {
                //means we are facing left
                targetPos = new Vector3(targetPos.x - aheadDistance, targetPos.y, targetPos.z);
            }

            // add offset
            targetPos = new Vector3(targetPos.x, targetPos.y + yOffset, targetPos.z);

            //Lerp 3 args
            //  current vector3
            //  new vector3 to move to
            //  how much time should pass to get
            //  there
            transform.position = Vector3.Lerp(transform.position, targetPos, slideInTime * Time.deltaTime);

        }
    }
}
