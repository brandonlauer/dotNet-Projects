using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform Target;
    public float damping = 1;
    public float lookAheadFactor = 3;
    public float lookAheadReturnSpeed = 0.5f;
    public float lookAheadMoveThreshold = 0.1f;
    public Transform CameraBounds;

    private float m_OffsetZ;
    private Vector3 m_LastTargetPosition;
    private Vector3 m_CurrentVelocity;
    private Vector3 m_LookAheadPos;
    private float minX, maxX, minY, maxY;

    private void Awake()
    {
        if(Target != null)
        {
            m_LastTargetPosition = Target.position;
            m_OffsetZ = (transform.position - Target.position).z;
            transform.parent = null;
        }
        if(CameraBounds == null)
        {
            Debug.LogError("No camera bounds assigned to camera!");
        }

        var vertExtent = Camera.main.orthographicSize;
        var horzExtent = vertExtent * Screen.width / Screen.height;
        minX = CameraBounds.Find("Left").position.x + horzExtent;
        maxX = CameraBounds.Find("Right").position.x - horzExtent;
        minY = CameraBounds.Find("Bottom").position.y + vertExtent;
        maxY = CameraBounds.Find("Top").position.y - vertExtent;
    }

    public void SetTarget(Transform target)
    {
        Target = target;
        m_LastTargetPosition = target.position;
        m_OffsetZ = (transform.position - target.position).z;
        transform.parent = null;
    }


    // Update is called once per frame
    private void Update()
    {
        if (Target == null)
        {
            return;
        }
        // only update lookahead pos if accelerating or changed direction
        float xMoveDelta = (Target.position - m_LastTargetPosition).x;

        bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

        if (updateLookAheadTarget)
        {
            m_LookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
        }
        else
        {
            m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
        }

        Vector3 aheadTargetPos = Target.position + m_LookAheadPos + Vector3.forward * m_OffsetZ;
        Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

        transform.position = newPos;

        var v3 = transform.position;
        v3.x = Mathf.Clamp(v3.x, minX, maxX);
        v3.y = Mathf.Clamp(v3.y, minY, maxY);
        transform.position = v3;

        m_LastTargetPosition = Target.position;
    }

    private void LateUpdate()
    {
        
    }
}
