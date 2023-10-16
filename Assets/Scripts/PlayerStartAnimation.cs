using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartAnimation : MonoBehaviour
{
    public float startTime = 2.0f;
    public Vector3 startMove = new Vector3(3, 0, 0);
    public float speed = 20;
    private Vector3 move = Vector3.zero;
    private PlayerController playerControllerScript;

    bool started = false;

    private void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();

        Invoke("MovePlayerInPlace", startTime);
    }

    void MovePlayerInPlace()
    {
        started = true;
    }

    void Update()
    {
        if (started)
        {
            float distanceOffset = startMove.magnitude - move.magnitude;
            Debug.Log("distance : " + distanceOffset);
            if (distanceOffset <= 0)
            {
                playerControllerScript.gameOver = false;
                Debug.Log("Destroy");
                Destroy(this);
            }
            else
            {
                Debug.Log("Translate");
                Vector3 deltaMove = Vector3.forward * speed * Time.deltaTime;
                transform.Translate(deltaMove);
                move += deltaMove;
            }
        }
    }
}
