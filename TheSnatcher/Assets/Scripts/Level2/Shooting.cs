using UnityEngine;
//Josh Castillo

public class Shooting : MonoBehaviour
{
    [SerializeField]
    public Transform firePoint;
    [SerializeField]
    public GameObject bullet;

    // Update is called once per frame
    void Update()
    {
        getMousePosition();
        
        if (Input.GetMouseButtonDown(0) && Player.player.GetMovement())
        {
            Shoot();
        }
    }

    public void Shoot()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }
    //gets the mouse postion and direction relative to the player
    public void getMousePosition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimDirection = (mousePosition - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, 0, angle);
        if(angle < -90 || angle > 90)
        {
            transform.localRotation = Quaternion.Euler(180, 0, -angle);
          
        }
    }
}
