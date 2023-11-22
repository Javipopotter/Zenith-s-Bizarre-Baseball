public class OtakuBoss : Enemy
{
    public override void OnDeath()
    {
        an.Rebind();
        enabled = false;
        DialoguesManager.dialoguesManager.ExecuteDialogViaKey("OtakuDefeat");
        AudioManager.instance.Stop("Boss_theme");
        GameManager.GM.OpenGates();
    }
}
