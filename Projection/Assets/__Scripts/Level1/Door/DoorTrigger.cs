using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField]
    public Door Door;

    public bool inCollider = false;

    private void OnTriggerEnter(Collider other)
    {

        inCollider = true;

        //if (other.TryGetComponent<CharacterController>(out CharacterController controller))
        //{
        //    if (!Door.IsOpen)
        //    {
        //        Door.Open(other.transform.position);
        //    }
        //}
    }

    private void OnTriggerExit(Collider other)
    {

        inCollider = false;

        //if (other.TryGetComponent<CharacterController>(out CharacterController controller))
        //{
        //    if (Door.IsOpen)
        //    {
        //        Door.Close();
        //    }
        //}
    }


    void Update()
    {
        if (inCollider == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Pressed E");

                if (!Door.IsOpen)
                {
                    Door.Open(transform.position);
                }

                else if (Door.IsOpen)
                {
                    Door.Close();
                }
            }
        }
    }
    

}