using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeManagement
{
    /// <summary>
    /// Defines a person. This class must be inherited from as it is defined as "abstract".
    /// </summary>
    public abstract class Person
    {

        #region Private variables/properties

        //Person properties
        private string _firstName;
        private string _surname;
        private DateTime _dateOfBirth;
        private string _address;
        private string _suburb;
        private string _state;
        private string _postcode;
        private string _homePhone;

        //Naming and exception handling variables
        private string _moduleName;
        private const string _exceptionMessage = "Application error. Detailed error information can be found in the Application Log.";


        #endregion

        #region Public constructors

        public Person()
        {
            this._moduleName = this.GetType().ToString();
        }

        #endregion

        #region Public properties


        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        public string Surname
        {
            get { return _surname; }
            set { _surname = value; }
        }

        public DateTime DateOfBirth
        {
            get
            {
                try
                {
                    return _dateOfBirth;
                }
                catch
                {
                    //Return a specific date
                    return new DateTime(1900, 1, 1);

                    //Return today's date
                    //return DateTime.Today;
                }
            }

            set
            {
                _dateOfBirth = value;
            }
        }

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }

        public string Suburb
        {
            get { return _suburb; }
            set { _suburb = value; }
        }

        public string State
        {
            get { return _state; }
            set { _state = value; }
        }

        public string Postcode
        {
            get { return _postcode; }
            set { _postcode = value; }
        }

        public string HomePhone
        {
            get { return _homePhone; }
            set { _homePhone = value; }
        }


        #endregion
    }
}
