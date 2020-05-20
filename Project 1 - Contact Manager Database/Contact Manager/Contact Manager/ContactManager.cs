using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Contact_Manager;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Globalization;

namespace Contact_Manager
{
    public struct PreUpdateValues
    {
        public string FirstName;
        public string MiddleName;
        public string LastName;
        public string BirthDate;
        public string Gender;
        public string DateFirstMet;
        public string DateAdded;

        public string EmailID;

        public string PhoneNo;

        public string AddressLines;
        public string City;
        public string State;
        public string Country;
        public string ZipCode;        
    };
    

    public partial class ContactManager : Form
    {
        //Used to store Original Values before Update
        PreUpdateValues preValueSet;

        /// <summary>
        /// Constructor.
        /// </summary>
        public ContactManager()
        {
            InitializeComponent();
        }
 

        /// <summary>
        /// Functionality to add contacts to a database - one at a time
        /// </summary>
        private void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(firstNameText.Text.Trim()))
                {
                    MessageBox.Show("Enter First Name");
                    return;
                }

                if (string.IsNullOrEmpty(genderComboBox.Text.Trim()))
                {
                    MessageBox.Show("Select a Gender");
                    return;
                }

                try
                {
                    DateTime tempDate = DateTime.ParseExact(dOBText.Text.Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Enter Birthdate in \"yyyy-MM-dd\" format");
                    return;
                }

                bool CanAddressBeAdded = false;                

                //Add address only if we have state, country and zipcode
                if ((!string.IsNullOrEmpty(stateText.Text.Trim())) && (!string.IsNullOrEmpty(countryText.Text.Trim())) && (!string.IsNullOrEmpty(zipCodeText.Text.Trim())))
                {
                    CanAddressBeAdded = true;
                }


                //Connection string to connect to the database is "Data Source=LAPTOP-U8QNP91K\SQL2017A1;Initial Catalog="Contact Manager";Integrated Security=True" - Windows authentication
                using (SqlConnection sqlCon = new SqlConnection("Data Source=LAPTOP-U8QNP91K\\SQL2017A1;Initial Catalog=\"Contact Manager\";Integrated Security=True"))
                {
                    sqlCon.Open();

                    SqlCommand sqlCmd = new SqlCommand("Add_Contact", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    sqlCmd.Parameters.AddWithValue("@FirstName", SqlDbType.VarChar).Value = firstNameText.Text.Trim();

                    if (!string.IsNullOrEmpty(middleNameText.Text.Trim()))
                    {
                        sqlCmd.Parameters.AddWithValue("@MiddleName", SqlDbType.VarChar).Value = middleNameText.Text.Trim(); 
                    }

                    if (!string.IsNullOrEmpty(lastNameText.Text.Trim()))
                    {
                        sqlCmd.Parameters.AddWithValue("@LastName", SqlDbType.VarChar).Value = lastNameText.Text.Trim(); 
                    }

                    if (!string.IsNullOrEmpty(dOBText.Text.Trim()))
                    {
                        sqlCmd.Parameters.AddWithValue("@BirthDate", SqlDbType.DateTime).Value = dOBText.Text.Trim(); 
                    }

                    sqlCmd.Parameters.AddWithValue("@Gender", SqlDbType.NChar).Value = genderComboBox.Text.Trim();

                    if (!string.IsNullOrEmpty(emailIDText.Text.Trim()))
                    {
                        sqlCmd.Parameters.AddWithValue("@EmailID", SqlDbType.VarChar).Value = emailIDText.Text.Trim(); 
                    }

                    if (!string.IsNullOrEmpty(phoneNoText.Text.Trim()))
                    {
                        sqlCmd.Parameters.AddWithValue("@PhoneNo", SqlDbType.VarChar).Value = phoneNoText.Text.Trim(); 
                    }

                    string AddressLines = addressLineText.Text.Trim();
                    string[] AddressLineList = addressLineText.Text.Replace("\r", "").Split('\n');
                    if (AddressLineList.Count() > 0 && (!string.IsNullOrEmpty(AddressLineList[0].Trim())) && CanAddressBeAdded)
                    {
                        sqlCmd.Parameters.AddWithValue("@AddressLines", SqlDbType.VarChar).Value = AddressLineList[0].Trim();
                    }

                    if (!string.IsNullOrEmpty(cityText.Text.Trim()))
                    {
                        sqlCmd.Parameters.AddWithValue("@City", SqlDbType.VarChar).Value = cityText.Text.Trim(); 
                    }

                    if (CanAddressBeAdded)
                    {
                        sqlCmd.Parameters.AddWithValue("@State", SqlDbType.VarChar).Value = stateText.Text.Trim();
                        sqlCmd.Parameters.AddWithValue("@Country", SqlDbType.VarChar).Value = countryText.Text.Trim();
                        sqlCmd.Parameters.AddWithValue("@ZipCode", SqlDbType.VarChar).Value = zipCodeText.Text.Trim(); 
                    }

                    sqlCmd.ExecuteNonQuery();

                    #region Need to add support for multiple address lines
                    //if (AddressLineList.Count() > 1)
                    //{
                    //    SqlCommand sqlCmd2 = new SqlCommand("Add_AddressLines", sqlCon);
                    //    sqlCmd2.CommandType = CommandType.StoredProcedure;

                    //    sqlCmd2.Parameters.AddWithValue("@AddressID", SqlDbType.VarChar).Value = firstNameText.Text.Trim();

                    //    if (!string.IsNullOrEmpty(middleNameText.Text.Trim()))
                    //    {
                    //        sqlCmd2.Parameters.AddWithValue("@AddressLines", SqlDbType.VarChar).Value = middleNameText.Text.Trim();
                    //    }
                    //} 
                    #endregion

                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Exception: " + ex.Message);
                MessageBox.Show("Unable to Add Contact");
            }
        }        

        /// <summary>
        /// Functionality to edit contacts on a database - one at a time
        /// </summary>
        private void EditButton_Click(object sender, EventArgs e)
        {
            try
            {
                //if (!this.preValueSet.Equals(default(PreUpdateValues)))
                {
                    if (string.IsNullOrEmpty(firstNameText.Text.Trim()))
                    {
                        MessageBox.Show("Enter First Name");
                        return;
                    }

                    if (string.IsNullOrEmpty(genderComboBox.Text.Trim()))
                    {
                        MessageBox.Show("Select a Gender");
                        return;
                    }

                    try
                    {
                        DateTime tempDate = DateTime.ParseExact(dOBText.Text.Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    }
                    catch
                    {
                        MessageBox.Show("Enter Birthdate in \"dd-MM-yyyy\" format");
                        return;
                    }

                    bool WasOldAddressAdded = false;
                    bool CanAddressBeAdded = false;

                    //check if old address was added
                    if ((!string.IsNullOrEmpty(preValueSet.State.Trim())) && (!string.IsNullOrEmpty(preValueSet.Country.Trim())) && (!string.IsNullOrEmpty(preValueSet.ZipCode.Trim())))
                    {
                        WasOldAddressAdded = true;
                    }

                    //Add address only if we have state, country and zipcode
                    if ((!string.IsNullOrEmpty(stateText.Text.Trim())) && (!string.IsNullOrEmpty(countryText.Text.Trim())) && (!string.IsNullOrEmpty(zipCodeText.Text.Trim())))
                    {
                        CanAddressBeAdded = true;
                    }

                    //Connection string to connect to the database is "Data Source=LAPTOP-U8QNP91K\SQL2017A1;Initial Catalog="Contact Manager";Integrated Security=True" - Windows authentication
                    using (SqlConnection sqlCon = new SqlConnection("Data Source=LAPTOP-U8QNP91K\\SQL2017A1;Initial Catalog=\"Contact Manager\";Integrated Security=True"))
                    {
                        sqlCon.Open();

                        SqlCommand sqlCmd = new SqlCommand("Update_Contact", sqlCon);
                        sqlCmd.CommandType = CommandType.StoredProcedure;

                        sqlCmd.Parameters.AddWithValue("@FirstName", SqlDbType.VarChar).Value = preValueSet.FirstName.Trim();

                        if (!string.IsNullOrEmpty(preValueSet.MiddleName.Trim()))
                        {
                            sqlCmd.Parameters.AddWithValue("@MiddleName", SqlDbType.VarChar).Value = preValueSet.MiddleName.Trim();
                        }

                        if (!string.IsNullOrEmpty(preValueSet.LastName.Trim()))
                        {
                            sqlCmd.Parameters.AddWithValue("@LastName", SqlDbType.VarChar).Value = preValueSet.LastName.Trim();
                        }

                        if (!string.IsNullOrEmpty(preValueSet.BirthDate.Trim()))
                        {
                            sqlCmd.Parameters.AddWithValue("@BirthDate", SqlDbType.DateTime).Value = DateTime.ParseExact(preValueSet.BirthDate.Trim(), "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        }

                        sqlCmd.Parameters.AddWithValue("@Gender", SqlDbType.NChar).Value = preValueSet.Gender.Trim();

                        if (!string.IsNullOrEmpty(preValueSet.EmailID.Trim()))
                        {
                            sqlCmd.Parameters.AddWithValue("@EmailID", SqlDbType.VarChar).Value = preValueSet.EmailID.Trim();
                        }

                        if (!string.IsNullOrEmpty(preValueSet.PhoneNo.Trim()))
                        {
                            sqlCmd.Parameters.AddWithValue("@PhoneNo", SqlDbType.VarChar).Value = preValueSet.PhoneNo.Trim();
                        }

                        string OldAddressLines = preValueSet.AddressLines.Trim();
                        string[] OldAddressLineList = preValueSet.AddressLines.Replace("\r", "").Split('\n');
                        if (OldAddressLineList.Count() > 0 && (!string.IsNullOrEmpty(OldAddressLineList[0].Trim())) && WasOldAddressAdded)
                        {
                            sqlCmd.Parameters.AddWithValue("@AddressLines", SqlDbType.VarChar).Value = OldAddressLineList[0].Trim();
                        }

                        if (!string.IsNullOrEmpty(preValueSet.City.Trim()))
                        {
                            sqlCmd.Parameters.AddWithValue("@City", SqlDbType.VarChar).Value = preValueSet.City.Trim();
                        }

                        if (WasOldAddressAdded)
                        {
                            sqlCmd.Parameters.AddWithValue("@State", SqlDbType.VarChar).Value = preValueSet.State.Trim();
                            sqlCmd.Parameters.AddWithValue("@Country", SqlDbType.VarChar).Value = preValueSet.Country.Trim();
                            sqlCmd.Parameters.AddWithValue("@ZipCode", SqlDbType.VarChar).Value = preValueSet.ZipCode.Trim();
                        }




                        sqlCmd.Parameters.AddWithValue("@NewFirstName", SqlDbType.VarChar).Value = firstNameText.Text.Trim();

                        if (!string.IsNullOrEmpty(middleNameText.Text.Trim()))
                        {
                            sqlCmd.Parameters.AddWithValue("@NewMiddleName", SqlDbType.VarChar).Value = middleNameText.Text.Trim();
                        }

                        if (!string.IsNullOrEmpty(lastNameText.Text.Trim()))
                        {
                            sqlCmd.Parameters.AddWithValue("@NewLastName", SqlDbType.VarChar).Value = lastNameText.Text.Trim();
                        }

                        if (!string.IsNullOrEmpty(dOBText.Text.Trim()))
                        {
                            sqlCmd.Parameters.AddWithValue("@NewBirthDate", SqlDbType.DateTime).Value = DateTime.ParseExact(dOBText.Text.Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                        }

                        sqlCmd.Parameters.AddWithValue("@NewGender", SqlDbType.NChar).Value = genderComboBox.Text.Trim();

                        if (!string.IsNullOrEmpty(emailIDText.Text.Trim()))
                        {
                            sqlCmd.Parameters.AddWithValue("@NewEmailID", SqlDbType.VarChar).Value = emailIDText.Text.Trim();
                        }

                        if (!string.IsNullOrEmpty(phoneNoText.Text.Trim()))
                        {
                            sqlCmd.Parameters.AddWithValue("@NewPhoneNo", SqlDbType.VarChar).Value = phoneNoText.Text.Trim();
                        }

                        string AddressLines = addressLineText.Text.Trim();
                        string[] AddressLineList = addressLineText.Text.Replace("\r", "").Split('\n');
                        if (AddressLineList.Count() > 0 && (!string.IsNullOrEmpty(AddressLineList[0].Trim())) && CanAddressBeAdded)
                        {
                            sqlCmd.Parameters.AddWithValue("@NewAddressLines", SqlDbType.VarChar).Value = AddressLineList[0].Trim();
                        }

                        if (!string.IsNullOrEmpty(cityText.Text.Trim()))
                        {
                            sqlCmd.Parameters.AddWithValue("@NewCity", SqlDbType.VarChar).Value = cityText.Text.Trim();
                        }

                        if (CanAddressBeAdded)
                        {
                            sqlCmd.Parameters.AddWithValue("@NewState", SqlDbType.VarChar).Value = stateText.Text.Trim();
                            sqlCmd.Parameters.AddWithValue("@NewCountry", SqlDbType.VarChar).Value = countryText.Text.Trim();
                            sqlCmd.Parameters.AddWithValue("@NewZipCode", SqlDbType.VarChar).Value = zipCodeText.Text.Trim();
                        }

                        sqlCmd.ExecuteNonQuery();

                        #region Need to add support for multiple address lines
                        //if (AddressLineList.Count() > 1)
                        //{
                        //    SqlCommand sqlCmd2 = new SqlCommand("Add_AddressLines", sqlCon);
                        //    sqlCmd2.CommandType = CommandType.StoredProcedure;

                        //    sqlCmd2.Parameters.AddWithValue("@AddressID", SqlDbType.VarChar).Value = firstNameText.Text.Trim();

                        //    if (!string.IsNullOrEmpty(middleNameText.Text.Trim()))
                        //    {
                        //        sqlCmd2.Parameters.AddWithValue("@AddressLines", SqlDbType.VarChar).Value = middleNameText.Text.Trim();
                        //    }
                        //} 
                        #endregion

                        sqlCon.Close();
                    } 
                }
            }
            catch (Exception ex)
            {
                int error = 1;
            }
        }

        /// <summary>
        /// Functionality to delete a contact on a database - one at a time
        /// </summary>
        private void DeleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(firstNameText.Text.Trim()))
                {
                    MessageBox.Show("Enter First Name");
                    return;
                }

                if (string.IsNullOrEmpty(genderComboBox.Text.Trim()))
                {
                    MessageBox.Show("Select a Gender");
                    return;
                }

                try
                {
                    DateTime tempDate = DateTime.ParseExact(dOBText.Text.Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                }
                catch
                {
                    MessageBox.Show("Enter Birthdate in \"dd-MM-yyyy\" format");
                    return;
                }

                //Connection string to connect to the database is "Data Source=LAPTOP-U8QNP91K\SQL2017A1;Initial Catalog="Contact Manager";Integrated Security=True" - Windows authentication
                using (SqlConnection sqlCon = new SqlConnection("Data Source=LAPTOP-U8QNP91K\\SQL2017A1;Initial Catalog=\"Contact Manager\";Integrated Security=True"))
                {
                    sqlCon.Open();

                    SqlCommand sqlCmd = new SqlCommand("Delete_Contact", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    sqlCmd.Parameters.AddWithValue("@FirstName", SqlDbType.VarChar).Value = firstNameText.Text.Trim();

                    if (!string.IsNullOrEmpty(middleNameText.Text.Trim()))
                    {
                        sqlCmd.Parameters.AddWithValue("@MiddleName", SqlDbType.VarChar).Value = middleNameText.Text.Trim();
                    }

                    if (!string.IsNullOrEmpty(lastNameText.Text.Trim()))
                    {
                        sqlCmd.Parameters.AddWithValue("@LastName", SqlDbType.VarChar).Value = lastNameText.Text.Trim();
                    }

                    if (!string.IsNullOrEmpty(dOBText.Text.Trim()))
                    {
                        sqlCmd.Parameters.AddWithValue("@BirthDate", SqlDbType.DateTime).Value = DateTime.ParseExact( dOBText.Text.Trim(), "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    }

                    sqlCmd.Parameters.AddWithValue("@Gender", SqlDbType.NChar).Value = genderComboBox.Text.Trim();

                    sqlCmd.ExecuteNonQuery();

                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to Delete Contact");
            }
        }

        /// <summary>
        /// This function is called when the form loads.
        /// </summary>
        private void ContactManager_Load(object sender, EventArgs e)
        {
            #region NOT used right now but can be used to load values to datagrid
            // TODO: This line of code loads data into the 'contact_ManagerGeneralDetailView.GeneralDetailView' table. You can move, or remove it, as needed.
            this.generalDetailViewTableAdapter.Fill(this.contact_ManagerGeneralDetailView.GeneralDetailView);
            
            // TODO: This line of code loads data into the 'contact_ManagerDataSet.Contact' table. You can move, or remove it, as needed.
            this.contactTableAdapter.Fill(this.contact_ManagerDataSet.Contact);
            // TODO: This line of code loads data into the 'contact_ManagerDataSet.Address' table. You can move, or remove it, as needed.
            this.addressTableAdapter.Fill(this.contact_ManagerDataSet.Address); 
            #endregion

        }        

        /// <summary>
        /// This function is triggered when the user clicks on the cells of the Data-Grid-view
        /// </summary>
        private void contactManDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if(e.RowIndex >= 0)
                {
                    preValueSet = new PreUpdateValues();
                    DataGridViewRow row = this.contactManDataGridView.Rows[e.RowIndex];

                    preValueSet.FirstName = firstNameText.Text = contactManDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString();

                    if (contactManDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString().Trim() != "NULL")
                    {
                        preValueSet.MiddleName = middleNameText.Text = contactManDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
                    }
                    else
                    {
                        middleNameText.Clear();
                    }

                    if (contactManDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString().Trim() != "NULL")
                    {
                        preValueSet.LastName = lastNameText.Text = contactManDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                    }
                    else
                    {
                        lastNameText.Clear();
                    }

                    if (contactManDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString().Trim() != "NULL")
                    {
                        preValueSet.BirthDate = dOBText.Text = contactManDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
                        //contactManDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString()
                    }
                    else
                    {
                        dOBText.Clear();
                    }

                    preValueSet.Gender = genderComboBox.Text = contactManDataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();

                    if (contactManDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString().Trim() != "NULL")
                    {
                        preValueSet.PhoneNo = phoneNoText.Text = contactManDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString();
                    }
                    else
                    {
                        phoneNoText.Clear();
                    }

                    if (contactManDataGridView.Rows[e.RowIndex].Cells[6].Value.ToString().Trim() != "NULL")
                    {
                        preValueSet.EmailID = emailIDText.Text = contactManDataGridView.Rows[e.RowIndex].Cells[6].Value.ToString();
                    }
                    else
                    {
                        emailIDText.Clear();
                    }

                    if (contactManDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString().Trim() != "NULL")
                    {
                        preValueSet.AddressLines = addressLineText.Text = contactManDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString();
                    }
                    else
                    {
                        addressLineText.Clear();
                    }

                    if (contactManDataGridView.Rows[e.RowIndex].Cells[8].Value.ToString().Trim() != "NULL")
                    {
                        preValueSet.City = cityText.Text = contactManDataGridView.Rows[e.RowIndex].Cells[8].Value.ToString();
                    }
                    else
                    {
                        cityText.Clear();
                    }

                    if (contactManDataGridView.Rows[e.RowIndex].Cells[9].Value.ToString().Trim() != "NULL")
                    {
                        preValueSet.State = stateText.Text = contactManDataGridView.Rows[e.RowIndex].Cells[9].Value.ToString();
                    }
                    else
                    {
                        stateText.Clear();
                    }

                    if (contactManDataGridView.Rows[e.RowIndex].Cells[10].Value.ToString().Trim() != "NULL")
                    {
                        preValueSet.Country = countryText.Text = contactManDataGridView.Rows[e.RowIndex].Cells[10].Value.ToString();
                    }
                    else
                    {
                        countryText.Clear();
                    }

                    if (contactManDataGridView.Rows[e.RowIndex].Cells[11].Value.ToString().Trim() != "NULL")
                    {
                        preValueSet.ZipCode = zipCodeText.Text = contactManDataGridView.Rows[e.RowIndex].Cells[11].Value.ToString();
                    }
                    else
                    {
                        zipCodeText.Clear();
                    }
                }

            }
            catch (Exception)
            {

            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            try
            {
                //Connection string to connect to the database is "Data Source=LAPTOP-U8QNP91K\SQL2017A1;Initial Catalog="Contact Manager";Integrated Security=True" - Windows authentication
                using (SqlConnection sqlCon = new SqlConnection("Data Source=LAPTOP-U8QNP91K\\SQL2017A1;Initial Catalog=\"Contact Manager\";Integrated Security=True"))
                {
                    sqlCon.Open();

                    SqlCommand sqlCmd = new SqlCommand("Display_Contacts", sqlCon);
                    sqlCmd.CommandType = CommandType.StoredProcedure;

                    DataTable sqlDT = new DataTable();
                    sqlDT.Load(sqlCmd.ExecuteReader());

                    contactManDataGridView.DataSource = sqlDT;

                    //sqlCmd.ExecuteNonQuery();

                    sqlCon.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load Contact" + ex.Message);
            }
        }
        private ToolTip tooltip;
        private void dOBText_MouseEnter(object sender, EventArgs e)
        {
            tooltip = new ToolTip();
            tooltip.InitialDelay = 0;
            tooltip.IsBalloon = false;
            tooltip.Show(string.Empty, dOBText);
            tooltip.Show("Date Format: yyyy-mm-dd", dOBText, 0);
        }

        private void dOBText_MouseLeave(object sender, EventArgs e)
        {
            tooltip.Dispose();
        }
    }
}
