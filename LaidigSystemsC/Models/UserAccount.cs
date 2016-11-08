using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LaidigSystemsC.Models
{
    public class UserAccount
    {
        [Key]
        public int UserId { get; set; }

        [Required(ErrorMessage ="First Name is Required !")]
        [MaxLength(15), MinLength(4)]

        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is Required !")]
        [MaxLength(15), MinLength(4)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is Required !")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
        [Remote("CheckExistingEmail", "Account", ErrorMessage ="Email Allready exits !")]
        public string Email { get; set; }


        [Required(ErrorMessage = "UserName is Required !")]
        [Remote("CheckExistingUserName", "Account", ErrorMessage = "UserName Allready exits !")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is Required !")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password is Required !")]
        [System.ComponentModel.DataAnnotations.Compare("Password",ErrorMessage ="Please Confirm Your Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        //public CsvFile CsvFile { get; set; }


    }




    //public class CsvFile
    //{
    //    [Key]

    //    public int CsvId { get; set; }


    //    public string DateofTime { get; set; }

    //    public string CurSysRateCFM { get; set; }

    //    public string ReqRecRateCFM { get; set; }

    //    public string RecSpeedRPM { get; set; }

    //    public string RecTorque { get; set; }

    //    public string RecPressurePSI { get; set; }

    //    public string RecFlowGPM { get; set; }

    //    public string RecPowerHP { get; set; }

    //    public string AdvSpeedDPH { get; set; }

    //    public string AdvTorque { get; set; }

    //    public string AdvPressurePSI { get; set; }

    //    public string Disc1Torq { get; set; }

    //    public string Disc1SpeedRPM { get; set; }

    //    public string Disc1PowerHP { get; set; }

    //    public string Disc2Torq { get; set; }

    //    public string Disc2SpeedRPM { get; set; }

    //    public string Disc2PowerHP { get; set; }
    }

