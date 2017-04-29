 
//$(document).ready(function () {
//    $("#select-menu").change(function () {
//        //this will give you value of first select box
//        var selectedval = ($("#select-menu option:selected").val());
//        //this will set the value from first select box in second select box
//        console.log(selectedval);
//    });
//});



$('#select-menu').on('change', function () {
    var selection = $(this).val();
    switch (selection) {
    case "NotFinished":
            $("#university-box").hide()
            break;
    case "InProgress":
        $("#university-box").show()
        break;
    case "Finished":
        $("#university-box").show()
        break;
    default:
            $("#university-box").hide()
    }
});