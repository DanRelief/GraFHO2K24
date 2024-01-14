using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] public float runSpeed;
    private Rigidbody2D rig;

    private float initalSpeed;
    private bool _isRunning;
    private bool _IsCutting;
    public int hit;
    public GameObject retryScreen;

    private Vector2 _direction;


    public Vector2 direction
    {
        get { return _direction; }
        set { _direction = value; }
    }

    public bool isRunning
    {
        get { return _isRunning; }
        set { _isRunning = value; }
    }
    public bool IsCutting
    {
        get { return _IsCutting; }
        set { _IsCutting = value; }
    }




    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        initalSpeed = speed;
        hit = 0;
        retryScreen.SetActive(false);


    }

    private void Update()
    {
        OnInput();

        OnRun();

        OnCutting();
        
        // jogador morre aqui
        if(hit>=5)
        {
            retryScreen.SetActive(true);

            Destroy(gameObject);
        }
       
        
        

        if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            // Se estiver no Editor da Unity, interrompe a execução (útil durante o desenvolvimento)
            UnityEditor.EditorApplication.isPlaying = false;
        }
#endif
    }
    private void FixedUpdate()
    {
        OnMove();
    }

    #region Movement
    void OnCutting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            IsCutting = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            IsCutting = false;
        }
    }

    void OnInput()
    {
        _direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

    }
    void OnMove()
    {
        rig.MovePosition(rig.position + direction * speed * Time.fixedDeltaTime);

    }
    void OnRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = runSpeed;
            isRunning = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = initalSpeed;
            isRunning = false;
        }

    }

   

}


    #endregion


