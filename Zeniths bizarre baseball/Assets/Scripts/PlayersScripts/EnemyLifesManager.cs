public class EnemyLifesManager : LifesManager
{
    public override void Death()
    {
        base.Death();

        AudioManager.instance.Play("enemy_death");
        Spawner.sp.killCount++;
        Spawner.sp.enemyCount--;
        
        gameObject.SetActive(false);
    }

    public void OnStateChange(GameStates state)
    {
        if(state == GameStates.gameOver)
        {
            an.enabled = false;
        }
    }

    public override void OnEnable()
    {
        base.OnEnable();

        if(GameManager.GM != null)
        {
            GameManager.GM.OnStateEnter.AddListener(OnStateChange);
        }
    }

    private void OnDisable() {
        if(GameManager.GM != null)
        {
            GameManager.GM.OnStateEnter.RemoveListener(OnStateChange);
        }
    }
}
