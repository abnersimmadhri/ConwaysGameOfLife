namespace ConwaysGameOfLife.GameEngine
{
    public interface ICell
    {
        States state { get; set; }
        Points Location { get; set; }
        void Draw();
    }

    public enum States
    {
        Alive,
        Dead
    }
}