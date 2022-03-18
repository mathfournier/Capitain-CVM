using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BatBehaviour : MonoBehaviour
{
    /// <summary>
    /// Point de vie du personnage
    /// </summary>
    [SerializeField]
    private int _pv = 2;
    /// <summary>
    /// Angle de tolérange pour le calcul du saut sur la tête
    /// </summary>
    [SerializeField]
    private float _toleranceAngle = 1.5f;
    /// <summary>
    /// Décrit la durée de l'invulnaribilité
    /// </summary>
    public const float DelaisInvulnerabilite = 1f;
    /// <summary>
    /// Décrit si l'entité est invulnérable
    /// </summary>
    private bool _invulnerable = false;
    /// <summary>
    /// Réfère à l'animator du GO
    /// </summary>
    private Animator _animator;
    /// <summary>
    /// Représente le moment où l'invulnaribilité a commencé
    /// </summary>
    private float _tempsDebutInvulnerabilite;
    /// <summary>
    /// Nombre de points octroyer lors de la destruction
    /// </summary>
    [SerializeField]
    private int _pointDestruction = 5;
    /// <summary>
    /// Défini si l'objet est en cours de destruction
    /// </summary>
    private bool _destructionEnCours = false;
    /// <summary>
    /// Défini si l'objet est down et prêt a etre attaqué
    /// </summary>
    private bool _estDowned = false;
    private float _timeDowned = 5f;
    private float _timeEntreAttaque = 2.5f;
    private float _tempsDebutDowned;
    private bool _wasAttacked = false;
    private bool _canAttack = false;

    private void Start()
    {
        _animator = this.gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (this._pv <= 0 && !this._destructionEnCours)
        {
            _animator.SetTrigger("Destruction");
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            this.gameObject.GetComponent<BatPatrol>().enabled = false;
            GameObject.Destroy(this.transform.parent.gameObject, 0.5f); 
            this._destructionEnCours = true;
            GameManager.Instance.PlayerData.IncrScore(this._pointDestruction);
        }

        if (Time.fixedTime > _tempsDebutInvulnerabilite + DelaisInvulnerabilite)
            _invulnerable = false;

        if (Time.fixedTime > _tempsDebutDowned + _timeEntreAttaque)
            _canAttack = true;

        if (Time.fixedTime > _tempsDebutDowned + _timeDowned)
        {
            _estDowned = false;
            if(!_wasAttacked)
                _animator.SetTrigger("NotAttacked");
            _wasAttacked = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player") && !_invulnerable)
        {
            PlayerBehaviour pb = collision.gameObject.GetComponent<PlayerBehaviour>();
            if (pb != null)
                pb.CallEnnemyCollision();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if(!_invulnerable)
            {
                if(_estDowned && _canAttack)
                {
                    this._pv--;
                    _animator.SetTrigger("DegatActif");
                    _tempsDebutInvulnerabilite = Time.fixedTime;
                    _invulnerable = true;
                    _estDowned = false;
                    _wasAttacked = true;
                    _timeDowned = 0;
                    _canAttack = false;
                }
                else
                {
                    _animator.SetTrigger("Downed");
                    _tempsDebutDowned = Time.fixedTime;
                    _estDowned = true;
                }
            }
        }
    }
}
