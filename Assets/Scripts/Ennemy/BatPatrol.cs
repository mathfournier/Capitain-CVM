using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BatPatrol : MonoBehaviour
{
    /// <summary>
    /// Vitesse de l'objet en patrouille
    /// </summary>
    [SerializeField]
    private float _vitesse = 3f;
    /// <summary>
    /// Liste de GO représentant les points à atteindre
    /// </summary>
    [SerializeField]
    private Transform[] _points;
    /// <summary>
    /// Référence vers la cible actuelle de l'objet
    /// </summary>
    private Transform _cible = null;
    /// <summary>
    /// Permet de connaître la position actuelle de la cible dans le tableau
    /// </summary>
    private int _indexPoint;
    /// <summary>
    /// Seuil où l'objet change de cible de déplacement
    /// </summary>
    private float _distanceSeuil = 0.3f;
    /// <summary>
    /// Référence vers le sprite Renderer
    /// </summary>
    private SpriteRenderer _sr;
    /// <summary>
    /// Réfère à l'animator du GO
    /// </summary>
    private Animator _animator;
    private bool _estDowned = false;
    private float _tempsDowned = 2f;
    private float _tempsDebutDowned;


    // Start is called before the first frame update
    void Start()
    {
        _sr = this.GetComponent<SpriteRenderer>();
        _indexPoint = 0;
        _cible = _points[_indexPoint];
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.fixedTime > _tempsDebutDowned + _tempsDowned)
            _estDowned = false;

        AnimatorStateInfo asi = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0);   /* https://stackoverflow.com/questions/34846287/get-name-of-current-animation-state Source pour la ligne 58*/  
        if (asi.IsName("Bat_Down") && !_estDowned)
        {
            Debug.Log("EST DOWNED");
            _estDowned = true;
            _tempsDebutDowned = Time.fixedTime;
        }
        
        if(!_estDowned && !asi.IsName("Bat_Mort"))
        {
            Vector3 direction = _cible.position - this.transform.position;
            this.transform.Translate(direction.normalized * _vitesse * Time.deltaTime, Space.World);

            if (direction.x < 0 && !_sr.flipX) _sr.flipX = true;
            else if (direction.x > 0 && _sr.flipX) _sr.flipX = false;

            if (Vector3.Distance(this.transform.position, _cible.position) < _distanceSeuil)
            {
                _indexPoint = (++_indexPoint) % _points.Length;
                _cible = _points[_indexPoint];
            }
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        // Ligne entre les cibles
        for (int i = 0; i < _points.Length - 1; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(_points[i].position, 
                _points[i + 1].position);
        }

        // Ligne entre l'ennemi et la cible
        if (_cible != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(this.transform.position, _cible.position);
        }
    }
#endif
}
