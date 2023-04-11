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
        for (int i = 0; i < snakeX.Count; i++) // vì set up list snakeX = list snakeY nên chỉ cần duyệt qua độ dài x
        {
            Write(snakeX[i], snakeY[i], "0"); // vẽ thân rắn
        }
    }

    static void DrawFood(int foodX, int foodY)
    {
        Write(foodX, foodY, "@"); // vẽ food
    }

    static void MoveSnake(List<int> snakeX, List<int> snakeY, int direction)
    {
        for (int i = snakeX.Count - 1; i > 0; i--) // duyệt từ đuôi về vị trí kế đầu cho giá trị sau = giá trị trước
        {
            snakeX[i] = snakeX[i - 1];
            snakeY[i] = snakeY[i - 1];
        }
        switch (direction) // check hướng hiện tại để set tọa độ của đầu, lưu ý là chỉ cần set tọa độ đầu thì thân sẽ đi theo, tham khảo vòng for trên
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
        Console.BackgroundColor = ConsoleColor.DarkBlue; // đặt màu nền
        ; // đặt màu cho mồi
        int sizeX = 60; //kích thước màn hình trục x
        int sizeY = 20; //kích thước màn hình trục y
        bool isOver = false; //biến check game over
        int foodX = 40; //tọa độ x của food
        int foodY = 10; //tọa độ y của food
        Random rand = new Random(); //biến để tạo random vị trí food
        int scoreX = 0; //biến lưu điêm
        int scoreY = sizeY + 1; //tọa độ x của điểm
        int score = 0; //tọa độ y của điểm
        List<int> snakeX = new List<int>() { 32, 31, 30 }; //danh sách tọa độ x của rắn (phần tử 0 là đầu)
        List<int> snakeY = new List<int>() { 12, 12, 12 }; //danh sách tọa độ y của rắn (phần tử 0 là đầu)
        int direction = 3; //biến lưu hướng (0 là up, 1 là left, 2 là down, 3 là right)
        // Game loop
        while (!isOver)
        {
            MoveSnake(snakeX, snakeY, direction); //Move snake

            // Check nếu snake chạm tường thì sẽ dịch chuyển đầu sang đối diện
            if (snakeX[0] <= 0 || snakeX[0] >= sizeX) // Check tọa độ x của đầu rắn nếu nhỏ hơn 0 hoặc lớn hơn kích thước màn hình
            {
                int temp = snakeX[0] - sizeX;
                snakeX[0] = Math.Abs(temp) + (temp >= 0 ? 1 : -1); // gán tọa độ x của đầu rắn cho phía đối diện
            }
            if (snakeY[0] == 0 || snakeY[0] == sizeY) // Check tọa độ y của đầu rắn nếu nhỏ hơn 0 hoặc lớn hơn kích thước màn hình
            {
                int temp = snakeY[0] - sizeY;
                snakeY[0] = Math.Abs(temp) + (temp >= 0 ? 1 : -1); // gán tọa độ y của đầu rắn cho phía đối diện
            }

            // check nếu rắn tự cắn mình
            for (int i = 1; i < snakeX.Count; i++)
            {
                if (snakeX[0] == snakeX[i] && snakeY[0] == snakeY[i])
                {
                    isOver = true;
                }
            }

            // check nếu rắn ăn food
            if (snakeX[0] == foodX && snakeY[0] == foodY)
            {
                score++; // cộng điểm
                foodX = rand.Next(1, sizeX - 1); // gán ngẫu nhiên tọa độ x cho food
                foodY = rand.Next(1, sizeY - 1); // gán ngẫu nhiên tọa độ y cho food
                snakeX.Add(snakeX[snakeX.Count - 1]); // thêm tọa độ x đuôi cho rắn
                snakeY.Add(snakeY[snakeY.Count - 1]); // thêm tọa độ y đuôi cho rắn
            }
            if (Console.KeyAvailable) // check nếu có phím nhập
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                switch (keyInfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (direction != 0) direction = 2; // chỉ đổi hướng nếu không phải đang up, rắn không thể di chuyển ngược
                        break;
                    case ConsoleKey.UpArrow:
                        if (direction != 2) direction = 0; // chỉ đổi hướng nếu không phải đang down, rắn không thể di chuyển ngược
                        break;
                    case ConsoleKey.LeftArrow:
                        if (direction != 3) direction = 1; // chỉ đổi hướng nếu không phải đang right, rắn không thể di chuyển ngược
                        break;
                    case ConsoleKey.RightArrow:
                        if (direction != 1) direction = 3; // chỉ đổi hướng nếu không phải đang left, rắn không thể di chuyển ngược
                        break;
                    case ConsoleKey.Escape:
                        isOver = true;
                        break;
                }
            }

            // vẽ giao diện
            Console.Clear();
            DrawBorder(sizeX, sizeY);
            DrawSnake(snakeX, snakeY);
            DrawFood(foodX, foodY);
            DrawScore(scoreX, scoreY, score);
            System.Threading.Thread.Sleep(50); // tốc độ game
        }
    }
}
