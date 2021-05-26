//    Main Namespace = SA
var dataAccess = {};

////    This object is responsible to provide constants throughout the project
////    SA Constants
dataAccess.constants = {

    login: "/User/Login",
    logout: "/User/Logout",
    forgotPassword: "/User/ForgotPassword",
    changePassword: "/User/UpdatePassword",
    markVerified: "/User/markVerifiedUser",
    deletePhysician: "/Physician/DeletePhysician"
};

dataAccess.Temp = {

  
};

////    This object is responsible to provide all authentication related stuff
////    Login Service

dataAccess.loginService = {
   
    login: function () {

        //debugger;
        //alert(1);

        $('#myfrm').submit(function (e) {
            e.preventDefault();
            if ($('#myfrm').parsley().isValid()) {

             //   debugger;
                $.ajax({
                    type: 'post',
                    data: { Email: $("#email").val(), Password: $("#password").val() },
                    url: dataAccess.constants.login,
                    success: function (res) {

                        var Result = JSON.parse(res);
                        if (Result.Code === "404") {
                            $("#LoginErrorMessage").text(Result.Message);
                            //    $('#preloader').fadeOut();
                            return;
                        }
                        else {
                            window.location.href = "/User/MainDashboard";
                        }
                    }
                });

            }
        });


        
    },

    forgotPassword: function () {
        //$('#preloader').fadeIn("slow");
        //NProgress.start();

        $('#myfrm').submit(function (e) {
            e.preventDefault();
            if ($('#myfrm').parsley().isValid()) {

        $.ajax({
            type: 'post',
            data: { Email: $("#emailForForgotPassword").val() },
            url: dataAccess.constants.forgotPassword,
            success: function (res) {
              //  NProgress.done();
                var Result = JSON.parse(res).toString();
                if (Result.Code === "404") {
                    $("#ForgotErrorMessage").text(Result.Message);
                 //   $('#preloader').fadeOut();
                    return;
                }
                else if (Result.Code === "400") {
                    $("#ForgotErrorMessage").text(Result.Message);
               //     $('#preloader').fadeOut();
                    return;
                }
                else {
                    window.location.href = '/User/Login';
                }
            }
        });

            }
        });

    },

    logout: function () {

        $.ajax({
            type: 'get',
            url: dataAccess.constants.logout,
            success: function (res) {

                window.location.href = '/User/Login';
                NProgress.done();
            }
        });
    },

    markVerified: function (arrayList) {
        //$('#preloader').fadeIn("slow");
        //NProgress.start();
        debugger;
        var data = { checkedList: arrayList };

        $.ajax({
            type: 'post',
            dataType: 'html',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data),
            url: dataAccess.constants.markVerified,
         //   traditional: true,
            success: function (res) {
                //  NProgress.done();
                debugger;
                var Result = JSON.parse(res).toString();
                if (Result.Code === "404") {
                    $("#ForgotErrorMessage").text(Result.Message);
                    //   $('#preloader').fadeOut();
                    return;
                }
                else if (Result.Code === "400") {
                    $("#ForgotErrorMessage").text(Result.Message);
                    //     $('#preloader').fadeOut();
                    return;
                }
                else {
                    alert("Selected users verified");
                    window.location.href = '/User/ManagemenListAndsearch';
                }
            }
        });
    },

    deletePhysician: function (physicianId) {
            //  $('#preloader').fadeIn("slow");
        alert(1);
        debugger;

        //swal({
        //    title: "Are you sure?",
        //    text: "Once deleted, you will not be able to recover this imaginary file!",
        //    icon: "warning",
        //    buttons: true,
        //    dangerMode: true,
        //})
        //.then((willDelete) => {
        //        if (willDelete) {
        //            swal("Poof! Your imaginary file has been deleted!", {
        //                icon: "success",
        //            });

        //        } else {
        //            swal("Your imaginary file is safe!");
        //        }
        //});



        var data = {
            PhysicianId: physicianId
        };

        $.ajax({
            url: dataAccess.constants.deletePhysician,
            type: 'POST',
            data: JSON.stringify(data),
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            success: function (res) {
                var Result = JSON.parse(res);
                debugger;
                if (Result.Code === "400") {

                    $('#ErrorModal').on('hidden.bs.modal', function () {
                        $('#preloader').fadeOut();
                        $("#ModalHeading").text("Message");

                        $(".ErrorMessage").text(Result.Message);
                        $('#ErrorModal').modal('show');

                    });
                    return;
                } else if (Result.Code === "200") {
                    alert("Record deleted");
                    window.location.href = '/Physician/physiciansManagementList';
                }
            }
        });
            
     

      
    }
 
};

dataAccess.changePassword = {

    updatePassword: function () {
      //  $('#preloader').fadeIn("slow");

        debugger;
        alert(1);
        var data = {
            OldPassword: $("#CurrentPassword").val(),
            NewPassword: $("#NewPassword").val(),
            ConfirmPassword: $("#ConfirmPassword").val()
        };

        var getdata = { model: data };

        $.ajax({
            type: 'post',
            data: getdata,
            url: dataAccess.constants.changePassword,
            success: function (res) {
                var Result = JSON.parse(res);
              //  $('#preloader').fadeOut();
                if (res === "Password has been successfully updated.")
                    $("#ModalHeading").text("Success!");
                else
                    $("#ModalHeading").text("Message");

                $(".ErrorMessage").text(Result.Message);
                $('#ErrorModal').modal('show');

                $('#ErrorModal').on('hidden.bs.modal', function () {
                    if (res === "Password Successfully updated")
                        window.location.href = '/User/Login';
                });
            },
            error: function () {
                alert("error");
            }
        });
    }
};

function getNotifiyMessage(that) {

    var mytitle = 'Regular Success';
    var mytext = 'That thing that you were trying to do worked!';

    return { title: mytitle, text: mytext, type: 'success', styling: 'bootstrap3' };
}

function validateEmail(email) {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}
