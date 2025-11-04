using UnityEngine;

public class MachinaState : MonoBehaviour
{
    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("No se encontró el componente Animator en " + gameObject.name);
        }
    }

    void Update()
    {
        bool estaAndando = Input.GetKey(KeyCode.W);
        anim.SetBool("Andar", estaAndando);
        anim.SetBool("Parado", !estaAndando);
    }
}
