using System.Collections;
using UnityEngine;
using TMPro;
public class Gun : MonoBehaviour
{
    private float rotateOffset = 180f;
    [SerializeField] private Transform firePos;
    [SerializeField] private GameObject bulletPrefabs;
    [SerializeField] private float shotDelay = 0.15f;
    private float nextShot;
    [SerializeField] private int maxAmmo = 9;
    public int currentAmmo;
    [SerializeField] private float reloadDelay = 1.5f;
    [SerializeField] private TextMeshProUGUI ammoText;
    void Start()
    {
        currentAmmo = maxAmmo;
        UpdateAmmoText();
    }

    void Update()
    {
        RotateGun();
        Shoot();
        if (Input.GetMouseButtonDown(1)  && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload());
        }
        
    }
    void RotateGun()
    {
        if (Input.mousePosition.x < 0 || Input.mousePosition.x > Screen.width || Input.mousePosition.y < 0 || Input.mousePosition.y > Screen.height)
        {
            return;
        }

       // vector 3 : khoang cach tu Gun den con tro : pos Gun - pos Mouse
        Vector3 displacement = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg; // goc xoay
        transform.rotation=Quaternion.Euler(0, 0, angle + rotateOffset); // xoay Gun quanh truc 1 goc angle
        
        if (angle < -90 || angle > 90) // neu goc xoay.... flip Gun
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, -1, 1);
        }
    }

    void Shoot()
    {
        if (Input.GetMouseButtonDown(0) && currentAmmo > 0 && Time.time > nextShot)
        {
            nextShot = Time.time + shotDelay;
            Instantiate(bulletPrefabs, firePos.position, firePos.rotation);
            currentAmmo--;
        }   
        UpdateAmmoText();
    }
    IEnumerator Reload()
    {
        
        yield return new WaitForSeconds(reloadDelay);
        currentAmmo = maxAmmo;
        UpdateAmmoText();
    }
    private void UpdateAmmoText()
    {
        if(ammoText != null)
        {
            if(currentAmmo > 0)
            {
                ammoText.text = currentAmmo.ToString();
            }
            else
            {
                ammoText.text = "Empty";
            }
        }
    }

}
