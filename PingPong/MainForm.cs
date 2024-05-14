// Author: Daniel Leal
// Why do programmers not like nature?
// Too many bugs and no documentation...

namespace PingPong;

public partial class MainForm : Form
{
  private (int Player, int Computer) score = (0, 0);
  private (int X, int Y) ballSpeed = (4, 4);
  private bool goDown, goUp;

  private readonly int currentPlayerSpeed = 8;
  private int currentComputerSpeed = 2;
  private int speedDelta = 50;

  /// <summary>
  /// The speed at which the ball can go when the computer hits it.
  /// </summary>
  private readonly int[] computerSpeedOptions = [5, 6, 8, 9];

  /// <summary>
  ///  The speed at which the ball can go when the player hits it.
  /// </summary>
  private readonly int[] playerSpeedOptions = [8, 9, 10, 11, 12];

  public MainForm() => this.InitializeComponent();

  private void StartGameEvent(object sender, EventArgs e)
  {
    this.UpdateBallMovement();

    // If the ball hits the left of the screen, give a point to the computer and reset the ball position!
    if (this.Ball.Left < -2)
    {
      this.Ball.Left = 300;
      this.ballSpeed.X = -this.ballSpeed.X;
      this.score.Computer++;
    }

    // If the ball hits the right of the screen, give a point to the player and reset the ball position!
    if (this.Ball.Right > this.ClientSize.Width + 2)
    {
      this.Ball.Left = 300;
      this.ballSpeed.X = -this.ballSpeed.X;
      this.score.Player++;
    }

    // Update the computer movement based on ball location.
    this.UpdateComputerMovement();

    // Update player movement based on key event.
    this.UpdatePlayerMovement();

    // Check if the ball has hit the player.
    this.CheckCollisions(this.Ball, this.Player, this.Player.Right + 5);

    // Check if the ball has hit the computer.
    this.CheckCollisions(this.Ball, this.Computer, this.Computer.Left - 35);

    // Present a message, if needed.
    if (this.score.Player is > 5 || this.score.Computer is > 5)
    {
      var message = this.score.Player > 5 ? "You Won!!" : "You lost, and you suck!";
      this.GameOverEvent(message);
    }
  }

  /// <summary>
  /// Ensures screen boundaries are being respected by computer. Also updates up, or down, reaction of computer.
  /// </summary>
  /// <remarks>
  /// Once the ball passes the halfway point of the computer box, it will either move up or down based on the ball trajectory.
  /// </remarks>
  private void UpdateComputerMovement()
  {
    // Ensure the computer does not hit the top or bottom of the screen.
    if (this.Computer.Top <= 1)
    {
      this.Computer.Top = 0;
    }
    else if (this.Computer.Bottom >= this.ClientSize.Height)
    {
      this.Computer.Top = this.ClientSize.Height - this.Computer.Height;
    }

    // Check if the ball is going up or down towards the computer.
    var ballIsGoingDown = this.Ball.Top < this.Computer.Top + (this.Computer.Height / 2);
    var ballIsGoingUp = this.Ball.Top > this.Computer.Top + (this.Computer.Height / 2);
    var ballIsComingTowardsComputer = this.Ball.Left > 300;

    // If the ball is coming towards the computer, adjust the responsive speed.
    if (ballIsGoingDown && ballIsComingTowardsComputer)
    {
      this.Computer.Top -= this.currentComputerSpeed;
    }

    if (ballIsGoingUp && ballIsComingTowardsComputer)
    {
      this.Computer.Top += this.currentComputerSpeed;
    }

    // Make the computer speed progressively go slower and reset once it is at zero.
    this.speedDelta--;
    if (this.speedDelta < 0)
    {
      var random = new Random();
      this.currentComputerSpeed = this.computerSpeedOptions[random.Next(this.computerSpeedOptions.Length)];
      this.speedDelta = 50;
    }
  }

  /// <summary>
  /// Updates the player movement and ensures screen boundaries are being respected.
  /// </summary>
  private void UpdatePlayerMovement()
  {
    // If the player is moving up or down, and is in possible moving terrain, update the speed.
    if (this.goDown && this.Player.Top + this.Player.Height < this.ClientSize.Height)
    {
      this.Player.Top += this.currentPlayerSpeed;
    }

    if (this.goUp && this.Player.Top > 0)
    {
      this.Player.Top -= this.currentPlayerSpeed;
    }
  }

  /// <summary>
  /// Checks if a collision has occured and adjusts behavior accordingly.
  /// </summary>
  /// <param name="ball">The ball box.</param>
  /// <param name="player">The player, or computer, box.</param>
  /// <param name="offset">The offset space between the ball and picture box.</param>
  private void CheckCollisions(PictureBox ball, PictureBox player, int offset)
  {
    if (ball.Bounds.IntersectsWith(player.Bounds))
    {
      ball.Left = offset;

      // Randomly pick the speed at which the ball will go in the X and Y directions once hit.
      var random = new Random();
      var x = this.computerSpeedOptions[random.Next(this.computerSpeedOptions.Length)];
      var y = this.playerSpeedOptions[random.Next(this.playerSpeedOptions.Length)];

      // If the ball was going left (< 0), make it go right (positive).
      // Otherwise, make it go left (negative).
      this.ballSpeed.X = this.ballSpeed.X < 0 ? x : -x;

      // Make sure the ball is going in the same direction it was previously going in, but in a different speed.
      this.ballSpeed.Y = this.ballSpeed.Y < 0 ? -y : y;
    }
  }

  /// <summary>
  /// Updates ball position/score and ensures screen boundaries are respected.
  /// </summary>
  private void UpdateBallMovement()
  {
    // Update the ball location based on the speed of it.
    this.Ball.Top -= this.ballSpeed.Y;
    this.Ball.Left -= this.ballSpeed.X;
    this.Text = $"Player Score: {this.score.Player} || Computer Score: {this.score.Computer}";

    // If ball hits the bottom of screen, make it go in the opposite direction.
    if (this.Ball.Top < 0 || this.Ball.Bottom > this.ClientSize.Height)
    {
      this.ballSpeed.Y = -this.ballSpeed.Y;
    }
  }

  private void KeyPressedEvent(object sender, KeyEventArgs e)
  {
    // If up or down button is being pressed, set respective
    // bool to true. Otherwise, keep its value as is.
    this.goDown = e.KeyCode is Keys.Down || this.goDown;
    this.goUp = e.KeyCode is Keys.Up || this.goUp;
  }

  private void KeyReleasedEvent(object sender, KeyEventArgs e)
  {
    // If up or down button is being released, set respective
    // bool to false. Otherwise, keep its value as is.
    this.goDown = e.KeyCode is not Keys.Down && this.goDown;
    this.goUp = e.KeyCode is not Keys.Up && this.goUp;
  }

  private void GameOverEvent(string message)
  {
    this.GameTimer.Stop();
    MessageBox.Show(message, "PingPong Says: ");

    this.ballSpeed = (4, 4);
    this.score = (0, 0);
    this.speedDelta = 50;
    this.GameTimer.Start();
  }
}
