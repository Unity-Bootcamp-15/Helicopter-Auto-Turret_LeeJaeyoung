using UnityEngine;
using UnityEngine.SceneManagement;

public class Helicopter : MonoBehaviour
{
    public GameObject propeller1;
    public GameObject propeller2;
    bool isEngineOn;
    float powerMax = 2f;
    float power;

    float verticalVelocity = 0f;
    float gravity = 9.81f;
    bool isGrounded;
    void Update()
    {
        CheckGround();

        if(Input.GetKeyDown(KeyCode.R))
            isEngineOn = !isEngineOn;
        if(Input.GetKeyDown(KeyCode.P))
            SceneManager.LoadScene("AutoTurretScene");
        if(isEngineOn)
            Run();
        else
            ApplyGravity();

        RotatePropeller1(propeller1);
        RotatePropeller2(propeller2);
    }
    void CheckGround()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 0.5f);
        
        if (isGrounded && !isEngineOn)
        {
            verticalVelocity = 0;
        }
    }
    void ApplyGravity()
    {
        power = Mathf.Max(power - Time.deltaTime, 0);

        if (!isGrounded)
        {
            verticalVelocity -= gravity * Time.deltaTime;
            transform.Translate(Vector3.up * verticalVelocity * Time.deltaTime, Space.World);
        }
    }
    void RotatePropeller1(GameObject go)
    {
        go.transform.Rotate(Vector3.up * Time.deltaTime * power * 1000f);
    }
    void RotatePropeller2(GameObject go)
    {
        go.transform.Rotate(Vector3.forward * Time.deltaTime * power * 1000f);
    }
    void Run()
    {
        power = Mathf.Min(power + Time.deltaTime, powerMax);

        if(power == powerMax)
        {
            verticalVelocity = 0;
            if (Input.GetKey(KeyCode.W))
                transform.Translate(Vector3.up * Time.deltaTime);
            
            if (Input.GetKey(KeyCode.S))
                transform.Translate(Vector3.down * Time.deltaTime);

            if (Input.GetKey(KeyCode.A))
                transform.Rotate(Vector3.down * Time.deltaTime * 100f);
        
            if (Input.GetKey(KeyCode.D))
                transform.Rotate(Vector3.up * Time.deltaTime * 100f);
        }
    }
}
