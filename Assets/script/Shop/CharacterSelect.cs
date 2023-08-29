using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    public GameObject[] skins;
    public int selectedCharacter = 0;
    public Character[] characters;
    // Start is called before the first frame update
    
    private void Awake()
    {
      selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);
      foreach (GameObject player in skins)
      {
        player.SetActive(false);
      }

      skins[selectedCharacter].SetActive(true);

      foreach(Character c in characters)
      {
        if(c.price == 0)
          c.isUnlocked = true;
        else 
        { 
          c.isUnlocked = PlayerPrefs.GetInt(c.name, 0) == 0 ? false : true;
        }
      }
    }
    
    public void ChangeNext()
    {
      skins[selectedCharacter].SetActive(false);
      selectedCharacter++;
      if (selectedCharacter == skins.Length)
      {
        selectedCharacter = 0;
      }
      skins[selectedCharacter].SetActive(true);
      PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
    }

    public void ChangePrevious()
    {
      skins[selectedCharacter].SetActive(false);
      selectedCharacter--;
      if (selectedCharacter == -1)
      {
        selectedCharacter = skins.Length - 1;
      }
      skins[selectedCharacter].SetActive(true);
      PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
    }
}