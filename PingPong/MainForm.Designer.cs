namespace PingPong;

using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

partial class MainForm
{
  /// <summary>
  ///  Required designer variable.
  /// </summary>
  private IContainer components = null;

  /// <summary>
  ///  Clean up any resources being used.
  /// </summary>
  /// <param name="disposing">True if managed resources should be disposed. Otherwise, false.</param>
  protected override void Dispose(bool disposing)
  {
    if (disposing && (components != null))
    {
        components.Dispose();
    }

    base.Dispose(disposing);
  }

  #region Windows Form Designer generated code
  /// <summary>
  ///  Required method for Designer support - do not modify
  ///  the contents of this method with the code editor.
  /// </summary>
  private void InitializeComponent()
  {
    components = new Container();
    Player = new PictureBox();
    Computer = new PictureBox();
    Ball = new PictureBox();
    GameTimer = new Timer(components);
    ((ISupportInitialize)Player).BeginInit();
    ((ISupportInitialize)Computer).BeginInit();
    ((ISupportInitialize)Ball).BeginInit();
    SuspendLayout();
    // 
    // Player
    // 
    Player.Image = Resources.Resources.player;
    Player.Location = new Point(12, 151);
    Player.Name = "Player";
    Player.Size = new Size(30, 120);
    Player.SizeMode = PictureBoxSizeMode.StretchImage;
    Player.TabIndex = 0;
    Player.TabStop = false;
    // 
    // Computer
    // 
    Computer.Image = Resources.Resources.computer;
    Computer.Location = new Point(758, 151);
    Computer.Name = "Computer";
    Computer.Size = new Size(30, 120);
    Computer.SizeMode = PictureBoxSizeMode.StretchImage;
    Computer.TabIndex = 1;
    Computer.TabStop = false;
    // 
    // Ball
    // 
    Ball.Image = Resources.Resources.ball;
    Ball.Location = new Point(395, 191);
    Ball.Name = "Ball";
    Ball.Size = new Size(30, 30);
    Ball.SizeMode = PictureBoxSizeMode.StretchImage;
    Ball.TabIndex = 2;
    Ball.TabStop = false;
    // 
    // GameTimer
    // 
    GameTimer.Enabled = true;
    GameTimer.Interval = 20;
    GameTimer.Tick += StartGameEvent;
    // 
    // MainForm
    // 
    AutoScaleDimensions = new SizeF(8F, 20F);
    AutoScaleMode = AutoScaleMode.Font;
    BackColor = Color.Black;
    ClientSize = new Size(800, 450);
    Controls.Add(Ball);
    Controls.Add(Computer);
    Controls.Add(Player);
    DoubleBuffered = true;
    Name = "MainForm";
    Text = "Player: 0 || Computer: 0";
    KeyDown += KeyPressedEvent;
    KeyUp += KeyReleasedEvent;
    ((ISupportInitialize)Player).EndInit();
    ((ISupportInitialize)Computer).EndInit();
    ((ISupportInitialize)Ball).EndInit();
    ResumeLayout(false);
  }
  #endregion

  private PictureBox Player;
  private PictureBox Computer;
  private PictureBox Ball;
  private Timer GameTimer;
}
