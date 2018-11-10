﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SfTreeGridDemo
{
    public class EmployeeInfo
    {
        #region private variables
        int _id;
        private string _firstName;
        private string _lastName;
        private string _title;
        private double? _salary;
        private int _reportsTo;
        #endregion

        #region public variables

        /// <summary>
        /// Denotes the employee first name
        /// </summary>
        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }

        /// <summary>
        /// Denotes the employee last name
        /// </summary>
        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        /// <summary>
        /// Denotes the employee id
        /// </summary>
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        /// <summary>
        /// Denotes the employee title
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        /// <summary>
        /// Denotes the employee salary
        /// </summary>
        public double? Salary
        {
            get { return _salary; }
            set { _salary = value; }
        }

        /// <summary>
        /// Denotes the employee who has reports to
        /// </summary>
        public int ReportsTo
        {
            get { return _reportsTo; }
            set { _reportsTo = value; }
        }
        #endregion
    }
}
