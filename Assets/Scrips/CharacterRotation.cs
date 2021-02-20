using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRotation : MonoBehaviour
{
    [Header("Paramètres")]
    public bool isRigidCamera = true;
    public float lerpElasticity = 5f;

    // Transform que la Caméra tentera de rattraper à chaque frame
    public Transform camPositioner;

    // Pour empêcher la caméra de flippé si la souris va trop vers le haut/bas
    float xAxisClamp = 0f;

    // Le transform de la caméra
    Transform mainCamTransform;

    // Start is called before the first frame update
    void Start()
    {
        // Vérouiller le curseur dans la fenètre
        Cursor.lockState = CursorLockMode.Locked;

        mainCamTransform = Camera.main.transform;
        mainCamTransform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamPosition();
        ReplaceCamera();
    }

    void RotateCamPosition()
    {
        // Positions X et Y du curseur
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        xAxisClamp -= mouseY;

        // Rotation du camPositioner et du character
        Vector3 rotCam = camPositioner.rotation.eulerAngles;
        Vector3 rotPlayer = transform.rotation.eulerAngles;

        // Le positioner tourne en fonction de la position de la souris (x seulement)
        rotCam.x -= mouseY;
        rotCam.z = 0f;
        // Le player tourne en fonction de la position de la souris (y seulement)
        rotPlayer.y += mouseX;

        // Empêcher la caméra de flip en rotation
        if (xAxisClamp > 90f)
        {
            xAxisClamp = 90f;
            rotCam.x = 90f;
        } else if (xAxisClamp < -90f)
        {
            xAxisClamp = -90f;
            rotCam.x = 270f;
        }

        // Appliquer les rotations
        camPositioner.rotation = Quaternion.Euler(rotCam);
        transform.rotation = Quaternion.Euler(rotPlayer);
    }

    /// <summary>
    /// La caméra se déplace vers le positioner pour que ça soit smooth
    /// </summary>
    void ReplaceCamera()
    {
        if (!isRigidCamera)
        {
            mainCamTransform.position = Vector3.Lerp(mainCamTransform.position, camPositioner.position, lerpElasticity * Time.deltaTime);
            mainCamTransform.rotation = Quaternion.Lerp(mainCamTransform.rotation, camPositioner.rotation, lerpElasticity * Time.deltaTime);
        }
        else
        {
            mainCamTransform.position = camPositioner.position;
            mainCamTransform.rotation = camPositioner.rotation;
        }
    }

}
