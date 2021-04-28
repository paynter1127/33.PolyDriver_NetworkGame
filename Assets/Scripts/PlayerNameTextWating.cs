using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;


public class PlayerNameTextWating : MonoBehaviour
{
    private Text nameText;

    private void Start()
    {
        nameText = GetComponent<Text>();
        
        if(AuthManager.User != null)
        {
            nameText.text = $"{AuthManager.User.Email}";
        }
    }
}
