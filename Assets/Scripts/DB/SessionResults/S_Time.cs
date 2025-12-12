public struct STime
{
    public int hours;
    public int minutes;
    public int seconds;

    public STime(int hours, int minutes, int seconds)
    {
        this.hours = hours;
        this.minutes = minutes;
        this.seconds = seconds;
    }

    public STime(int totalSeconds)
    {
        hours = totalSeconds / 3600;
        minutes = (totalSeconds % 3600) / 60;
        seconds = totalSeconds % 60;
    }
}