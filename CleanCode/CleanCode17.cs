public static void GenerateNewObject()
{
    //Создание объекта на карте
}

public static void RandomizeChance()
{
    _chance = Random.Range(0, 100);
}

public static int GetSalary(int hoursWorked)
{
    return _hourlyRate * hoursWorked;
}
