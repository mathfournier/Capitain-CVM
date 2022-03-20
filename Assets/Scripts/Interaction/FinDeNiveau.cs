using UnityEngine;
using UnityEngine.SceneManagement;

public class FinDeNiveau : MonoBehaviour
{
    public int prochaineScene;

    void Start()
    {
        prochaineScene = SceneManager.GetActiveScene().buildIndex + 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {

            if(SceneManager.GetActiveScene().buildIndex == 3)   /* https://www.youtube.com/watch?v=vpbPd6jNEBs Source pour le level selection */
            {
                Debug.Log("Vous avez completez les 3 niveaux");
                SceneManager.LoadScene("MainMenu");
            }
            else
            {
                Debug.Log("Félicitation, le niveau est terminé.");
                GameManager.Instance.SaveData();
                SceneManager.LoadScene(prochaineScene);

                if (prochaineScene > PlayerPrefs.GetInt("AcutalLvl"))
                {
                    PlayerPrefs.SetInt("ActualLvl", prochaineScene);
                }

                Debug.Log("Niveau" + PlayerPrefs.GetInt("ActualLvl") + " débloqué");
            }
        }
    }
}
