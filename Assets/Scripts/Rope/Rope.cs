using UnityEngine;
using Obi;
using static Obi.ObiRope;
using System.Collections;

public class Rope : MonoBehaviour
{
    [SerializeField] private ObiRope _obiRope;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private ObiParticleAttachment _endAttachment;

    private readonly float _movingDownSpeed = 0.5f;
    private readonly float _movingDownTime = 2f;
    private bool _isTorn;
    private bool _isConncted;
    private TeamId _teamId;

    public ObiRope ObiRope => _obiRope;
    public Transform StartPoint => _startPoint;
    public Transform EndPoint => _endPoint;
    public bool IsTorn => _isTorn;
    public bool IsConnected => _isConncted;
    public TeamId TeamId => _teamId;

    private void OnEnable()
    {
        _obiRope.OnRopeTorn += Disappear;
    }

    private void OnDisable()
    {
        _obiRope.OnRopeTorn -= Disappear;
    }

    public void SetTeamId(TeamId teamId)
    {
        _teamId = teamId;
    }

    public void Connect()
    {
        _isConncted = true;
    }
    public void Disconnect()
    {
        _isConncted = false;
    }

    private void Disappear(ObiRope obiRope, ObiRopeTornEventArgs tearInfo)
    {
        _isTorn = true;
        StartCoroutine(Disappearing());
    }

    private IEnumerator Disappearing()
    {
        yield return new WaitForSeconds(0.5f);

        _endAttachment.enabled = false;

        yield return new WaitForSeconds(2f);

        transform.SetParent(null);
        float elaspedTime = 0;

        while (elaspedTime <= _movingDownTime)
        {
            elaspedTime += Time.deltaTime;
            transform.Translate(_movingDownSpeed * Time.deltaTime * Vector3.down);
           
            yield return null;
        }

        Destroy(gameObject);
    }
}
