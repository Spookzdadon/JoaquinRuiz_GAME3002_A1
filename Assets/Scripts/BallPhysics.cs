using UnityEngine.Assertions;
using UnityEngine;

public class BallPhysics : MonoBehaviour
{
    [SerializeField]
    private Vector3 m_vTargetPos;
    [SerializeField]
    private Vector3 m_vInitialVel;
    [SerializeField]
    private bool m_bDebugKickBall = false;

    private Rigidbody m_rb = null;
    private GameObject m_TargetDisplay = null;

    private bool m_bIsGrounded = true;

    private float m_fDistanceToTarget = 0f;

    private Vector3 vDebugHeading;

    // Start is called before the first frame update
    void Start()
    {
        m_rb = GetComponent<Rigidbody>();
        Assert.IsNotNull(m_rb, "Houston, we've got a problem here! No Rigidbody attached");

        CreateTargetDisplay();
        m_fDistanceToTarget = (m_TargetDisplay.transform.position - transform.position).magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_TargetDisplay != null && m_bIsGrounded)
        {
            m_TargetDisplay.transform.position = m_vTargetPos;
            vDebugHeading = m_vTargetPos - transform.position;
        }
        if (m_bDebugKickBall && m_bIsGrounded)
        {
            m_bDebugKickBall = false;
            OnKickBall();
        }

    }

    private void CreateTargetDisplay()
    {
        m_TargetDisplay = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        m_TargetDisplay.transform.position = Vector3.zero;
        m_TargetDisplay.transform.localScale = new Vector3(1.0f, 0.1f, 1.0f);
        m_TargetDisplay.transform.rotation = Quaternion.Euler(90f, 0f, 0f);

        m_TargetDisplay.GetComponent<Renderer>().material.color = Color.red;
        m_TargetDisplay.GetComponent<Collider>().enabled = false;
    }

    public void OnKickBall()
    {

        // H = Vi^2 * sin^2(theta) / 2g
        // R = 2Vi^2 * cos(theta) * sin(theta) / g

        // Vi = sqrt(2gh) / sin(tan^-1(4h/r))
        // theta = tan^-1(4h/r)

        // Vy = V * sin(theta)
        // Vz = V * cos(theta)

        float fMaxHeight = m_TargetDisplay.transform.position.y;
        float fRange = (m_fDistanceToTarget * 2);
        float fTheta = Mathf.Atan((4 * fMaxHeight) / (fRange));
       

        float fInitVelMag = Mathf.Sqrt((2 * Mathf.Abs(Physics.gravity.y) * fMaxHeight)) / Mathf.Sin(fTheta);

        Vector3 Direction = (m_TargetDisplay.transform.position - transform.position);
        Debug.Log(Direction.ToString("F3"));
        Direction.y = 0;
        Direction = Direction.normalized;

        m_vInitialVel.y = fInitVelMag * Mathf.Sin(fTheta);
        m_vInitialVel.x = fInitVelMag * Mathf.Cos(fTheta) * Direction.x;
        m_vInitialVel.z = fInitVelMag * Mathf.Cos(fTheta) * Direction.z;

        m_rb.velocity = m_vInitialVel;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + vDebugHeading, transform.position);
    }

    #region CALLBACKS
    public void OnLaunchProjectile()
    {

        float fMaxHeight = m_TargetDisplay.transform.position.y;
        float fRange = (m_fDistanceToTarget * 2);
        float fTheta = Mathf.Atan((4 * fMaxHeight) / (fRange));


        float fInitVelMag = Mathf.Sqrt((2 * Mathf.Abs(Physics.gravity.y) * fMaxHeight)) / Mathf.Sin(fTheta);

        // Gets the direction to the TargetDisplay/where we want to aim
        Vector3 direction = (m_TargetDisplay.transform.position - transform.position);
        Debug.Log(direction.ToString());
        // y component is 0 because this vector will not be used in calculating the vertical motion
        direction.y = 0;
        // Normalized to isolate the direction
        direction = direction.normalized;

        m_vInitialVel.x = fInitVelMag * Mathf.Cos(fTheta) * direction.x;
        m_vInitialVel.y = fInitVelMag * Mathf.Sin(fTheta);
        m_vInitialVel.z = fInitVelMag * Mathf.Cos(fTheta) * direction.z;

        m_rb.velocity = m_vInitialVel;
    }

    public void OnMoveForward(float fDelta)
    {
        m_vTargetPos.z += fDelta;
    }

    public void OnMoveBackward(float fDelta)
    {
        m_vTargetPos.z -= fDelta;
    }

    public void OnMoveRight(float fDelta)
    {
        m_vTargetPos.x += fDelta;
    }

    public void OnMoveLeft(float fDelta)
    {
        m_vTargetPos.x -= fDelta;
    }

    public void OnMoveUp(float fDelta)
    {
        m_vTargetPos.y += fDelta;
    }

    public void OnMoveDown(float fDelta)
    {
        m_vTargetPos.y -= fDelta;
    }
    #endregion
}
