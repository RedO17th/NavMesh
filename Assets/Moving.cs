using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class Moving : MonoBehaviour
{
    public Camera camera;
    public NavMeshAgent navMesh;
    private bool MoveAcrossNavMeshesStarted = true;

    private void Start()
    {
        //navMesh.updateRotation = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
            {
                navMesh.destination = hit.point;
            }
        }
        if (navMesh.isOnOffMeshLink && !MoveAcrossNavMeshesStarted)
        {
            StartCoroutine(MoveAcrossNavMeshLink());
            MoveAcrossNavMeshesStarted = true;
        }
            //if (Input.GetMouseButtonDown(0))
            //{
            //    Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            //    if (Physics.Raycast(ray, out RaycastHit hit))
            //    {
            //        navMesh.SetDestination(hit.point);
            //    }
            //}
            ////if (navMesh.remainingDistance > navMesh.stoppingDistance)
            ////{
            ////    tpCharacter.Move(navMesh.desiredVelocity, false, false);
            ////}
            ////else 
            ////{
            ////    tpCharacter.Move(Vector3.zero, false, false);
            ////}
        }
    IEnumerator MoveAcrossNavMeshLink()
    {
        OffMeshLinkData data = navMesh.currentOffMeshLinkData;
        navMesh.updateRotation = false;

        Vector3 startPos = navMesh.transform.position;
        Vector3 endPos = data.endPos + Vector3.up * navMesh.baseOffset;
        float duration = (endPos - startPos).magnitude / navMesh.velocity.magnitude;
        float t = 0.0f;
        float tStep = .02f / duration;
        while (t < 1.0f)
        {
            transform.position = Vector3.Lerp(startPos, endPos, t);
            navMesh.destination = transform.position;
            t += tStep * Time.deltaTime;
            yield return null;
        }
        transform.position = endPos;
        navMesh.updateRotation = true;
        navMesh.CompleteOffMeshLink();
        MoveAcrossNavMeshesStarted = false;
    }
}
