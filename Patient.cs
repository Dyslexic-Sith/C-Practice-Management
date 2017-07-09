using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Configuration;
using System.Collections.Specialized;

namespace PracticeManagement
{

    /// <summary>
    /// Defines a Patient object that inherits from Person.
    /// </summary>
    [DataObject(true)]  //True - this object can be bound to a object data source
    class Patient : Person
    {
        #region Private variables/properties

        //Properties that are NOT provided in the Person class
        private string _PatientID;
        private string _originalPatientID;
        private string _Gender;
        private string _Phone;
        private string _MedicareNo;
        private string _PrivateHealth;
        private string _diseaseHistory;

        private DataTable _dtPatient;
        private string _connectionString = ConfigurationManager.ConnectionStrings["cnnStrPractice"].ConnectionString;

        #endregion

        #region Public constructors
        //Default constructor - return an empty patient
        public Patient()
            : base()
        {
            //Does nothing... except for calling its parent's constructor
        }

        //Return a specific patient using the patientID
        public Patient(string patientID)
            : base()
        {
            //Get the DataAccessLayer object
            DataAccessLayer objDAL = new DataAccessLayer(this._connectionString);

            //Assign parameters = patients ID
            objDAL.addParameter("@PatientID", patientID, DataAccessLayer.GenericDataType.genString);

            //Get patient details
            this._dtPatient = objDAL.RunSPDataTable("usp_GetPatient");

            //Populate patient properties using the DataTable
            this.LoadPatientProperties();

            //Clean up!
            objDAL.Dispose();
            this._dtPatient.Dispose();
            objDAL = null;
            this._dtPatient = null;
        }

        //Return a patient using the specified details via a DataRow. This supports the PatientCollection class.
        public Patient(DataRow drPatient)
            : base()
        {
            //Assign patient details to properties
            this.PatientID = drPatient["Patient_ID"].ToString();
            this.FirstName = drPatient["FirstName"].ToString();
            this.Surname = drPatient["Surname"].ToString();
            this.Gender = drPatient["Gender"].ToString();
            this.DateOfBirth = (DateTime)drPatient["Date_Of_Birth"];
            this.Address = drPatient["Address"].ToString();
            this.Suburb = drPatient["Suburb"].ToString();
            this.Postcode = drPatient["Postcode"].ToString();
            this.HomePhone = drPatient["Phone"].ToString();
            this.MedicareNo = drPatient["Medicare_No"].ToString();
            this.PrivateHealth = drPatient["Private_Health"].ToString();
            this.DiseaseHistory = drPatient["Disease_History"].ToString();
        }

        #endregion Public constructors

        #region Public properties
        public string PatientID
        {
            get { return _PatientID; }
            set { _PatientID = value; }
        }

        public string OriginalPatientID
        {
            get { return _originalPatientID; }
            set { _originalPatientID = value; }
        }


        public string Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }


        public string MedicareNo
        {
            get { return _MedicareNo; }
            set { _MedicareNo = value; }
        }

        public string DiseaseHistory
        {
            get { return _diseaseHistory; }
            set { _diseaseHistory = value; }
        }

        public string PrivateHealth
        {
            get { return _PrivateHealth; }
            set { _PrivateHealth = value; }
        }
        #endregion Public properties

        #region Public methods
        public void DeletePatient()
        {
            try
            {
                //Create DAL
                DataAccessLayer objDAL = new DataAccessLayer(this._connectionString);

                //Assign parameters from the patient properties
                objDAL.addParameter("@PatientID", this.PatientID, DataAccessLayer.GenericDataType.genString);

                //Execute the SP
                int rowsAffected = objDAL.RunSPExecNonQuery("usp_DeletePatient");

                //Check if the pilot was actually deleted from the database
                if (rowsAffected == 0)
                {
                    throw new Exception("Pilot could not be deleted from the database");
                }
            }
            catch (Exception ex)
            {

                //Catch the exception and pass it back up to the calling code (most likely the code behind page of our Window)
                //throw new Exception(ex.Message);
                throw ex;
                //throw new Exception("Pilot could not be added to the database.", ex);
            }
        }

        public void UpdatePatient()
        {
            try
            {
                //Create DAL
                DataAccessLayer objDAL = new DataAccessLayer(this._connectionString);

                //Assign parameters from patient properties
                objDAL.addParameter("@NewPatientID", this.PatientID, DataAccessLayer.GenericDataType.genString);
                objDAL.addParameter("@OldPatientID", this.OriginalPatientID, DataAccessLayer.GenericDataType.genString);
                objDAL.addParameter("@FName", this.FirstName, DataAccessLayer.GenericDataType.genString);
                objDAL.addParameter("@SName", this.Surname, DataAccessLayer.GenericDataType.genString);
                objDAL.addParameter("@Gender", this.Gender, DataAccessLayer.GenericDataType.genChar);
                objDAL.addParameter("@DOB", this.DateOfBirth, DataAccessLayer.GenericDataType.genDateTime);
                objDAL.addParameter("@Address", this.Address, DataAccessLayer.GenericDataType.genString);
                objDAL.addParameter("@Suburb", this.Suburb, DataAccessLayer.GenericDataType.genString);
                objDAL.addParameter("@Postcode", this.Postcode, DataAccessLayer.GenericDataType.genString);
                objDAL.addParameter("@MedicareNo", this.MedicareNo, DataAccessLayer.GenericDataType.genString);
                objDAL.addParameter("@Phone", this.HomePhone, DataAccessLayer.GenericDataType.genString);
                objDAL.addParameter("@Private_Health", this.PrivateHealth, DataAccessLayer.GenericDataType.genString);
                objDAL.addParameter("@History", this.DiseaseHistory, DataAccessLayer.GenericDataType.genString);

                //Execute the SP
                int rowsAffected = objDAL.RunSPExecNonQuery("usp_UpdatePatient");

                //Check if the pilot was actually updated
                if (rowsAffected == 0)
                {
                    throw new Exception("Pilot was not updated in the database");
              
                }
            }
            catch (Exception ex)
            {
                //Catch the exception and pass it back up to the calling code (most likely the code behind page of our Window)
                //throw new Exception(ex.Message);
                throw ex;
                //throw new Exception("Pilot could not be added to the database.", ex);
            }
        }

        public void AddPatient()
        {
            try
            {
                //Create DAL
                DataAccessLayer objDAL = new DataAccessLayer(this._connectionString);

                //Assign parameters from patient properties
                objDAL.addParameter("@PatientID", this.PatientID, DataAccessLayer.GenericDataType.genString);
                objDAL.addParameter("@FName", this.FirstName, DataAccessLayer.GenericDataType.genString);
                objDAL.addParameter("@SName", this.Surname, DataAccessLayer.GenericDataType.genString);
                objDAL.addParameter("@Gender", this.Gender, DataAccessLayer.GenericDataType.genChar);
                objDAL.addParameter("@DOB", this.DateOfBirth, DataAccessLayer.GenericDataType.genDateTime);
                objDAL.addParameter("@Address", this.Address, DataAccessLayer.GenericDataType.genString);
                objDAL.addParameter("@Suburb", this.Suburb, DataAccessLayer.GenericDataType.genString);
                objDAL.addParameter("@Postcode", this.Postcode, DataAccessLayer.GenericDataType.genString);
                objDAL.addParameter("@MedicareNo", this.MedicareNo, DataAccessLayer.GenericDataType.genString);
                objDAL.addParameter("@Phone", this.HomePhone, DataAccessLayer.GenericDataType.genString);
                objDAL.addParameter("@PrivateHealth", this.PrivateHealth, DataAccessLayer.GenericDataType.genString);
                objDAL.addParameter("@History", this.DiseaseHistory, DataAccessLayer.GenericDataType.genString);

                //Execute the SP
                int rowsAffected = objDAL.RunSPExecNonQuery("usp_InsertNewPatient");

                //Check if the pilot was actually added to the database (1 row SHOULD be affected)
                if (rowsAffected == 0)
                {
                    throw new Exception("Pilot was not added to the database");
                }


            }
            catch ( Exception ex)
            {
                //Catch the exception and pass it back up to the calling code (most likely the code behind page of our Window)
                //throw new Exception(ex.Message);
                throw ex;
                //throw new Exception("Pilot could not be added to the database.", ex);
            }
        }

        #endregion Public methods

        #region Private methods
        private void LoadPatientProperties()
        {
            //Check if the DataTable has any rows...
            if(this._dtPatient.Rows.Count > 0)
            {

                //Get the first and only row of the DataTable
                DataRow patientRow = this._dtPatient.Rows[0];

                //Assign patient details to the properties
                this.PatientID = patientRow["Patient_ID"].ToString();
                this.FirstName = patientRow["FirstName"].ToString();
                this.Surname = patientRow["Surname"].ToString();
                this.Gender = patientRow["Gender"].ToString();
                this.DateOfBirth = (DateTime)patientRow["Date_Of_Birth"];
                this.Address = patientRow["Address"].ToString();
                this.Suburb = patientRow["Suburb"].ToString();
                this.Postcode = patientRow["Postcode"].ToString();
                this.MedicareNo = patientRow["Medicare_No"].ToString();
                this.HomePhone = patientRow["Phone"].ToString();
                this.PrivateHealth = patientRow["Private_Health"].ToString();
                this.DiseaseHistory = patientRow["Disease_History"].ToString();
            }
        }
        #endregion Private methods
    }
}
