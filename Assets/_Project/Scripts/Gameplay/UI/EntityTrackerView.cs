using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

public class EntityTrackerView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textWorkersDead;
    [SerializeField] private TextMeshProUGUI textPredatorsDead;

    [Inject] private IEntityTracker tracker;

    private CompositeDisposable disp = new();

    private void Start()
    {
        SubscribeOnDeadValue(EntityId.AntWorker, textWorkersDead);
        SubscribeOnDeadValue(EntityId.BugPredator, textPredatorsDead);
    }
    private void SubscribeOnDeadValue(EntityId entityId, TextMeshProUGUI text)
    {
        tracker.DestructedEntities.
            ObserveValue(entityId).
            Subscribe(value => text.text = value.ToString()).
            AddTo(disp);
    }
    private void OnDestroy()
    {
        disp.Dispose();
    }
}
