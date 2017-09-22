using UnityEngine;
using System.Collections;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class MouseOrbit : MonoBehaviour
{

    public Transform target;
    public float distance = 5.0f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;
    public float zSpeed = 20.0f;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = .5f;
    public float distanceMax = 15f;

    public GameObject CelestialCamera;

    private Rigidbody rigidbody;

    float x = 0.0f;
    float y = 0.0f;

    public Vector2 shakeLimits;
    public float shakeCurrent;
    public float shakeDuration;

    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        rigidbody = GetComponent<Rigidbody>();

        // Make the rigid body not change rotation
        if (rigidbody != null)
        {
            rigidbody.freezeRotation = true;
        }
    }

    public void doShake()
    {
        shakeCurrent = 1;
        print("shake");
    }

    void LateUpdate()
    {
        shakeCurrent = Mathf.MoveTowards(shakeCurrent, 0, shakeDuration);

        if (target)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            }

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * zSpeed, distanceMin, distanceMax);

            

            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;

            float shakeX = Random.Range(-shakeLimits.x, shakeLimits.x) * shakeCurrent;
            float shakeY = Random.Range(-shakeLimits.y, shakeLimits.y) * shakeCurrent;

            Vector3 shakeFinal = new Vector3(shakeX, shakeY, 0);

            transform.rotation =  Quaternion.Euler(shakeFinal + transform.rotation.eulerAngles);
        }
        else
        {
            if (GameManager.PlayerShip)
            {
                if (GameManager.PlayerShip.transform.Find("CamFocalPoint"))
                {
                    target = GameManager.PlayerShip.transform.Find("CamFocalPoint");
                }
            }
        }
    }

    private void Update()
    {

    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}