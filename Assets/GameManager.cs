using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    List<int> leftSide = new List<int>();
    List<int> rightSide = new List<int>();

    enum JumpDirection
    {
        Left = 0,
        Right,
        None
    };

    int currentIndex = 0;
    int upperIndex = 0;

    JumpDirection jumpDirection = JumpDirection.None;

    public Transform prefab;
    public Transform pig;

    public Transform camera;

    public float jumpTime = 1.0f;
    float timePassed = 0.0f;
    // Use this for initialization
    void Start()
    {
        leftSide.Add(0);
        leftSide.Add(2);
        leftSide.Add(1);
        leftSide.Add(0);

        rightSide.Add(0);
        rightSide.Add(1);
        rightSide.Add(1);
        rightSide.Add(0);

        print(prefab);

        for (int i = 0; i < leftSide.Count; ++i)
        {
            Instantiate(prefab, new Vector3(4, (upperIndex + 1) * 4, -1), Quaternion.identity);
            Instantiate(prefab, new Vector3(12, (upperIndex + 1) * 4, -1), Quaternion.identity);
            upperIndex++;
        }
    }

    void JumpLeft()
    {
        print("Jumping left");
        jumpDirection = JumpDirection.Left;
        timePassed = 0.0f;
    }

    void JumpRight()
    {
        print("Jumping right");
        jumpDirection = JumpDirection.Right;
        timePassed = 0.0f;
    }

    void UpdateCameraPosition()
    {
        Vector3 cameraPosition = camera.transform.position;
        cameraPosition.y = pig.transform.position.y + 6 * Time.deltaTime;;//+= 8f * Time.deltaTime;
        camera.transform.position = cameraPosition;
    }

    // Update is called once per frame
    void Update()
    {

        if (jumpDirection != JumpDirection.None)
        {
            print("Jump time: " + jumpTime + "Time passed: " + timePassed);
            timePassed += Time.deltaTime;// * jumpTime;
            float multiplier = jumpDirection == JumpDirection.Left ? -1 : 1;
            if(timePassed > jumpTime) {
                jumpDirection = JumpDirection.None;
                timePassed = jumpTime;
                //return;
            }
            float x = timePassed / jumpTime * 4;
            float d = 1.4f;
            float y = (-Mathf.Pow(x, 2f) + (d + 4f)*x) / d;
            x *= multiplier;
            Vector3 newPosition = new Vector3(x + 8, y, -1);
            pig.position = newPosition;
        }
        else
        {
            if (Input.GetKeyDown("left"))
            {
                currentIndex++;
                JumpLeft();
            }
            else if (Input.GetKeyDown("right"))
            {
                currentIndex++;
                JumpRight();
            }
        }
    }
}
