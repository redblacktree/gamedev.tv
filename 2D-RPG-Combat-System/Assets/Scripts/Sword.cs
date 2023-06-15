using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject slashAnimationPrefab;
    [SerializeField] private Transform slashAnimationSpawnPoint;

    private PlayerControls playerControls;
    private Animator anim;
    private PlayerController playerController;
    private ActiveWeapon activeWeapon;
    private GameObject slashAnimation;
    
    private void Awake()
    {
        playerControls = new PlayerControls();
        anim = GetComponent<Animator>();
        playerController = GetComponentInParent<PlayerController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    void Start()
    {
        playerControls.Combat.Attack.started += _ => Attack();
    }

    void Update()
    {
        MouseFollowWithOffset();
    }

    public void SwingUpFlipAnim() 
    {
        if (playerController.FacingLeft)
        {
            this.slashAnimation.transform.rotation = Quaternion.Euler(-180f, 180f, 0f);
        }
        else
        {
            this.slashAnimation.transform.rotation = Quaternion.Euler(-180f, 0f, 0f);
        }
    }

    public void SwingDownFlipAnim() 
    {
        if (playerController.FacingLeft)
        {
            this.slashAnimation.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else
        {
            this.slashAnimation.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }
    }

    private void Attack()
    {
        anim.SetTrigger("Attack");
        
        slashAnimation = Instantiate(slashAnimationPrefab, slashAnimationSpawnPoint.position, Quaternion.identity);
        slashAnimation.transform.parent = this.transform.parent;
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerPos = Camera.main.WorldToScreenPoint(transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x > playerPos.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
        else
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0f, -180f, angle);
        }
    }
}
