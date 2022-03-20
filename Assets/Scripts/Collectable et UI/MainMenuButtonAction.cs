using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButtonAction : MonoBehaviour
{
    public Button[] lvlButtons;
        
   void Start() 
    {
        int ActualLvl = PlayerPrefs.GetInt("AcutalLvl", 1);    /* https://www.youtube.com/watch?v=vpbPd6jNEBs Source pour le level selection */

        for (int i = 0; i < lvlButtons.Length; i++)
        {
            lvlButtons[i].interactable = false;
        }

        for(int i = 0; i < ActualLvl; i++)
        {
            lvlButtons[i].interactable = true;
        }
    }

    /// <summary>
    /// Permet d'afficher un panel transmis en paramètre
    /// </summary>
    /// <param name="PanelAOuvrir">Panel à afficher</param>
    public void AfficherPanel(GameObject PanelAOuvrir)
    {
        PanelAOuvrir.SetActive(true);
    }

    /// <summary>
    /// Permet de ferme aussi le panel actuel
    /// </summary>
    /// <param name="PanelAFermer">Panel à fermer</param>
    public void FermerPanel(GameObject PanelAFermer)
    {
        PanelAFermer.SetActive(false);
    }

    /// <summary>
    /// Permet de charger un niveau
    /// </summary>
    /// <param name="nom">Nom du niveau à charger</param>
    public void ChargerNiveau(string nom)
    {
        SceneManager.LoadScene(nom);
    }

    /// <summary>
    /// Permet de fermer l'application
    /// </summary>
    public void Quitter()
    {
        Application.Quit();
    }
}
