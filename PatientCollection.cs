using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Configuration;
using System.Data;

namespace PracticeManagement
{
    [DataObject(true)]
    class PatientCollection : List<Patient>
    {
        public PatientCollection(string fieldName, string searchString)
        {
            //Get all the patients from the database
            DataAccessLayer objDAL = new DataAccessLayer(ConfigurationManager.ConnectionStrings["cnnStrPractice"].ConnectionString);
            objDAL.addParameter("@FieldName", fieldName, DataAccessLayer.GenericDataType.genString);
            objDAL.addParameter("@SearchString", searchString, DataAccessLayer.GenericDataType.genString);
            DataTable dtPatients = objDAL.RunSPDataTable("usp_GetAllPatients");

            //Iterate through each patient (row of the DataTable)
            foreach (DataRow drPatient in dtPatients.Rows)
            {
                //Create new patient from DataRow
                Patient newPatient = new Patient(drPatient);

                //Add pilot to the PilotCollection's list
                this.Add(newPatient);

            }

        }
    }
}
