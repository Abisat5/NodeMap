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
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            button7 = new Button();
            button8 = new Button();
            button9 = new Button();
            button10 = new Button();
            button11 = new Button();
            button12 = new Button();
            txtNodeId = new TextBox();
            txtNodeName = new TextBox();
            txtEdgeFrom = new TextBox();
            txtEdgeTo = new TextBox();
            button13 = new Button();
            button14 = new Button();
            button15 = new Button();
            button16 = new Button();
            button17 = new Button();
            button18 = new Button();
            button19 = new Button();
            SuspendLayout();
            // 
            // btnTest
            // 
            btnTest.Location = new Point(1272, 37);
            btnTest.Margin = new Padding(3, 4, 3, 4);
            btnTest.Name = "btnTest";
            btnTest.Size = new Size(141, 111);
            btnTest.TabIndex = 0;
            btnTest.Text = "Test Graph";
            btnTest.UseVisualStyleBackColor = true;
            btnTest.Click += btnCreateGraph;
            // 
            // button1
            // 
            button1.Location = new Point(1272, 281);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(121, 53);
            button1.TabIndex = 1;
            button1.Text = "BFS Çalıştır";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnBFS;
            // 
            // button2
            // 
            button2.Location = new Point(1272, 443);
            button2.Margin = new Padding(3, 4, 3, 4);
            button2.Name = "button2";
            button2.Size = new Size(121, 53);
            button2.TabIndex = 2;
            button2.Text = "DFS Çalıştır";
            button2.UseVisualStyleBackColor = true;
            button2.Click += btnDFS;
            // 
            // button3
            // 
            button3.Location = new Point(1272, 361);
            button3.Margin = new Padding(3, 4, 3, 4);
            button3.Name = "button3";
            button3.Size = new Size(121, 53);
            button3.TabIndex = 3;
            button3.Text = "Dijkstra Çalıştır";
            button3.UseVisualStyleBackColor = true;
            button3.Click += btnDijkstra_Click;
            // 
            // button4
            // 
            button4.Location = new Point(1272, 519);
            button4.Margin = new Padding(3, 4, 3, 4);
            button4.Name = "button4";
            button4.Size = new Size(121, 53);
            button4.TabIndex = 4;
            button4.Text = "Degree Centrality";
            button4.UseVisualStyleBackColor = true;
            button4.Click += btnCentrality;
            // 
            // button5
            // 
            button5.Location = new Point(1272, 205);
            button5.Margin = new Padding(3, 4, 3, 4);
            button5.Name = "button5";
            button5.Size = new Size(121, 53);
            button5.TabIndex = 5;
            button5.Text = "AStar Çalıştır";
            button5.UseVisualStyleBackColor = true;
            button5.Click += btnAStar_Click;
            // 
            // button6
            // 
            button6.Location = new Point(1272, 673);
            button6.Margin = new Padding(3, 4, 3, 4);
            button6.Name = "button6";
            button6.Size = new Size(121, 53);
            button6.TabIndex = 6;
            button6.Text = "Closeness ";
            button6.UseVisualStyleBackColor = true;
            button6.Click += btnCloseness_Click;
            // 
            // button7
            // 
            button7.Location = new Point(1272, 748);
            button7.Margin = new Padding(3, 4, 3, 4);
            button7.Name = "button7";
            button7.Size = new Size(121, 53);
            button7.TabIndex = 7;
            button7.Text = "btnBetweenness_Click";
            button7.UseVisualStyleBackColor = true;
            button7.Click += btnBetweenness_Click;
            // 
            // button8
            // 
            button8.Location = new Point(1603, 225);
            button8.Margin = new Padding(3, 4, 3, 4);
            button8.Name = "button8";
            button8.Size = new Size(121, 33);
            button8.TabIndex = 8;
            button8.Text = "Random Graph";
            button8.UseVisualStyleBackColor = true;
            button8.Click += btnRandomGraph_Click;
            // 
            // button9
            // 
            button9.Location = new Point(1475, 205);
            button9.Margin = new Padding(3, 4, 3, 4);
            button9.Name = "button9";
            button9.Size = new Size(121, 32);
            button9.TabIndex = 9;
            button9.Text = "Edge Aç/Kapat";
            button9.UseVisualStyleBackColor = true;
            button9.Click += btnToggleEdges_Click;
            // 
            // button10
            // 
            button10.Location = new Point(1475, 259);
            button10.Margin = new Padding(3, 4, 3, 4);
            button10.Name = "button10";
            button10.Size = new Size(121, 32);
            button10.TabIndex = 10;
            button10.Text = "Auto node ekle";
            button10.UseVisualStyleBackColor = true;
            button10.Click += btnAddNode_Click;
            // 
            // button11
            // 
            button11.Location = new Point(1443, 61);
            button11.Margin = new Padding(3, 4, 3, 4);
            button11.Name = "button11";
            button11.Size = new Size(101, 31);
            button11.TabIndex = 11;
            button11.Text = "Manuel Node ekle";
            button11.UseVisualStyleBackColor = true;
            button11.Click += btnAddNodeWithId_Click;
            // 
            // button12
            // 
            button12.Location = new Point(1443, 100);
            button12.Margin = new Padding(3, 4, 3, 4);
            button12.Name = "button12";
            button12.Size = new Size(101, 31);
            button12.TabIndex = 12;
            button12.Text = "Manuel Edge";
            button12.UseVisualStyleBackColor = true;
            button12.Click += btnAddEdge_Click;
            // 
            // txtNodeId
            // 
            txtNodeId.Location = new Point(1667, 61);
            txtNodeId.Margin = new Padding(3, 4, 3, 4);
            txtNodeId.Name = "txtNodeId";
            txtNodeId.PlaceholderText = "ID";
            txtNodeId.Size = new Size(57, 27);
            txtNodeId.TabIndex = 13;
            // 
            // txtNodeName
            // 
            txtNodeName.Location = new Point(1573, 61);
            txtNodeName.Margin = new Padding(3, 4, 3, 4);
            txtNodeName.Name = "txtNodeName";
            txtNodeName.PlaceholderText = "Name";
            txtNodeName.Size = new Size(68, 27);
            txtNodeName.TabIndex = 14;
            // 
            // txtEdgeFrom
            // 
            txtEdgeFrom.Location = new Point(1573, 101);
            txtEdgeFrom.Margin = new Padding(3, 4, 3, 4);
            txtEdgeFrom.Name = "txtEdgeFrom";
            txtEdgeFrom.PlaceholderText = "From";
            txtEdgeFrom.Size = new Size(57, 27);
            txtEdgeFrom.TabIndex = 15;
            // 
            // txtEdgeTo
            // 
            txtEdgeTo.Location = new Point(1667, 101);
            txtEdgeTo.Margin = new Padding(3, 4, 3, 4);
            txtEdgeTo.Name = "txtEdgeTo";
            txtEdgeTo.PlaceholderText = "To";
            txtEdgeTo.Size = new Size(57, 27);
            txtEdgeTo.TabIndex = 16;
            // 
            // button13
            // 
            button13.Location = new Point(1135, 227);
            button13.Name = "button13";
            button13.Size = new Size(94, 29);
            button13.TabIndex = 17;
            button13.Text = "JSON Dışa Aktar";
            button13.UseVisualStyleBackColor = true;
            button13.Click += btnExportCsv_Click;
            // 
            // button14
            // 
            button14.Location = new Point(1135, 259);
            button14.Name = "button14";
            button14.Size = new Size(94, 29);
            button14.TabIndex = 18;
            button14.Text = "CSV Dışa Aktar";
            button14.UseVisualStyleBackColor = true;
            button14.Click += btnExportJson_Click;
            // 
            // button15
            // 
            button15.Location = new Point(1135, 294);
            button15.Name = "button15";
            button15.Size = new Size(94, 29);
            button15.TabIndex = 19;
            button15.Text = "Komşuluk Listesi CSV";
            button15.UseVisualStyleBackColor = true;
            button15.Click += btnAdjList_Click;
            // 
            // button16
            // 
            button16.Location = new Point(1135, 329);
            button16.Name = "button16";
            button16.Size = new Size(94, 29);
            button16.TabIndex = 20;
            button16.Text = "Komşuluk Matrisi CSV";
            button16.UseVisualStyleBackColor = true;
            button16.Click += btnAdjMatrix_Click;
            // 
            // button17
            // 
            button17.Location = new Point(1158, 387);
            button17.Name = "button17";
            button17.Size = new Size(94, 29);
            button17.TabIndex = 21;
            button17.Text = "CSV İçe Aktar";
            button17.UseVisualStyleBackColor = true;
            button17.Click += btnImportCsv_Click;
            // 
            // button18
            // 
            button18.Location = new Point(1171, 434);
            button18.Name = "button18";
            button18.Size = new Size(94, 29);
            button18.TabIndex = 22;
            button18.Text = "JSON İçe Aktar";
            button18.UseVisualStyleBackColor = true;
            button18.Click += btnImportJson_Click;
            // 
            // button19
            // 
            button19.Location = new Point(1182, 478);
            button19.Name = "button19";
            button19.Size = new Size(94, 29);
            button19.TabIndex = 23;
            button19.Text = "BAŞLANGIÇ GRAFA DÖN";
            button19.UseVisualStyleBackColor = true;
            button19.Click += btnRestore_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1749, 855);
            Controls.Add(button19);
            Controls.Add(button18);
            Controls.Add(button17);
            Controls.Add(button16);
            Controls.Add(button15);
            Controls.Add(button14);
            Controls.Add(button13);
            Controls.Add(button12);
            Controls.Add(button11);
            Controls.Add(button10);
            Controls.Add(button9);
            Controls.Add(button8);
            Controls.Add(button7);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(btnTest);
            Controls.Add(txtNodeId);
            Controls.Add(txtNodeName);
            Controls.Add(txtEdgeFrom);
            Controls.Add(txtEdgeTo);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            Paint += Form1_Paint;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnTest;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button8;
        private Button button9;
        private Button button10;
        private Button button11;
        private Button button12;

        private TextBox txtNodeId;
        private TextBox txtNodeName;
        private TextBox txtEdgeFrom;
        private TextBox txtEdgeTo;
        private Button button13;
        private Button button14;
        private Button button15;
        private Button button16;
        private Button button17;
        private Button button18;
        private Button button19;
    }



}
