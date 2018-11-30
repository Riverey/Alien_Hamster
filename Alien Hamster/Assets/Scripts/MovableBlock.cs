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

    public AudioSource alert;
    private bool alertIsAnoying = false;

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
        if (collision.gameObject.tag != "Player")
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

    private void Impact(Collision collision)
    {
        impactSound.Play();
        CameraControl.ShakeRequest(0.1f, 0.2f);

        if (collision.gameObject.name.Contains("Construction"))
            collision.gameObject.GetComponent<ConstructionBuilding>().DestroyTheBuilding();

        GameObject impactEffectLoaded = null;
        switch (movingDirection)
            {
                case MovingDirection.UpDown:
                    if (collision.relativeVelocity.z < 0.0f)
                    {
                        impactEffectLoaded = Instantiate(impactEffect, colliderOne.transform);
                        impactEffectLoaded.transform.localPosition = Vector3.zero;
                    }
                    else
                    {
                        impactEffectLoaded = Instantiate(impactEffect, colliderTwo.transform);
                        impactEffectLoaded.transform.localPosition = Vector3.zero;
                    }
                    break;
                case MovingDirection.LeftRight:
                    if (collision.relativeVelocity.x < 0.0f)
                    {
                        impactEffectLoaded = Instantiate(impactEffect, colliderOne.transform);
                        impactEffectLoaded.transform.localPosition = Vector3.zero;
                        impactEffectLoaded.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 90.0f, 0.0f));
                    }
                    else
                    {
                        impactEffectLoaded = Instantiate(impactEffect, colliderTwo.transform);
                        impactEffectLoaded.transform.localPosition = Vector3.zero;
                        impactEffectLoaded.transform.rotation = Quaternion.Euler(new Vector3(0.0f, 90.0f, 0.0f));
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
            if (!movingSound.isPlaying)
                movingSound.Play();
            if (!alert.isPlaying && !alertIsAnoying)
            {
                alert.Play();
                alertIsAnoying = true;
            }
        }
        else
        {
            dangerBlock.SetActive(false);
            if (movingSound.isPlaying)
            {
                movingSound.Stop();
                alertIsAnoying = false;
            }
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

