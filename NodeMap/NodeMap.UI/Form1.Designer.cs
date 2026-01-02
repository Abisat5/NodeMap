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
            button20 = new Button();
            button21 = new Button();
            button19 = new Button();
            dgvDegreeCentrality = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dgvWelshPowell = new DataGridView();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            button22 = new Button();
            button23 = new Button();
            btnCloseDegreeCentrality = new Button();
            btnCloseWelshPowell = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvDegreeCentrality).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvWelshPowell).BeginInit();
            SuspendLayout();
            // 
            // btnTest
            // 
            btnTest.BackColor = Color.GreenYellow;
            btnTest.ForeColor = SystemColors.ActiveCaptionText;
            btnTest.Location = new Point(1082, 26);
            btnTest.Name = "btnTest";
            btnTest.Size = new Size(123, 82);
            btnTest.TabIndex = 0;
            btnTest.Text = "Grafik Testini Başlat";
            btnTest.UseVisualStyleBackColor = false;
            btnTest.Click += btnCreateGraph;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.ActiveCaption;
            button1.Location = new Point(1082, 160);
            button1.Name = "button1";
            button1.Size = new Size(106, 40);
            button1.TabIndex = 1;
            button1.Text = "BFS Çalıştır";
            button1.UseVisualStyleBackColor = false;
            button1.Click += btnBFS;
            // 
            // button2
            // 
            button2.BackColor = SystemColors.ActiveCaption;
            button2.Location = new Point(1082, 252);
            button2.Name = "button2";
            button2.Size = new Size(106, 40);
            button2.TabIndex = 2;
            button2.Text = "DFS Çalıştır";
            button2.UseVisualStyleBackColor = false;
            button2.Click += btnDFS;
            // 
            // button3
            // 
            button3.BackColor = SystemColors.ActiveCaption;
            button3.Location = new Point(1082, 206);
            button3.Name = "button3";
            button3.Size = new Size(106, 40);
            button3.TabIndex = 3;
            button3.Text = "Dijkstra Çalıştır";
            button3.UseVisualStyleBackColor = false;
            button3.Click += btnDijkstra_Click;
            // 
            // button4
            // 
            button4.BackColor = SystemColors.ActiveCaption;
            button4.Location = new Point(1082, 298);
            button4.Name = "button4";
            button4.Size = new Size(106, 40);
            button4.TabIndex = 4;
            button4.Text = "Derece Merkeziyeti";
            button4.UseVisualStyleBackColor = false;
            button4.Click += btnCentrality;
            // 
            // button5
            // 
            button5.BackColor = SystemColors.ActiveCaption;
            button5.Location = new Point(1082, 114);
            button5.Name = "button5";
            button5.Size = new Size(106, 40);
            button5.TabIndex = 5;
            button5.Text = "AStar Çalıştır";
            button5.UseVisualStyleBackColor = false;
            button5.Click += btnAStar_Click;
            // 
            // button6
            // 
            button6.BackColor = SystemColors.ActiveCaption;
            button6.Location = new Point(1082, 344);
            button6.Name = "button6";
            button6.Size = new Size(106, 40);
            button6.TabIndex = 6;
            button6.Text = "Yakınlık Merkeziyeti";
            button6.UseVisualStyleBackColor = false;
            button6.Click += btnCloseness_Click;
            // 
            // button7
            // 
            button7.BackColor = SystemColors.ActiveCaption;
            button7.Location = new Point(1082, 390);
            button7.Name = "button7";
            button7.Size = new Size(106, 40);
            button7.TabIndex = 7;
            button7.Text = "Arasındalık Merkeziyeti";
            button7.UseVisualStyleBackColor = false;
            button7.Click += btnBetweenness_Click;
            // 
            // button8
            // 
            button8.BackColor = Color.BlanchedAlmond;
            button8.Location = new Point(1316, 114);
            button8.Name = "button8";
            button8.Size = new Size(106, 40);
            button8.TabIndex = 8;
            button8.Text = "Rastgele Grafik Oluştur";
            button8.UseVisualStyleBackColor = false;
            button8.Click += btnRandomGraph_Click;
            // 
            // button9
            // 
            button9.BackColor = Color.BlanchedAlmond;
            button9.Location = new Point(1194, 114);
            button9.Name = "button9";
            button9.Size = new Size(106, 40);
            button9.TabIndex = 9;
            button9.Text = "Edge Aç/Kapat";
            button9.UseVisualStyleBackColor = false;
            button9.Click += btnToggleEdges_Click;
            // 
            // button10
            // 
            button10.BackColor = Color.BlanchedAlmond;
            button10.Location = new Point(1194, 169);
            button10.Name = "button10";
            button10.Size = new Size(106, 40);
            button10.TabIndex = 10;
            button10.Text = "Auto node ekle";
            button10.UseVisualStyleBackColor = false;
            button10.Click += btnAddNode_Click;
            // 
            // button11
            // 
            button11.BackColor = Color.BlanchedAlmond;
            button11.Location = new Point(1212, 26);
            button11.Name = "button11";
            button11.Size = new Size(88, 23);
            button11.TabIndex = 11;
            button11.Text = "Manuel Node ekle";
            button11.UseVisualStyleBackColor = false;
            button11.Click += btnAddNodeWithId_Click;
            // 
            // button12
            // 
            button12.BackColor = Color.BlanchedAlmond;
            button12.Location = new Point(1212, 76);
            button12.Name = "button12";
            button12.Size = new Size(88, 23);
            button12.TabIndex = 12;
            button12.Text = "Manuel Edge";
            button12.UseVisualStyleBackColor = false;
            button12.Click += btnAddEdge_Click;
            // 
            // txtNodeId
            // 
            txtNodeId.BackColor = Color.BlanchedAlmond;
            txtNodeId.Location = new Point(1429, 27);
            txtNodeId.Name = "txtNodeId";
            txtNodeId.PlaceholderText = "ID";
            txtNodeId.Size = new Size(50, 23);
            txtNodeId.TabIndex = 13;
            // 
            // txtNodeName
            // 
            txtNodeName.BackColor = Color.BlanchedAlmond;
            txtNodeName.Location = new Point(1341, 27);
            txtNodeName.Name = "txtNodeName";
            txtNodeName.PlaceholderText = "Node";
            txtNodeName.Size = new Size(60, 23);
            txtNodeName.TabIndex = 14;
            // 
            // txtEdgeFrom
            // 
            txtEdgeFrom.BackColor = Color.BlanchedAlmond;
            txtEdgeFrom.Location = new Point(1351, 75);
            txtEdgeFrom.Name = "txtEdgeFrom";
            txtEdgeFrom.PlaceholderText = "From";
            txtEdgeFrom.Size = new Size(50, 23);
            txtEdgeFrom.TabIndex = 15;
            // 
            // txtEdgeTo
            // 
            txtEdgeTo.BackColor = Color.BlanchedAlmond;
            txtEdgeTo.Location = new Point(1429, 76);
            txtEdgeTo.Name = "txtEdgeTo";
            txtEdgeTo.PlaceholderText = "To";
            txtEdgeTo.Size = new Size(50, 23);
            txtEdgeTo.TabIndex = 16;
            // 
            // button13
            // 
            button13.BackColor = Color.LemonChiffon;
            button13.Location = new Point(1351, 479);
            button13.Margin = new Padding(3, 2, 3, 2);
            button13.Name = "button13";
            button13.Size = new Size(106, 37);
            button13.TabIndex = 17;
            button13.Text = "CSV Dışa Aktar";
            button13.UseVisualStyleBackColor = false;
            button13.Click += btnExportCsv_Click;
            // 
            // button14
            // 
            button14.BackColor = Color.LemonChiffon;
            button14.Location = new Point(1088, 479);
            button14.Margin = new Padding(3, 2, 3, 2);
            button14.Name = "button14";
            button14.Size = new Size(106, 37);
            button14.TabIndex = 18;
            button14.Text = "JSON Dışa Aktar";
            button14.UseVisualStyleBackColor = false;
            button14.Click += btnExportJson_Click;
            // 
            // button15
            // 
            button15.BackColor = Color.LemonChiffon;
            button15.Location = new Point(1088, 563);
            button15.Margin = new Padding(3, 2, 3, 2);
            button15.Name = "button15";
            button15.Size = new Size(177, 33);
            button15.TabIndex = 19;
            button15.Text = "Komşuluk Listesi Dışarı Aktar";
            button15.UseVisualStyleBackColor = false;
            button15.Click += btnAdjList_Click;
            // 
            // button16
            // 
            button16.BackColor = Color.LemonChiffon;
            button16.Location = new Point(1316, 563);
            button16.Margin = new Padding(3, 2, 3, 2);
            button16.Name = "button16";
            button16.Size = new Size(173, 33);
            button16.TabIndex = 20;
            button16.Text = "Komşul Matrisi Dışarı Aktar";
            button16.UseVisualStyleBackColor = false;
            button16.Click += btnAdjMatrix_Click;
            // 
            // button17
            // 
            button17.BackColor = Color.LemonChiffon;
            button17.Location = new Point(1351, 520);
            button17.Margin = new Padding(3, 2, 3, 2);
            button17.Name = "button17";
            button17.Size = new Size(106, 37);
            button17.TabIndex = 21;
            button17.Text = "CSV İçe Aktar";
            button17.UseVisualStyleBackColor = false;
            button17.Click += btnImportCsv_Click;
            // 
            // button18
            // 
            button18.BackColor = Color.LemonChiffon;
            button18.Location = new Point(1085, 520);
            button18.Margin = new Padding(3, 2, 3, 2);
            button18.Name = "button18";
            button18.Size = new Size(109, 37);
            button18.TabIndex = 22;
            button18.Text = "JSON İçe Aktar";
            button18.UseVisualStyleBackColor = false;
            button18.Click += btnImportJson_Click;
            // 
            // button20
            // 
            button20.BackColor = Color.LemonChiffon;
            button20.Location = new Point(1088, 601);
            button20.Name = "button20";
            button20.Size = new Size(180, 31);
            button20.TabIndex = 24;
            button20.Text = "Komşuluk Listesi İçeri Aktar";
            button20.UseVisualStyleBackColor = false;
            button20.Click += btnImportAdjList_Click;
            // 
            // button21
            // 
            button21.BackColor = Color.LemonChiffon;
            button21.Location = new Point(1316, 601);
            button21.Name = "button21";
            button21.Size = new Size(173, 31);
            button21.TabIndex = 25;
            button21.Text = "Komşuluk Matrisi İçeri Aktar";
            button21.UseVisualStyleBackColor = false;
            button21.Click += btnImportAdjMatrix_Click;
            // 
            // button19
            // 
            button19.BackColor = SystemColors.ActiveCaption;
            button19.Location = new Point(1082, 434);
            button19.Name = "button19";
            button19.Size = new Size(103, 40);
            button19.TabIndex = 26;
            button19.Text = " Dinamik Ağırlık Hesaplama ";
            button19.UseVisualStyleBackColor = false;
            button19.Click += Dinamik_Click;
            // 
            // dgvDegreeCentrality
            // 
            dgvDegreeCentrality.AllowUserToAddRows = false;
            dgvDegreeCentrality.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDegreeCentrality.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3 });
            dgvDegreeCentrality.Location = new Point(805, 252);
            dgvDegreeCentrality.Name = "dgvDegreeCentrality";
            dgvDegreeCentrality.ReadOnly = true;
            dgvDegreeCentrality.Size = new Size(274, 203);
            dgvDegreeCentrality.TabIndex = 27;
            dgvDegreeCentrality.Visible = false;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "Düğüm ID";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "Düğüm Adı";
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.HeaderText = "Derece";
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dgvWelshPowell
            // 
            dgvWelshPowell.AllowUserToAddRows = false;
            dgvWelshPowell.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvWelshPowell.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn4, dataGridViewTextBoxColumn5, dataGridViewTextBoxColumn6 });
            dgvWelshPowell.Location = new Point(805, 461);
            dgvWelshPowell.Name = "dgvWelshPowell";
            dgvWelshPowell.ReadOnly = true;
            dgvWelshPowell.Size = new Size(274, 182);
            dgvWelshPowell.TabIndex = 28;
            dgvWelshPowell.Visible = false;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.HeaderText = "Düğüm ID";
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            dataGridViewTextBoxColumn5.HeaderText = "Düğüm Adı";
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            dataGridViewTextBoxColumn6.HeaderText = "Renk Numarası";
            dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // button22
            // 
            button22.BackColor = SystemColors.ActiveCaption;
            button22.Location = new Point(1194, 434);
            button22.Name = "button22";
            button22.Size = new Size(106, 40);
            button22.TabIndex = 27;
            button22.Text = "Welsh-Powell Renklendirme";
            button22.UseVisualStyleBackColor = false;
            button22.Click += btnWelshPowell_Click;
            // 
            // button23
            // 
            button23.BackColor = SystemColors.ActiveCaption;
            button23.Location = new Point(1316, 434);
            button23.Name = "button23";
            button23.Size = new Size(106, 40);
            button23.TabIndex = 29;
            button23.Text = "Bağlı Bileşenler";
            button23.UseVisualStyleBackColor = false;
            button23.Click += btnConnectedComponents_Click;
            // 
            // btnCloseDegreeCentrality
            // 
            btnCloseDegreeCentrality.BackColor = Color.Red;
            btnCloseDegreeCentrality.FlatAppearance.BorderSize = 0;
            btnCloseDegreeCentrality.FlatStyle = FlatStyle.Flat;
            btnCloseDegreeCentrality.Font = new Font("Arial", 10F, FontStyle.Bold);
            btnCloseDegreeCentrality.ForeColor = Color.White;
            btnCloseDegreeCentrality.Location = new Point(1054, 252);
            btnCloseDegreeCentrality.Name = "btnCloseDegreeCentrality";
            btnCloseDegreeCentrality.Size = new Size(25, 25);
            btnCloseDegreeCentrality.TabIndex = 30;
            btnCloseDegreeCentrality.Text = "✕";
            btnCloseDegreeCentrality.UseVisualStyleBackColor = false;
            btnCloseDegreeCentrality.Visible = false;
            btnCloseDegreeCentrality.Click += BtnCloseDegreeCentrality_Click;
            // 
            // btnCloseWelshPowell
            // 
            btnCloseWelshPowell.BackColor = Color.Red;
            btnCloseWelshPowell.FlatAppearance.BorderSize = 0;
            btnCloseWelshPowell.FlatStyle = FlatStyle.Flat;
            btnCloseWelshPowell.Font = new Font("Arial", 10F, FontStyle.Bold);
            btnCloseWelshPowell.ForeColor = Color.White;
            btnCloseWelshPowell.Location = new Point(1054, 461);
            btnCloseWelshPowell.Name = "btnCloseWelshPowell";
            btnCloseWelshPowell.Size = new Size(25, 25);
            btnCloseWelshPowell.TabIndex = 31;
            btnCloseWelshPowell.Text = "✕";
            btnCloseWelshPowell.UseVisualStyleBackColor = false;
            btnCloseWelshPowell.Visible = false;
            btnCloseWelshPowell.Click += BtnCloseWelshPowell_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(1530, 641);
            Controls.Add(button19);
            Controls.Add(button21);
            Controls.Add(button20);
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
            Controls.Add(button22);
            Controls.Add(button23);
            Controls.Add(dgvDegreeCentrality);
            Controls.Add(dgvWelshPowell);
            Controls.Add(btnCloseDegreeCentrality);
            Controls.Add(btnCloseWelshPowell);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            Paint += Form1_Paint;
            ((System.ComponentModel.ISupportInitialize)dgvDegreeCentrality).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvWelshPowell).EndInit();
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
        private Button button20;
        private Button button21;
        private Button button19;
        private Button button22;
        private Button button23;
        private DataGridView dgvDegreeCentrality;
        private DataGridView dgvWelshPowell;
        private Button btnCloseDegreeCentrality;
        private Button btnCloseWelshPowell;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
    }



}
