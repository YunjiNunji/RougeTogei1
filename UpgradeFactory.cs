public static class UpgradeFactory
{
    public static Upgrade GenerateUpgrade(int actLevel)
    {
        Upgrade newUpgrade = new Upgrade
        {
            upgradeName = "Extra Roll",
            description = "Gain 1 extra roll per turn."
        };

        return newUpgrade;
    }
}

