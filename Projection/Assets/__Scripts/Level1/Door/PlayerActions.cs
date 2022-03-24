using TMPro;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro UseText;
    [SerializeField]
    private Transform Camera;
    [SerializeField]
    private float MaxUseDistance = 10f;
    [SerializeField]
    private LayerMask UseLayers;

    public bool inCollider = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Doors")
        {
            inCollider = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Doors")
        {
            inCollider = false;
        }
    }

    //public void OnUse()
    //{
    //    if ((inCollider == true) || (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, MaxUseDistance, UseLayers)))
    //    {
    //        if (TryGetComponent<Door>(out Door door))
    //        {
    //            if (door.IsOpen)
    //            {
    //                door.Close();
    //            }
    //            else
    //            {
    //                door.Open(transform.position);
    //            }
    //        }
    //    }
    //}

    public void OnUse()
    {
        if ((inCollider == true))
        {
            if (TryGetComponent<Door>(out Door door))
            {
                if (door.IsOpen)
                {
                    door.Close();
                }
                else
                {
                    door.Open(transform.position);
                }
            }
        }
    }

    private void Update()
    {
        if ((inCollider == true) || (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, MaxUseDistance, UseLayers)
            && hit.collider.TryGetComponent<Door>(out Door door)))
        {
            //if (door.IsOpen)
            //{
            //    UseText.SetText("Close \"E\"");
            //}
            //else
            //{
            //    UseText.SetText("Open \"E\"");
            //}
            //UseText.gameObject.SetActive(true);
            //UseText.transform.position = hit.point - (hit.point - Camera.position).normalized * 0.0000000000001f;
            //UseText.transform.rotation = Quaternion.LookRotation((hit.point - Camera.position).normalized);
        }
        else
        {
            UseText.gameObject.SetActive(false);
        }
    }
}