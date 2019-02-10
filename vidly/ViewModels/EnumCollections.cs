using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace vidly.ViewModels
{
    public class EnumCollections
    {
        public enum MoviesDropDownList
        {
            [Description("Add Movie")]
            AddMovie,
            [Description("Browse Movies")]
            BrowseMovies
        }

        public enum RentsDropDownList
        {
            [Description("Add New Rent")]
            AddNewRent,
            [Description("Movie Return")]
            MovieReturn,
            [Description("Manage Rents")]
            ManageRents
        }
    }
}