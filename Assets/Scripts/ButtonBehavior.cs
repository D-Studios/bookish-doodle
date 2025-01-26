using UnityEngine;
using UnityEngine.EventSystems;  
using UnityEngine.SceneManagement; 

public class ButtonBehavior : MonoBehaviour, IPointerDownHandler
{
    [SerializeField]
    int scene;

    public void OnPointerDown(PointerEventData eventData){
    	SceneManager.LoadScene(scene);
    }
}
