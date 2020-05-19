namespace Contact_Manager
{
    partial class ContactManager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ContactList = new System.Windows.Forms.Label();
            this.EditButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.FirstNameLabel = new System.Windows.Forms.Label();
            this.firstNameText = new System.Windows.Forms.TextBox();
            this.MiddleNameLabel = new System.Windows.Forms.Label();
            this.middleNameText = new System.Windows.Forms.TextBox();
            this.LastNameLabel = new System.Windows.Forms.Label();
            this.lastNameText = new System.Windows.Forms.TextBox();
            this.DOBLabel = new System.Windows.Forms.Label();
            this.dOBText = new System.Windows.Forms.TextBox();
            this.GenderLabel = new System.Windows.Forms.Label();
            this.genderComboBox = new System.Windows.Forms.ComboBox();
            this.emailIDText = new System.Windows.Forms.TextBox();
            this.AddressLineLabel = new System.Windows.Forms.Label();
            this.addressLineText = new System.Windows.Forms.TextBox();
            this.EmailIDLabel = new System.Windows.Forms.Label();
            this.PNoLabel = new System.Windows.Forms.Label();
            this.phoneNoText = new System.Windows.Forms.TextBox();
            this.contactManDataGridView = new System.Windows.Forms.DataGridView();
            this.contact_ManagerDataSet = new Contact_Manager.Contact_ManagerDataSet();
            this.addressBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.addressTableAdapter = new Contact_Manager.Contact_ManagerDataSetTableAdapters.AddressTableAdapter();
            this.contactBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.contactTableAdapter = new Contact_Manager.Contact_ManagerDataSetTableAdapters.ContactTableAdapter();
            this.contact_ManagerGeneralDetailView = new Contact_Manager.Contact_ManagerGeneralDetailView();
            this.generalDetailViewBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.generalDetailViewTableAdapter = new Contact_Manager.Contact_ManagerGeneralDetailViewTableAdapters.GeneralDetailViewTableAdapter();
            this.cityLabel = new System.Windows.Forms.Label();
            this.cityText = new System.Windows.Forms.TextBox();
            this.stateLabel = new System.Windows.Forms.Label();
            this.stateText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ZipCodeLabel = new System.Windows.Forms.Label();
            this.zipCodeText = new System.Windows.Forms.TextBox();
            this.countryLabel = new System.Windows.Forms.Label();
            this.countryText = new System.Windows.Forms.TextBox();
            this.loadButton = new System.Windows.Forms.Button();
            this.firstNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.middleNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dOBDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.genderDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.phoneNumberDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.emailIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addressLineDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.countryDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.zipCodeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.contactManDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contact_ManagerDataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.addressBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contactBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contact_ManagerGeneralDetailView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.generalDetailViewBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // ContactList
            // 
            this.ContactList.AutoSize = true;
            this.ContactList.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ContactList.Location = new System.Drawing.Point(34, 21);
            this.ContactList.Name = "ContactList";
            this.ContactList.Size = new System.Drawing.Size(129, 18);
            this.ContactList.TabIndex = 1;
            this.ContactList.Text = "Contact Details:";
            // 
            // EditButton
            // 
            this.EditButton.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EditButton.Location = new System.Drawing.Point(169, 554);
            this.EditButton.Name = "EditButton";
            this.EditButton.Size = new System.Drawing.Size(90, 28);
            this.EditButton.TabIndex = 14;
            this.EditButton.Text = "Update";
            this.EditButton.UseVisualStyleBackColor = true;
            this.EditButton.Click += new System.EventHandler(this.EditButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddButton.Location = new System.Drawing.Point(36, 554);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(90, 28);
            this.AddButton.TabIndex = 13;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DeleteButton.Location = new System.Drawing.Point(301, 554);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(90, 28);
            this.DeleteButton.TabIndex = 15;
            this.DeleteButton.Text = "Delete";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // FirstNameLabel
            // 
            this.FirstNameLabel.AutoSize = true;
            this.FirstNameLabel.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FirstNameLabel.Location = new System.Drawing.Point(33, 330);
            this.FirstNameLabel.Name = "FirstNameLabel";
            this.FirstNameLabel.Size = new System.Drawing.Size(74, 17);
            this.FirstNameLabel.TabIndex = 3;
            this.FirstNameLabel.Text = "First Name";
            // 
            // firstNameText
            // 
            this.firstNameText.Location = new System.Drawing.Point(119, 327);
            this.firstNameText.Name = "firstNameText";
            this.firstNameText.Size = new System.Drawing.Size(130, 20);
            this.firstNameText.TabIndex = 1;
            // 
            // MiddleNameLabel
            // 
            this.MiddleNameLabel.AutoSize = true;
            this.MiddleNameLabel.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MiddleNameLabel.Location = new System.Drawing.Point(284, 327);
            this.MiddleNameLabel.Name = "MiddleNameLabel";
            this.MiddleNameLabel.Size = new System.Drawing.Size(88, 17);
            this.MiddleNameLabel.TabIndex = 3;
            this.MiddleNameLabel.Text = "Middle Name";
            // 
            // middleNameText
            // 
            this.middleNameText.Location = new System.Drawing.Point(397, 326);
            this.middleNameText.Name = "middleNameText";
            this.middleNameText.Size = new System.Drawing.Size(130, 20);
            this.middleNameText.TabIndex = 2;
            // 
            // LastNameLabel
            // 
            this.LastNameLabel.AutoSize = true;
            this.LastNameLabel.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LastNameLabel.Location = new System.Drawing.Point(554, 327);
            this.LastNameLabel.Name = "LastNameLabel";
            this.LastNameLabel.Size = new System.Drawing.Size(74, 17);
            this.LastNameLabel.TabIndex = 3;
            this.LastNameLabel.Text = "Last Name";
            // 
            // lastNameText
            // 
            this.lastNameText.Location = new System.Drawing.Point(647, 324);
            this.lastNameText.Name = "lastNameText";
            this.lastNameText.Size = new System.Drawing.Size(130, 20);
            this.lastNameText.TabIndex = 3;
            // 
            // DOBLabel
            // 
            this.DOBLabel.AutoSize = true;
            this.DOBLabel.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DOBLabel.Location = new System.Drawing.Point(33, 373);
            this.DOBLabel.Name = "DOBLabel";
            this.DOBLabel.Size = new System.Drawing.Size(62, 17);
            this.DOBLabel.TabIndex = 3;
            this.DOBLabel.Text = "Birthdate";
            // 
            // dOBText
            // 
            this.dOBText.Location = new System.Drawing.Point(119, 370);
            this.dOBText.Name = "dOBText";
            this.dOBText.Size = new System.Drawing.Size(130, 20);
            this.dOBText.TabIndex = 4;
            this.dOBText.MouseEnter += new System.EventHandler(this.dOBText_MouseEnter);
            this.dOBText.MouseLeave += new System.EventHandler(this.dOBText_MouseLeave);
            // 
            // GenderLabel
            // 
            this.GenderLabel.AutoSize = true;
            this.GenderLabel.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GenderLabel.Location = new System.Drawing.Point(284, 373);
            this.GenderLabel.Name = "GenderLabel";
            this.GenderLabel.Size = new System.Drawing.Size(51, 17);
            this.GenderLabel.TabIndex = 3;
            this.GenderLabel.Text = "Gender";
            // 
            // genderComboBox
            // 
            this.genderComboBox.AllowDrop = true;
            this.genderComboBox.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.genderComboBox.FormattingEnabled = true;
            this.genderComboBox.Items.AddRange(new object[] {
            "M",
            "F",
            "Other"});
            this.genderComboBox.Location = new System.Drawing.Point(397, 370);
            this.genderComboBox.Name = "genderComboBox";
            this.genderComboBox.Size = new System.Drawing.Size(68, 25);
            this.genderComboBox.TabIndex = 5;
            // 
            // emailIDText
            // 
            this.emailIDText.Location = new System.Drawing.Point(647, 369);
            this.emailIDText.Name = "emailIDText";
            this.emailIDText.Size = new System.Drawing.Size(130, 20);
            this.emailIDText.TabIndex = 6;
            // 
            // AddressLineLabel
            // 
            this.AddressLineLabel.AutoSize = true;
            this.AddressLineLabel.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddressLineLabel.Location = new System.Drawing.Point(322, 419);
            this.AddressLineLabel.Name = "AddressLineLabel";
            this.AddressLineLabel.Size = new System.Drawing.Size(83, 17);
            this.AddressLineLabel.TabIndex = 3;
            this.AddressLineLabel.Text = "AddressLine";
            // 
            // addressLineText
            // 
            this.addressLineText.Location = new System.Drawing.Point(451, 418);
            this.addressLineText.Name = "addressLineText";
            this.addressLineText.Size = new System.Drawing.Size(326, 20);
            this.addressLineText.TabIndex = 8;
            // 
            // EmailIDLabel
            // 
            this.EmailIDLabel.AutoSize = true;
            this.EmailIDLabel.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EmailIDLabel.Location = new System.Drawing.Point(554, 371);
            this.EmailIDLabel.Name = "EmailIDLabel";
            this.EmailIDLabel.Size = new System.Drawing.Size(61, 17);
            this.EmailIDLabel.TabIndex = 3;
            this.EmailIDLabel.Text = "Email ID";
            // 
            // PNoLabel
            // 
            this.PNoLabel.AutoSize = true;
            this.PNoLabel.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PNoLabel.Location = new System.Drawing.Point(34, 417);
            this.PNoLabel.Name = "PNoLabel";
            this.PNoLabel.Size = new System.Drawing.Size(97, 17);
            this.PNoLabel.TabIndex = 3;
            this.PNoLabel.Text = "Phone Number";
            // 
            // phoneNoText
            // 
            this.phoneNoText.Location = new System.Drawing.Point(152, 416);
            this.phoneNoText.Name = "phoneNoText";
            this.phoneNoText.Size = new System.Drawing.Size(130, 20);
            this.phoneNoText.TabIndex = 7;
            // 
            // contactManDataGridView
            // 
            this.contactManDataGridView.AllowUserToAddRows = false;
            this.contactManDataGridView.AllowUserToDeleteRows = false;
            this.contactManDataGridView.AutoGenerateColumns = false;
            this.contactManDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.contactManDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.firstNameDataGridViewTextBoxColumn,
            this.middleNameDataGridViewTextBoxColumn,
            this.lastNameDataGridViewTextBoxColumn,
            this.dOBDataGridViewTextBoxColumn,
            this.genderDataGridViewTextBoxColumn,
            this.phoneNumberDataGridViewTextBoxColumn,
            this.emailIDDataGridViewTextBoxColumn,
            this.addressLineDataGridViewTextBoxColumn,
            this.cityDataGridViewTextBoxColumn,
            this.stateDataGridViewTextBoxColumn,
            this.countryDataGridViewTextBoxColumn,
            this.zipCodeDataGridViewTextBoxColumn});
            this.contactManDataGridView.DataSource = this.generalDetailViewBindingSource;
            this.contactManDataGridView.Location = new System.Drawing.Point(37, 55);
            this.contactManDataGridView.Name = "contactManDataGridView";
            this.contactManDataGridView.ReadOnly = true;
            this.contactManDataGridView.Size = new System.Drawing.Size(740, 245);
            this.contactManDataGridView.TabIndex = 0;
            this.contactManDataGridView.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.contactManDataGridView_CellClick);
            // 
            // contact_ManagerDataSet
            // 
            this.contact_ManagerDataSet.DataSetName = "Contact_ManagerDataSet";
            this.contact_ManagerDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // addressBindingSource
            // 
            this.addressBindingSource.DataMember = "Address";
            this.addressBindingSource.DataSource = this.contact_ManagerDataSet;
            // 
            // addressTableAdapter
            // 
            this.addressTableAdapter.ClearBeforeFill = true;
            // 
            // contactBindingSource
            // 
            this.contactBindingSource.DataMember = "Contact";
            this.contactBindingSource.DataSource = this.contact_ManagerDataSet;
            // 
            // contactTableAdapter
            // 
            this.contactTableAdapter.ClearBeforeFill = true;
            // 
            // contact_ManagerGeneralDetailView
            // 
            this.contact_ManagerGeneralDetailView.DataSetName = "Contact_ManagerGeneralDetailView";
            this.contact_ManagerGeneralDetailView.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // generalDetailViewBindingSource
            // 
            this.generalDetailViewBindingSource.DataMember = "GeneralDetailView";
            this.generalDetailViewBindingSource.DataSource = this.contact_ManagerGeneralDetailView;
            // 
            // generalDetailViewTableAdapter
            // 
            this.generalDetailViewTableAdapter.ClearBeforeFill = true;
            // 
            // cityLabel
            // 
            this.cityLabel.AutoSize = true;
            this.cityLabel.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cityLabel.Location = new System.Drawing.Point(39, 462);
            this.cityLabel.Name = "cityLabel";
            this.cityLabel.Size = new System.Drawing.Size(32, 17);
            this.cityLabel.TabIndex = 3;
            this.cityLabel.Text = "City";
            // 
            // cityText
            // 
            this.cityText.Location = new System.Drawing.Point(119, 461);
            this.cityText.Name = "cityText";
            this.cityText.Size = new System.Drawing.Size(130, 20);
            this.cityText.TabIndex = 9;
            // 
            // stateLabel
            // 
            this.stateLabel.AutoSize = true;
            this.stateLabel.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stateLabel.Location = new System.Drawing.Point(284, 461);
            this.stateLabel.Name = "stateLabel";
            this.stateLabel.Size = new System.Drawing.Size(38, 17);
            this.stateLabel.TabIndex = 3;
            this.stateLabel.Text = "State";
            // 
            // stateText
            // 
            this.stateText.Location = new System.Drawing.Point(398, 460);
            this.stateText.Name = "stateText";
            this.stateText.Size = new System.Drawing.Size(130, 20);
            this.stateText.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(294, 503);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "City";
            // 
            // ZipCodeLabel
            // 
            this.ZipCodeLabel.AutoSize = true;
            this.ZipCodeLabel.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ZipCodeLabel.Location = new System.Drawing.Point(284, 502);
            this.ZipCodeLabel.Name = "ZipCodeLabel";
            this.ZipCodeLabel.Size = new System.Drawing.Size(58, 17);
            this.ZipCodeLabel.TabIndex = 3;
            this.ZipCodeLabel.Text = "ZipCode";
            // 
            // zipCodeText
            // 
            this.zipCodeText.Location = new System.Drawing.Point(397, 501);
            this.zipCodeText.Name = "zipCodeText";
            this.zipCodeText.Size = new System.Drawing.Size(130, 20);
            this.zipCodeText.TabIndex = 12;
            // 
            // countryLabel
            // 
            this.countryLabel.AutoSize = true;
            this.countryLabel.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.countryLabel.Location = new System.Drawing.Point(39, 504);
            this.countryLabel.Name = "countryLabel";
            this.countryLabel.Size = new System.Drawing.Size(55, 17);
            this.countryLabel.TabIndex = 3;
            this.countryLabel.Text = "Country";
            // 
            // countryText
            // 
            this.countryText.Location = new System.Drawing.Point(119, 503);
            this.countryText.Name = "countryText";
            this.countryText.Size = new System.Drawing.Size(130, 20);
            this.countryText.TabIndex = 11;
            // 
            // loadButton
            // 
            this.loadButton.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.loadButton.Location = new System.Drawing.Point(416, 554);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(90, 28);
            this.loadButton.TabIndex = 16;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // firstNameDataGridViewTextBoxColumn
            // 
            this.firstNameDataGridViewTextBoxColumn.DataPropertyName = "FirstName";
            this.firstNameDataGridViewTextBoxColumn.HeaderText = "FirstName";
            this.firstNameDataGridViewTextBoxColumn.Name = "firstNameDataGridViewTextBoxColumn";
            this.firstNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // middleNameDataGridViewTextBoxColumn
            // 
            this.middleNameDataGridViewTextBoxColumn.DataPropertyName = "MiddleName";
            this.middleNameDataGridViewTextBoxColumn.HeaderText = "MiddleName";
            this.middleNameDataGridViewTextBoxColumn.Name = "middleNameDataGridViewTextBoxColumn";
            this.middleNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // lastNameDataGridViewTextBoxColumn
            // 
            this.lastNameDataGridViewTextBoxColumn.DataPropertyName = "LastName";
            this.lastNameDataGridViewTextBoxColumn.HeaderText = "LastName";
            this.lastNameDataGridViewTextBoxColumn.Name = "lastNameDataGridViewTextBoxColumn";
            this.lastNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // dOBDataGridViewTextBoxColumn
            // 
            this.dOBDataGridViewTextBoxColumn.DataPropertyName = "DOB";
            this.dOBDataGridViewTextBoxColumn.HeaderText = "DOB";
            this.dOBDataGridViewTextBoxColumn.Name = "dOBDataGridViewTextBoxColumn";
            this.dOBDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // genderDataGridViewTextBoxColumn
            // 
            this.genderDataGridViewTextBoxColumn.DataPropertyName = "Gender";
            this.genderDataGridViewTextBoxColumn.HeaderText = "Gender";
            this.genderDataGridViewTextBoxColumn.Name = "genderDataGridViewTextBoxColumn";
            this.genderDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // phoneNumberDataGridViewTextBoxColumn
            // 
            this.phoneNumberDataGridViewTextBoxColumn.DataPropertyName = "PhoneNumber";
            this.phoneNumberDataGridViewTextBoxColumn.HeaderText = "PhoneNumber";
            this.phoneNumberDataGridViewTextBoxColumn.Name = "phoneNumberDataGridViewTextBoxColumn";
            this.phoneNumberDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // emailIDDataGridViewTextBoxColumn
            // 
            this.emailIDDataGridViewTextBoxColumn.DataPropertyName = "EmailID";
            this.emailIDDataGridViewTextBoxColumn.HeaderText = "EmailID";
            this.emailIDDataGridViewTextBoxColumn.Name = "emailIDDataGridViewTextBoxColumn";
            this.emailIDDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // addressLineDataGridViewTextBoxColumn
            // 
            this.addressLineDataGridViewTextBoxColumn.DataPropertyName = "AddressLine";
            this.addressLineDataGridViewTextBoxColumn.HeaderText = "AddressLine";
            this.addressLineDataGridViewTextBoxColumn.Name = "addressLineDataGridViewTextBoxColumn";
            this.addressLineDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // cityDataGridViewTextBoxColumn
            // 
            this.cityDataGridViewTextBoxColumn.DataPropertyName = "City";
            this.cityDataGridViewTextBoxColumn.HeaderText = "City";
            this.cityDataGridViewTextBoxColumn.Name = "cityDataGridViewTextBoxColumn";
            this.cityDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // stateDataGridViewTextBoxColumn
            // 
            this.stateDataGridViewTextBoxColumn.DataPropertyName = "State";
            this.stateDataGridViewTextBoxColumn.HeaderText = "State";
            this.stateDataGridViewTextBoxColumn.Name = "stateDataGridViewTextBoxColumn";
            this.stateDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // countryDataGridViewTextBoxColumn
            // 
            this.countryDataGridViewTextBoxColumn.DataPropertyName = "Country";
            this.countryDataGridViewTextBoxColumn.HeaderText = "Country";
            this.countryDataGridViewTextBoxColumn.Name = "countryDataGridViewTextBoxColumn";
            this.countryDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // zipCodeDataGridViewTextBoxColumn
            // 
            this.zipCodeDataGridViewTextBoxColumn.DataPropertyName = "ZipCode";
            this.zipCodeDataGridViewTextBoxColumn.HeaderText = "ZipCode";
            this.zipCodeDataGridViewTextBoxColumn.Name = "zipCodeDataGridViewTextBoxColumn";
            this.zipCodeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // ContactManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 598);
            this.Controls.Add(this.contactManDataGridView);
            this.Controls.Add(this.genderComboBox);
            this.Controls.Add(this.lastNameText);
            this.Controls.Add(this.middleNameText);
            this.Controls.Add(this.addressLineText);
            this.Controls.Add(this.stateText);
            this.Controls.Add(this.zipCodeText);
            this.Controls.Add(this.countryText);
            this.Controls.Add(this.cityText);
            this.Controls.Add(this.emailIDText);
            this.Controls.Add(this.phoneNoText);
            this.Controls.Add(this.dOBText);
            this.Controls.Add(this.firstNameText);
            this.Controls.Add(this.LastNameLabel);
            this.Controls.Add(this.MiddleNameLabel);
            this.Controls.Add(this.ZipCodeLabel);
            this.Controls.Add(this.GenderLabel);
            this.Controls.Add(this.stateLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.AddressLineLabel);
            this.Controls.Add(this.countryLabel);
            this.Controls.Add(this.cityLabel);
            this.Controls.Add(this.EmailIDLabel);
            this.Controls.Add(this.PNoLabel);
            this.Controls.Add(this.DOBLabel);
            this.Controls.Add(this.FirstNameLabel);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.DeleteButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.EditButton);
            this.Controls.Add(this.ContactList);
            this.Name = "ContactManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Contact Manager";
            this.Load += new System.EventHandler(this.ContactManager_Load);
            ((System.ComponentModel.ISupportInitialize)(this.contactManDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contact_ManagerDataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.addressBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contactBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contact_ManagerGeneralDetailView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.generalDetailViewBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label ContactList;
        private System.Windows.Forms.Button EditButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Label FirstNameLabel;
        private System.Windows.Forms.TextBox firstNameText;
        private System.Windows.Forms.Label MiddleNameLabel;
        private System.Windows.Forms.TextBox middleNameText;
        private System.Windows.Forms.Label LastNameLabel;
        private System.Windows.Forms.TextBox lastNameText;
        private System.Windows.Forms.Label DOBLabel;
        private System.Windows.Forms.TextBox dOBText;
        private System.Windows.Forms.Label GenderLabel;
        private System.Windows.Forms.ComboBox genderComboBox;
        private System.Windows.Forms.TextBox emailIDText;
        private System.Windows.Forms.Label AddressLineLabel;
        private System.Windows.Forms.TextBox addressLineText;
        private System.Windows.Forms.Label EmailIDLabel;
        private System.Windows.Forms.Label PNoLabel;
        private System.Windows.Forms.TextBox phoneNoText;
        private System.Windows.Forms.DataGridView contactManDataGridView;
        private Contact_ManagerDataSet contact_ManagerDataSet;
        private System.Windows.Forms.BindingSource addressBindingSource;
        private Contact_ManagerDataSetTableAdapters.AddressTableAdapter addressTableAdapter;
        private System.Windows.Forms.BindingSource contactBindingSource;
        private Contact_ManagerDataSetTableAdapters.ContactTableAdapter contactTableAdapter;
        private Contact_ManagerGeneralDetailView contact_ManagerGeneralDetailView;
        private System.Windows.Forms.BindingSource generalDetailViewBindingSource;
        private Contact_ManagerGeneralDetailViewTableAdapters.GeneralDetailViewTableAdapter generalDetailViewTableAdapter;
        private System.Windows.Forms.Label cityLabel;
        private System.Windows.Forms.TextBox cityText;
        private System.Windows.Forms.Label stateLabel;
        private System.Windows.Forms.TextBox stateText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label ZipCodeLabel;
        private System.Windows.Forms.TextBox zipCodeText;
        private System.Windows.Forms.Label countryLabel;
        private System.Windows.Forms.TextBox countryText;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.DataGridViewTextBoxColumn firstNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn middleNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn dOBDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn genderDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn phoneNumberDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn emailIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn addressLineDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn cityDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stateDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn countryDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn zipCodeDataGridViewTextBoxColumn;
    }
}