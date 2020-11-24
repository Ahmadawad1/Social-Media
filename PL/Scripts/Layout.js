$(document).ready(function () {
    $("#OptionsIcon").click(function () {
   
        $("#navBarY").slideToggle(500);

    });
   
    $(document).keyup(function (event) {
        if (event.which === 13) {
            Search();

        }
    });
    $("#ProfileIcon").click(function () {
        
        $("#MenuAccount").toggle();
    });
    $("#BellIcon").click(function () {

        $("#Notification").toggle();
    });
    $(window).click(function (e) {
        if (e.target.id != 'SearchResult' && e.target.id != 'Search') {
            $("#SearchResult").hide();
        }
        else {
            $("#SearchResult").show();
        }
    });
    
    $("#Search").keyup(function () {
        $("#SearchResult").empty();
        var txt = $(this).val();
        var fd = new FormData();
        fd.append('txt', txt);
        $.ajax({
            type: "POST",
            url: '/MyProfile/Search',
            contentType: false,
            processData: false,
            data: fd,
            success: function (result)
            {
                if (txt.length > 0)
                {
                    if (result.list.length == 0)
                    {
                        $("#SearchResult").append("<li id='SearchItem'><text id='SearchTxt'>Result Not Found</text></li>")
                    }
                    else
                    {
                        for (var i = 0; i < result.list.length; i++)
                        {
                            var name = result.list[i].Name;
                            if (name != ($("#MyName").val()))
                            {

                                $("#SearchResult").append("<li id='SearchItem' onclick='Fill("+name+")'  ><text id='SearchTxt'>" + name + "</text></li>")
                            }
                        }
                    }
                }
            }

        });

    });
   
   
});
function Fill(i) {

    document.getElementById('Search').value =i;
    
}
function ShowNoti() {
    $('#navBarNoti').show();
}
function HideNoti() {
    $('#navBarNoti').hide();
}
function Search() {
    if (document.getElementById("Search").value != "") {
        window.location.href = '/MyProfile/Result?name=' + document.getElementById("Search").value;
    }
}