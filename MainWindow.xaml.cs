using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//Add the following namespaces
using System.Data;              //This namespace contains the DISCONNECTED ADO.Net classes
using System.Data.SqlClient;    //This namespace contains the CONNECTED ADO.Net classes for SQL Server
using System.Configuration;     //This namespace contains classes for reading the App.config file (amoungst other things).
using System.Diagnostics;       //Debug.Print(strSql) out to the output window;

namespace PracticeManagement
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["cnnStrPractice"].ConnectionString;
        private string selectedPatientNo;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadFieldNamesCombo();
            ShowData();
        }

        private void ShowData()
        {
            //Use the PatientCollection class to display the patients to the listview
            PatientCollection myPatients;
            try
            {
                //Check the combobox and the textbox for values
                if (cmbBoxSearch.SelectedValue != null && txtBoxSearch.Text != null)
                {
                    //Search criteria specified (i.e. user has selected field to search and provided search string)
                    myPatients = new PatientCollection(cmbBoxSearch.SelectedValue.ToString(), txtBoxSearch.Text);
                    lvPatients.DataContext = myPatients; //Here we're actually setting the DataContext of the control to the PilotCollection class.
                }
                else
                {
                    //First time window loads (or clicks the search button without specifying search criteria)
                    myPatients = new PatientCollection(null, null);
                    lvPatients.DataContext = myPatients;
                }
            }

            catch (Exception ex)
            {

                MessageBox.Show("Unable to load the data! " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

            //SqlCommand myCmd = new SqlCommand();
            try
            {
                Patient selectedPatient;

                selectedPatient = (Patient)lvPatients.SelectedItem;
                selectedPatient.DeletePatient();
                ShowData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error!  The Patient was not deleted.  Please try again later. " + ex.Message);
            }
            
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Patient newPatient = new Patient();
                newPatient.PatientID = txtBxPatientID.Text;
                newPatient.FirstName = txtBxFirstName.Text;
                newPatient.Surname = txtBxSurname.Text;
                newPatient.Gender = txtBxGender.Text;
                newPatient.DateOfBirth = DateTime.Parse(txtBxDOB.Text);
                newPatient.Address = txtBxAddress.Text;
                newPatient.Suburb = txtBxSuburb.Text;
                newPatient.Postcode = txtBxPostcode.Text;
                newPatient.MedicareNo = txtBxMedicare.Text;
                newPatient.HomePhone = txtBxHome.Text;
                newPatient.PrivateHealth = txtBxPrivateHealth.Text;
                newPatient.DiseaseHistory = txtBxDiseaseHistory.Text;

                //Add patient to database
                newPatient.AddPatient();

                //Refresh the listview
                ShowData();

                //Thank the baby jesus
                MessageBox.Show("You fucking rock, this bitch was added.");
                
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error!  The Patient was not added. Please try again later. " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (lvPatients.SelectedIndex == -1)
            {
                MessageBox.Show("Yo, select a patient first!");
                return;
            }

                Patient selectedPatient = (Patient)lvPatients.SelectedItem;
                selectedPatient.OriginalPatientID = selectedPatient.PatientID;
                selectedPatient.PatientID = txtBxPatientID.Text;
                selectedPatient.FirstName = txtBxFirstName.Text;
                selectedPatient.Surname = txtBxSurname.Text;
                selectedPatient.Gender = txtBxGender.Text;
                selectedPatient.DateOfBirth = DateTime.Parse(txtBxDOB.Text);
                selectedPatient.Address = txtBxAddress.Text;
                selectedPatient.Suburb = txtBxSuburb.Text;
                selectedPatient.Postcode = txtBxPostcode.Text;
                selectedPatient.MedicareNo = txtBxMedicare.Text;
                selectedPatient.HomePhone = txtBxHome.Text;
                selectedPatient.PrivateHealth = txtBxPrivateHealth.Text;
                selectedPatient.DiseaseHistory = txtBxDiseaseHistory.Text;
            try
            {
                selectedPatient.UpdatePatient();

                ShowData();

                lvPatients.Focus();

                MessageBox.Show("You fucking rock, this bitch was updated.");


            }
            catch (Exception ex)
            {

                MessageBox.Show("Error!  The Patient was not updated. Please try again later. " + ex.Message);
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtBxAddress.Clear();
            txtBxDiseaseHistory.Clear();
            txtBxDOB.Clear();
            txtBxFirstName.Clear();
            txtBxGender.Clear();
            txtBxHome.Clear();
            txtBxMedicare.Clear();
            txtBxPatientID.Clear();
            txtBxPostcode.Clear();
            txtBxPrivateHealth.Clear();
            txtBxSuburb.Clear();
            txtBxSurname.Clear();

            lvPatients.SelectedIndex = -1;
        }

        private void LoadFieldNamesCombo()
        {
            try
            {
                DataAccessLayer myDAL = new DataAccessLayer(this.connectionString);
                myDAL.addParameter("@TableName", "Patient", DataAccessLayer.GenericDataType.genString);

                DataTable dtFieldNames = new DataTable();
                dtFieldNames = myDAL.RunSPDataTable("usp_GetPatientFieldNames");

                //Map the database table field names to more human friendly names.
                //Define a new column with the column name "Friendly names"
                DataColumn colFriendlyNames = new DataColumn("FRIENDLY_NAMES", System.Type.GetType("System.String"));
                //Add this column to our DataTable
                dtFieldNames.Columns.Add(colFriendlyNames);

                foreach (DataRow currentRow in dtFieldNames.Rows)
                {
                    switch (currentRow[0].ToString())
                    {
                        case "Patient_ID":
                            currentRow[1] = "Patient ID";
                            break;
                        case "FirstName":
                            currentRow[1] = "First Name";
                            break;
                        case "Surname":
                            currentRow[1] = "Surname";
                            break;
                        case "Gender":
                            currentRow[1] = "Gender";
                            break;
                        case "Date_Of_Birth":
                            currentRow[1] = "DOB";
                            break;
                        case "Address":
                            currentRow[1] = "Address";
                            break;
                        case "Suburb":
                            currentRow[1] = "Suburb";
                            break;
                        case "Postcode":
                            currentRow[1] = "Postcode";
                            break;
                        case "Medicare_No":
                            currentRow[1] = "Medicare Number";
                            break;
                        case "Phone":
                            currentRow[1] = "Phone";
                            break;
                        case "Private_Health":
                            currentRow[1] = "Health Insurance Number";
                            break;
                        case "Disease_History":
                            currentRow[1] = "Disease";
                            break;
                        default:
                            //currentRow[1] = "-- Please Select Field --";
                            break;
                    }
                }
                cmbBoxSearch.SelectedValuePath = "COLUMN_NAME";
                cmbBoxSearch.DisplayMemberPath = "FRIENDLY_NAMES";

                DataRow dflt;
                dflt = dtFieldNames.NewRow();
                dflt["COLUMN_NAME"] = "Default";
                dflt["FRIENDLY_NAMES"] = "--Select--";
                dtFieldNames.Rows.InsertAt(dflt, 0);
                cmbBoxSearch.ItemsSource = dtFieldNames.DefaultView;

            }
            catch (Exception ex)
            {

                MessageBox.Show("An error occured loading the field names. " + ex.Message);
            }


        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            ShowData();
        }
    }
}
