using UnityEngine.Assertions;
using UnityEngine;

//[RequireComponent(typeof(ProjectileComponent))]
public class ProjectileController : MonoBehaviour
{
    [SerializeField]
    private float m_fInputDeltaVal = 0.1f;

    private BallPhysics m_projectile = null;

    // Start is called before the first frame update
    void Start()
    {
        m_projectile = GetComponent<BallPhysics>();
        Assert.IsNotNull(m_projectile, "Houston, we've got a problem! ProjectileComponent is not attached!");
    }

    // Update is called once per frame
    void Update()
    {
        HandleUserInput();    
    }

    private void HandleUserInput()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            m_projectile.OnLaunchProjectile();
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            m_projectile.OnMoveForward(m_fInputDeltaVal);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            m_projectile.OnMoveBackward(m_fInputDeltaVal);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            m_projectile.OnMoveRight(m_fInputDeltaVal);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            m_projectile.OnMoveLeft(m_fInputDeltaVal);
        }

        if (Input.GetKey(KeyCode.Keypad8))
        {
            m_projectile.OnMoveUp(m_fInputDeltaVal);
        }

        if (Input.GetKey(KeyCode.Keypad2))
        {
            m_projectile.OnMoveDown(m_fInputDeltaVal);
        }
    }
}
