using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

[System.Serializable]
public class EnemySave
{
    public List<string> EnemyMoveInStageSave;
    public List<string> EnemyPinInStageSave;
    public List<string> EnemySavestageClearList;

    public int EnemySaveLastBtnIndex;
    public int EnemySaveFixDifficultyInStage;
    public int EnemySaveFixDifficulty;
    public int EnemySaveMoveDifficultyInStage;
    public int EnemySaveMoveDifficulty;

    public EnemySave()
    {
        EnemyMoveInStageSave = ConvertToStringList(GameManager.MoveInStage);
        EnemyPinInStageSave = ConvertToStringList(GameManager.PinInStage);
        EnemySavestageClearList = ConvertToDicList(GameManager.stageClearDict);

        EnemySaveLastBtnIndex = EnemyManager.Instance.LastBtnIndex;
        EnemySaveFixDifficultyInStage = EnemyManager.Instance.FixDifficultyInStage;
        EnemySaveFixDifficulty = EnemyManager.Instance.FixDifficulty;
        EnemySaveMoveDifficultyInStage = EnemyManager.Instance.MoveDifficultyInStage;
        EnemySaveMoveDifficulty = EnemyManager.Instance.MoveDifficulty;
    }
    private List<string> ConvertToDicList(Dictionary<string, bool[]> dict)
    {
        List<string> list = new List<string>();
        foreach (var kvp in dict)
        {
            string key = kvp.Key;
            string valueString = BoolArrayToString(kvp.Value);
            string serializedPair = key + ":" + valueString;
            list.Add(serializedPair);
        }
        return list;
    }

    private string BoolArrayToString(bool[] array)
    {
        string arrayString = string.Join(",", Array.ConvertAll(array, b => b ? "1" : "0"));
        return arrayString;
    }

    private List<string> ConvertToStringList(bool[][] array)
    {
        List<string> stringList = new List<string>();
        foreach (bool[] row in array)
        {
            stringList.Add(string.Join(",", row));
        }
        return stringList;
    }
}

[System.Serializable]
public class PlayerSave
{
    public float Player_BaiscDamage;
    public float Player_BaiscAttackSpeed;
    public float Player_BaiscCriticalChance;
    public float Player_BaiscCriticalDamage;
    public float Player_BaiscArmorPenetration;

    public int Player_PP;

    public long PMUP_EarnMoney;
    public float PMUP_Damage;
    public float PMUP_AttackSpeed;
    public float PMUP_CriticalChance;
    public float PMUP_CriticalDamage;
    public float PMUP_ArmorPenetration;

    public int Player_Lv;
    public float Player_maxExp;
    public float Player_curExp;
    public long Player_money;
    public int Player_diamond;
    public int Player_redstone;

    public int Player_RelicsScroll;
    public int[] Player_HaveMobScroll;

    public int Player_PPUP_DMGLv;
    public int Player_PPUP_ATSLv;
    public int Player_PPUP_CRCLv;
    public int Player_PPUP_CRDLv;
    public int Player_PPUP_ARPLv;

    public int Player_MoneyUP_ENMLv;
    public int Player_MoneyUP_DMGLv;
    public int Player_MoneyUP_ATSLv;
    public int Player_MoneyUP_CRCLv;
    public int Player_MoneyUP_CRDLv;
    public int Player_MoneyUP_ARPLv;

    public long StarGrey;
    public long StarBrown;
    public long StarBlue;
    public long StarGreen;
    public long StarRed;
    public long StarYellow;
    public long StarPurple;
    public long StarOrange;
    public long StarDark;

    public bool[] C_Boss;

    public PlayerSave()
    {
        Player_BaiscDamage = (float)GameManager.Player_Damage;
        Player_BaiscAttackSpeed = (float)GameManager.Player_AttackSpeed;
        Player_BaiscCriticalChance = (float)GameManager.Player_CriticalChance;
        Player_BaiscCriticalDamage = (float)GameManager.Player_CriticalDamage;
        Player_BaiscArmorPenetration = (float)GameManager.Player_ArmorPenetration;

        Player_PP = GameManager.Player_PassivePoint;

        PMUP_EarnMoney = GameManager.Player_MoneyUp_EarnMoney;
        PMUP_Damage = (float)GameManager.Player_MoneyUp_Damage;
        PMUP_AttackSpeed = (float)GameManager.Player_MoneyUp_AttackSpeed;
        PMUP_CriticalChance = (float)GameManager.Player_MoneyUp_CriticalChance;
        PMUP_CriticalDamage = (float)GameManager.Player_MoneyUp_CriticalDamage;
        PMUP_ArmorPenetration = (float)GameManager.Player_MoneyUp_ArmorPenetration;

        Player_Lv = GameManager.Player_Level;
        Player_maxExp = GameManager.Player_MaxExp;
        Player_curExp = GameManager.Player_CurExp;
        Player_money = GameManager.Player_Money;
        Player_diamond = GameManager.Player_Diamond;
        Player_redstone = GameManager.Player_RedStone;

        Player_RelicsScroll = GameManager.RelicsReinforceScroll;
        Player_HaveMobScroll = GameManager.PlayerHaveMobScroll;

        Player_PPUP_DMGLv = GameManager.Player_PPUP_DamageLevel;
        Player_PPUP_ATSLv = GameManager.Player_PPUP_AttackSpeedLevel;
        Player_PPUP_CRCLv = GameManager.Player_PPUP_CriticalChanceLevel;
        Player_PPUP_CRDLv = GameManager.Player_PPUP_CriticalDamageLevel;
        Player_PPUP_ARPLv = GameManager.Player_PPUP_ArmorPenetrationLevel;

        Player_MoneyUP_ENMLv = GameManager.Player_MoneyUp_EarnMoneyLevel;
        Player_MoneyUP_DMGLv = GameManager.Player_MoneyUp_DamageLevel;
        Player_MoneyUP_ATSLv = GameManager.Player_MoneyUp_AttackSpeedLevel;
        Player_MoneyUP_CRCLv = GameManager.Player_MoneyUp_CriticalChanceLevel;
        Player_MoneyUP_CRDLv = GameManager.Player_MoneyUp_CriticalDamageLevel;
        Player_MoneyUP_ARPLv = GameManager.Player_MoneyUp_ArmorPenetrationLevel;

        StarGrey = GameManager.HaveStarGrey;
        StarBrown = GameManager.HaveStarBrown;
        StarBlue = GameManager.HaveStarBlue;
        StarGreen = GameManager.HaveStarGreen;
        StarRed = GameManager.HaveStarRed;
        StarYellow= GameManager.HaveStarYellow;
        StarPurple= GameManager.HaveStarPurple;
        StarOrange = GameManager.HaveStarOrange;
        StarDark = GameManager.HaveStarDark;

        C_Boss = BossManager.ClearBoss;

    }

}

public class MobScrollSave
{
    public bool[] MS_BuyChecking;

    public int Save_UpPower;
    public float Save_UpAttackSpeed;
    public int Save_UpExp;
    public int Save_UpArmorPenetration;
    public int Save_UpEarnGold;

    public MobScrollSave()
    {
        MS_BuyChecking = MobScrollManager.MS_BuyCheck;
        Save_UpPower = MobScrollManager.MS_UpPower;
        Save_UpAttackSpeed = MobScrollManager.MS_UpAttackSpeed;
        Save_UpExp = MobScrollManager.MS_UpExp;
        Save_UpArmorPenetration = MobScrollManager.MS_UpArmorPenetration;
        Save_UpEarnGold = MobScrollManager.MS_UpEarnGold;
    }
}


[System.Serializable]
public class EquipSave
{
    public List<Sprite> A_Sprites; // 키에 해당하는 Sprite 리스트
    public List<int> A_Counts;     // 값에 해당하는 int 리스트

    public float A_Count;
    public int A_Level;
    public bool[] A_Drawn;

    public float A_EquipExperience;
    public float A_OwnExperience;

    public bool[] A_OwnIncreased;

    public List<Sprite> W_Sprites; // 키에 해당하는 Sprite 리스트
    public List<int> W_Counts;     // 값에 해당하는 int 리스트

    public float W_Count;
    public int W_Level;
    public bool[] W_Drawn;

    public float W_EquipDMG;
    public float W_OwnDMG;

    public bool[] W_OwnIncreased;

    public int A_ThisEquipNum;
    public int W_ThisEquipNum;

    public int[] A_ReinforceValue;
    public int[] W_ReinforceValue;

    public float RelicsRank;

    public int[] WarrantLevel;

    public EquipSave()
    {
        A_Sprites = new List<Sprite>(GameManager.AccessoryCount.Keys); // 키에 해당하는 Sprite 리스트 초기화
        A_Counts = new List<int>(GameManager.AccessoryCount.Values);   // 값에 해당하는 int 리스트 초기화
        A_Count = GameManager.AccessoryCounts;
        A_Level = GameManager.AccessoryShopLevel;
        A_Drawn = GameManager.AccessoryDrawn;

        A_EquipExperience = (float)GameManager.AccessoryEquipExperience;
        A_OwnExperience = (float)GameManager.AccessoryOwnExperience;

        A_OwnIncreased = GameManager.AccessoryOwnExpIncreased;

        W_Sprites = new List<Sprite>(GameManager.itemCounts.Keys); // 키에 해당하는 Sprite 리스트 초기화
        W_Counts = new List<int>(GameManager.itemCounts.Values);   // 값에 해당하는 int 리스트 초기화
        W_Count = GameManager.WeaponCounts;
        W_Level = GameManager.WeaponShopLevel;
        W_Drawn = GameManager.WeaponDrawn;

        W_EquipDMG = (float)GameManager.WeaponEquipDamage;
        W_OwnDMG = (float)GameManager.WeaponOwnDamage;

        W_OwnIncreased = GameManager.weaponOwnDamageIncreased;


        A_ThisEquipNum = ItemManager.Accessory_currentEquipIndex;
        W_ThisEquipNum = ItemManager.Weapon_currentEquipIndex;

        A_ReinforceValue = EquipReinforceManager.AccessoryReinValue;
        W_ReinforceValue = EquipReinforceManager.WeaponReinValue;

        RelicsRank = GameManager.Relics_Rank;
        WarrantLevel = GameManager.WarrantLevel;
    }


}





public class SaveLoadManager : MonoBehaviour
{
    private static SaveLoadManager instance;

    public static SaveLoadManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SaveLoadManager>();
                if (instance == null)
                {
                    Debug.LogError("SaveLoadManager 인스턴스를 찾을 수 없습니다.");
                }
            }
            return instance;
        }
    }

    private string enemySaveFilePath;
    private string PlayerSaveFilePath;
    private string EquipSaveFilePath;
    private string MSSaveFilePath;

    void Awake()
    {
        enemySaveFilePath = Application.persistentDataPath + "/Enemy.json";
        PlayerSaveFilePath = Application.persistentDataPath + "/Player.json";
        EquipSaveFilePath = Application.persistentDataPath + "/Equip.json";
        MSSaveFilePath = Application.persistentDataPath + "/MS.json";
    }

    public void SaveEnemy(EnemySave saveData)
    {
        string json = JsonUtility.ToJson(saveData, true); // 들여쓰기 활성화
        File.WriteAllText(enemySaveFilePath, json);
        Debug.Log("Enemy data saved to: " + enemySaveFilePath);
        Debug.Log("Persistent Data Path: " + Application.persistentDataPath);
    }
    public void SavePlayer(PlayerSave saveData)
    {
        string json = JsonUtility.ToJson(saveData, true); // 들여쓰기 활성화
        File.WriteAllText(PlayerSaveFilePath, json);
    }
    public void SaveEquip(EquipSave saveData)
    {
        string json = JsonUtility.ToJson(saveData, true); // 들여쓰기 활성화
        File.WriteAllText(EquipSaveFilePath, json);
    }
    public void SaveMS(MobScrollSave saveData)
    {
        string json = JsonUtility.ToJson(saveData, true); // 들여쓰기 활성화
        File.WriteAllText(MSSaveFilePath, json);
    }

    public void LoadEnemy()
    {
        if (File.Exists(enemySaveFilePath))
        {
            string json = File.ReadAllText(enemySaveFilePath);
            EnemySave saveData = JsonUtility.FromJson<EnemySave>(json);

            GameManager.MoveInStage = ConvertToBoolArray(saveData.EnemyMoveInStageSave);
            GameManager.PinInStage = ConvertToBoolArray(saveData.EnemyPinInStageSave);
            GameManager.stageClearDict = ConvertToDictionary(saveData.EnemySavestageClearList);

            EnemyManager.Instance.LastBtnIndex = saveData.EnemySaveLastBtnIndex;
            EnemyManager.Instance.FixDifficultyInStage = saveData.EnemySaveFixDifficultyInStage;
            EnemyManager.Instance.FixDifficulty = saveData.EnemySaveFixDifficulty;
            EnemyManager.Instance.MoveDifficultyInStage = saveData.EnemySaveMoveDifficultyInStage;
            EnemyManager.Instance.MoveDifficulty = saveData.EnemySaveMoveDifficulty;

            Debug.Log("로드했음!!!!");
        }
        Debug.Log("일단 실행은 됐음 : " + File.Exists(enemySaveFilePath));
    }

    public void LoadPlayer()
    {
        if (File.Exists(PlayerSaveFilePath))
        {
            string json = File.ReadAllText(PlayerSaveFilePath);
            PlayerSave saveData = JsonUtility.FromJson<PlayerSave>(json);

            GameManager.Player_Damage = (decimal)saveData.Player_BaiscDamage;
            GameManager.Player_AttackSpeed = (decimal)saveData.Player_BaiscAttackSpeed;
            GameManager.Player_CriticalChance = (decimal)saveData.Player_BaiscCriticalChance;
            GameManager.Player_CriticalDamage = (decimal)saveData.Player_BaiscCriticalDamage;
            GameManager.Player_ArmorPenetration = (decimal)saveData.Player_BaiscArmorPenetration;

            GameManager.Player_PassivePoint = saveData.Player_PP;

            GameManager.Player_MoneyUp_EarnMoney = saveData.PMUP_EarnMoney;
            GameManager.Player_MoneyUp_Damage = (decimal)saveData.PMUP_Damage;
            GameManager.Player_MoneyUp_AttackSpeed = (decimal)saveData.PMUP_AttackSpeed;
            GameManager.Player_MoneyUp_CriticalChance = (decimal)saveData.PMUP_CriticalChance;
            GameManager.Player_MoneyUp_CriticalDamage = (decimal)saveData.PMUP_CriticalDamage;
            GameManager.Player_MoneyUp_ArmorPenetration = (decimal)saveData.PMUP_ArmorPenetration;

            GameManager.Player_Level = saveData.Player_Lv;
            GameManager.Player_MaxExp = saveData.Player_maxExp;
            GameManager.Player_CurExp = saveData.Player_curExp;
            GameManager.Player_Money = saveData.Player_money;
            GameManager.Player_Diamond = saveData.Player_diamond;
            GameManager.Player_RedStone = saveData.Player_redstone;

            GameManager.RelicsReinforceScroll = saveData.Player_RelicsScroll;
            GameManager.PlayerHaveMobScroll = saveData.Player_HaveMobScroll;

            GameManager.Player_PPUP_DamageLevel = saveData.Player_PPUP_DMGLv;
            GameManager.Player_PPUP_AttackSpeedLevel = saveData.Player_PPUP_ATSLv;
            GameManager.Player_PPUP_CriticalChanceLevel = saveData.Player_PPUP_CRCLv;
            GameManager.Player_PPUP_CriticalDamageLevel = saveData.Player_PPUP_CRDLv;
            GameManager.Player_PPUP_ArmorPenetrationLevel = saveData.Player_PPUP_ARPLv;

            GameManager.Player_MoneyUp_EarnMoneyLevel = saveData.Player_MoneyUP_ENMLv;
            GameManager.Player_MoneyUp_DamageLevel = saveData.Player_MoneyUP_DMGLv;
            GameManager.Player_MoneyUp_AttackSpeedLevel = saveData.Player_MoneyUP_ATSLv;
            GameManager.Player_MoneyUp_CriticalChanceLevel = saveData.Player_MoneyUP_CRCLv;
            GameManager.Player_MoneyUp_CriticalDamageLevel = saveData.Player_MoneyUP_CRDLv;
            GameManager.Player_MoneyUp_ArmorPenetrationLevel = saveData.Player_MoneyUP_ARPLv;

            GameManager.HaveStarGrey = saveData.StarGrey;
            GameManager.HaveStarBrown = saveData.StarBrown;
            GameManager.HaveStarBlue = saveData.StarBlue;
            GameManager.HaveStarGreen = saveData.StarGreen;
            GameManager.HaveStarRed = saveData.StarRed;
            GameManager.HaveStarYellow = saveData.StarYellow;
            GameManager.HaveStarPurple = saveData.StarPurple;
            GameManager.HaveStarOrange = saveData.StarOrange;
            GameManager.HaveStarDark = saveData.StarDark;

            BossManager.ClearBoss = saveData.C_Boss;
        }
    }

    public void LoadEquip()
    {
        if (File.Exists(EquipSaveFilePath))
        {
            string json = File.ReadAllText(EquipSaveFilePath);
            EquipSave saveData = JsonUtility.FromJson<EquipSave>(json);

            // AccessorySprites 리스트와 AccessoryCounts 리스트의 길이가 같을 때만 데이터를 복원합니다.
            if (saveData.A_Sprites.Count == saveData.A_Counts.Count)
            {
                GameManager.AccessoryCount = new Dictionary<Sprite, int>();

                // AccessorySprites와 AccessoryCounts 리스트를 사용하여 AccessoryCount 딕셔너리를 초기화합니다.
                for (int i = 0; i < saveData.A_Sprites.Count; i++)
                {
                    Sprite sprite = saveData.A_Sprites[i];
                    int count = saveData.A_Counts[i];
                    GameManager.AccessoryCount[sprite] = count;
                }
            }
            else
            {
                Debug.LogError("AccessorySprites and AccessoryCounts lists have different lengths!");
            }

            GameManager.AccessoryCounts = saveData.A_Count;
            GameManager.AccessoryShopLevel = saveData.A_Level;
            GameManager.AccessoryDrawn = saveData.A_Drawn;

            GameManager.AccessoryEquipExperience = (decimal)saveData.A_EquipExperience;
            GameManager.AccessoryOwnExperience = (decimal)saveData.A_OwnExperience;
            GameManager.AccessoryOwnExpIncreased = saveData.A_OwnIncreased;

            if (saveData.W_Sprites.Count == saveData.W_Counts.Count)
            {
                GameManager.itemCounts = new Dictionary<Sprite, int>();

                // AccessorySprites와 AccessoryCounts 리스트를 사용하여 AccessoryCount 딕셔너리를 초기화합니다.
                for (int i = 0; i < saveData.W_Sprites.Count; i++)
                {
                    Sprite sprite = saveData.W_Sprites[i];
                    int count = saveData.W_Counts[i];
                    GameManager.itemCounts[sprite] = count;
                }
            }
            else
            {
                Debug.LogError("WeaponSprites and AccessoryCounts lists have different lengths!");
            }

            GameManager.WeaponCounts = saveData.W_Count;
            GameManager.WeaponShopLevel = saveData.W_Level;
            GameManager.WeaponDrawn = saveData.W_Drawn;

            GameManager.WeaponEquipDamage = (decimal)saveData.W_EquipDMG;
            GameManager.WeaponOwnDamage   = (decimal)saveData.W_OwnDMG;
            GameManager.weaponOwnDamageIncreased = saveData.W_OwnIncreased;

            ItemManager.Accessory_currentEquipIndex = saveData.A_ThisEquipNum;
            ItemManager.Weapon_currentEquipIndex = saveData.W_ThisEquipNum;

            EquipReinforceManager.AccessoryReinValue = saveData.A_ReinforceValue;
            EquipReinforceManager.WeaponReinValue = saveData.W_ReinforceValue;

            GameManager.Relics_Rank = saveData.RelicsRank;
            GameManager.WarrantLevel = saveData.WarrantLevel;
        }
    }

    public void LoadMobScroll()
    {
        if (File.Exists(MSSaveFilePath))
        {
            string json = File.ReadAllText(MSSaveFilePath);
            MobScrollSave saveData = JsonUtility.FromJson<MobScrollSave>(json);

            MobScrollManager.MS_BuyCheck = saveData.MS_BuyChecking;
            MobScrollManager.MS_UpPower = saveData.Save_UpPower;
            MobScrollManager.MS_UpAttackSpeed = saveData.Save_UpAttackSpeed;
            MobScrollManager.MS_UpExp = saveData.Save_UpExp;
            MobScrollManager.MS_UpArmorPenetration = saveData.Save_UpArmorPenetration;
            MobScrollManager.MS_UpEarnGold = saveData.Save_UpEarnGold;
        }
    }

    private bool[][] ConvertToBoolArray(List<string> stringList)
    {
        bool[][] array = new bool[stringList.Count][];
        for (int i = 0; i < stringList.Count; i++)
        {
            string[] rowValues = stringList[i].Split(',');
            array[i] = new bool[rowValues.Length];
            for (int j = 0; j < rowValues.Length; j++)
            {
                array[i][j] = bool.Parse(rowValues[j]);
            }
        }
        return array;
    }
    private Dictionary<string, bool[]> ConvertToDictionary(List<string> list)
    {
        Dictionary<string, bool[]> dict = new Dictionary<string, bool[]>();

        foreach (string serializedPair in list)
        {
            // 각 serializedPair는 "key:value" 형식입니다.
            string[] pair = serializedPair.Split(':');
            string key = pair[0];
            string valueString = pair[1];

            // valueString을 bool 배열로 변환합니다.
            string[] boolStrings = valueString.Split(',');
            bool[] boolArray = Array.ConvertAll(boolStrings, s => s == "1");

            // 딕셔너리에 추가합니다.
            dict.Add(key, boolArray);
        }

        return dict;
    }

}
