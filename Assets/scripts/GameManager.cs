using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject platformPrefab;
    public GameObject icePrefab;
    public GameObject scrapPrefab;
    public GameObject earthCore;
    public GameObject background;
    public Canvas winScreen;
    public Canvas HUD;
    public Canvas RocketScreen;
    public CinemachineVirtualCamera CMcamera;

    public GameObject PauseMenu;

    public float timeBetweenItemsSpawn = 2f;
    float timeBeforeNextItemSpawn;

    public float lifeSpanIce;
    public float lifeSpanScrap;
    public int amount = 3;
    public int iceSpawnRate = 1;
    int amountIce = 0;
    int amountScrap = 0;
    public bool gameOver;
    bool gamePause;
    //Rocket Health Varibiable
    public int maxHealthRocket = 100;
    public int healthRocket = 1;
    int maxRetryPlatform = 50000;
    int retryPlatform = 0;
    AudioSource MainMusic;
    AudioLowPassFilter MusicFilter;

    Gradient grad;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;
    // Start is called before the first frame update
    void Start()
    {
        winScreen.enabled = false;
        MainMusic = this.GetComponent<AudioSource>();
        MusicFilter = this.GetComponent<AudioLowPassFilter>();
        MusicFilter.cutoffFrequency = 22000;
        grad = new Gradient();
        CreateGradient();
        gameOver = false;
        gamePause = false;
        timeBeforeNextItemSpawn = timeBetweenItemsSpawn;
        for(int i =0; i< 16; i++){
            InstanciateLayer1Platform(32.5f, 35) ;
        }
        for(int i =0; i< 12; i++){
            InstanciateLayer1Platform(36.5f, 38);
        }

        for (int i = 0; i < 12; i++)
        {
            InstanciateLayer1Platform(38.5f, 42);
        }
        for (int i = 0; i < 18; i++)
        {
            InstanciateLayer1Platform(45, 50);
        }

        for (int i = 0; i < 26; i++)
        {
            InstanciateLayer1Platform(54, 60);
        }

        for (int i = 0; i < 10; i++)
        {
            InstanciateScrapItemPlatform();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if(!gamePause && (Input.GetKeyDown("p"))){
            PauseGame();
        }
        else if(gamePause && (Input.GetKeyDown("p"))){
            UnPauseGame();
        }
        if (gameOver)
            return;
        ChangeBackgroundColor();
    }
    public void healRocket(int heal)
    {
        healthRocket += heal;
        if (healthRocket > maxHealthRocket)
        {
            HUD.enabled = false;
            RocketScreen.enabled = false;
            winScreen.enabled = true;
            Time.timeScale = 0f;

        }
    }
    public void PauseGame(){
        Time.timeScale = 0f;
        gamePause = true;
       MusicFilter.cutoffFrequency = 500;
        PauseMenu.SetActive(true);
    }
    public void UnPauseGame(){        
        gamePause = false;
        PauseMenu.SetActive(false);
        MusicFilter.cutoffFrequency = 22000;
        Time.timeScale = 1f;
    }

    void FixedUpdate(){
        if(!gameOver && timeBeforeNextItemSpawn <= 0f){
            timeBeforeNextItemSpawn = timeBetweenItemsSpawn;
            for(int i = 0; i < amount; i++){
                InstanciateScrapItemPlatform();
            }
            for(int i =0; i < iceSpawnRate; i++){
                InstanciateIceItemPlatform(); 

            }
        }
        timeBeforeNextItemSpawn -= Time.deltaTime;
    }

    void InstanciateLayer1Platform(float min, float max){
        if (retryPlatform > maxRetryPlatform)
            return;
        int angle = (int)Random.Range(0f,359f);
        float posX = Mathf.Cos(angle * Mathf.Deg2Rad);
        float posY = Mathf.Sin(angle * Mathf.Deg2Rad);
        float size = Random.Range(-0.2f, 0.7f);
        float height = Random.Range(min,max);
        Vector3 position = height * new Vector3(posX,posY,0);
        GameObject platform = Instantiate(platformPrefab, position, Quaternion.identity) as GameObject;
        platform.transform.eulerAngles = new Vector3(0,0,angle - 90);
        platform.transform.parent = earthCore.transform;
        platform.transform.localScale = new Vector3(platform.transform.localScale.x + size, platform.transform.localScale.y );
        if (retryPlatform > maxRetryPlatform)
            return;
        if(!platform.GetComponent<Platform>().isCorrect){
            Destroy(platform.gameObject);
            Debug.Log("Error platform L1");
            InstanciateLayer1Platform(min, max);
            retryPlatform++;
        }
    }

    void InstanciateIceItemRandom(){
        int angle = (int)Random.Range(0f,359f);
        float posX = Mathf.Cos(angle * Mathf.Deg2Rad);
        float posY = Mathf.Sin(angle * Mathf.Deg2Rad);
        float height = Random.Range(32f,40f);
        Vector3 position = height * new Vector3(posX,posY,0);
        GameObject ice = Instantiate(icePrefab, position, Quaternion.identity);
        ice.transform.parent = earthCore.transform;
        ice.transform.eulerAngles = new Vector3(0,0,angle-90f);
        ice.GetComponent<Collectible>().lifeSpan = lifeSpanIce;
        amountIce++;
    }

    void InstanciateScrapItemRandom(){
        int angle = (int)Random.Range(0f,359f);
        float posX = Mathf.Cos(angle * Mathf.Deg2Rad);
        float posY = Mathf.Sin(angle * Mathf.Deg2Rad);
        float height = Random.Range(32f,40f);
        Vector3 position = height * new Vector3(posX,posY,0);
        GameObject scrap = Instantiate(scrapPrefab, position, Quaternion.identity);
        scrap.transform.parent = earthCore.transform;
        scrap.transform.eulerAngles = new Vector3(0,0,angle-90f);
        scrap.GetComponent<Collectible>().lifeSpan = lifeSpanScrap;
        amountScrap++;
    }

    void InstanciateIceItemPlatform(){
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
        float angle = Mathf.Atan2(earthCore.transform.position.normalized.y , earthCore.transform.position.normalized.x);

        int index = (int)Random.Range(0,platforms.Length-1);
        while (platforms[index].GetComponent<Platform>().hasItem || platforms[index].transform.position.magnitude < 40 || platforms[index].transform.position.y > 20 || platforms[index].transform.position.y < -30)
        {
            index = (int)Random.Range(0,platforms.Length-1);
            Debug.Log(platforms[index].transform.localPosition.y + " , " + platforms[index].transform.position.y);
        }
        platforms[index].GetComponent<Platform>().hasItem = true;
        Vector3 position = platforms[index].transform.position;
        Vector3 rotation = platforms[index].transform.rotation.eulerAngles;

        GameObject ice = Instantiate(icePrefab, position, Quaternion.identity, earthCore.transform);
        ice.transform.parent = earthCore.transform;
        ice.transform.eulerAngles = rotation;    
        ice.transform.position += 2*position.normalized;
        ice.GetComponent<Collectible>().lifeSpan = lifeSpanIce;
        ice.GetComponent<Collectible>().platform = platforms[index].GetComponent<Platform>();
        amountIce++;
    }

    void InstanciateScrapItemPlatform(){
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
        float angle = Mathf.Atan2(earthCore.transform.position.normalized.y , earthCore.transform.position.normalized.x);

        int index = (int)Random.Range(0,platforms.Length-1);
        while(platforms[index].GetComponent<Platform>().hasItem || platforms[index].transform.position.y > 20 ){
            index = (int)Random.Range(0,platforms.Length-1);
        }
        platforms[index].GetComponent<Platform>().hasItem = true;
        Vector3 position = platforms[index].transform.position;
        Vector3 rotation = platforms[index].transform.rotation.eulerAngles;

        GameObject scrap = Instantiate(scrapPrefab, position, Quaternion.identity, earthCore.transform);
        scrap.transform.parent = earthCore.transform;
        scrap.transform.eulerAngles = rotation;
        scrap.transform.position += 2*position.normalized;
        scrap.GetComponent<Collectible>().lifeSpan = lifeSpanScrap;
        scrap.GetComponent<Collectible>().platform = platforms[index].GetComponent<Platform>();
        amountScrap++;
    }

    public void BackToMenu(){
        SceneManager.LoadScene(0);
    }

    public void CameraShake(){
    }

    void CreateGradient(){
        colorKey = new GradientColorKey[4];
        Color color1 = new Color(0.60f, 0.79f, 0.49f);
        Color color2 = new Color(0.69f, 0.27f,0.015f, 1);
        Color color3 = new Color(1, 0, 0.08f, 1);

        colorKey[0].color = color1;
        colorKey[0].time = 0f;
        colorKey[1].color = color2;
        colorKey[1].time = 0.50f;
        colorKey[2].color = color3;
        colorKey[2].time = 0.70f;
        colorKey[3].color = Color.black;
        colorKey[3].time = 0.95f;

        alphaKey = new GradientAlphaKey[4];
        alphaKey[0].alpha = 1f;
        alphaKey[0].time = 0f;
        alphaKey[1].alpha = 1f;
        alphaKey[1].time = 0.50f;
        alphaKey[2].alpha = 1f;
        alphaKey[2].time = 0.70f;
        alphaKey[3].alpha = 1f;
        alphaKey[3].time = 0.95f;

        grad.SetKeys(colorKey, alphaKey);
    }

    void ChangeBackgroundColor(){
        float value = earthCore.transform.GetChild(0).GetComponent<Planet>().health / earthCore.transform.GetChild(0).GetComponent<Planet>().maxHealth;
        background.GetComponent<SpriteRenderer>().color = grad.Evaluate(1-value);
    }


    public void EndGame(){
        this.gameObject.GetComponent<AudioSource>().enabled = false;
        FindObjectOfType<AudioManager>().Play("Planet Explode");
        gameOver = true;        
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        HUD.enabled = false;
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
        foreach(GameObject go in platforms){
            Destroy(go);
        }
        GameObject[] shop = GameObject.FindGameObjectsWithTag("IceStation");
        foreach (GameObject go in shop)
        {
            Destroy(go);
        }
        GameObject[] rockets = GameObject.FindGameObjectsWithTag("Rocket");
        foreach (GameObject go in rockets)
        {
            Destroy(go);
        }
        GameObject[] longplatforms = GameObject.FindGameObjectsWithTag("LongPlatform");
        foreach (GameObject go in longplatforms)
        {
            Destroy(go);
        }
        GameObject[] iceItems = GameObject.FindGameObjectsWithTag("Ice");
        foreach(GameObject go in iceItems){
            Destroy(go);
        }
        GameObject[] scrapItems = GameObject.FindGameObjectsWithTag("Scrap");
        foreach(GameObject go in scrapItems){
            Destroy(go);
        }
        GameObject[] meteors = GameObject.FindGameObjectsWithTag("Killable");
        foreach(GameObject go in meteors){
            Destroy(go);
        }
        StartCoroutine(DezoomCamera());
        earthCore.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;        
        earthCore.transform.GetChild(0).GetChild(0).gameObject.GetComponent<ParticleSystem>().Play();
        earthCore.transform.GetChild(0).GetChild(1).gameObject.GetComponent<ParticleSystem>().Play();
        StartCoroutine(DestroyEverything());
    }

    IEnumerator DezoomCamera(){
        if(CMcamera.transform.position.z > -250)
            CMcamera.transform.position = CMcamera.transform.position + new Vector3(0, 0, -5);
        yield return new WaitForSeconds(1f);
    }

    IEnumerator DestroyEverything(){
        yield return new WaitForSeconds(5f);
        Destroy(earthCore.gameObject);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
    }


}