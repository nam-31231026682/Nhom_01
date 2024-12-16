using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Towel;
using Towel.DataStructures;
using static Towel.Statics;

#region Ascii

// ╔═══════════════════╦═══════════════════╗
// ║ · · · · · · · · · ║ · · · · · · · · · ║
// ║ · ╔═╗ · ╔═════╗ · ║ · ╔═════╗ · ╔═╗ · ║
// ║ + ╚═╝ · ╚═════╝ · ╨ · ╚═════╝ · ╚═╝ + ║
// ║ · · · · · · · · · · · · · · · · · · · ║
// ║ · ═══ · ╥ · ══════╦══════ · ╥ · ═══ · ║
// ║ · · · · ║ · · · · ║ · · · · ║ · · · · ║
// ╚═════╗ · ╠══════   ╨   ══════╣ · ╔═════╝
//       ║ · ║                   ║ · ║
// ══════╝ · ╨   ╔════---════╗   ╨ · ╚══════
//         ·     ║ █ █   █ █ ║     ·
// ══════╗ · ╥   ║           ║   ╥ · ╔══════
//       ║ · ║   ╚═══════════╝   ║ · ║
//       ║ · ║       READY       ║ · ║
// ╔═════╝ · ╨   ══════╦══════   ╨ · ╚═════╗
// ║ · · · · · · · · · ║ · · · · · · · · · ║
// ║ · ══╗ · ═══════ · ╨ · ═══════ · ╔══ · ║
// ║ + · ║ · · · · · · █ · · · · · · ║ · + ║-
// ╠══ · ╨ · ╥ · ══════╦══════ · ╥ · ╨ · ══╣
// ║ · · · · ║ · · · · ║ · · · · ║ · · · · ║
// ║ · ══════╩══════ · ╨ · ══════╩══════ · ║
// ║ · · · · · · · · · · · · · · · · · · · ║
// ╚═══════════════════════════════════════╝

internal class Program
{
    private static void Main(string[] args)
    {
        string WallsString =
    "╔═══════════════════╦═══════════════════╗\n" +
    "║                   ║                   ║\n" +
    "║   ╔═╗   ╔═════╗   ║   ╔═════╗   ╔═╗   ║\n" +
    "║   ╚═╝   ╚═════╝   ╨   ╚═════╝   ╚═╝   ║\n" +
    "║                                       ║\n" +
    "║   ═══   ╥   ══════╦══════   ╥   ═══   ║\n" +
    "║         ║         ║         ║         ║\n" +
    "╚═════╗   ╠══════   ╨   ══════╣   ╔═════╝\n" +
    "      ║   ║                   ║   ║      \n" +
    "══════╝   ╨   ╔════   ════╗   ╨   ╚══════\n" +
    "              ║           ║              \n" +
    "══════╗   ╥   ║           ║   ╥   ╔══════\n" +
    "      ║   ║   ╚═══════════╝   ║   ║      \n" +
    "      ║   ║                   ║   ║      \n" +
    "╔═════╝   ╨   ══════╦══════   ╨   ╚═════╗\n" +
    "║                   ║                   ║\n" +
    "║   ══╗   ═══════   ╨   ═══════   ╔══   ║\n" +
    "║     ║                           ║     ║\n" +
    "╠══   ╨   ╥   ══════╦══════   ╥   ╨   ══╣\n" +
    "║         ║         ║         ║         ║\n" +
    "║   ══════╩══════   ╨   ══════╩══════   ║\n" +
    "║                                       ║\n" +
    "╚═══════════════════════════════════════╝";

        string GhostWallsString =
            "╔═══════════════════╦═══════════════════╗\n" +
            "║█                 █║█                 █║\n" +
            "║█ █╔═╗█ █╔═════╗█ █║█ █╔═════╗█ █╔═╗█ █║\n" +
            "║█ █╚═╝█ █╚═════╝█ █╨█ █╚═════╝█ █╚═╝█ █║\n" +
            "║█                                     █║\n" +
            "║█ █═══█ █╥█ █══════╦══════█ █╥█ █═══█ █║\n" +
            "║█       █║█       █║█       █║█       █║\n" +
            "╚═════╗█ █╠══════█ █╨█ █══════╣█ █╔═════╝\n" +
            "██████║█ █║█                 █║█ █║██████\n" +
            "══════╝█ █╨█ █╔════█ █════╗█ █╨█ █╚══════\n" +
            "             █║█         █║█             \n" +
            "══════╗█  ╥█ █║███████████║█ █╥█ █╔══════\n" +
            "██████║█  ║█ █╚═══════════╝█ █║█ █║██████\n" +
            "██████║█  ║█                 █║█ █║██████\n" +
            "╔═════╝█  ╨█ █══════╦══════█ █╨█ █╚═════╗\n" +
            "║█                 █║█                 █║\n" +
            "║█ █══╗█ █═══════█ █╨█ █═══════█ █╔══█ █║\n" +
            "║█   █║█                         █║█   █║\n" +
            "╠══█ █╨█ █╥█ █══════╦══════█ █╥█ █╨█ █══╣\n" +
            "║█       █║█       █║█       █║█       █║\n" +
            "║█ █══════╩══════█ █╨█ █══════╩══════█ █║\n" +
            "║█                                     █║\n" +
            "╚═══════════════════════════════════════╝";

        string DotsString =
            "                                         \n" +
            "  · · · · · · · · ·   · · · · · · · · ·  \n" +
            "  ·     ·         ·   ·         ·     ·  \n" +
            "  +     ·         ·   ·         ·     +  \n" +
            "  · · · · · · · · · · · · · · · · · · ·  \n" +
            "  ·     ·   ·               ·   ·     ·  \n" +
            "  · · · ·   · · · ·   · · · ·   · · · ·  \n" +
            "        ·                       ·        \n" +
            "        ·                       ·        \n" +
            "        ·                       ·        \n" +
            "        ·                       ·        \n" +
            "        ·                       ·        \n" +
            "        ·                       ·        \n" +
            "        ·                       ·        \n" +
            "        ·                       ·        \n" +
            "  · · · · · · · · ·   · · · · · · · · ·  \n" +
            "  ·     ·         ·   ·         ·     ·  \n" +
            "  + ·   · · · · · ·   · · · · · ·   · +  \n" +
            "    ·   ·   ·               ·   ·   ·    \n" +
            "  · · · ·   · · · ·   · · · ·   · · · ·  \n" +
            "  ·               ·   ·               ·  \n" +
            "  · · · · · · · · · · · · · · · · · · ·  \n" +
            "                                         ";
        string[] PacManAnimations =
        [
            "\"' '\"",
            "n. .n",
            ")>- ->",
            "(<- -<",
        ];

        #endregion Ascii

        int OriginalWindowWidth = Console.WindowWidth;
        int OriginalWindowHeight = Console.WindowHeight;
        ConsoleColor OriginalBackgroundColor = Console.BackgroundColor;
        ConsoleColor OriginalForegroundColor = Console.ForegroundColor;
        string playerName = string.Empty;

        void ShowLogo()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(45, 5);
            Console.WriteLine("╔════════════════════════════════════╗");
            Console.SetCursorPosition(45, 6);
            Console.WriteLine("║              PAC-MAN !             ║");
            Console.SetCursorPosition(45, 7);
            Console.WriteLine("╚════════════════════════════════════╝");
            // Hien thi Pac-Man duoi dang ASCII
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            // ho tro hien thi tat ca cac ki tu, ki hieu
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(53, 8);
            Console.WriteLine("⠀⠀⠀⠀⣀⣤⣴⣶⣶⣶⣦⣤⣀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀");
            Console.SetCursorPosition(53, 9);
            Console.WriteLine("⠀⠀⣠⣾⣿⣿⣿⣿⣿⣿⢿⣿⣿⣷⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀");
            Console.SetCursorPosition(53, 10);
            Console.WriteLine("⢀⣾⣿⣿⣿⣿⣿⣿⣿⣅⢀⣽⣿⣿⡿⠃⠀⠀⠀⠀⠀⠀⠀⠀");
            Console.SetCursorPosition(53, 11);
            Console.WriteLine("⣼⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠛⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀");
            Console.SetCursorPosition(53, 12);
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⠛⠁⠀⠀⣴⣶⡄⠀⣶⣶⡄⠀⣴⣶⡄");
            Console.SetCursorPosition(53, 13);
            Console.WriteLine("⣿⣿⣿⣿⣿⣿⣿⣿⣷⣦⣀⠀⠙⠋⠁⠀⠉⠋⠁⠀⠙⠋⠀");
            Console.SetCursorPosition(53, 14);
            Console.WriteLine("⠸⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣦⣄⠀⠀⠀⠀⠀⠀⠀⠀⠀");
            Console.SetCursorPosition(53, 15);
            Console.WriteLine("⠀⠙⢿⣿⣿⣿⣿⣿⣿⣿⣿⣿⣿⡿⠃⠀⠀⠀⠀⠀⠀⠀⠀");
            Console.SetCursorPosition(53, 16);
            Console.WriteLine("⠀⠀⠈⠙⠿⣿⣿⣿⣿⣿⣿⠿⠋⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀");
            Console.SetCursorPosition(53, 17);
            Console.WriteLine("⠀⠀⠀⠀⠀⠀⠉⠉⠉⠉⠁⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀⠀");
            Console.SetCursorPosition(48, 18);
            Console.WriteLine("Nhan nut bat ki de tiep tuc...");
            // Cho nguoi dung nhan 1 phim bat ky
            Console.ReadKey(true);
            Console.Clear();
        }

        void AskName()
        {
            while (string.IsNullOrWhiteSpace(playerName)) // loop cho den khi user input gia tri
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(45, 5);
                Console.WriteLine("╔════════════════════════════════════╗");
                Console.SetCursorPosition(45, 6);
                Console.Write("║ Nhap ten:                          ║");
                Console.SetCursorPosition(45, 7);
                Console.WriteLine("╚════════════════════════════════════╝");
                Console.SetCursorPosition(45 + 12, 6); // Dat con tro ve vi tri sau "Nhap ten:"
                Console.ForegroundColor = ConsoleColor.White;
                playerName = Console.ReadLine(); // Read user input
                Console.ForegroundColor = ConsoleColor.Yellow;

                if (string.IsNullOrWhiteSpace(playerName)) // kiem tra input cua user
                {
                    Console.SetCursorPosition(45, 8);
                    Console.WriteLine("Xin hay nhap ten!"); // bao loi input cho user
                    System.Threading.Thread.Sleep(1350); // doi 1.35s roi xoa thong bao loi
                }
            }
        }

        void ShowMenu()
        {
            LoadLeaderboard(); //tai bang xep hang
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(35, 4);
                Console.WriteLine("╔════════════════════════════════════════════════╗");
                Console.SetCursorPosition(35, 5);
                Console.WriteLine("║    Chao nguoi choi: ");
                Console.SetCursorPosition(62, 5);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(playerName); //hien thi ten user da nhap
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(70, 5);
                Console.WriteLine("              ║");
                Console.SetCursorPosition(35, 6);
                Console.WriteLine("║                                                ║");
                Console.SetCursorPosition(35, 7);
                Console.WriteLine("║    [1] Bat dau tro choi                        ║");
                Console.SetCursorPosition(35, 8);
                Console.WriteLine("║    [2] Huong dan                               ║");
                Console.SetCursorPosition(35, 9);
                Console.WriteLine("║    [3] Bang xep hang                           ║");
                Console.SetCursorPosition(35, 10);
                Console.WriteLine("║    [4] Thoat game                              ║");
                Console.SetCursorPosition(35, 11);
                Console.WriteLine("║                                                ║");
                Console.SetCursorPosition(35, 12);
                Console.WriteLine("║                                                ║");
                Console.SetCursorPosition(35, 13);
                Console.WriteLine("║                                                ║");
                Console.SetCursorPosition(40, 12);
                Console.Write("Chon: ");
                Console.SetCursorPosition(35, 14);
                Console.Write("╚════════════════════════════════════════════════╝");
                Console.ResetColor();
                ConsoleKey choice = Console.ReadKey(true).Key;
                switch (choice)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.SetCursorPosition(40, 11);
                        Console.WriteLine("Dang tai...");
                        Console.SetCursorPosition(52, 11);
                        Thread.Sleep(1100); //tao cam giac load game
                        return; // thoat menu va qua game
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.SetCursorPosition(15, 1);
                        Console.WriteLine("╔═══════════════════════════════════════════════════════════════════════════════════════════════════╗");
                        Console.SetCursorPosition(15, 2);
                        Console.WriteLine("║                                                                                                   ║");
                        Console.SetCursorPosition(15, 3);
                        Console.WriteLine("║   1. De bat dau tro choi, nhan nut di chuyen trai hoac phai.                                      ║");
                        Console.SetCursorPosition(15, 4);
                        Console.WriteLine("║   2. Nhiem vu cua ban la dieu khien PacMan an het cac hat diem (.)                                ║");
                        Console.SetCursorPosition(15, 5);
                        Console.WriteLine("║      va hat nang luong (+). Neu Ma bat duoc ban 1 lan, man choi se ket thuc.                      ║");
                        Console.SetCursorPosition(15, 6);
                        Console.WriteLine("║   3. Khi PacMan an duoc hat nang luong (+), Ma se bi te liet va PacMan co the cham vao chung.     ║");
                        Console.SetCursorPosition(15, 7);
                        Console.WriteLine("║      Ma se quay tro ve vi tri ban dau khi bi cham.                                                ║");
                        Console.SetCursorPosition(15, 8);
                        Console.WriteLine("║   4. PacMan se thang khi an het cac hat tren ban do.                                              ║");
                        Console.SetCursorPosition(15, 9);
                        Console.WriteLine("║                                                                                                   ║");
                        Console.SetCursorPosition(15, 10);
                        Console.Write("╚═══════════════════════════════════════════════════════════════════════════════════════════════════╝");
                        Console.SetCursorPosition(15, 12);
                        Console.WriteLine("Nhan nut bat ki de quay ve Menu...");
                        Console.SetCursorPosition(49, 12);
                        Console.ReadKey(true);
                        break;

                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        Console.Clear();
                        ShowLeaderboard();
                        Console.WriteLine("\nNhan nut bat ki de quay ve Menu...");
                        Console.SetCursorPosition(34, 16);
                        Console.ReadKey(true); // Cho user input
                        break;

                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        Console.Clear();
                        Console.SetCursorPosition(30, 5);
                        Console.WriteLine("Hen gap lai!");
                        Console.SetCursorPosition(44, 5);
                        Thread.Sleep(1100); //hien thi thong bao trong 1s
                        Environment.Exit(0); //thoat game
                        break;

                    default:
                        int positionX = 48;// neu nhap khac 1, 2, 3, 4 thi de con tro chuot sau "Chon: "
                        int positionY = 12;
                        Console.SetCursorPosition(positionX, positionY);
                        break;
                }
            }
        }

        void BackgroundMusic(string filePath)
        {
            try
            {
                using (var audioFile = new AudioFileReader(filePath))
                using (var outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(audioFile);
                    audioFile.Volume = 0.2f; // de nhac nen o muc 20%
                    outputDevice.Play();
                    // Loop the music
                    while (true)
                    {
                        if (audioFile.Position >= audioFile.Length)
                        {
                            audioFile.Position = 0; // Restart music
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing background music: {ex.Message}");
            }
        }

        void EatingDotSound()
        {
            string eatSoundFile = "eatingSound.wav";
            // phat am thanh doc lap voi logic cua game
            Task.Run(() =>
            {
                try
                {
                    //NAudio doc file am thanh de kiem tra co phu hop khong
                    using (var eatingSound = new AudioFileReader(eatSoundFile))
                    //dieu khien phat am thanh qua loa
                    using (var eatingPlayer = new WaveOutEvent())
                    {
                        eatingPlayer.Init(eatingSound);
                        eatingPlayer.Play(); //phat nhac
                        while (eatingPlayer.PlaybackState == PlaybackState.Playing)
                        {
                            Thread.Sleep(10); //tam dung 10ms de am thanh duoc thong suot
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Loi khi bat am thanh an cham: {ex.Message}");
                }
            });
        }

        void DeadSound()
        {
            Task.Run(() =>
            {
                try
                {
                    string deadSoundFile = "DeadSound.wav";
                    //NAudio doc file am thanh de kiem tra co phu hop khong
                    using (var deadSound = new AudioFileReader(deadSoundFile))
                    using (var player = new WaveOutEvent())
                    {
                        player.Init(deadSound);
                        player.Play();
                        while (player.PlaybackState == PlaybackState.Playing)
                        {
                            Thread.Sleep(10); //tam dung 10ms de am thanh duoc thong suot
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Loi khi bat am thanh chet: {ex.Message}");
                }
            });
        }

        Stopwatch gameTimer = new Stopwatch(); // dem thoi gian choi
        void ShowGameTimer()
        {
            int timerX = 10; // truc X
            int timerY = 23; // truc Y
            TimeSpan elapsed = gameTimer.Elapsed; Console.SetCursorPosition(timerX, timerY);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Thoi gian choi: {elapsed.Hours:D2}:{elapsed.Minutes:D2}:{elapsed.Seconds:D2}");
        }

        List<(string playerName, int Score)> Leaderboard = new();

        void ShowLeaderboard()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("      ╔═══════════════════════╗");
            Console.WriteLine("      ║     BANG XEP HANG     ║");
            Console.WriteLine("      ╚═══════════════════════╝");
            if (Leaderboard.Count == 0)
            {
                Console.WriteLine("Chua co thong tin");
            }
            else
            {
                Console.WriteLine("{0, 4} {1,-25} {2,5}", "Hang", "Ten", "Diem");
                Console.WriteLine(new string('-', 35));
                int rank = 1;
                foreach (var (playerName, Score) in Leaderboard)
                {
                    // tu dong gioi han ten neu qua dai
                    string formattedName = playerName.Length > 20 ? playerName.Substring(0, 15) + "..." : playerName;
                    Console.WriteLine("{0,4} {1,-25} {2,5}", rank, formattedName, Score);
                    rank++;
                }
            }
        }

        void UpdateLeaderboard(string playerName, int score)
        {
            if (!string.IsNullOrWhiteSpace(playerName) && score > 0)
            {
                Leaderboard.Add((playerName, score));
                Leaderboard = Leaderboard
                .OrderByDescending(record => record.Score)
                .Take(10)
                .ToList();
                SaveLeaderboard();
            }
        }

        void SaveLeaderboard()
        {
            string filePath = @"C:\Users\nhn30\source\repos\PacMan_UseThis\leaderboard\leaderboard.txt";
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            // Luu bang xep hang moi vao file nhu tren duong dan
            File.WriteAllLines(filePath, Leaderboard.Select(record => $"{record.playerName},{record.Score}"));
        }

        void LoadLeaderboard()
        {
            string filePath = @"C:\Users\nhn30\source\repos\PacMan_UseThis\leaderboard\leaderboard.txt";
            try
            {
                if (File.Exists(filePath))
                {
                    Leaderboard = File.ReadAllLines(filePath)
                    .Select(line => line.Split(','))
                    .Where(parts => parts.Length == 2 && int.TryParse(parts[1], out _))
                    .Select(parts => (Name: parts[0].Trim(), Score: int.Parse(parts[1].Trim())))
                    .OrderByDescending(record => record.Score)
                    .Take(10)
                    .ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading leaderboard: {ex.Message}");
                Leaderboard = new List<(string Name, int Score)>();
            }
        }

        char[,] Dots; //Mang 2 chieu de luu vi tri cac diem tren ban do
        int Score; //bien luu tru diem cua nguoi choi
        (int X, int Y) PacManPosition; // Toa do hien tai cua PacMan
        Direction? PacManMovingDirection = default; // Huong di chuyen hien tai cua PacMan
        int? PacManMovingFrame = default; // Khung hinh hien tai trong qua trinh di chuyen cua PacMan
        const int FramesToMoveHorizontal = 8; // PacMan di chuyen theo chieu ngang 1 o mat 8 frame
        const int FramesToMoveVertical = 10; // PacMan di chuyen theo chieu doc 1 o mat 10 frame
        Ghost[] Ghosts; //tạo mảng
        const int GhostWeakTime = 150; //Ma bi yeu trong 150 khung hinh, tuong duong gan 5 giay
        (int X, int Y)[] Locations = GetLocations();
        Console.Clear(); //xoa man hinh

        ShowLogo(); //
        Task.Run(() => BackgroundMusic("background.wav"));
        AskName();
        ShowMenu();
        try
        {
            if (OperatingSystem.IsWindows())
            {
                Console.WindowWidth = 100;
                Console.WindowHeight = 30;
            }
            Console.CursorVisible = false;
            Console.BackgroundColor = ConsoleColor.Black; //mau nen
            Console.ForegroundColor = ConsoleColor.Yellow; //mau chu

        NextRound:
            Score = 0;
            Console.Clear();
            SetUpDots(); //Gọi hàm để tạo .
            PacManPosition = (20, 17);

            Ghost a = new();
            a.Position = a.StartPosition = (16, 10);
            a.Color = ConsoleColor.Red;
            a.FramesToUpdate = 8;
            a.Update = () => UpdateGhost(a);

            Ghost b = new();
            b.Position = b.StartPosition = (18, 10);
            b.Color = ConsoleColor.DarkGreen;
            b.Destination = GetRandomLocation();
            b.FramesToUpdate = 10;
            b.Update = () => UpdateGhost(b);

            Ghost c = new();
            c.Position = c.StartPosition = (22, 10);
            c.Color = ConsoleColor.Magenta;
            c.FramesToUpdate = 8;
            c.Update = () => UpdateGhost(c);

            Ghost d = new();
            d.Position = d.StartPosition = (24, 10);
            d.Color = ConsoleColor.DarkCyan;
            d.Destination = GetRandomLocation();
            d.FramesToUpdate = 10;
            d.Update = () => UpdateGhost(d);

            Ghosts = [a, b, c, d,];

            RenderWalls(); //hien tuong
            RenderGate(); //hien cong
            RenderDots(); //hien diem
            RenderReady(); //hien màn hình ready
            RenderPacMan(); //hien PacMan
            RenderGhosts(); //hien Ma
            RenderScore(); //Hien diem
            ShowGameTimer(); //hien thoi gian choi
            if (GetStartingDirectionInput())
            {
                return; // nguoi dung nhan Escape
            }
            PacManMovingFrame = 0;
            EraseReady();
            gameTimer.Start();
            while (CountDots() > 0) //tiếp tục chạy khi . > 0
            {
                if (HandleInput())
                {
                    return; // user hit escape
                }
                UpdatePacMan(); //cập nhật trạng thái
                UpdateGhosts();
                RenderScore();
                RenderDots();
                RenderPacMan();
                RenderGhosts();
                ShowGameTimer();
                foreach (Ghost ghost in Ghosts)
                {
                    if (ghost.Position == PacManPosition)
                    {
                        if (ghost.Weak) //nếu Ghost đang Weak thì
                        {
                            ghost.Position = ghost.StartPosition;
                            ghost.Weak = false;
                            Score += 10;
                        }
                        else
                        {
                            DeadSound();
                            Console.SetCursorPosition(0, 24);
                            Console.WriteLine("Game Over!");
                            Console.WriteLine("[Enter] choi lai  [Escape] thoat || [M] Quay ve Menu ");
                            UpdateLeaderboard(playerName, Score); // Pass ket qua gom: ten nguoi choi, diem
                            SaveLeaderboard(); // luu bang xep hang moi voi ket qua vua co
                            while (true)
                            {
                                var key = Console.ReadKey(true).Key;
                                switch (key)
                                {
                                    case ConsoleKey.Enter: // choi lai game
                                        goto NextRound;
                                    case ConsoleKey.Escape: // thoat game
                                        Console.Clear();
                                        Console.WriteLine("Goodbye!");
                                        Thread.Sleep(1000);
                                        Environment.Exit(0); //thoat chuong trinh
                                        break;

                                    case ConsoleKey.M: // quay lai menu
                                        Console.Clear();
                                        ShowMenu(); //  khoi tao menu
                                        return; // thoat loop game va quay ve Menu
                                    default:
                                        Console.WriteLine("Nhap lai!");
                                        break;
                                }
                            }
                        }
                    }
                }
                Thread.Sleep(TimeSpan.FromMilliseconds(15)); //ngat 15ms giua cac loop.
                                                             //fps = 1000/15 = 66
            }
            goto NextRound;
        }
        finally //don dep va khoi phuc cai dat
        {
            Console.CursorVisible = false;
            if (OperatingSystem.IsWindows())
            {
                Console.WindowWidth = OriginalWindowWidth;
                Console.WindowHeight = OriginalWindowHeight;
            }
            Console.BackgroundColor = OriginalBackgroundColor;
            Console.ForegroundColor = OriginalForegroundColor;
            Score = 0;
        }

        bool GetStartingDirectionInput()
        {
        GetInput: //chờ input từ người chơi rồi mới bắt đầu
            ConsoleKey key = Console.ReadKey(true).Key; //đọc user input và xử lí
            switch (key) //xử lí
            {
                case ConsoleKey.LeftArrow: PacManMovingDirection = Direction.Left; break;
                case ConsoleKey.RightArrow: PacManMovingDirection = Direction.Right; break;
                case ConsoleKey.Escape: Console.Clear(); Console.Write("Thoat game!"); return true;
                default: goto GetInput; //user input không hợp lệ thì quay lại chờ input khác
            }
            return false; //khi user ấn escape
        }

        bool HandleInput()  //trả về true nếu nhấn phím Escape.
                            //Trả về false nếu không có phím yêu cầu thoát trò chơi nào được nhấn.
        {
            bool moved = false;
            void TrySetPacManDirection(Direction direction)
            {
                if (!moved && //dam bao PacMan chi di chuyen theo 1 huong moi frame
                    PacManMovingDirection != direction && //hướng mới phải # hướng đang di chuyển
                    CanMove(PacManPosition.X, PacManPosition.Y, direction)) //check đường đi có avai ko
                {
                    PacManMovingDirection = direction; //update hướng di chuyển
                    PacManMovingFrame = 0; //khi chuyen huong, animation cua PacMan reset
                    moved = true;
                }
            }
            while (Console.KeyAvailable) //kiem tra tat ca input tu user
            {
                switch (Console.ReadKey(true).Key) //xu li input tu nguoi choi
                {
                    case ConsoleKey.UpArrow: TrySetPacManDirection(Direction.Up); break;
                    case ConsoleKey.DownArrow: TrySetPacManDirection(Direction.Down); break;
                    case ConsoleKey.LeftArrow: TrySetPacManDirection(Direction.Left); break;
                    case ConsoleKey.RightArrow: TrySetPacManDirection(Direction.Right); break;
                    case ConsoleKey.Escape:
                        Console.Clear();
                        Console.Write("Ban da an Escape. Thoat tro choi!");
                        Console.ReadKey();
                        return true;
                }
            }
            return false;
        }

        //x là cột, y là hàng
        char BoardAt(int x, int y) => WallsString[y * 42 + x];

        bool IsWall(int x, int y) => BoardAt(x, y) is not ' ';

        bool CanMove(int x, int y, Direction direction) => direction switch
        {
            Direction.Up => //check 3 ô phía trên
                !IsWall(x - 1, y - 1) && //ô trên trái
                !IsWall(x, y - 1) &&  //ô trên giữa
                !IsWall(x + 1, y - 1), //ô trên phải
            Direction.Down =>
                !IsWall(x - 1, y + 1) && //ô dưới trái
                !IsWall(x, y + 1) && //ô dưới giữa
                !IsWall(x + 1, y + 1), //ô dưới phải
            Direction.Left =>
                x - 2 < 0 || !IsWall(x - 2, y), //0 là border trái của map
            Direction.Right =>
                x + 2 > 40 || !IsWall(x + 2, y), //40 là border phải của map
            _ => throw new NotImplementedException(),
        };

        void SetUpDots()
        {
            string[] rows = DotsString.Split("\n"); //chia DotsString thành các hàng
            int rowCount = rows.Length;
            int columnCount = rows[0].Length;
            Dots = new char[columnCount, rowCount]; //tạo ma trận Dots
            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; column++)
                {
                    Dots[column, row] = rows[row][column];
                }
            }
        }

        int CountDots()
        {
            int count = 0;
            int columnCount = Dots.GetLength(0);
            int rowCount = Dots.GetLength(1);
            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; column++)
                {
                    if (!char.IsWhiteSpace(Dots[column, row]))
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        void UpdatePacMan()
        {
            if (PacManMovingDirection.HasValue) //kiem tra PacMan co dang di chuyen hay khong
            {
                if ((PacManMovingDirection == Direction.Left || PacManMovingDirection == Direction.Right) && PacManMovingFrame >= FramesToMoveHorizontal ||
                    (PacManMovingDirection == Direction.Up || PacManMovingDirection == Direction.Down) && PacManMovingFrame >= FramesToMoveVertical)
                {
                    PacManMovingFrame = 1; //tốc độ của PacMan
                    int x_adjust =
                        PacManMovingDirection == Direction.Left ? -1 : //di chuyen sang trai thi toa do la: x - 1
                        PacManMovingDirection == Direction.Right ? 1 : //di chuyen sang phai thi toa do la: x + 1
                        0; // x = 0 khi y da co gia tri
                    int y_adjust =
                        PacManMovingDirection == Direction.Up ? -1 : //di chuyen len tren thi toa do la: y - 1
                        PacManMovingDirection == Direction.Down ? 1 : //di chuyen len tren thi toa do la: y + 1
                        0; // y = 0 khi x da co gia tri
                    Console.SetCursorPosition(PacManPosition.X, PacManPosition.Y);
                    Console.Write(" ");
                    //toa do cua PacMan sau khi dieu chinh
                    PacManPosition = (PacManPosition.X + x_adjust, PacManPosition.Y + y_adjust);
                    if (PacManPosition.X < 0) //neu di chuyen khoi cong ben trai, dich chuyen qua ben phai
                    {
                        PacManPosition.X = 40;
                    }
                    else if (PacManPosition.X > 40) //neu di chuyen khoi cong ben phai, dich chuyen qua ben trai
                    {
                        PacManPosition.X = 0;
                    }
                    if (Dots[PacManPosition.X, PacManPosition.Y] is '·') //an . thi
                    {
                        Dots[PacManPosition.X, PacManPosition.Y] = ' '; //xoa .
                        Score += 1;
                        EatingDotSound();
                    }
                    if (Dots[PacManPosition.X, PacManPosition.Y] is '+') //ăn + thì
                    {
                        foreach (Ghost ghost in Ghosts)
                        {
                            ghost.Weak = true;
                            ghost.WeakTime = 0;
                        }
                        Dots[PacManPosition.X, PacManPosition.Y] = ' ';
                        Score += 3;
                    }
                    if (!CanMove(PacManPosition.X, PacManPosition.Y, PacManMovingDirection.Value))
                    {
                        PacManMovingDirection = null; //neu huong dang di chuyen la tuong, dung PacMan lai
                    }
                }
                else
                {
                    PacManMovingFrame++;
                }
            }
        }

        void RenderReady()
        {
            Console.SetCursorPosition(18, 13);
            WithColors(ConsoleColor.White, ConsoleColor.Black, () =>
            {
                Console.Write("READY");
            });
        }

        void EraseReady() //xoa chu Ready
        {
            Console.SetCursorPosition(18, 13);
            Console.Write("     ");
        }

        void RenderScore()
        {
            Console.SetCursorPosition(0, 23);
            Console.Write("Diem: " + Score);
        }

        void RenderGate()
        {
            Console.SetCursorPosition(19, 9);
            WithColors(ConsoleColor.Magenta, ConsoleColor.Black, () =>
            {
                Console.Write("---");
            });
        }

        void RenderWalls()
        {
            Console.SetCursorPosition(0, 0);
            WithColors(ConsoleColor.Blue, ConsoleColor.Black, () =>
            {
                Render(WallsString, false);
            });
        }

        void RenderDots()
        {
            Console.SetCursorPosition(0, 0);
            WithColors(ConsoleColor.DarkYellow, ConsoleColor.Black, () =>
            {
                for (int row = 0; row < Dots.GetLength(1); row++)
                {
                    for (int column = 0; column < Dots.GetLength(0); column++)
                    {
                        if (!char.IsWhiteSpace(Dots[column, row]))
                        {
                            Console.SetCursorPosition(column, row);
                            Console.Write(Dots[column, row]);
                        }
                    }
                }
            });
        }

        void RenderPacMan()
        {
            Console.SetCursorPosition(PacManPosition.X, PacManPosition.Y);
            WithColors(ConsoleColor.Black, ConsoleColor.Yellow, () =>
            {
                if (PacManMovingDirection.HasValue && PacManMovingFrame.HasValue)
                {
                    int frame = (int)PacManMovingFrame % PacManAnimations[(int)PacManMovingDirection].Length;
                    Console.Write(PacManAnimations[(int)PacManMovingDirection][frame]);
                }
                else
                {
                    Console.Write(' ');
                }
            });
        }

        void RenderGhosts()
        {
            foreach (Ghost ghost in Ghosts)
            {
                Console.SetCursorPosition(ghost.Position.X, ghost.Position.Y);
                WithColors(ConsoleColor.White, ghost.Weak ? ConsoleColor.Blue : ghost.Color, () => Console.Write('"'));
            }
        }

        void WithColors(ConsoleColor foreground, ConsoleColor background, Action action)
        {
            ConsoleColor originalForeground = Console.ForegroundColor;
            ConsoleColor originalBackground = Console.BackgroundColor;
            try
            {
                Console.ForegroundColor = foreground;
                Console.BackgroundColor = background;
                action();
            }
            finally
            {
                Console.ForegroundColor = originalForeground;
                Console.BackgroundColor = originalBackground;
            }
        }

        void Render(string @string, bool renderSpace = true)
        {
            int x = Console.CursorLeft;
            int y = Console.CursorTop;
            foreach (char c in @string)
            {
                if (c is '\n')
                {
                    Console.SetCursorPosition(x, ++y);
                }
                else if (c is not ' ' || renderSpace)
                {
                    Console.Write(c);
                }
                else
                {
                    Console.SetCursorPosition(Console.CursorLeft + 1, Console.CursorTop);
                }
            }
        }

        void UpdateGhosts()
        {
            foreach (Ghost ghost in Ghosts)
            {
                ghost.Update!();
            }
        }

        void UpdateGhost(Ghost ghost)
        {
            if (ghost.Destination.HasValue && ghost.Destination == ghost.Position)
            {
                ghost.Destination = GetRandomLocation();
            }
            if (ghost.Weak)
            {
                ghost.WeakTime++;
                if (ghost.WeakTime > GhostWeakTime)
                {
                    ghost.Weak = false;
                }
            }
            else if (ghost.UpdateFrame < ghost.FramesToUpdate)
            {
                ghost.UpdateFrame++;
            }
            else
            {
                Console.SetCursorPosition(ghost.Position.X, ghost.Position.Y);  // Xoa vi tri cua Ma
                Console.Write(' ');
                // Tinh toan vi tri moi va di chuyen con ma
                ghost.Position = GetGhostNextMove(ghost.Position, ghost.Destination ?? PacManPosition);
                ghost.UpdateFrame = 0; // Dat lai so khung hinh da xu ly
            }
        }

        (int X, int Y)[] GetLocations()
        {
            // Tao danh sach cac vi tri tu chuoi GhostWallString
            List<(int X, int Y)> list = new();
            int x = 0;
            int y = 0;
            foreach (char c in GhostWallsString)
            {
                if (c is '\n') // Khi gap \n, chuyen sang hang tiep theo
                {
                    x = 0;
                    y++;
                }
                else
                {
                    if (c is ' ') // Neu ky tu la khoang trang, them toa do vao danh sach
                    {
                        list.Add((x, y));
                    }
                    x++;
                }
            }
            return [.. list]; // Tra ve danh sach cac toa do
        }

        (int X, int Y) GetRandomLocation() => Random.Shared.Choose(Locations);
        // Chon vi tri ngau nhien tu danh sach cac toa do co the su dung duoc
        (int X, int Y) GetGhostNextMove((int X, int Y) position, (int X, int Y) destination)
        {
            HashSet<(int X, int Y)> alreadyUsed = new(); // Tap hop luu cac toa do da kiem tra
            char BoardAt(int x, int y) => GhostWallsString[y * 42 + x];
            // Lay ky tu tai mot vi tri cu the tren bang
            bool IsWall(int x, int y) => BoardAt(x, y) is not ' ';
            // Kiem tra xem co phai tuong khong
            void Neighbors((int X, int Y) currentLocation, Action<(int X, int Y)> neighbors)
            // Tim cac vi tri lang gieng cua vi tri hien tai
            {
                void HandleNeighbor(int x, int y)
                {
                    if (!alreadyUsed.Contains((x, y)) && x >= 0 && x <= 40 && !IsWall(x, y))
                    // Neu vi tri chua duoc kiem tra, khong nam ngoai bien va khong phai tuong, them vao danh sach
                    {
                        alreadyUsed.Add((x, y));
                        neighbors((x, y));
                    }
                }
                int x = currentLocation.X;
                int y = currentLocation.Y;
                // Kiem tra cac lang gieng theo huong: trai, tren, phai, duoi
                HandleNeighbor(x - 1, y); // left
                HandleNeighbor(x, y + 1); // up
                HandleNeighbor(x + 1, y); // right
                HandleNeighbor(x, y - 1); // down
            }
            int Heuristic((int X, int Y) node) //danh gia khoang cach tu vi tri hien tai toi PacMan

            {
                int x = node.X - PacManPosition.X;
                int y = node.Y - PacManPosition.Y;
                return x * x + y * y;
            }

            // Tim duong di toi uu tu vi tri hien tai toi dich
            Action<Action<(int X, int Y)>> path = SearchGraph(position, Neighbors, Heuristic, node => node == destination)!;
            (int X, int Y)[] array = path.ToArray();
            return array[1];  // Tra ve buoc tiep theo trong duong di
        }
    }
}

//Lop mo ta thong tin va trang thai cua Ma
internal class Ghost
{
    public (int X, int Y) StartPosition; // vi tri ban dau cua Ma
    public (int X, int Y) Position; // vi tri hien tai cua Ma
    public bool Weak;   //trang thai yeu cua Ma
    public int WeakTime; //thoi gian con lai khi Ma yeu
    public ConsoleColor Color;   // mau cua Ma
    public Action? Update;  //hanh dong cap nhat trang thai va vi tri cua Ma
    public int UpdateFrame; // so khung hinh da cap nhat
    public int FramesToUpdate; // so khung hinh can de Ma di chuyen
    public (int X, int Y)? Destination; // diem den cua Ma
}

internal enum Direction //Xac dinh huong di chuyen cua PacMan dua tren input cua nguoi choi
{
    Up = 0,
    Down = 1,
    Left = 2,
    Right = 3,
}