using UnityEngine;

public class MovingObject : MonoBehaviour
{
    //want to know object we are  moving
    //  GameObject
    //want to know where to start
    //want to know where to end
    //want to knwo how fast to move
    //between start and end
    //want to be able to move from
    //start to end and then end to start
    //  Vector3
    public GameObject movingObject;
    public Transform startPoint;
    public Transform endPoint;
    public float moveSpeed;
    public Vector3 currentTargetPos;




    void Start()
    {
        //want to be moving towards
        //the end point always
        currentTargetPos = endPoint.position;

    }


    void Update()
    {
        //MoveTowards method
        //consisently move in the 
        //same direction at a constant speed
        //has three args
        //  current position
        //  target going towards
        //  how long should it take
        //  to get there
        //movingObject.transform.position = Vector3.MoveTowards(
            //movingObject.transform.position,
            //currentTargetPos,
            //moveSpeed * Time.deltaTime);

        movingObject.transform.position = Vector3.Lerp(movingObject.transform.position, currentTargetPos, moveSpeed * Time.deltaTime);

        //need to update our currentTargetPos
        if (Vector3.Distance(movingObject.transform.position, endPoint.position) < 0.01f)
        {
            currentTargetPos = startPoint.position;
            Debug.Log("changing to start");
        }

        if (Vector3.Distance(movingObject.transform.position, startPoint.position) < 0.01f)
        {
            currentTargetPos = endPoint.position;
            Debug.Log("changing to end");
        }

    }
}
