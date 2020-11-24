using System;

[Serializable]
public class SaveData
{
    public int[] _amount;
    public int[] _background;
    public int[] _asteroidType;
    public int CurrentLevel 
    { 
        set
        {
            _currentLevel = value >= _currentLevel ? value : _currentLevel; 
        }
        get
        {
            return _currentLevel;
        }
    }
    private int _currentLevel;
    public SaveData()
    {
        _amount = new int[3];
        for(int i = 0; i<_amount.Length;i++)
        {
            _amount[i] = 0;
        }
        _background = new int[3];
        _asteroidType = new int[3];
    }
    public void SetData(int amount,int background,int asteroidType,int level)
    {
        _amount[level] = amount;
        _background[level] = background;
        _asteroidType[level] = asteroidType;
    }


}
