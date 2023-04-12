using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiBikeController : MonoBehaviour
{
    [SerializeField] private enum groundCheck { rayCast, sphereCaste };
    [SerializeField] private enum MovementMode { Velocity, AngularVelocity };
    [SerializeField] private MovementMode movementMode;
    [SerializeField] private groundCheck GroundCheck;
    [SerializeField] private LayerMask drivableSurface;

    [SerializeField] private float _maxSpeed, accelaration, turn;
    [SerializeField] private Rigidbody rbSphere, carBody;

    [HideInInspector]
    [SerializeField] private RaycastHit hit;
    [SerializeField] private AnimationCurve frictionCurve;
    [SerializeField] private AnimationCurve turnCurve;
    [SerializeField] private PhysicMaterial frictionMaterial;
    [Header("Visuals")]
    [SerializeField] private Transform BodyMesh;
    [SerializeField] private Transform[] FrontWheels = new Transform[1];
    [SerializeField] private Transform[] RearWheels = new Transform[1];
    [HideInInspector]
    [SerializeField] private Vector3 carVelocity;
    [SerializeField] private GameObject ExplodeEffect;
    [Range(0, 10)]
    [SerializeField] private float BodyTilt;
    [Header("Audio settings")]
    [SerializeField] private AudioSource engineSound;
    [Range(0, 1)]
    [SerializeField] private float minPitch;
    [Range(1, 3)]
    [SerializeField] private float MaxPitch;
    [SerializeField] private AudioSource SkidSound;

    [SerializeField] private float skidWidth;

    private float radius;
    private Vector3 origin;

    //ai
    [Header("AI settings")]
    [SerializeField] private Transform target;

    //Ai stuff
    [SerializeField] private AIBikeLineChanger _aiBikeLineChanger;
    [SerializeField] private float _turnAI = 1f;
    [SerializeField] private float SpeedAI = 1f;
    [SerializeField] private float brakeAI = 0f;
    [SerializeField] private float brakeAngle = 30f;

    private float desiredTurning;
    //public GameObject Sphere,body, wheel1, wheel2;
    public bool Rot;

    private float _changedSpeed;
    private bool _isSpeedChange;

    public float SkidWidth => skidWidth;
    public Vector3 CarVelocity => carVelocity;
    public float TurnAI => _turnAI;
    public float MaxSpeed=> _maxSpeed;

    private void OnValidate()
    {
        radius = rbSphere.gameObject.GetComponent<SphereCollider>().radius;
    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    private void Start()
    {
        carBody.inertiaTensor = Vector3.up;
        if (movementMode == MovementMode.AngularVelocity)
        {
            Physics.defaultMaxAngularSpeed = 100;
        }
    }
    private void Update()
    {
        //
        if (_isSpeedChange)
        {
            _maxSpeed = Mathf.MoveTowards(_maxSpeed, _changedSpeed, Time.deltaTime * 570);

            if (_maxSpeed == _changedSpeed)
            {
                _isSpeedChange = false;
            }
        }
        //

        Visuals();
        AudioManager();

        //
        // the new method of calculating turn value
        Vector3 aimedPoint = target.position;
        aimedPoint.y = transform.position.y;
        Vector3 aimedDir = (aimedPoint - transform.position).normalized;
        Vector3 myDir = transform.forward;
        myDir.Normalize();
        desiredTurning = Mathf.Abs(Vector3.Angle(myDir, Vector3.ProjectOnPlane(aimedDir, transform.up)));
        //

        float reachedTargetDistance = 1f;
        float distanceToTarget = Vector3.Distance(transform.position, target.position);
        Vector3 dirToMovePosition = (target.position - transform.position).normalized;
        float dot = Vector3.Dot(transform.forward, dirToMovePosition);
        float angleToMove = Vector3.Angle(transform.forward, dirToMovePosition);
        if (angleToMove > brakeAngle)
        {
            if (carVelocity.z > 15)
            {
                brakeAI = 1;
            }
            else
            {
                brakeAI = 0;
            }

        }
        else { brakeAI = 0; }

        if (distanceToTarget > reachedTargetDistance)
        {

            if (dot > 0)
            {
                SpeedAI = 1f;

                float stoppingDistance = 5f;
                if (distanceToTarget < stoppingDistance)
                {
                    brakeAI = 1f;
                }
                else
                {
                    brakeAI = 0f;
                }
            }
            else
            {
                float reverseDistance = 5f;
                if (distanceToTarget > reverseDistance)
                {
                    SpeedAI = 1f;
                }
                else
                {
                    brakeAI = -1f;
                }
            }

            float angleToDir = Vector3.SignedAngle(transform.forward, dirToMovePosition, Vector3.up);

            if (angleToDir > 0)
            {
                _turnAI = 1f * turnCurve.Evaluate(desiredTurning / 90);
            }
            else
            {
                _turnAI = -1f * turnCurve.Evaluate(desiredTurning / 90);
            }

        }
        else
        {
            if (carVelocity.z > 1f)
            {
                brakeAI = -1f;
            }
            else
            {
                brakeAI = 0f;
            }
            _turnAI = 0f;
        }


    }

    private void FixedUpdate()
    {

        carVelocity = carBody.transform.InverseTransformDirection(carBody.velocity);

        if (Mathf.Abs(carVelocity.x) > 0)
        {
            //changes friction according to sideways speed of car
            frictionMaterial.dynamicFriction = frictionCurve.Evaluate(Mathf.Abs(carVelocity.x / 100));
        }


        if (grounded())
        {
            //turnlogic
            float sign = Mathf.Sign(carVelocity.z);
            float TurnMultiplyer = turnCurve.Evaluate(carVelocity.magnitude / _maxSpeed);
            if (SpeedAI > 0.1f || carVelocity.z > 1)
            {
                carBody.AddTorque(Vector3.up * _turnAI * sign * turn * 100 * TurnMultiplyer);
            }
            else if (SpeedAI < -0.1f || carVelocity.z < -1)
            {
                carBody.AddTorque(Vector3.up * _turnAI * sign * turn * 100 * TurnMultiplyer);
            }

            //brakelogic
            if (brakeAI > 0.1f)
            {
                rbSphere.constraints = RigidbodyConstraints.FreezeRotationX;
            }
            else
            {
                rbSphere.constraints = RigidbodyConstraints.None;
            }

            //accelaration logic

            if (movementMode == MovementMode.AngularVelocity)
            {
                if (Mathf.Abs(SpeedAI) > 0.1f)
                {
                    rbSphere.angularVelocity = Vector3.Lerp(rbSphere.angularVelocity, carBody.transform.right * SpeedAI * _maxSpeed / radius, accelaration * Time.deltaTime);
                }
            }
            else if (movementMode == MovementMode.Velocity)
            {
                if (Mathf.Abs(SpeedAI) > 0.1f && brakeAI < 0.1f)
                {
                    rbSphere.velocity = Vector3.Lerp(rbSphere.velocity, carBody.transform.forward * SpeedAI * _maxSpeed, accelaration / 10 * Time.deltaTime);
                }
            }

            //body tilt
            carBody.MoveRotation(Quaternion.Slerp(carBody.rotation, Quaternion.FromToRotation(carBody.transform.up, hit.normal) * carBody.transform.rotation, 0.12f));
        }
        else
        {
            carBody.MoveRotation(Quaternion.Slerp(carBody.rotation, Quaternion.FromToRotation(carBody.transform.up, Vector3.up) * carBody.transform.rotation, 0.02f));
        }

    }

    public void ChangeSpeed(float speed, bool isFast = true)
    {
        if (isFast)
        {
            _maxSpeed = speed;
        }
        else
        {
            _changedSpeed = speed;
            _isSpeedChange = true;
        }
    }

    public bool grounded() //checks for if vehicle is grounded or not
    {
        origin = rbSphere.position + radius * Vector3.up;
        var direction = -transform.up;
        var maxdistance = radius + 0.2f;

        if (GroundCheck == groundCheck.rayCast)
        {
            if (Physics.Raycast(rbSphere.position, Vector3.down, out hit, maxdistance, drivableSurface))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        else if (GroundCheck == groundCheck.sphereCaste)
        {
            if (Physics.SphereCast(origin, radius + 0.1f, direction, out hit, maxdistance, drivableSurface))
            {
                return true;

            }
            else
            {
                return false;
            }
        }
        else { return false; }
    }

    private void Visuals()
    {
        FrontWheels[0].localRotation = rbSphere.transform.localRotation;
        RearWheels[0].localRotation = rbSphere.transform.localRotation;
        BodyMesh.localRotation = Quaternion.Slerp(BodyMesh.localRotation, Quaternion.Euler(BodyMesh.localRotation.eulerAngles.x, BodyMesh.localRotation.eulerAngles.y, -30 * _turnAI), 0.1f);
    }

    private void AudioManager()
    {
        engineSound.pitch = Mathf.Lerp(minPitch, MaxPitch, Mathf.Abs(carVelocity.z) / _maxSpeed);
        if (Mathf.Abs(carVelocity.x) > 10 && grounded())
        {
            SkidSound.mute = false;
        }
        else
        {
            SkidSound.mute = true;
        }
    }

    private void OnDrawGizmos()
    {
        //debug gizmos
        float width = 0.02f;
        if (!Application.isPlaying)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(rbSphere.transform.position + ((radius + width) * Vector3.down), new Vector3(2 * radius, 2 * width, 4 * radius));
            if (GetComponent<BoxCollider>())
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(transform.position, GetComponent<BoxCollider>().size);
            }

        }

    }
}
