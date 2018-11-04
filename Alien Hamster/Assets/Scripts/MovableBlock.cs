using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(BoxCollider))]
public class MovableBlock : MovableBlockSettings
{
    private void Update()
    {
        if (isActive)
            tilt = Quaternion.Euler(90, 0, 0) * tiltControl.globalTilt * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.isStatic)
        {
            if (movingDirection == MovingDirection.UpDown)
            {
                if (Mathf.Abs(collision.relativeVelocity.z) > 1.0f)
                {
                    Impact(collision);
                }
            }
            else if (movingDirection == MovingDirection.LeftRight)
            {
                if (Mathf.Abs(collision.relativeVelocity.x) > 1.0f)
                {
                    Impact(collision);
                }
            }
            else if (movingDirection == MovingDirection.BothDirections)
            {
                if (Mathf.Abs(collision.relativeVelocity.x) > 1.0f || Mathf.Abs(collision.relativeVelocity.z) > 1.0f)
                {
                    Impact(collision);
                }
            }
        }
    }

    private void Impact (Collision collision)
    {
        impactSound.Play();
        GameObject impactEffectLoaded = GameObject.Instantiate(impactEffect, collision.transform);
        //impactEffectLoaded.transform.position = (new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z - 0.5f));
        cameraControl.ShakeRequest(0.1f, 0.2f);
    }

    private void FixedUpdate()
    {
        if (isActive)
        {
            Debug.DrawRay(transform.position, tiltControl.virtualTilt, Color.yellow);
            Debug.DrawRay(transform.position + Vector3.up, tilt, Color.cyan);
            if (movingDirection == MovingDirection.UpDown)
                rigid.AddForce(new Vector3(0, 0, tilt.z));
            else if (movingDirection == MovingDirection.LeftRight)
                rigid.AddForce(new Vector3(tilt.x, 0, 0));
            else if (movingDirection == MovingDirection.BothDirections)
                rigid.AddForce(new Vector3(tilt.x, 0, tilt.z));

            rigid.AddForce(rigid.velocity * -0.5f);
        }
        else
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
}

[ExecuteInEditMode]
public class MovableBlockSettings : MonoBehaviour
{
    protected CameraControl cameraControl;

    protected Rigidbody rigid;
    protected BoxCollider collider;
    protected Vector3 colliderSize;
    [Header("Sounds")]
    public AudioSource impactSound;
    public AudioSource movingSound;

    public GameObject impactEffect;

    public GameObject dangerBlock;
    public float dangerVelocity = 0.0f;

    protected TiltControl tiltControl;

    protected GameObject buildingModel;

    public enum MovingDirection { UpDown, LeftRight, BothDirections };
    public MovingDirection movingDirection;
    protected MovingDirection oldDirection = MovingDirection.UpDown;

    public Vector2 blockSize = new Vector2(1.0f, 1.0f);

    protected Vector3 tilt;

    public bool isActive = false;

    [Range(1, 4)]
    public float speed = 1.0f;


    public void Start()
    {
        cameraControl = FindObjectOfType<CameraControl>();
        

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
    void OnDrawGizmos()
    {
        if (movingDirection == MovingDirection.UpDown)
            Gizmos.DrawIcon(transform.position, "MB_UpDown.png", true);
        else if (movingDirection == MovingDirection.LeftRight)
            Gizmos.DrawIcon(transform.position, "MB_LeftRight.png", true);
        else if (movingDirection == MovingDirection.BothDirections)
            Gizmos.DrawIcon(transform.position, "MB_BothDirections.png", true);
    }
}


