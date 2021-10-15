using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private GameObject ball;
    [SerializeField]
    private float throwingForce = 1000f;
    [SerializeField]
    private InputActionReference toggleReference = null;
    [SerializeField]
    private InputActionReference gripReference = null;
    [SerializeField]
    private GameObject leftHand, rightHand;

    private bool isGripped = false;
    private GameObject throwingHand = null;
    private XRRayInteractor left, right;

    // Dam subscribe la 3 eventuri
    // Toggle - Cand apasam tasta Q, se arunca mingea pe directia mainii in care se afla
    // Gripped - Setam variabila isGripped pe TRUE daca mingea se afla intr-o mana
    // UnGripped - Setam variabila isGripped pe FALSE daca mingea NU se afla intr-o mana
    // Adaugam cate un listener pe fiecare mana in care determinam in care mana se afla mingea
    private void Awake()
    {
        toggleReference.action.started += Toggle;
        gripReference.action.started += Gripped;
        gripReference.action.canceled += UnGripped;

        left = leftHand.GetComponent<XRRayInteractor>();
        right = rightHand.GetComponent<XRRayInteractor>();
        left.selectEntered.AddListener(GetHand);
        right.selectEntered.AddListener(GetHand);
    }

    private void GetHand(SelectEnterEventArgs arg0)
    {
        if (throwingHand == null)
        {
            if (arg0.interactor.gameObject.Equals(leftHand))
                throwingHand = leftHand;
            else
                throwingHand = rightHand;
        }
    }

    private void Gripped(InputAction.CallbackContext context)
    {
        isGripped = true;
    }
    private void UnGripped(InputAction.CallbackContext context)
    {
        isGripped = false;
    }

    // Fortam mingea sa fie afectata de gravitatie
    public void Toggle(InputAction.CallbackContext context)
    {
        ball.GetComponent<Rigidbody>().useGravity = true;
        ball.GetComponent<Rigidbody>().isKinematic = false;
        // Daca se afla intr-o mana
        if (isGripped)
        {
            gripReference.action.Disable();
            // Ii dam mingii velocity in functie de directia in care este indreptata mana
            ball.GetComponent<Rigidbody>().AddForce(throwingHand.transform.forward * throwingForce);
            gripReference.action.Enable();
            // Setam mana cu care se arunca pe null
            throwingHand = null;
        }
    }
}
