using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSprintandCrouch : MonoBehaviour
{

    private PlayerMovement playerMovement;



    public float sprint_Speed = 10f;
    public float move_Speed = 5f;
    public float crouch_Speed = 2f;
    //stilli hraða á spring crouch og svo framveigis
    private Transform look_Root;
    private float stand_Height = 1.6f;
    private float crouch_Height = 1f;

    private bool is_Crouching;

    private PlayerFootsteps Player_Footsteps;

    private float sprint_Volume = 1f;
    private float crouch_Volume = 0.1f;
    private float walk_Volume_Min = 0.2f, walk_Volume_Max = 0.6f;

    private float walk_step_Distance = 0.4f;
    private float sprint_Step_Distance = 0.25f;
    private float crouch_Step_Distace = 0.5f;



    // Start is called before the first frame update
    void Awake()
    {

        playerMovement = GetComponent<PlayerMovement>();

        look_Root = transform.GetChild(0);

        Player_Footsteps = GetComponentInChildren<PlayerFootsteps>();

        // næ í fista child aka Look Root
    }
    void Start()
    {
        Player_Footsteps.volume_Min = walk_Volume_Min;
        Player_Footsteps.volume_Max = walk_Volume_Max;
        Player_Footsteps.step_Distance = walk_step_Distance;
    }

    // Update is called once per frame
    void Update()
    {
        Sprint();
        Crouch();
        
    }


    void Sprint()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift) && !is_Crouching)
        {
            playerMovement.speed = sprint_Speed;

            Player_Footsteps.step_Distance = sprint_Step_Distance;
            Player_Footsteps.volume_Min = sprint_Volume;
            Player_Footsteps.volume_Max = sprint_Volume;
        }

        if(Input.GetKeyUp(KeyCode.LeftShift) && !is_Crouching)
        {
            playerMovement.speed = move_Speed;

            Player_Footsteps.step_Distance = walk_step_Distance;
            Player_Footsteps.volume_Min = walk_Volume_Min;
            Player_Footsteps.volume_Max = walk_Volume_Max;
            
        }


    } //sprint


    void Crouch()
    {

        if(Input.GetKeyDown(KeyCode.C))
        {
            // ef við erum að að beja okkur standa upp
            if(is_Crouching)
            {
                look_Root.localPosition = new Vector3(0f, stand_Height, 0f);
                playerMovement.speed = move_Speed;

                Player_Footsteps.step_Distance = walk_step_Distance;
                Player_Footsteps.volume_Min = walk_Volume_Min;
                Player_Footsteps.volume_Max = walk_Volume_Max;


                is_Crouching = false;
            }
            else
            {
                look_Root.localPosition = new Vector3(0f, crouch_Height, 0f);
                playerMovement.speed = crouch_Speed;

                Player_Footsteps.step_Distance = crouch_Step_Distace;
                Player_Footsteps.volume_Min = crouch_Volume;
                Player_Footsteps.volume_Max = crouch_Volume;

                is_Crouching = true;

                // ef við erum ekki að að beja okkur þá beigum við okkur
            }

        } // ef við ítum á C



    } // Crouch



















































}
