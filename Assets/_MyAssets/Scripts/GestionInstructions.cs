using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionInstructions : MonoBehaviour
{
    [SerializeField] public GameObject Inst;
    public void Instruction()
    {
        Inst.SetActive(true);

    }
    public void FinInstruction()
    {
        Inst.SetActive(false);
    }
}
