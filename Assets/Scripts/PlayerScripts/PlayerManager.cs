using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
using UnityEngine.Playables;

public class PlayerManager : MonoBehaviour
{
    private RoadsManager roadsManager;
    private GameManager gameManager;
    [SerializeField] private ShieldBehavior shield;
    [SerializeField] private GameObject shieldUIPrefab;
    [SerializeField] private GameObject shieldUISlot;
    [SerializeField] private TextMeshProUGUI diamondsText;
    [SerializeField] private CinemachineVirtualCamera followingCam;
    [SerializeField] private CinemachineVirtualCamera endCam;
    private int runShields;
    private int runDiamonds;
    private float endBonus = 1;
    private bool invincibility = false;

    private void Start()
    {
        roadsManager = RoadsManager.GetInstance();
        gameManager = GameManager.GetInstance();
        GetComponent<PlayerMovements>().SetPaused(true);
        runShields = PlayerPrefs.GetInt("Shields");
        UpdateDiamondsCount();
        UpdateShieldCount();
        CameraSwitcher.SwitchCamera(followingCam);
    }

    private void OnEnable()
    {
        CameraSwitcher.Register(followingCam);
        CameraSwitcher.Register(followingCam);
    }

    private void OnDisable()
    {
        CameraSwitcher.Unregister(followingCam);
        CameraSwitcher.Unregister(endCam);
    }

    public void StartGame()
    {
        GetComponent<PlayerMovements>().SetPaused(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RoadSpawn"))
        {
            roadsManager.MoveRoad();
        }
        if (other.CompareTag("Gatherable"))
        {
            Gatherable gatherable = other.GetComponent<Gatherable>();
            switch (gatherable.Type)
            {
                case GatherableType.Diamond:
                    runDiamonds++;
                    UpdateDiamondsCount();
                    break;
                case GatherableType.Life:
                    runShields++;
                    UpdateShieldCount();
                    break;
            }
            other.GetComponent<Gatherable>().TriggerGathered();
        }
        if (other.CompareTag("FinishLine") && gameManager.GetGameState() == GameState.Game)
        {
            gameManager.SetGameState(GameState.End);
            CameraSwitcher.SwitchCamera(endCam);
            GetComponent<PlayerMovements>().TriggerEnd();
        }
        if (other.gameObject.CompareTag("EndBonus"))
        {
            endBonus = other.gameObject.GetComponent<EndBonus>().GetBonus();
            if (runShields < 1)
            {
                other.gameObject.GetComponent<EndBonus>().TriggerBonus();
                StartCoroutine(GoToEndMenu(4.5f));
                GetComponent<PlayerMovements>().SetPaused(true);
            }
        }
        if (other.gameObject.CompareTag("BossEnd")) {
            endBonus = other.gameObject.GetComponent<BossEnd>().GetBonus();
            other.gameObject.GetComponent<BossEnd>().TriggerBonus();
            StartCoroutine(GoToEndMenu(4.5f));
            GetComponent<PlayerMovements>().SetPaused(true);
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Obstacle") && !invincibility)
        {
            if (runShields >= 1)
            {
                runShields--;
                UpdateShieldCount();
                hit.gameObject.GetComponent<BarrierBehavior>().BurstBarrier();
                GetComponent<PlayerMovements>().SlowPlayer();
                invincibility = true;
                shield.EnableShield();
                StartCoroutine(InvincibilityTimer(.2f));
            }
            else if (gameManager.GetGameState() == GameState.Game)
            {
                gameManager.EndGame(runDiamonds, endBonus);
            }
        }
        if (hit.gameObject.CompareTag("Ramp"))
        {
            GetComponent<PlayerMovements>().Boost();
            hit.gameObject.GetComponent<AudioSource>().Play();
        }
    }

    public void UpdateDiamondsCount()
    {
        diamondsText.text = (PlayerPrefs.GetInt("Diamonds") + runDiamonds).ToString();
    }

    public void EditShields(int value)
    {
        runShields += value;
        if (runShields > 0)
            UpdateShieldCount();
        else
        {
            runShields = 0;
            gameManager.EndGame(runDiamonds, endBonus);
        }
    }

    private void UpdateShieldCount()
    {
        while (shieldUISlot.transform.childCount != runShields)
        {
            if (shieldUISlot.transform.childCount < runShields)
                Instantiate(shieldUIPrefab, shieldUISlot.transform);
            else
            {
                GameObject shieldToDestroy = shieldUISlot.transform.GetChild(0).gameObject;
                shieldToDestroy.transform.SetParent(null);
                Destroy(shieldToDestroy);
            }
        }
    }

    public void ResetShield()
    {
        runShields = PlayerPrefs.GetInt("Shields");;
        UpdateShieldCount();
    }

    public void DisplayShields(bool active)
    {
        shieldUISlot.SetActive(active);
    }

    private IEnumerator InvincibilityTimer(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        invincibility = false;
    }

    private IEnumerator GoToEndMenu (float _delay)
    {
        yield return new WaitForSeconds(_delay);
        gameManager.EndGame(runDiamonds, endBonus);
    }
}
