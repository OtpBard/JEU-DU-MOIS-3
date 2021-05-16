using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Experimental.Rendering.Universal; 
using UnityEngine.Events;

public class RifleControler : MonoBehaviour
{
    public GameObject FusilPosition;
    public float MaxLeftLenght = -10.0f;
    public float MaxRightLength = 10.0f;

    public float MoveSpeed = 2.0f;

    public GameObject ProjectilesSpawnPosition;
    public Projectiles ProjectilesPrefab;
    public float FireRate = 5.0f;
    private float LastFireTimer = 0.0f;

    public int Damage = 1;

    public float KnockbackStrenght = 2.0f;
    public float BaseDeviation = 20.0f;
    public float CurrentDeviation;
    private float CurrentAngle = 0.0f;

    public float DeviationSpeed = 2.0f;

    private float DeviationDirection = 1.0f;

    public float SideStrenght = 100.0f;
    
    public int Score = 0;
    public int Life = 3;
    public float ScoreMultiplier = 1.0f;
    public int CurrentGold = 0;

    public TextMeshProUGUI PV;
    public TextMeshProUGUI Degat;
    public TextMeshProUGUI Precision;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI xScore;
    public TextMeshProUGUI CadenceTir;

    public TextMeshProUGUI Gold;

    public Light2D DamageLight;
    public float Intensity = 0.5f;
    public float DamageTime = 0.5f;
    public float DamageTimer = 0.0f;

    public UnityEvent OnDeath;

    private bool Dead = false;

    public AudioSource FireSound;




    // Start is called before the first frame update
    void Start()
    {
        CurrentDeviation = BaseDeviation;

    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float displacement = Mathf.Clamp(horizontalInput*MoveSpeed*Time.deltaTime + FusilPosition.transform.position.x, MaxLeftLenght, MaxRightLength);
        FusilPosition.transform.position = new Vector3(displacement, FusilPosition.transform.position.y, FusilPosition.transform.position.z);

        if (LastFireTimer > 0.0f)
        {
            LastFireTimer -= Time.deltaTime;
        }

        if (Input.GetKey("space") && LastFireTimer <= 0.0f)
        {
            LastFireTimer = 1.0f / FireRate;
            Projectiles bullet = Instantiate(ProjectilesPrefab, ProjectilesSpawnPosition.transform.position, Quaternion.identity);
            bullet.Direction = (ProjectilesSpawnPosition.transform.position - transform.position).normalized;
            bullet.Damage = Damage;

            GetComponent<Rigidbody2D>().AddForce(bullet.Direction * -1.0f * KnockbackStrenght, ForceMode2D.Impulse);

            FireSound.Play();
        }

        CurrentAngle += DeviationSpeed * Time.deltaTime * DeviationDirection;
        if (CurrentAngle > CurrentDeviation || CurrentAngle < - CurrentDeviation)
        {
            DeviationDirection = - DeviationDirection;
            
        }
        
        
        GetComponent<Rigidbody2D>().SetRotation(CurrentAngle);

        PV.text = "x" + Life.ToString();
        Degat.text = Damage.ToString();
        Precision.text = ((20 - CurrentDeviation) / 20 * 100).ToString() + "%";
        ScoreText.text = Score.ToString();
        xScore.text = "x" + ScoreMultiplier.ToString();
        CadenceTir.text = FireRate.ToString();
        Gold.text = CurrentGold.ToString();

        if (DamageTimer > 0.0f)
        {
            float x = DamageTime - DamageTimer;
            x = x - DamageTime / 2.0f;
            float y = Mathf.Abs(x) - DamageTime / 2.0f;
            DamageLight.intensity = Intensity * Mathf.Abs(y) / DamageTime * 2.0f;
            DamageTimer -= Time.deltaTime;
        }
        else
        {
            DamageLight.intensity = 0;
        }

        if (Dead)
        {
            if (LoadingScreen.Instance.Timer <= 0.5f)
            {
                OnDeath.Invoke();
                
            }
        }

        if (Life <= 0 && !Dead)
        {
            ScoreGlobal.score = Score;
            Dead = true;
            LoadingScreen.Instance.ShowLoadingScreen(1.0f);
            
        }
    }
}
