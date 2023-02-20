using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    //Damage structure
    private int[] damagePoint = { 1, 1, 2, 2, 4, 3, 5, 10 };
    private float[] pushForce = { 1.0f, 2.0f, 2.2f, 100.0f, 1.5f, 2.5f, 3.5f, 2.0f };

    // Upgrade
    public int weaponLevel = 0;
    public SpriteRenderer spriteRenderer;

    // Swing
    public float cooldown = 0.5f;
    private Animator anim;
    private float lastSwing;
    private float swingVariety;

    protected override void Start() {
        base.Start();
        // Ova e za posle da moze da se upgrade oruzjeto pri upgrade od weapon_0 odi na weapon_1 itn. Smeni go ova!
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    protected override void Update() {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (Time.time - lastSwing > cooldown) {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    protected override void OnCollide(Collider2D coll) {
        if (coll.tag == "Fighter") {
            if (coll.name == "Player")
                return;

            // Create new damage object and send it to the fighter we've hit
            Damage dmg = new Damage
            {
                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel]
            };

            coll.SendMessage("ReceiveDamage", dmg);

            if (coll.name == "Boss")
                GameManager.instance.onBossHit();
        }
    }

    private void Swing() {
        swingVariety = Random.Range(0, 2);
        if ( swingVariety == 1)
            FindObjectOfType<AudioManager>().Play("sword-swipe-1");
        else
            FindObjectOfType<AudioManager>().Play("sword-swipe-2");

        anim.SetTrigger("Swing");
    }

    public void UpgradeWeapon()
    {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }

    public void SetWeaponLevel(int level)   
    {
        weaponLevel = level;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }
}