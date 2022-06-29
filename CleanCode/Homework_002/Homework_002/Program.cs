public static int GetNumberInBorders(int number, int leftBorder, int rightBorder)
{
    if (number < leftBorder)
        return leftBorder;
    else if (number > rightBorder)
        return rightBorder;
    else
        return number;
}