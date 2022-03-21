using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuButtonAction : MonoBehaviour
{
    /// <summary>
    /// Représente les boutons des niveaux
    /// </summary>
    public Button[] lvlButtons;
    /// <summary>
    /// Représente les coullectables dans le menu
    /// </summary>
    public Button[] Collectables;

    // Start is called before the first frame update    
    void Start() 
    {
        int ActualLvl = PlayerPrefs.GetInt("AcutalLvl", 1);    /* https://www.youtube.com/watch?v=vpbPd6jNEBs Source pour le level selection */
        if (ActualLvl > 4)
            ActualLvl = 4;
        for (int i = 0; i < lvlButtons.Length; i++)
        {
            lvlButtons[i].interactable = false;
        }

        for(int i = 0; i < ActualLvl -1; i++)
        {
            lvlButtons[i].interactable = true;
        }

        if(PlayerPrefs.GetInt("Collectable1") > 0) 
        {
            Collectables[0].interactable = true;
        }

        if(PlayerPrefs.GetInt("Collectable2") > 0)
        {
            Collectables[1].interactable = true;
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
