using System;

[Serializable]
public class BallData
{

    public int colorIdx;
    public int posX;
    public int posY;

    public BallData(int colorIdx, int posX, int posY)
    {
        this.colorIdx = colorIdx;
        this.posX = posX;
        this.posY = posY;
    }
}
