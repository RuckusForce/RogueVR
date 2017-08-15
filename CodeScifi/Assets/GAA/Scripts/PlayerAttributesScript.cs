using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttributesScript : MonoBehaviour
{
    [SerializeField]
    private bool invincible;
    private float lerpSpeed;
    private float proportion;

	public bool grounded;
	public bool sliding;

	[SerializeField]
    GameObject gameController;
    RetryGame retryGame;

    #region Health Variables
    [SerializeField]
    private StatScript health;
    [SerializeField]
    private BarScript healthBar;
    private Image healthHUD;
    #endregion

    #region Energy Variables
    [SerializeField]
    private StatScript energy;
    [SerializeField]
    private BarScript energyBar;
    private Image energyHUD;
    #endregion

    #region Shield Variables
    [SerializeField]
    private StatScript shield;
    [SerializeField]
    private BarScript shieldBar;
    private Image shieldHUD;
    #endregion


    void Awake()
    {

		healthBar = GameObject.Find("HealthBar").GetComponent<BarScript>();
		energyBar = GameObject.Find("EnergyBar").GetComponent<BarScript>();
		shieldBar = GameObject.Find("ShieldBar").GetComponent<BarScript>();

		healthHUD = healthBar.GetComponentsInChildren<Image>()[2];
        //0: health bar img, 1: mask img, 2: content img

        energyHUD = energyBar.GetComponentsInChildren<Image>()[2];
        //0: health bar img, 1: mask img, 2: content img

        shieldHUD = shieldBar.GetComponentsInChildren<Image>()[2];
        //0: health bar img, 1: mask img, 2: content img

        //health.Initialize(healthBar);
        //energy.Initialize(energyBar);
        //shield.Initialize(shieldBar);

        gameController = GameObject.Find("GameController");
		retryGame = gameController.GetComponent<RetryGame>();
	}

    void Start() {
        invincible = false;
        lerpSpeed = 10f;

        #region Health
        health.Bar = healthBar;
        health.MaxVal = 100f;
        health.CurrentVal = 100f;

        #endregion

        #region Energy
        energy.Bar = energyBar;
        energy.MaxVal = 100f;
        energy.CurrentVal = 80f;
        #endregion

        #region Shield
        shield.Bar = shieldBar;
        shield.MaxVal = 100f;
        shield.CurrentVal = 60f;
        #endregion
    }

    void FixedUpdate() {
        #region GameOver
        if (this.GetCurrentHealth() <= 0)
        {
            //SceneManager.LoadScene("You Lose Screen");
            Debug.Log("You died.");
            retryGame.Retry();
        }
        #endregion
    }

    //void OnTriggerStay2D(Collider2D other) {
    //	#region Ground Check == true
    //	if (other.gameObject.CompareTag("Ground")) {
    //		grounded = true;
    //	}
    //	#endregion
    //}

    //void OnTriggerExit2D(Collider2D other) {
    //	#region Ground Check == false
    //	if (other.gameObject.CompareTag("Ground")) {
    //		grounded = false;
    //	}
    //	#endregion
    //}

    #region Attribute Decrease/Recover/Get Methods
    public void PlayerDecreaseHealth(float amount)
    {
        health.CurrentVal -= amount;
        proportion = GetCurrentHealth() / health.MaxVal;
        healthHUD.fillAmount = Mathf.Lerp(healthHUD.fillAmount, proportion, Time.deltaTime * lerpSpeed);
    }

    public void PlayerRecoverHealth(float amount) {
        health.CurrentVal += amount;
        Debug.Log("health.CurrentVal: " + health.CurrentVal);
        proportion = GetCurrentHealth() / health.MaxVal;
        Debug.Log("proportion: " + proportion);
        Debug.Log("healthHUD.fillAmount: " + healthHUD.fillAmount);
        healthHUD.fillAmount = Mathf.Lerp(healthHUD.fillAmount, proportion, Time.deltaTime * lerpSpeed);
        //healthHUD.fillAmount = proportion;
        Debug.Log("healthHUD.fillAmount: " + healthHUD.fillAmount);
    }

    public void PlayerDecreaseEnergy(float amount) {
        energy.CurrentVal -= amount;
        proportion = GetCurrentEnergy() / energy.MaxVal;
        energyHUD.fillAmount = Mathf.Lerp(energyHUD.fillAmount, proportion, Time.deltaTime * lerpSpeed);
    }

    public void PlayerRecoverEnergy(float amount) {
        energy.CurrentVal += amount;
        proportion = GetCurrentEnergy() / energy.MaxVal;
        energyHUD.fillAmount = Mathf.Lerp(energyHUD.fillAmount, proportion, Time.deltaTime * lerpSpeed);
    }

    public void PlayerDecreaseShield(float amount) {
        shield.CurrentVal -= amount;
        proportion = GetCurrentShield() / shield.MaxVal;
        shieldHUD.fillAmount = Mathf.Lerp(shieldHUD.fillAmount, proportion, Time.deltaTime * lerpSpeed);
    }

    public void PlayerRecoverShield(float amount) {
        shield.CurrentVal += amount;
        proportion = GetCurrentShield() / shield.MaxVal;
        shieldHUD.fillAmount = Mathf.Lerp(shieldHUD.fillAmount, proportion, Time.deltaTime * lerpSpeed);
    }

    public float GetCurrentHealth(){
        return health.CurrentVal;
    }

    public float GetCurrentEnergy() {
        return energy.CurrentVal;
    }

    public float GetCurrentShield() {
        return shield.CurrentVal;
    }

    public float GetMaxHealth() {
        return health.MaxVal;
    }

    public float GetMaxEnergy() {
        return energy.MaxVal;
    }

    public float GetMaxShield() {
        return shield.MaxVal;
    }
    #endregion

}
