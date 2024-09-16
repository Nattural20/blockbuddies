using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class GhostSpocksController : MonoBehaviour
{
    public Transform refPoint;
    public Transform player;
    public Transform ghostSpocks;

    public int maxYHeight = 3, minYHeight = 0, newYHeight;
    public float ghostDistanceMax = 7, ghostDistanceMin = 3, ghostDistance = 5, ghostMoveSpeed = 5;

    PlayerControls controls;

    private void Awake()
    {
        //controls = new PlayerControls();

        //ghostbusters starring bill murray except the stay puft marshmallow man has won cause idk what is not happening here
        //controls.Gameplay.GhostHeightIncrease.performed += ctx => IncreaseGhostHeight();
        //controls.Gameplay.GhostHeightDecrease.performed += ctx => IncreaseGhostHeight();

    }
    void FixedUpdate()
    {
        transform.position = player.position;
        //this.transform.position = new Vector3(refPoint.transform.position.x, player.position.y, refPoint.transform.position.z);
        this.transform.rotation = Quaternion.Euler(0, refPoint.transform.rotation.eulerAngles.y-90, 0);

        ghostSpocks.position = transform.position + transform.right * ghostDistance + new Vector3(0, newYHeight, 0);
        ghostSpocks.rotation = transform.rotation;

    }

    void Update() //< /\ > \/
    {

        if (Input.GetKeyDown(KeyCode.Joystick1Button4))
        {
            UnIncreaseGhostHeight();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            IncreaseGhostHeight();
        }
        if (Input.GetAxis("L2") > 0.1f) //Use a custom axis input for the triggers
        {
            SpiritedCloser();
        }
        if (Input.GetAxis("R2") > 0.1f) //Use a custom axis input for the triggers
        {
            SpiritedAway();
        }
    }
    void IncreaseGhostHeight() // paul blart mall cop
    {
        if (newYHeight < maxYHeight)
            newYHeight ++;
    }
    void UnIncreaseGhostHeight() //paul cop mall blart
    {
        if (newYHeight > minYHeight)
            newYHeight --;
    }
    void SpiritedAway() // mall blart cop plart blart
    {
        if (ghostDistance < ghostDistanceMax)
            ghostDistance += Time.deltaTime * ghostMoveSpeed;
    }
    void SpiritedCloser() // paul cop mlart cop blart paul mall cop blart paul
    //Ok
    {
        if (ghostDistance > ghostDistanceMin)
            ghostDistance -= Time.deltaTime * ghostMoveSpeed;
    }
}
