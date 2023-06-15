using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject slashAnimationPrefab;
    [SerializeField] private Transform slashAnimationSpawnPoint;
    [SerializeField] private Transform weaponCollider;

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

    public void SwingUpAnimEvent() 
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

    public void SwingDownAnimEvent()
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
        weaponCollider.gameObject.SetActive(true);
        
        slashAnimation = Instantiate(slashAnimationPrefab, slashAnimationSpawnPoint.position, Quaternion.identity);
        slashAnimation.transform.parent = this.transform.parent;
    }

    public void DoneAttackingAnimEvent()
    {
        weaponCollider.gameObject.SetActive(false);
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerPos = Camera.main.WorldToScreenPoint(transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x > playerPos.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0f, 0f, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0f, 0f, 0);
        }
        else
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0f, -180f, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0f, -180f, 0);
        }
    }
}
