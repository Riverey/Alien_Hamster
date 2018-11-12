using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class MovableBlock : MonoBehaviour
{
    private Rigidbody rigid;
    private BoxCollider collider;
    
    public GameObject dangerBlock;

    protected GameObject buildingModel;

    public enum MovingDirection { UpDown, LeftRight };
    public MovingDirection movingDirection;
    protected MovingDirection oldDirection = MovingDirection.UpDown;

    protected Vector3 tilt;

    public bool isActive = false;

    [Range(1, 4)]
    public float speed = 1.0f; // speed of the block
    public Vector2 blockSize = new Vector2(1.0f, 1.0f);
    public float dangerVelocity = 0.0f;

    [Header("Sounds")]
    public AudioSource impactSound;
    public AudioSource movingSound;
    public GameObject impactEffect;
    public GameObject colliderOne;
    public GameObject colliderTwo;

    private void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody>();
        rigid.useGravity = false;
        rigid.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
        switch (movingDirection)
        {
            case MovingDirection.UpDown:
                rigid.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
                break;
            case MovingDirection.LeftRight:
                rigid.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
                break;
        }

        collider = gameObject.GetComponent<BoxCollider>();
    }

    private void Update()
    {
        if (isActive)
            tilt = TiltControl.globalTilt * speed; // rotate the tilt direction;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.isStatic)
        {
            switch (movingDirection)
            {
                case MovingDirection.UpDown:
                    if (Mathf.Abs(collision.relativeVelocity.z) > 1.0f)
                        Impact(collision);
                    break;
                case MovingDirection.LeftRight:
                    if (Mathf.Abs(collision.relativeVelocity.x) > 1.0f)
                        Impact(collision);
                    break;
            }
        }
    }

    private void Impact (Collision collision)
    {
        impactSound.Play();
        CameraControl.ShakeRequest(0.1f, 0.2f);
        GameObject impactEffectLoaded = null;

        switch (movingDirection)
        {
            case MovingDirection.UpDown:
                if (collision.relativeVelocity.z < 0.0f)
                {
                    impactEffectLoaded = Instantiate(impactEffect, colliderOne.transform);
                    impactEffectLoaded.transform.position = Vector3.zero;
                }
                else
                {
                    Instantiate(impactEffect, colliderTwo.transform);
                    impactEffectLoaded.transform.position = Vector3.zero;
                }
                    break;
            case MovingDirection.LeftRight:
                if (collision.relativeVelocity.x < 0.0f)
                {
                    Instantiate(impactEffect, colliderOne.transform);
                    impactEffectLoaded.transform.position = Vector3.zero;
                }
                else
                {
                    Instantiate(impactEffect, colliderTwo.transform);
                    impactEffectLoaded.transform.position = Vector3.zero;
                }
                break;
        }
        
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            if (DebugManager.debugEnabled) Debug.DrawRay(transform.position + Vector3.up, tilt, Color.cyan);

            switch (movingDirection)
            {
                case MovingDirection.UpDown:
                    rigid.AddForce(new Vector3(0, 0, tilt.z));
                    break;
                case MovingDirection.LeftRight:
                    rigid.AddForce(new Vector3(tilt.x, 0, 0));
                    break;
            }
        }
        rigid.AddForce(rigid.velocity * -0.5f);

        if (dangerBlock != null && (Mathf.Abs(rigid.velocity.x) > dangerVelocity || Mathf.Abs(rigid.velocity.z) > dangerVelocity))
        {
            dangerBlock.SetActive(true);
            if(!movingSound.isPlaying)
                movingSound.Play();
        }
        else
        {
            dangerBlock.SetActive(false);
            if (movingSound.isPlaying)
                movingSound.Stop();
        }
    }

    void OnDrawGizmos()
    {
        if (movingDirection == MovingDirection.UpDown)
            Gizmos.DrawIcon(transform.position, "MB_UpDown.png", true);
        else if (movingDirection == MovingDirection.LeftRight)
            Gizmos.DrawIcon(transform.position, "MB_LeftRight.png", true);
    }
}

/*public class MovableBlockSettings : MonoBehaviour
{

    public void Start()
    {

        if (gameObject.GetComponent<Rigidbody>() == null)
            rigid = gameObject.AddComponent<Rigidbody>();
        else
            rigid = gameObject.GetComponent<Rigidbody>();

        rigid.useGravity = false;
        rigid.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;

        if (gameObject.GetComponent<BoxCollider>() == null)
        { collider = gameObject.AddComponent<BoxCollider>(); }
        else
        { collider = gameObject.GetComponent<BoxCollider>(); }

        tiltControl = FindObjectOfType<TiltControl>();

        if (dangerBlock == null)
        {
            dangerBlock = GameObject.CreatePrimitive(PrimitiveType.Cube);
            dangerBlock.transform.parent = gameObject.transform;
            dangerBlock.name = "Danger_block";
            DestroyImmediate(dangerBlock.GetComponent<BoxCollider>());
            dangerBlock.transform.localScale = new Vector3(blockSize.x + 0.1f, dangerBlock.transform.localScale.y, blockSize.y + 0.1f);
            dangerBlock.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        }

    }
    public void LateUpdate()
    {
        if (movingDirection != oldDirection)
        {
            if (movingDirection == MovingDirection.UpDown)
                rigid.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
            else if (movingDirection == MovingDirection.LeftRight)
                rigid.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
            else if (movingDirection == MovingDirection.BothDirections)
                rigid.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
            oldDirection = movingDirection;
        }
        if (new Vector3(blockSize.x, 1, blockSize.y) != collider.size)
        {
            collider.size = new Vector3(blockSize.x - 0.1f, 0.8f, blockSize.y - 0.1f);
            colliderSize = collider.size;
        }
        if (dangerBlock != null)
        {
            dangerBlock.transform.localScale = new Vector3(blockSize.x + 0.1f, dangerBlock.transform.localScale.y, blockSize.y + 0.1f);
        }
    }
}*/


