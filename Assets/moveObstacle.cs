using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveObstacle : MonoBehaviour
{
    [SerializeField] private Vector3 startPos;
    [SerializeField] private Vector3 endPos;
    [SerializeField] private float speed = 1;

    private bool back = false;
    void Start()
    {
        startPos = transform.position;
        endPos = startPos - new Vector3(0, 0, 5.8f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!back) 
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, Time.deltaTime * speed);
            if(transform.position == endPos)
                back = true;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPos, Time.deltaTime * speed);
            if(transform.position == startPos)
                back = false;
        }   
    }
}
