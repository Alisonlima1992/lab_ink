public class Enemy
{
    private string name;
    private BigNumber maxHitPoints;
    private BigNumber currentHitPoints;
    private BigNumber goldReward;
    private bool isDead;

    public string Name => name;
    public BigNumber MaxHitPoints => maxHitPoints;
    public BigNumber CurrentHitPoints => currentHitPoints;
    public BigNumber GoldReward => goldReward;
    public bool IsDead => isDead;

    public Enemy(string name, string maxHp, string gold)
    {
        this.name = name;
        maxHitPoints = new BigNumber(maxHp);
        currentHitPoints = new BigNumber(maxHp);
        goldReward = new BigNumber(gold);
        isDead = false;
    }

    public bool TakeDamage(BigNumber dmg, out BigNumber reward)
    {

        reward = new BigNumber("0");
        return false;
    }
}