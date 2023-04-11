namespace SnakeGame;
class Program
{
    static void Write(int x, int y, string c)
    {
        Console.SetCursorPosition(x, y);
        Console.Write(c);
    }
    static void DrawBorder(int width, int height)
    {
        for (int i = 0; i <= height; i++)
        {
            for (int j = 0; j <= width; j++)
            {
                if (i == 0 || i == height)
                {
                    Write(j, i, "═");
                }
                if (j == 0 || j == width)
                {
                    Write(j, i, "║");
                }
            }
        }
        Write(0, 0, "╔");
        Write(width, 0, "╗");
        Write(width, height, "╝");
        Write(0, height, "╚");
    }


    static void DrawSnake(List<int> snakeX, List<int> snakeY)
    {
        for (int i = 0; i < snakeX.Count; i++)
        {
            Write(snakeX[i], snakeY[i], "0");
        }
    }

    static void DrawFood(int foodX, int foodY)
    {
        Write(foodX, foodY, "@");
    }

    static void MoveSnake(List<int> snakeX, List<int> snakeY, int direction)
    {
        for (int i = snakeX.Count - 1; i > 0; i--)
        {
            snakeX[i] = snakeX[i - 1];
            snakeY[i] = snakeY[i - 1];
        }
        switch (direction)
        {
            case 0:
                snakeY[0]--;
                break;
            case 1:
                snakeX[0]--;
                break;
            case 2:
                snakeY[0]++;
                break;
            case 3:
                snakeX[0]++;
                break;
            default:
                break;
        }
    }


    static void DrawScore(int scoreX, int scoreY, int score)
    {
        Write(scoreX, scoreY, $"Score: {score}");
    }
    static void Main(string[] args)
    {
        Console.SetCursorPosition(5, 5); // đặt vị trí của rắn hoặc mồi tại tọa độ (x, y)
        Console.ForegroundColor = ConsoleColor.White; // đặt màu cho rắn
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        ; // đặt màu cho mồi
        int sizeX = 60;
        int sizeY = 20;
        bool isOver = false;
        int foodX = 40;
        int foodY = 10;
        Random rand = new Random();
        int scoreX = 0;
        int scoreY = sizeY + 1;
        int score = 0;
        List<int> snakeX = new List<int>() { 32, 31, 30 };
        List<int> snakeY = new List<int>() { 12, 12, 12 };
        int direction = 3;
        while (!isOver)
        {
            MoveSnake(snakeX, snakeY, direction);

            if (snakeX[0] <= 0 || snakeX[0] >= sizeX)
            {
                int temp = snakeX[0] - sizeX;
                snakeX[0] = Math.Abs(temp) + (temp >= 0 ? 1 : -1);
            }
            if (snakeY[0] == 0 || snakeY[0] == sizeY)
            {
                int temp = snakeY[0] - sizeY;
                snakeY[0] = Math.Abs(temp) + (temp >= 0 ? 1 : -1);
            }

            for (int i = 1; i < snakeX.Count; i++)
            {
                if (snakeX[0] == snakeX[i] && snakeY[0] == snakeY[i])
                {
                    isOver = true;
                }
            }

            if (snakeX[0] == foodX && snakeY[0] == foodY)
            {
                score++;
                foodX = rand.Next(1, sizeX - 1);
                foodY = rand.Next(1, sizeY - 1);
                snakeX.Add(snakeX[snakeX.Count - 1]);
                snakeY.Add(snakeY[snakeY.Count - 1]);
            }
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (direction != 0) direction = 2;
                        break;
                    case ConsoleKey.UpArrow:
                        if (direction != 2) direction = 0;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (direction != 3) direction = 1;
                        break;
                    case ConsoleKey.RightArrow:
                        if (direction != 1) direction = 3;
                        break;
                    case ConsoleKey.Escape:
                        isOver = true;
                        break;
                }
            }
            Console.Clear();
            DrawBorder(sizeX, sizeY);
            DrawSnake(snakeX, snakeY);
            DrawFood(foodX, foodY);
            DrawScore(scoreX, scoreY, score);
            System.Threading.Thread.Sleep(50);
        }
    }

}
