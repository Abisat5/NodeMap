namespace NodeMap.UI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            btnTest = new Button();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            SuspendLayout();
            // 
            // btnTest
            // 
            btnTest.Location = new Point(575, 76);
            btnTest.Name = "btnTest";
            btnTest.Size = new Size(125, 80);
            btnTest.TabIndex = 0;
            btnTest.Text = "Test Graph";
            btnTest.UseVisualStyleBackColor = true;
            btnTest.Click += btnCreateGraph;
            // 
            // button1
            // 
            button1.Location = new Point(585, 254);
            button1.Name = "button1";
            button1.Size = new Size(106, 70);
            button1.TabIndex = 1;
            button1.Text = "BFS Çalıştır";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnBFS;
            // 
            // button2
            // 
            button2.Location = new Point(663, 345);
            button2.Name = "button2";
            button2.Size = new Size(125, 69);
            button2.TabIndex = 2;
            button2.Text = "DFS Çalıştır";
            button2.UseVisualStyleBackColor = true;
            button2.Click += btnDFS;
            // 
            // button3
            // 
            button3.Location = new Point(504, 345);
            button3.Name = "button3";
            button3.Size = new Size(115, 69);
            button3.TabIndex = 3;
            button3.Text = "Dijkstra Çalıştır";
            button3.UseVisualStyleBackColor = true;
            button3.Click += btnDijkstra_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(btnTest);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            Paint += Form1_Paint;
            ResumeLayout(false);
        }

        #endregion

        private Button btnTest;
        private Button button1;
        private Button button2;
        private Button button3;
    }
}
