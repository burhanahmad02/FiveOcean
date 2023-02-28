/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuTransitionManager : MonoBehaviour
{
    public static MainMenuTransitionManager Instance;
    //Panel Transition
    public Animator transitionAnim;
    // Update is called once per frame
    void Awake()
    {

        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }
    }
    public IEnumerator SubmarineSelectionCo()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(10f);
        
       
        MainMenuUI.Instance.panelFade.GetComponent<Animator>().enabled = false;
        MainMenuUI.Instance.panelTransition.GetComponent<Image>().enabled = false;
    }
}
*/